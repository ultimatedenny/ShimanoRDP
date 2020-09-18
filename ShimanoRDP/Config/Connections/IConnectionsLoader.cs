using ShimanoRDP.Tree;

namespace ShimanoRDP.Config.Connections
{
    public interface IConnectionsLoader
    {
        ConnectionTreeModel Load();
    }
}