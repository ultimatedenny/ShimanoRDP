using System;

namespace ShimanoRDP.Config.Serializers.Versioning
{
    public interface IVersionUpgrader
    {
        bool CanUpgrade(Version currentVersion);
        Version Upgrade();
    }
}