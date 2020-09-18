using ShimanoRDP.Connection;
using ShimanoRDP.Container;
using ShimanoRDP.UI.Controls;
using System;
using System.Linq;
using ShimanoRDP.UI.Controls.ConnectionTree;


namespace ShimanoRDP.Tree
{
    public class PreviousSessionOpener : IConnectionTreeDelegate
    {
        private readonly IConnectionInitiator _connectionInitiator;

        public PreviousSessionOpener(IConnectionInitiator connectionInitiator)
        {
            if (connectionInitiator == null)
                throw new ArgumentNullException(nameof(connectionInitiator));
            _connectionInitiator = connectionInitiator;
        }

        public void Execute(IConnectionTree connectionTree)
        {
            var connectionInfoList = connectionTree.GetRootConnectionNode().GetRecursiveChildList()
                                                   .Where(node => !(node is ContainerInfo));
            var previouslyOpenedConnections = connectionInfoList
                .Where(item =>
                           item.PleaseConnect &&
                           //ignore items that have already connected
                           !_connectionInitiator.ActiveConnections.Contains(item.ConstantID));

            foreach (var connectionInfo in previouslyOpenedConnections)
            {
                _connectionInitiator.OpenConnection(connectionInfo);
            }
        }
    }
}