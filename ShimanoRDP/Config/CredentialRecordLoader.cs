using System;
using System.Collections.Generic;
using System.Security;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers;
using ShimanoRDP.Credential;


namespace ShimanoRDP.Config
{
    public class CredentialRecordLoader
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly ISecureDeserializer<string, IEnumerable<ICredentialRecord>> _deserializer;

        public CredentialRecordLoader(IDataProvider<string> dataProvider,
                                      ISecureDeserializer<string, IEnumerable<ICredentialRecord>> deserializer)
        {
            if (dataProvider == null)
                throw new ArgumentNullException(nameof(dataProvider));
            if (deserializer == null)
                throw new ArgumentNullException(nameof(deserializer));

            _dataProvider = dataProvider;
            _deserializer = deserializer;
        }

        public IEnumerable<ICredentialRecord> Load(SecureString key)
        {
            var serializedCredentials = _dataProvider.Load();
            return _deserializer.Deserialize(serializedCredentials, key);
        }
    }
}