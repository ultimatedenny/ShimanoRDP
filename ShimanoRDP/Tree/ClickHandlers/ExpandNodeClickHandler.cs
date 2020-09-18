using System;
using ShimanoRDP.Connection;
using ShimanoRDP.Container;
using ShimanoRDP.UI.Controls.ConnectionTree;

namespace ShimanoRDP.Tree.ClickHandlers
{
    public class ExpandNodeClickHandler : ITreeNodeClickHandler<ConnectionInfo>
    {
        private readonly IConnectionTree _connectionTree;

        public ExpandNodeClickHandler(IConnectionTree connectionTree)
        {
            if (connectionTree == null)
                throw new ArgumentNullException(nameof(connectionTree));

            _connectionTree = connectionTree;
        }

        public void Execute(ConnectionInfo clickedNode)
        {
            var clickedNodeAsContainer = clickedNode as ContainerInfo;
            if (clickedNodeAsContainer == null) return;
            _connectionTree.ToggleExpansion(clickedNodeAsContainer);
        }
    }
}