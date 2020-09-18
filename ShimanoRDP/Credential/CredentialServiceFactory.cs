using System.IO;
using ShimanoRDP.App;
using ShimanoRDP.App.Info;
using ShimanoRDP.Config;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers.CredentialProviderSerializer;
using ShimanoRDP.Config.Serializers.CredentialSerializer;
using ShimanoRDP.Security.Factories;

namespace ShimanoRDP.Credential
{
    public class CredentialServiceFactory
    {
        // When we get a true CompositionRoot we can move this to that class. We should only require 1 instance of this service at a time
        public CredentialServiceFacade Build()
        {
            var cryptoFromSettings = new CryptoProviderFactoryFromSettings();
            var credRepoSerializer = new XmlCredentialPasswordEncryptorDecorator(
                                                                                 cryptoFromSettings.Build(),
                                                                                 new XmlCredentialRecordSerializer());
            var credRepoDeserializer =
                new XmlCredentialPasswordDecryptorDecorator(new XmlCredentialRecordDeserializer());

            var credentialRepoListPath = Path.Combine(SettingsFileInfo.SettingsPath, "credentialRepositories.xml");
            var repoListDataProvider = new FileDataProvider(credentialRepoListPath);
            var repoListLoader = new CredentialRepositoryListLoader(
                                                                    repoListDataProvider,
                                                                    new
                                                                        CredentialRepositoryListDeserializer(credRepoSerializer,
                                                                                                             credRepoDeserializer));
            var repoListSaver = new CredentialRepositoryListSaver(repoListDataProvider);

            return new CredentialServiceFacade(Runtime.CredentialProviderCatalog, repoListLoader, repoListSaver);
        }
    }
}