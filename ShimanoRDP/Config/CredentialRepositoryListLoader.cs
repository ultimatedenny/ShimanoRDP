using System;
using System.Collections.Generic;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers.CredentialProviderSerializer;
using ShimanoRDP.Credential;

namespace ShimanoRDP.Config
{
    public class CredentialRepositoryListLoader : ILoader<IEnumerable<ICredentialRepository>>
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly CredentialRepositoryListDeserializer _deserializer;

        public CredentialRepositoryListLoader(IDataProvider<string> dataProvider,
                                              CredentialRepositoryListDeserializer deserializer)
        {
            if (dataProvider == null)
                throw new ArgumentNullException(nameof(dataProvider));
            if (deserializer == null)
                throw new ArgumentNullException(nameof(deserializer));

            _dataProvider = dataProvider;
            _deserializer = deserializer;
        }

        public IEnumerable<ICredentialRepository> Load()
        {
            var data = _dataProvider.Load();
            return _deserializer.Deserialize(data);
        }
    }
}