using System.Collections.Generic;
using System.Linq;
using ShimanoRDP.Config.Serializers;
using ShimanoRDP.Config.Serializers.MiscSerializers;
using ShimanoRDP.Connection.Protocol;
using ShimanoRDP.Container;
using ShimanoRDP.Tools;


namespace ShimanoRDP.Config.Import
{
    public class PortScanImporter : IConnectionImporter<IEnumerable<ScanHost>>
    {
        private readonly ProtocolType _targetProtocolType;

        public PortScanImporter(ProtocolType targetProtocolType)
        {
            _targetProtocolType = targetProtocolType;
        }

        public void Import(IEnumerable<ScanHost> hosts, ContainerInfo destinationContainer)
        {
            var deserializer = new PortScanDeserializer(_targetProtocolType);
            var connectionTreeModel = deserializer.Deserialize(hosts);

            var importedRootNode = connectionTreeModel.RootNodes.First();
            if (importedRootNode == null) return;
            var childrenToAdd = importedRootNode.Children.ToArray();
            destinationContainer.AddChildRange(childrenToAdd);
        }
    }
}