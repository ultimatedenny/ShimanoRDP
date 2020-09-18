using System.Threading;
using ShimanoRDPTests.UI.Window;
using NUnit.Framework;
using WeifenLuo.WinFormsUI.Docking;
using ShimanoRDP.UI.Window;

namespace ShimanoRDPTests.UI.Window
{
    public class ConnectionTreeWindowTests
    {
        private ConnectionTreeWindow _connectionTreeWindow;

        [SetUp]
        public void Setup()
        {
            _connectionTreeWindow = new ConnectionTreeWindow(new DockContent());
        }

        [TearDown]
        public void Teardown()
        {
            _connectionTreeWindow.Close();
        }

        [Test, Apartment(ApartmentState.STA)]
        public void CanShowWindow()
        {
            _connectionTreeWindow.Show();
            Assert.That(_connectionTreeWindow.Visible);
        }
    }
}