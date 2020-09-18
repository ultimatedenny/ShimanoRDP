using ShimanoRDP.Connection.Protocol;
using ShimanoRDP.Connection.Protocol.RDP;
using NUnit.Framework;

namespace ShimanoRDPTests.UI.Window.ConfigWindowTests
{
    public class ConfigWindowRdpSpecialTests : ConfigWindowSpecialTestsBase
    {
        protected override ProtocolType Protocol => ProtocolType.RDP;

        [Test]
        public void PropertyShownWhenActive_RdpMinutesToIdleTimeout()
        {
            ConnectionInfo.RDPMinutesToIdleTimeout = 1;
            ExpectedPropertyList.Add(nameof(ShimanoRDP.Connection.ConnectionInfo.RDPAlertIdleTimeout));

            RunVerification();
        }

        [TestCase(RDGatewayUsageMethod.Always)]
        [TestCase(RDGatewayUsageMethod.Detect)]
        public void RdGatewayPropertiesShown_WhenRdGatewayUsageMethodIsNotNever(RDGatewayUsageMethod gatewayUsageMethod)
        {
            ConnectionInfo.RDGatewayUsageMethod = gatewayUsageMethod;
            ConnectionInfo.RDGatewayUseConnectionCredentials = RDGatewayUseConnectionCredentials.Yes;
            ExpectedPropertyList.AddRange(new []
            {
                nameof(ShimanoRDP.Connection.ConnectionInfo.RDGatewayHostname),
                nameof(ShimanoRDP.Connection.ConnectionInfo.RDGatewayUseConnectionCredentials)
            });

            RunVerification();
        }

        [TestCase(RDGatewayUseConnectionCredentials.No)]
        [TestCase(RDGatewayUseConnectionCredentials.SmartCard)]
        public void RdGatewayPropertiesShown_WhenRDGatewayUseConnectionCredentialsIsNotYes(RDGatewayUseConnectionCredentials useConnectionCredentials)
        {
            ConnectionInfo.RDGatewayUsageMethod = RDGatewayUsageMethod.Always;
            ConnectionInfo.RDGatewayUseConnectionCredentials = useConnectionCredentials;
            ExpectedPropertyList.AddRange(new []
            {
                nameof(ShimanoRDP.Connection.ConnectionInfo.RDGatewayHostname),
                nameof(ShimanoRDP.Connection.ConnectionInfo.RDGatewayUsername),
                nameof(ShimanoRDP.Connection.ConnectionInfo.RDGatewayPassword),
                nameof(ShimanoRDP.Connection.ConnectionInfo.RDGatewayDomain),
                nameof(ShimanoRDP.Connection.ConnectionInfo.RDGatewayUseConnectionCredentials)
            });

            RunVerification();
        }

        [Test]
        public void SoundQualityPropertyShown_WhenRdpSoundsSetToBringToThisComputer()
        {
            ConnectionInfo.RedirectSound = RDPSounds.BringToThisComputer;
            ExpectedPropertyList.Add(nameof(ShimanoRDP.Connection.ConnectionInfo.SoundQuality));

            RunVerification();
        }

        [TestCase(RDPResolutions.FitToWindow)]
        [TestCase(RDPResolutions.Fullscreen)]
        public void AutomaticResizePropertyShown_WhenResolutionIsDynamic(RDPResolutions resolution)
        {
            ConnectionInfo.Resolution = resolution;
            ExpectedPropertyList.Add(nameof(ShimanoRDP.Connection.ConnectionInfo.AutomaticResize));

            RunVerification();
        }
    }
}
