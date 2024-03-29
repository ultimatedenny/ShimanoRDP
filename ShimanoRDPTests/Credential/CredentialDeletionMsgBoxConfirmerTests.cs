﻿using System.Windows.Forms;
using ShimanoRDP.Credential;
using NSubstitute;
using NUnit.Framework;

namespace ShimanoRDPTests.Credential
{
    public class CredentialDeletionMsgBoxConfirmerTests
    {
        private ICredentialRecord _credentialRecord;

        [SetUp]
        public void Setup()
        {
            _credentialRecord = Substitute.For<ICredentialRecord>();
        }

        [Test]
        public void ClickingYesReturnsTrue()
        {
            var deletionConfirmer = new CredentialDeletionMsgBoxConfirmer(MockClickYes);
            Assert.That(deletionConfirmer.Confirm(new[] { _credentialRecord }), Is.True);
        }

        [Test]
        public void ClickingNoReturnsFalse()
        {
            var deletionConfirmer = new CredentialDeletionMsgBoxConfirmer(MockClickNo);
            Assert.That(deletionConfirmer.Confirm(new [] { _credentialRecord }), Is.False);
        }

        private DialogResult MockClickYes(string promptMessage, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return DialogResult.Yes;
        }

        private DialogResult MockClickNo(string promptMessage, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return DialogResult.No;
        }
    }
}
