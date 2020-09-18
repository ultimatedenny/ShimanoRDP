using System.Security;

namespace ShimanoRDP.Config.Serializers
{
    public interface ISecureDeserializer<in TIn, out TOut>
    {
        TOut Deserialize(TIn serializedData, SecureString key);
    }
}