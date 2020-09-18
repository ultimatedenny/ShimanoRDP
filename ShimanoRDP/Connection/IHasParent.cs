using ShimanoRDP.Container;

namespace ShimanoRDP.Connection
{
    public interface IHasParent
    {
        ContainerInfo Parent { get; }

        void SetParent(ContainerInfo containerInfo);

        void RemoveParent();
    }
}