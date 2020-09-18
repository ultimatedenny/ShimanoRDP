﻿using System;
using ShimanoRDP.App;
using ShimanoRDP.Config.Connections;
using ShimanoRDP.Config.Connections.Multiuser;
using ShimanoRDP.Config.DatabaseConnectors;
using ShimanoRDP.Properties;
using ShimanoRDP.Resources.Language;
using ShimanoRDP.Security.SymmetricEncryption;

namespace ShimanoRDP.UI.Forms.OptionsPages
{
    public sealed partial class SqlServerPage
    {
        private readonly DatabaseConnectionTester _databaseConnectionTester;

        public SqlServerPage()
        {
            InitializeComponent();
            ApplyTheme();
            PageIcon = Properties.Resources.Database_Icon;
            _databaseConnectionTester = new DatabaseConnectionTester();
        }

        public override string PageName
        {
            get => Language.SQLServer.TrimEnd(':');
            set { }
        }

        public override void ApplyLanguage()
        {
            base.ApplyLanguage();

            lblExperimental.Text = Language.Experimental.ToUpper();
            lblSQLInfo.Text = Language.SQLInfo;

            chkUseSQLServer.Text = Language.UseSQLServer;
            lblSQLServer.Text = Language.Hostname;
            lblSQLDatabaseName.Text = Language.Database;
            lblSQLUsername.Text = Language.Username;
            lblSQLPassword.Text = Language.Password;
            lblSQLReadOnly.Text = Language.ReadOnly;
            btnTestConnection.Text = Language.TestConnection;
        }

        public override void LoadSettings()
        {
            chkUseSQLServer.Checked = Settings.Default.UseSQLServer;
            txtSQLType.Text = Settings.Default.SQLServerType;
            txtSQLServer.Text = Settings.Default.SQLHost;
            txtSQLDatabaseName.Text = Settings.Default.SQLDatabaseName;
            txtSQLUsername.Text = Settings.Default.SQLUser;
            var cryptographyProvider = new LegacyRijndaelCryptographyProvider();
            txtSQLPassword.Text = cryptographyProvider.Decrypt(Settings.Default.SQLPass, Runtime.EncryptionKey);
            chkSQLReadOnly.Checked = Settings.Default.SQLReadOnly;
            lblTestConnectionResults.Text = "";
        }

        public override void SaveSettings()
        {
            base.SaveSettings();
            var sqlServerWasPreviouslyEnabled = Settings.Default.UseSQLServer;

            Settings.Default.UseSQLServer = chkUseSQLServer.Checked;
            Settings.Default.SQLServerType = txtSQLType.Text;
            Settings.Default.SQLHost = txtSQLServer.Text;
            Settings.Default.SQLDatabaseName = txtSQLDatabaseName.Text;
            Settings.Default.SQLUser = txtSQLUsername.Text;
            var cryptographyProvider = new LegacyRijndaelCryptographyProvider();
            Settings.Default.SQLPass = cryptographyProvider.Encrypt(txtSQLPassword.Text, Runtime.EncryptionKey);
            Settings.Default.SQLReadOnly = chkSQLReadOnly.Checked;

            if (Settings.Default.UseSQLServer)
                ReinitializeSqlUpdater();
            else if (!Settings.Default.UseSQLServer && sqlServerWasPreviouslyEnabled)
                DisableSql();
        }

        private static void ReinitializeSqlUpdater()
        {
            Runtime.ConnectionsService.RemoteConnectionsSyncronizer?.Dispose();
            Runtime.ConnectionsService.RemoteConnectionsSyncronizer =
                new RemoteConnectionsSyncronizer(new SqlConnectionsUpdateChecker());
            Runtime.ConnectionsService.LoadConnections(true, false, "");
        }

        private void DisableSql()
        {
            Runtime.ConnectionsService.RemoteConnectionsSyncronizer?.Dispose();
            Runtime.ConnectionsService.RemoteConnectionsSyncronizer = null;
            Runtime.LoadConnections(true);
        }

        private void chkUseSQLServer_CheckedChanged(object sender, EventArgs e)
        {
            toggleSQLPageControls(chkUseSQLServer.Checked);
        }

        private void toggleSQLPageControls(bool useSQLServer)
        {
            lblSQLType.Enabled = useSQLServer;
            lblSQLServer.Enabled = useSQLServer;
            lblSQLDatabaseName.Enabled = useSQLServer;
            lblSQLUsername.Enabled = useSQLServer;
            lblSQLPassword.Enabled = useSQLServer;
            lblSQLReadOnly.Enabled = useSQLServer;
            txtSQLType.Enabled = useSQLServer;
            txtSQLServer.Enabled = useSQLServer;
            txtSQLDatabaseName.Enabled = useSQLServer;
            txtSQLUsername.Enabled = useSQLServer;
            txtSQLPassword.Enabled = useSQLServer;
            chkSQLReadOnly.Enabled = useSQLServer;
            btnTestConnection.Enabled = useSQLServer;
        }

        private async void btnTestConnection_Click(object sender, EventArgs e)
        {
            var type = txtSQLType.Text;
            var server = txtSQLServer.Text;
            var database = txtSQLDatabaseName.Text;
            var username = txtSQLUsername.Text;
            var password = txtSQLPassword.Text;

            lblTestConnectionResults.Text = Language.TestingConnection;
            imgConnectionStatus.Image = Properties.Resources.loading_spinner;
            btnTestConnection.Enabled = false;

            var connectionTestResult =
                await _databaseConnectionTester.TestConnectivity(type, server, database, username, password);

            btnTestConnection.Enabled = true;

            switch (connectionTestResult)
            {
                case ConnectionTestResult.ConnectionSucceded:
                    UpdateConnectionImage(true);
                    lblTestConnectionResults.Text = Language.ConnectionSuccessful;
                    break;
                case ConnectionTestResult.ServerNotAccessible:
                    UpdateConnectionImage(false);
                    lblTestConnectionResults.Text =
                        BuildTestFailedMessage(string.Format(Language.ServerNotAccessible, server));
                    break;
                case ConnectionTestResult.CredentialsRejected:
                    UpdateConnectionImage(false);
                    lblTestConnectionResults.Text =
                        BuildTestFailedMessage(string.Format(Language.LoginFailedForUser, username));
                    break;
                case ConnectionTestResult.UnknownDatabase:
                    UpdateConnectionImage(false);
                    lblTestConnectionResults.Text =
                        BuildTestFailedMessage(string.Format(Language.DatabaseNotAvailable, database));
                    break;
                case ConnectionTestResult.UnknownError:
                    UpdateConnectionImage(false);
                    lblTestConnectionResults.Text = BuildTestFailedMessage(Language.RdpErrorUnknown);
                    break;
                default:
                    UpdateConnectionImage(false);
                    lblTestConnectionResults.Text = BuildTestFailedMessage(Language.RdpErrorUnknown);
                    break;
            }
        }

        private void UpdateConnectionImage(bool connectionSuccess)
        {
            imgConnectionStatus.Image = connectionSuccess
                ? Properties.Resources.tick
                : Properties.Resources.exclamation;
        }

        private string BuildTestFailedMessage(string specificMessage)
        {
            return Language.ConnectionOpenFailed +
                   Environment.NewLine +
                   specificMessage;
        }
    }
}