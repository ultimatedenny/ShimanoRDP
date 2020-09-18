using System;
using ShimanoRDP.App;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers.ConnectionSerializers.Csv;
using ShimanoRDP.Security;
using ShimanoRDP.Tree;

namespace ShimanoRDP.Config.Connections
{
    public class CsvConnectionsSaver : ISaver<ConnectionTreeModel>
    {
        private readonly string _connectionFileName;
        private readonly SaveFilter _saveFilter;

        public CsvConnectionsSaver(string connectionFileName, SaveFilter saveFilter)
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
            var csvConnectionsSerializer =
                new CsvConnectionsSerializerShimanoRDPFormat(_saveFilter, Runtime.CredentialProviderCatalog);
            var dataProvider = new FileDataProvider(_connectionFileName);
            var csvContent = csvConnectionsSerializer.Serialize(connectionTreeModel);
            dataProvider.Save(csvContent);
        }
    }
}