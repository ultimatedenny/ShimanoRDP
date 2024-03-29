﻿using ShimanoRDP.Resources.Language;
using ShimanoRDP.Tools;

namespace ShimanoRDP.Connection.Protocol.RDP
{
    public enum RDPSounds
    {
        [LocalizedAttributes.LocalizedDescription(nameof(Language.RdpSoundBringToThisComputer))]
        BringToThisComputer = 0,

        [LocalizedAttributes.LocalizedDescription(nameof(Language.RdpSoundLeaveAtRemoteComputer))]
        LeaveAtRemoteComputer = 1,

        [LocalizedAttributes.LocalizedDescription(nameof(Language.DoNotPlay))]
        DoNotPlay = 2
    }
}