﻿using ShimanoRDP.Messages;
using ShimanoRDP.Messages.MessageWriters;
using ShimanoRDP.Messages.WriterDecorators;
using NSubstitute;
using NUnit.Framework;

namespace ShimanoRDPTests.Messages.MessageWriters
{
    public class OnlyLogMessageFilterTests
    {
        private OnlyLogMessageFilter _sut;
        private IMessageWriter _mockWriter;

        [SetUp]
        public void Setup()
        {
            _mockWriter = Substitute.For<IMessageWriter>();
            _sut = new OnlyLogMessageFilter(_mockWriter);
        }

        [Test]
        public void WillWriteIfTheOnlyLogFlagIsNotSet()
        {
            var msg = Substitute.For<IMessage>();
            msg.OnlyLog.Returns(false);
            _sut.Write(msg);
            _mockWriter.Received().Write(msg);
        }

        [Test]
        public void WillNotWriteIfTheOnlyLogFlagIsSet()
        {
            var msg = Substitute.For<IMessage>();
            msg.OnlyLog.Returns(true);
            _sut.Write(msg);
            _mockWriter.DidNotReceiveWithAnyArgs().Write(msg);
        }
    }
}