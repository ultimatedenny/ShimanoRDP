using System.Threading;
using ShimanoRDP.Container;
using ShimanoRDP.Tree;
using ShimanoRDP.Tree.Root;
using ShimanoRDP.UI.Controls;
using ShimanoRDP.UI.Controls.ConnectionTree;
using NUnit.Framework;


namespace ShimanoRDPTests.Tree
{
    public class ConnectionTreeTests
    {
        private ConnectionTree _connectionTree;
        private ConnectionTreeModel _connectionTreeModel;

        [SetUp]
        public void Setup()
        {
            _connectionTreeModel = CreateConnectionTreeModel();
            _connectionTree = new ConnectionTree
            {
                PostSetupActions = new IConnectionTreeDelegate[] {new RootNodeExpander()}
            };
        }

        [TearDown]
        public void Teardown()
        {
            _connectionTree.Dispose();
        }


        [Test, Apartment(ApartmentState.STA)]
        public void CanDeleteLastFolderInTheTree()
        {
            var lastFolder = new ContainerInfo();
            _connectionTreeModel.RootNodes[0].AddChild(lastFolder);
            _connectionTree.ConnectionTreeModel = _connectionTreeModel;
            _connectionTree.SelectObject(lastFolder);
            _connectionTree.DeleteSelectedNode();
            Assert.That(_connectionTree.GetRootConnectionNode().HasChildren, Is.False);
        }

        private ConnectionTreeModel CreateConnectionTreeModel()
        {
            var connectionTreeModel = new ConnectionTreeModel();
            connectionTreeModel.AddRootNode(new RootNodeInfo(RootNodeType.Connection));
            return connectionTreeModel;
        }
    }
}