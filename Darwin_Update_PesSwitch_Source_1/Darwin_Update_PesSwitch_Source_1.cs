/*
****************************************************************************
*  Copyright (c) 2024,  Skyline Communications NV  All Rights Reserved.    *
****************************************************************************

By using this script, you expressly agree with the usage terms and
conditions set out below.
This script and all related materials are protected by copyrights and
other intellectual property rights that exclusively belong
to Skyline Communications.

A user license granted for this script is strictly for personal use only.
This script may not be used in any way by anyone without the prior
written consent of Skyline Communications. Any sublicensing of this
script is forbidden.

Any modifications to this script by the user are only allowed for
personal use and within the intended purpose of the script,
and will remain the sole responsibility of the user.
Skyline Communications will not be responsible for any damages or
malfunctions whatsoever of the script resulting from a modification
or adaptation by the user.

The content of this script is confidential information.
The user hereby agrees to keep this confidential information strictly
secret and confidential and not to disclose or reveal it, in whole
or in part, directly or indirectly to any person, entity, organization
or administration without the prior written consent of
Skyline Communications.

Any inquiries can be addressed to:

	Skyline Communications NV
	Ambachtenstraat 33
	B-8870 Izegem
	Belgium
	Tel.	: +32 51 31 35 69
	Fax.	: +32 51 31 01 29
	E-mail	: info@skyline.be
	Web		: www.skyline.be
	Contact	: Ben Vandenberghe

****************************************************************************
Revision History:

DATE		VERSION		AUTHOR			COMMENTS

02/04/2024	1.0.0.1		SDT, Skyline	Initial version
****************************************************************************
*/

namespace Darwin_Update_PesSwitch_Source_1
{
    using System;
    using System.Text.RegularExpressions;
    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Core.InterAppCalls.Common.CallBulk;
    using Skyline.DataMiner.TechexDarwin;
    using Skyline.DataMiner.TechexDarwin.InterApp;
    using Skyline.DataMiner.TechexDarwin.InterApp.Messages;

    /// <summary>
    /// Represents a DataMiner Automation script.
    /// </summary>
    public class Script
    {
        /// <summary>
        /// The script entry point.
        /// </summary>
        /// <param name="engine">Link with SLAutomation process.</param>
        public void Run(Engine engine)
        {
            string elementName = Regex.Replace(engine.GetScriptParam("ElementName").Value, @"[\[\]]", String.Empty).Split(',')[0].Replace("\"", String.Empty);
            string pesSwitch = Regex.Replace(engine.GetScriptParam("PesSwitch").Value, @"[\[\]]", String.Empty).Split(',')[0].Replace("\"", String.Empty);
            string input = Regex.Replace(engine.GetScriptParam("InputName").Value, @"[\[\]]", String.Empty).Split(',')[0].Replace("\"", String.Empty);
            bool forced = Convert.ToBoolean(Regex.Replace(engine.GetScriptParam("Forced").Value, @"[\[\]]", String.Empty).Split(',')[0].Replace("\"", String.Empty));

            string parsedInput;
            if (input.ToLower().Contains("one"))
            {
                parsedInput = "One";
            }
            else if (input.ToLower().Contains("two"))
            {
                parsedInput = "Two";
            }
            else
            {
                throw new InvalidOperationException("Invalid input");
            }

            Element darwinElement = DarwinHelper.GetElementByName(engine, elementName);

            IInterAppCall myCommands = InterAppCallFactory.CreateNew();
            PesSwitchActionMessage updateMessage = new PesSwitchActionMessage
            {
                PesSwitchId = pesSwitch,
                Action = ModuleActions.Switch,
                Forced = forced,
                Input = parsedInput,
            };

            myCommands.Messages.Add(updateMessage);
            myCommands.Send(engine.GetUserConnection(), darwinElement.DmaId, darwinElement.ElementId, 9000000, InterAppHelper.KnownTypes);
        }
    }
}