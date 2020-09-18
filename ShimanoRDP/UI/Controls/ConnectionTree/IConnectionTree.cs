using System.Collections;
using ShimanoRDP.Connection;
using ShimanoRDP.Tree;
using ShimanoRDP.Tree.Root;

namespace ShimanoRDP.UI.Controls.ConnectionTree
{
    public interface IConnectionTree
    {
        ConnectionTreeModel ConnectionTreeModel { get; set; }

        ConnectionInfo SelectedNode { get; }

        IEnumerable ExpandedObjects { get; set; }

        RootNodeInfo GetRootConnectionNode();

        void InvokeExpand(object model);

        void InvokeRebuildAll(bool preserveState);

        void ToggleExpansion(object model);
    }
}