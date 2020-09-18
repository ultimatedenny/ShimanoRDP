using System;
using ShimanoRDP.Config.DatabaseConnectors;
using ShimanoRDP.Config.Serializers.Versioning;
using NSubstitute;
using NUnit.Framework;

namespace ShimanoRDPTests.Config.Serializers.Versioning
{
    public class SqlVersion23To24UpgraderTests
    {
        private SqlVersion23To24Upgrader _versionUpgrader;

        [SetUp]
        public void Setup()
        {
            var sqlConnector = Substitute.For<MSSqlDatabaseConnector>("", "", "", "");
            _versionUpgrader = new SqlVersion23To24Upgrader(sqlConnector);
        }

        [Test]
        public void CanUpgradeIfVersionIs23()
        {
            var currentVersion = new Version(2, 3);
            var canUpgrade = _versionUpgrader.CanUpgrade(currentVersion);
            Assert.That(canUpgrade, Is.True);
        }
    }
}