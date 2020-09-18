using System;
using System.Collections.Generic;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers.CredentialProviderSerializer;
using ShimanoRDP.Credential;

namespace ShimanoRDP.Config
{
    public class CredentialRepositoryListSaver : ISaver<IEnumerable<ICredentialRepository>>
    {
        private readonly IDataProvider<string> _dataProvider;

        public CredentialRepositoryListSaver(IDataProvider<string> dataProvider)
        {
            if (dataProvider == null)
                throw new ArgumentNullException(nameof(dataProvider));

            _dataProvider = dataProvider;
        }

        public void Save(IEnumerable<ICredentialRepository> repositories, string propertyNameTrigger = "")
        {
            var serializer = new CredentialRepositoryListSerializer();
            var data = serializer.Serialize(repositories);
            _dataProvider.Save(data);
        }
    }
}