using System;
using System.Collections.Generic;
using System.Security;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers;
using ShimanoRDP.Credential;


namespace ShimanoRDP.Config
{
    public class CredentialRecordSaver
    {
        private readonly IDataProvider<string> _dataProvider;
        private readonly ISecureSerializer<IEnumerable<ICredentialRecord>, string> _serializer;

        public CredentialRecordSaver(IDataProvider<string> dataProvider,
                                     ISecureSerializer<IEnumerable<ICredentialRecord>, string> serializer)
        {
            if (dataProvider == null)
                throw new ArgumentNullException(nameof(dataProvider));
            if (serializer == null)
                throw new ArgumentNullException(nameof(serializer));

            _dataProvider = dataProvider;
            _serializer = serializer;
        }

        public void Save(IEnumerable<ICredentialRecord> credentialRecords, SecureString key)
        {
            var serializedCredentials = _serializer.Serialize(credentialRecords, key);
            _dataProvider.Save(serializedCredentials);
        }
    }
}