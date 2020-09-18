using System.Security;
using ShimanoRDP.Tools;

namespace ShimanoRDP.Security
{
    public interface IKeyProvider
    {
        Optional<SecureString> GetKey();
    }
}