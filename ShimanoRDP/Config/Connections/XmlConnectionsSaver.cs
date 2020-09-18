using System;
using System.Linq;
using ShimanoRDP.App;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers.ConnectionSerializers.Xml;
using ShimanoRDP.Security;
using ShimanoRDP.Security.Factories;
using ShimanoRDP.Tree;
using ShimanoRDP.Tree.Root;

namespace ShimanoRDP.Config.Connections
{
    public class XmlConnectionsSaver : ISaver<ConnectionTreeModel>
    {
        private readonly string _connectionFileName;
        private readonly SaveFilter _saveFilter;

        public XmlConnectionsSaver(string connectionFileName, SaveFilter saveFilter)
        {
            if (string.IsNullOrEmpty(connectionFileName))
                throw new ArgumentException($"Argument '{nameof(connectionFileName)}' cannot be null or empty");
            if (saveFilter == null)
                throw new ArgumentNullException(nameof(saveFilter));

            _connectionFileName = connectionFileName;
            _saveFilter = saveFilter;
        }

        public void Save(ConnectionTreeModel connectionTreeModel, string propertyNameTrigger = "")
        {
            try
            {
                var cryptographyProvider = new CryptoProviderFactoryFromSettings().Build();
                var serializerFactory = new XmlConnectionSerializerFactory();
                
                var xmlConnectionsSerializer = serializerFactory.Build(
                    cryptographyProvider,
                    connectionTreeModel,
                    _saveFilter,
                    Properties.Settings.Default.EncryptCompleteConnectionsFile);

                var rootNode = connectionTreeModel.RootNodes.OfType<RootNodeInfo>().First();
                var xml = xmlConnectionsSerializer.Serialize(rootNode);

                var fileDataProvider = new FileDataProviderWithRollingBackup(_connectionFileName);
                fileDataProvider.Save(xml);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector?.AddExceptionStackTrace("SaveToXml failed", ex);
            }
        }
    }
}