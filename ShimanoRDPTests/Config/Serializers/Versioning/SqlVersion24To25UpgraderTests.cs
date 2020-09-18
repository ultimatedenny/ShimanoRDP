using System;
using ShimanoRDP.Config.DatabaseConnectors;
using ShimanoRDP.Config.Serializers.Versioning;
using NSubstitute;
using NUnit.Framework;

namespace ShimanoRDPTests.Config.Serializers.Versioning
{
    public class SqlVersion24To25UpgraderTests
    {
        private SqlVersion24To25Upgrader _versionUpgrader;

        [SetUp]
        public void Setup()
        {
            var sqlConnector = Substitute.For<MSSqlDatabaseConnector>("", "", "", "");
            _versionUpgrader = new SqlVersion24To25Upgrader(sqlConnector);
        }

        [Test]
        public void CanUpgradeIfVersionIs24()
        {
            var currentVersion = new Version(2, 4);
            var canUpgrade = _versionUpgrader.CanUpgrade(currentVersion);
            Assert.That(canUpgrade, Is.True);
        }
    }
}