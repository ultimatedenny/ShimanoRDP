using System.Linq;
using ShimanoRDP.Container;
using ShimanoRDP.UI.Controls;
using ShimanoRDP.UI.Controls.ConnectionTree;


namespace ShimanoRDP.Tree
{
    public class PreviouslyOpenedFolderExpander : IConnectionTreeDelegate
    {
        public void Execute(IConnectionTree connectionTree)
        {
            var rootNode = connectionTree.GetRootConnectionNode();
            var containerList = connectionTree.ConnectionTreeModel.GetRecursiveChildList(rootNode)
                                              .OfType<ContainerInfo>();
            var previouslyExpandedNodes = containerList.Where(container => container.IsExpanded);
            connectionTree.ExpandedObjects = previouslyExpandedNodes;
            connectionTree.InvokeRebuildAll(true);
        }
    }
}