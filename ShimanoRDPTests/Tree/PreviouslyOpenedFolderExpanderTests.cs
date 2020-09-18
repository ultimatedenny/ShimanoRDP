using System.Linq;
using ShimanoRDP.Connection;
using ShimanoRDP.Container;
using ShimanoRDP.Tree;
using ShimanoRDP.Tree.Root;
using ShimanoRDP.UI.Controls;
using ShimanoRDP.UI.Controls.ConnectionTree;
using NSubstitute;
using NUnit.Framework;


namespace ShimanoRDPTests.Tree
{
    public class PreviouslyOpenedFolderExpanderTests
    {
        private PreviouslyOpenedFolderExpander _folderExpander;
        private IConnectionTree _connectionTree;

        [SetUp]
        public void Setup()
        {
            _folderExpander = new PreviouslyOpenedFolderExpander();
            _connectionTree = Substitute.For<IConnectionTree>();
        }

        [Test]
        public void ExpandsAllFoldersThatAreMarkedForExpansion()
        {
            var connectionTreeModel = GenerateConnectionTreeModel();
            _connectionTree.ConnectionTreeModel.Returns(connectionTreeModel);
            _connectionTree.GetRootConnectionNode().Returns(connectionTreeModel.RootNodes[0]);
            _folderExpander.Execute(_connectionTree);
            Assert.That(_connectionTree.ExpandedObjects, Is.EquivalentTo(connectionTreeModel.GetRecursiveChildList().OfType<ContainerInfo>().Where(info => info.IsExpanded)));
        }

        private ConnectionTreeModel GenerateConnectionTreeModel()
        {
            var connectionTreeModel = new ConnectionTreeModel();
            var root = new RootNodeInfo(RootNodeType.Connection) { IsExpanded = true };
            var folder1 = new ContainerInfo { IsExpanded = true };
            var folder2 = new ContainerInfo();
            var con1 = new ConnectionInfo();
            root.AddChild(folder1);
            folder1.AddChild(folder2);
            root.AddChild(con1);
            connectionTreeModel.AddRootNode(root);
            return connectionTreeModel;
        }
    }
}