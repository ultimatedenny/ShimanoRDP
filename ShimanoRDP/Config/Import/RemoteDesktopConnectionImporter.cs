using System.IO;
using System.Linq;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers;
using ShimanoRDP.Config.Serializers.MiscSerializers;
using ShimanoRDP.Container;


namespace ShimanoRDP.Config.Import
{
    public class RemoteDesktopConnectionImporter : IConnectionImporter<string>
    {
        public void Import(string fileName, ContainerInfo destinationContainer)
        {
            var dataProvider = new FileDataProvider(fileName);
            var content = dataProvider.Load();

            var deserializer = new RemoteDesktopConnectionDeserializer();
            var connectionTreeModel = deserializer.Deserialize(content);

            var importedConnection = connectionTreeModel.RootNodes.First().Children.First();

            if (importedConnection == null) return;
            importedConnection.Name = Path.GetFileNameWithoutExtension(fileName);
            destinationContainer.AddChild(importedConnection);
        }
    }
}