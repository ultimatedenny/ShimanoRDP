﻿using ShimanoRDP.Connection;
using ShimanoRDP.Tree;
using NUnit.Framework;
using System.Windows.Forms;


namespace ShimanoRDPTests.Tree
{
    public class SelectedConnectionDeletionConfirmerTests
    {
        private ConnectionInfo _testConnectionInfo;

        [SetUp]
        public void Setup()
        {
            _testConnectionInfo = new ConnectionInfo();
        }

        [Test]
        public void ClickingYesReturnsTrue()
        {
            var deletionConfirmer = new SelectedConnectionDeletionConfirmer(MockClickYes);
            Assert.That(deletionConfirmer.Confirm(_testConnectionInfo), Is.True);
        }

        [Test]
        public void ClickingNoReturnsFalse()
        {
            var deletionConfirmer = new SelectedConnectionDeletionConfirmer(MockClickNo);
            Assert.That(deletionConfirmer.Confirm(_testConnectionInfo), Is.False);
        }

        private DialogResult MockClickYes(string promptMessage)
        {
            return DialogResult.Yes;
        }

        private DialogResult MockClickNo(string promptMessage)
        {
            return DialogResult.No;
        }
    }
}