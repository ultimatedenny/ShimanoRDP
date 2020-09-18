namespace ShimanoRDP.Connection.Protocol.Telnet
{
    public class ProtocolTelnet : PuttyBase
    {
        public ProtocolTelnet()
        {
            this.PuttyProtocol = Putty_Protocol.telnet;
        }

        public enum Defaults
        {
            Port = 23
        }
    }
}