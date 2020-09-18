using ShimanoRDP.Resources.Language;
using ShimanoRDP.Tools;

namespace ShimanoRDP.Connection.Protocol.RDP
{
    public enum AuthenticationLevel
    {
        [LocalizedAttributes.LocalizedDescription(nameof(Language.AlwaysConnectEvenIfAuthFails))]
        NoAuth = 0,

        [LocalizedAttributes.LocalizedDescription(nameof(Language.DontConnectWhenAuthFails))]
        AuthRequired = 1,

        [LocalizedAttributes.LocalizedDescription(nameof(Language.WarnIfAuthFails))]
        WarnOnFailedAuth = 2
    }
}