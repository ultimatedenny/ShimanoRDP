using System.IO;
using System.Linq;
using ShimanoRDP.App;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers.ConnectionSerializers.Csv;
using ShimanoRDP.Container;
using ShimanoRDP.Messages;

namespace ShimanoRDP.Config.Import
{
    public class ShimanoRDPCsvImporter : IConnectionImporter<string>
    {
        public void Import(string filePath, ContainerInfo destinationContainer)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                Runtime.MessageCollector.AddMessage(MessageClass.ErrorMsg, "Unable to import file. File path is null.");
                return;
            }

            if (!File.Exists(filePath))
                Runtime.MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                    $"Unable to import file. File does not exist. Path: {filePath}");

            var dataProvider = new FileDataProvider(filePath);
            var xmlString = dataProvider.Load();
            var xmlConnectionsDeserializer = new CsvConnectionsDeserializerShimanoRDPFormat();
            var connectionTreeModel = xmlConnectionsDeserializer.Deserialize(xmlString);

            var rootImportContainer = new ContainerInfo {Name = Path.GetFileNameWithoutExtension(filePath)};
            rootImportContainer.AddChildRange(connectionTreeModel.RootNodes.First().Children.ToArray());
            destinationContainer.AddChild(rootImportContainer);
        }
    }
}