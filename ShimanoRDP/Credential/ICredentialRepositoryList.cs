using System;
using System.Collections.Generic;
using ShimanoRDP.Tools.CustomCollections;

namespace ShimanoRDP.Credential
{
    public interface ICredentialRepositoryList : IEnumerable<ICredentialRepository>
    {
        IEnumerable<ICredentialRepository> CredentialProviders { get; }

        void AddProvider(ICredentialRepository credentialProvider);

        void RemoveProvider(ICredentialRepository credentialProvider);

        bool Contains(Guid repositoryId);

        IEnumerable<ICredentialRecord> GetCredentialRecords();

        ICredentialRecord GetCredentialRecord(Guid id);

        event EventHandler<CollectionUpdatedEventArgs<ICredentialRepository>> RepositoriesUpdated;
        event EventHandler<CollectionUpdatedEventArgs<ICredentialRecord>> CredentialsUpdated;
    }
}