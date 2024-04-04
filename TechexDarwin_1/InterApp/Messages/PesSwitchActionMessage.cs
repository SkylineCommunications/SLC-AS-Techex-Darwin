namespace Skyline.DataMiner.TechexDarwin.InterApp.Messages
{
    using Skyline.DataMiner.Core.InterAppCalls.Common.CallSingle;

    public class PesSwitchActionMessage : Message
    {
        public string PesSwitchId { get; set; }

        public string Input { get; set; }

        public bool Forced { get; set; }

        public ModuleActions Action { get; set; }
    }
}
