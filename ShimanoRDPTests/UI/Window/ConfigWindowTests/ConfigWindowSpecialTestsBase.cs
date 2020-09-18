using System.Collections.Generic;
using ShimanoRDP.Connection;
using ShimanoRDP.Connection.Protocol;
using ShimanoRDP.UI.Window;
using NUnit.Framework;

namespace ShimanoRDPTests.UI.Window.ConfigWindowTests
{
	public abstract class ConfigWindowSpecialTestsBase
    {
        protected abstract ProtocolType Protocol { get; }
        protected bool TestAgainstContainerInfo { get; set; } = false;
        protected ConfigWindow ConfigWindow;
        protected ConnectionInfo ConnectionInfo;
        protected List<string> ExpectedPropertyList;

        [SetUp]
        public virtual void Setup()
        {
            ConnectionInfo = ConfigWindowGeneralTests.ConstructConnectionInfo(Protocol, TestAgainstContainerInfo);
            ExpectedPropertyList = ConfigWindowGeneralTests.BuildExpectedConnectionInfoPropertyList(Protocol, TestAgainstContainerInfo);

            ConfigWindow = new ConfigWindow();
        }

        public void RunVerification()
        {
            ConfigWindow.SelectedTreeNode = ConnectionInfo;
            Assert.That(
                ConfigWindow.VisibleObjectProperties,
                Is.EquivalentTo(ExpectedPropertyList));
        }
    }
}
