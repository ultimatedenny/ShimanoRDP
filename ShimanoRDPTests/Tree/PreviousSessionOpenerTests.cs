﻿using System;
using ShimanoRDP.Connection;
using ShimanoRDP.Tree;
using ShimanoRDP.Tree.Root;
using ShimanoRDP.UI.Controls;
using ShimanoRDP.UI.Controls.ConnectionTree;
using NSubstitute;
using NUnit.Framework;


namespace ShimanoRDPTests.Tree
{
    public class PreviousSessionOpenerTests
    {
        private PreviousSessionOpener _previousSessionOpener;
        private IConnectionInitiator _connectionInitiator;
        private IConnectionTree _connectionTree;

        [SetUp]
        public void Setup()
        {
            _connectionInitiator = Substitute.For<IConnectionInitiator>();
            _previousSessionOpener = new PreviousSessionOpener(_connectionInitiator);
            _connectionTree = Substitute.For<IConnectionTree>();
            _connectionTree.GetRootConnectionNode().Returns(BuildTree());
        }

        [Test]
        public void AllRequestedSessionsAreReopened()
        {
            _previousSessionOpener.Execute(_connectionTree);
            _connectionInitiator.ReceivedWithAnyArgs(2).OpenConnection(new ConnectionInfo());
        }

        [Test]
        public void ExceptionThrownWhenConstructorGivenNullArg()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new PreviousSessionOpener(null));
        }

        private RootNodeInfo BuildTree()
        {
            var root = new RootNodeInfo(RootNodeType.Connection);
            root.AddChild(new ConnectionInfo { PleaseConnect = true });
            root.AddChild(new ConnectionInfo());
            root.AddChild(new ConnectionInfo { PleaseConnect = true });
            return root;
        }
    }
}