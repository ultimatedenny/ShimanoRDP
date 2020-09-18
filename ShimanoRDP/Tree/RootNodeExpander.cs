using ShimanoRDP.UI.Controls;
using ShimanoRDP.UI.Controls.ConnectionTree;


namespace ShimanoRDP.Tree
{
    public class RootNodeExpander : IConnectionTreeDelegate
    {
        public void Execute(IConnectionTree connectionTree)
        {
            var rootConnectionNode = connectionTree.GetRootConnectionNode();
            connectionTree.InvokeExpand(rootConnectionNode);
        }
    }
}