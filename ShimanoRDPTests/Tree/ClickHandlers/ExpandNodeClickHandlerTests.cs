using System;
using ShimanoRDP.Connection;
using ShimanoRDP.Container;
using ShimanoRDP.Tree;
using ShimanoRDP.Tree.ClickHandlers;
using ShimanoRDP.UI.Controls;
using ShimanoRDP.UI.Controls.ConnectionTree;
using NSubstitute;
using NUnit.Framework;


namespace ShimanoRDPTests.Tree
{
    public class ExpandNodeClickHandlerTests
    {
        private ExpandNodeClickHandler _clickHandler;
        private IConnectionTree _connectionTree;

        [SetUp]
        public void Setup()
        {
            _connectionTree = Substitute.For<IConnectionTree>();
            _clickHandler = new ExpandNodeClickHandler(_connectionTree);
        }

        [Test]
        public void TargetedNodeIsExpanded()
        {
            var folder = new ContainerInfo();
            _clickHandler.Execute(folder);
            _connectionTree.Received().ToggleExpansion(folder);
        }

        [Test]
        public void NothingHappensWhenConnectionInfoProvided()
        {
            _clickHandler.Execute(new ConnectionInfo());
            _connectionTree.DidNotReceiveWithAnyArgs().ToggleExpansion(new ConnectionInfo());
        }

        [Test]
        public void ExceptionThrownOnConstructorNullArg()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new ExpandNodeClickHandler(null));
        }
    }
}