using System;

namespace ShimanoRDP.Config.Connections.Multiuser
{
    public delegate void UpdateCheckFinishedEventHandler(object sender, ConnectionsUpdateCheckFinishedEventArgs args);

    public class ConnectionsUpdateCheckFinishedEventArgs : EventArgs
    {
        public bool UpdateAvailable { get; set; }
    }
}