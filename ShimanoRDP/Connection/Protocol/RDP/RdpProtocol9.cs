using System.Windows.Forms;
using AxMSTSCLib;

namespace ShimanoRDP.Connection.Protocol.RDP
{
    public class RdpProtocol9 : RdpProtocol8
    {
        protected override RdpVersion RdpProtocolVersion => RdpVersion.Rdc9;

        protected override AxHost CreateActiveXRdpClientControl()
        {
            return new AxMsRdpClient9NotSafeForScripting();
        }
    }
}
