using System.Collections.Generic;

namespace ShimanoRDP.Credential.Repositories
{
    public class CredentialRepoUnlockerBuilder
    {
        public CompositeRepositoryUnlocker Build(IEnumerable<ICredentialRepository> repos)
        {
            return new CompositeRepositoryUnlocker(repos);
        }
    }
}