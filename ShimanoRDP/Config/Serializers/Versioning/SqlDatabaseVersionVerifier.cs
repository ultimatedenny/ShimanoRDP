﻿using ShimanoRDP.App;
using ShimanoRDP.App.Info;
using ShimanoRDP.Config.DatabaseConnectors;
using ShimanoRDP.Messages;
using System;
using ShimanoRDP.Resources.Language;

namespace ShimanoRDP.Config.Serializers.Versioning
{
    public class SqlDatabaseVersionVerifier
    {
        private readonly IDatabaseConnector _databaseConnector;

        public SqlDatabaseVersionVerifier(IDatabaseConnector DatabaseConnector)
        {
            if (DatabaseConnector == null)
                throw new ArgumentNullException(nameof(DatabaseConnector));

            _databaseConnector = DatabaseConnector;
        }

        public bool VerifyDatabaseVersion(Version dbVersion)
        {
            var isVerified = false;
            try
            {
                var databaseVersion = dbVersion;

                if (databaseVersion.Equals(new Version()))
                {
                    return true;
                }

                var dbUpgraders = new IVersionUpgrader[]
                {
                    new SqlVersion22To23Upgrader(_databaseConnector),
                    new SqlVersion23To24Upgrader(_databaseConnector),
                    new SqlVersion24To25Upgrader(_databaseConnector),
                    new SqlVersion25To26Upgrader(_databaseConnector),
                    new SqlVersion26To27Upgrader(_databaseConnector),
                    new SqlVersion27To28Upgrader(_databaseConnector),
                };

                foreach (var upgrader in dbUpgraders)
                {
                    if (upgrader.CanUpgrade(databaseVersion))
                    {
                        databaseVersion = upgrader.Upgrade();
                    }
                }

                // DB is at the highest current supported version
                if (databaseVersion.CompareTo(new Version(2, 8)) == 0)
                    isVerified = true;

                if (isVerified == false)
                    Runtime.MessageCollector.AddMessage(MessageClass.WarningMsg,
                                                        string.Format(Language.ErrorBadDatabaseVersion,
                                                                      databaseVersion,
                                                                      GeneralAppInfo.ProductName));
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddMessage(MessageClass.ErrorMsg,
                                                    string.Format(Language.ErrorVerifyDatabaseVersionFailed,
                                                                  ex.Message));
            }

            return isVerified;
        }
    }
}