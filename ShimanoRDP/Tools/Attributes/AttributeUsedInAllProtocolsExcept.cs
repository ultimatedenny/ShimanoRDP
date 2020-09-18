using System;
using System.Linq;
using ShimanoRDP.Connection.Protocol;

namespace ShimanoRDP.Tools.Attributes
{
    public class AttributeUsedInAllProtocolsExcept : AttributeUsedInProtocol
    {
        public AttributeUsedInAllProtocolsExcept(params ProtocolType[] exceptions)
            : base(Enum
                .GetValues(typeof(ProtocolType))
                .Cast<ProtocolType>()
                .Except(exceptions)
                .ToArray())
        {
        }
    }
}
