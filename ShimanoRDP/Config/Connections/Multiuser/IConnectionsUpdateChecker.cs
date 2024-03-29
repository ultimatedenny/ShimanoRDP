﻿using System;

namespace ShimanoRDP.Config.Connections.Multiuser
{
    public interface IConnectionsUpdateChecker : IDisposable
    {
        bool IsUpdateAvailable();

        void IsUpdateAvailableAsync();

        event EventHandler UpdateCheckStarted;
        event UpdateCheckFinishedEventHandler UpdateCheckFinished;
        event ConnectionsUpdateAvailableEventHandler ConnectionsUpdateAvailable;
    }
}