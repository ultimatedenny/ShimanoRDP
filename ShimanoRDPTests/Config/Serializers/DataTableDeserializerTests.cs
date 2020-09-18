using System.Data;
using System.Security;
using ShimanoRDP.Config.Serializers.ConnectionSerializers.MsSql;
using ShimanoRDP.Connection;
using ShimanoRDP.Security;
using ShimanoRDP.Security.SymmetricEncryption;
using ShimanoRDP.Tree;
using ShimanoRDPTests.TestHelpers;
using NUnit.Framework;

namespace ShimanoRDPTests.Config.Serializers
{
    public class DataTableDeserializerTests
    {
        private DataTableDeserializer _deserializer;
        private ICryptographyProvider _cryptographyProvider;

        [SetUp]
        public void Setup()
        {
            _cryptographyProvider = new LegacyRijndaelCryptographyProvider();
        }

        [Test]
        public void WeCanDeserializeATree()
        {
            var model = CreateConnectionTreeModel();
            var dataTable = CreateDataTable(model.RootNodes[0]);
            _deserializer = new DataTableDeserializer(_cryptographyProvider, new SecureString());
            var output = _deserializer.Deserialize(dataTable);
            Assert.That(output.GetRecursiveChildList().Count, Is.EqualTo(model.GetRecursiveChildList().Count));
        }

        [Test]
        public void WeCanDeserializeASingleEntry()
        {
            var dataTable = CreateDataTable(new ConnectionInfo());
            _deserializer = new DataTableDeserializer(_cryptographyProvider, new SecureString());
            var output = _deserializer.Deserialize(dataTable);
            Assert.That(output.GetRecursiveChildList().Count, Is.EqualTo(1));
        }


        private DataTable CreateDataTable(ConnectionInfo tableContent)
        {
            var serializer = new DataTableSerializer(new SaveFilter(), _cryptographyProvider, new SecureString());
            return serializer.Serialize(tableContent);
        }

        private ConnectionTreeModel CreateConnectionTreeModel()
        {
            var builder = new ConnectionTreeModelBuilder();
            return builder.Build();
        }
    }
}