using System.Security;

namespace ShimanoRDP.Security.Authentication
{
    public interface IAuthenticator
    {
        bool Authenticate(SecureString password);
    }
}