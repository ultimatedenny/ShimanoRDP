namespace ShimanoRDP.Security
{
    public interface ICryptoProviderFactory
    {
        ICryptographyProvider Build();
    }
}