using System;
using System.Collections.Generic;
using System.Security;
using ShimanoRDP.Credential.Repositories;
using ShimanoRDP.Tools.CustomCollections;


namespace ShimanoRDP.Credential
{
    public interface ICredentialRepository
    {
        ICredentialRepositoryConfig Config { get; }
        IList<ICredentialRecord> CredentialRecords { get; }
        bool IsLoaded { get; }
        void LoadCredentials(SecureString key);
        void SaveCredentials(SecureString key);
        void UnloadCredentials();
        event EventHandler RepositoryConfigUpdated;
        event EventHandler<CollectionUpdatedEventArgs<ICredentialRecord>> CredentialsUpdated;
    }
}