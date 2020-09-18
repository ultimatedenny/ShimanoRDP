using ShimanoRDP.Connection.Protocol.Http;
using ShimanoRDP.Connection.Protocol.RAW;
using ShimanoRDP.Connection.Protocol.RDP;
using ShimanoRDP.Connection.Protocol.Rlogin;
using ShimanoRDP.Connection.Protocol.SSH;
using ShimanoRDP.Connection.Protocol.Telnet;
using ShimanoRDP.Connection.Protocol.VNC;
using System;
using ShimanoRDP.Connection.Protocol.PowerShell;
using ShimanoRDP.Properties;
using ShimanoRDP.Resources.Language;

namespace ShimanoRDP.Connection.Protocol
{
    public class ProtocolFactory
    {
        private readonly RdpProtocolFactory _rdpProtocolFactory = new RdpProtocolFactory();

        public ProtocolBase CreateProtocol(ConnectionInfo connectionInfo)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (connectionInfo.Protocol)
            {
                case ProtocolType.RDP:
                    var rdp = _rdpProtocolFactory.Build(connectionInfo.RdpVersion);
                    rdp.LoadBalanceInfoUseUtf8 = Settings.Default.RdpLoadBalanceInfoUseUtf8;
                    return rdp;
                case ProtocolType.VNC:
                    return new ProtocolVNC();
                case ProtocolType.SSH1:
                    return new ProtocolSSH1();
                case ProtocolType.SSH2:
                    return new ProtocolSSH2();
                case ProtocolType.Telnet:
                    return new ProtocolTelnet();
                case ProtocolType.Rlogin:
                    return new ProtocolRlogin();
                case ProtocolType.RAW:
                    return new RawProtocol();
                case ProtocolType.HTTP:
                    return new ProtocolHTTP(connectionInfo.RenderingEngine);
                case ProtocolType.HTTPS:
                    return new ProtocolHTTPS(connectionInfo.RenderingEngine);
                case ProtocolType.PowerShell:
                    return new ProtocolPowerShell(connectionInfo);
                case ProtocolType.IntApp:
                    if (connectionInfo.ExtApp == "")
                    {
                        throw (new Exception(Language.NoExtAppDefined));
                    }
                    return new IntegratedProgram();
            }

            return default;
        }
    }
}