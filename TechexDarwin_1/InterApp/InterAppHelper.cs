namespace Skyline.DataMiner.TechexDarwin.InterApp
{
    using System;
    using System.Collections.Generic;
    using Skyline.DataMiner.TechexDarwin.InterApp.Messages;

    public static class InterAppHelper
    {
        private static readonly List<Type> knownTypes = new List<Type>
        {
            typeof(PesSwitchActionMessage),
        };

        public static List<Type> KnownTypes => knownTypes;
    }
}
