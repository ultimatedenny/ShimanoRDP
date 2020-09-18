using System;
using ShimanoRDP.Connection.Protocol;

namespace ShimanoRDP.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AttributeUsedInProtocol : Attribute
    {
        public ProtocolType[] SupportedProtocolTypes { get; }

        public AttributeUsedInProtocol(params ProtocolType[] supportedProtocolTypes)
        {
            SupportedProtocolTypes = supportedProtocolTypes;
        }
    }
}
