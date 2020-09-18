using System;
using ShimanoRDP.Config.DatabaseConnectors;
using ShimanoRDP.Config.Serializers.Versioning;
using NSubstitute;
using NUnit.Framework;

namespace ShimanoRDPTests.Config.Serializers.Versioning
{
    public class SqlVersion22To23UpgraderTests
    {
        private SqlVersion22To23Upgrader _versionUpgrader;

        [SetUp]
        public void Setup()
        {
            var sqlConnector = Substitute.For<MSSqlDatabaseConnector>("", "", "", "");
            _versionUpgrader = new SqlVersion22To23Upgrader(sqlConnector);
        }

        [Test]
        public void CanUpgradeIfVersionIs22()
        {
            var currentVersion = new Version(2, 2);
            var canUpgrade = _versionUpgrader.CanUpgrade(currentVersion);
            Assert.That(canUpgrade, Is.True);
        }
    }
}