using System.Security;

namespace ShimanoRDP.Config.Serializers
{
    public interface ISecureSerializer<in TIn, out TOut>
    {
        TOut Serialize(TIn model, SecureString key);
    }
}