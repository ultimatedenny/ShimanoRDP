using ShimanoRDP.Security.SymmetricEncryption;

namespace ShimanoRDP.Security.Factories
{
    public class LegacyInsecureCryptoProviderFactory : ICryptoProviderFactory
    {
        public ICryptographyProvider Build()
        {
            return new LegacyRijndaelCryptographyProvider();
        }
    }
}