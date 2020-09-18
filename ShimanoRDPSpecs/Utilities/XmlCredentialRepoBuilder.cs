using System.Security;
using ShimanoRDP.Config;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers.CredentialSerializer;
using ShimanoRDP.Credential;
using ShimanoRDP.Credential.Repositories;
using ShimanoRDP.Security;
using ShimanoRDP.Security.SymmetricEncryption;

namespace ShimanoRDPSpecs.Utilities
{
    public class XmlCredentialRepoBuilder
    {
        public SecureString EncryptionKey { get; set; } = "someKey1".ConvertToSecureString();
        public ICryptographyProvider CryptographyProvider { get; set; } = new AeadCryptographyProvider();

        public ICredentialRepository BuildXmlCredentialRepo()
        {
            var xmlFileBuilder = new CredRepoXmlFileBuilder();
            var xmlFileContent = xmlFileBuilder.Build(CryptographyProvider.Encrypt("someheaderdata", EncryptionKey));
            var dataProvider = new InMemoryStringDataProvider(xmlFileContent);
            var encryptor = new XmlCredentialPasswordEncryptorDecorator(
                CryptographyProvider,
                new XmlCredentialRecordSerializer()
            );
            var decryptor = new XmlCredentialPasswordDecryptorDecorator(
                new XmlCredentialRecordDeserializer()
            );

            return new XmlCredentialRepository(
                new CredentialRepositoryConfig(),
                new CredentialRecordSaver(
                    dataProvider,
                    encryptor
                ), new CredentialRecordLoader(
                    dataProvider,
                    decryptor
                )
            );
        }
    }
}