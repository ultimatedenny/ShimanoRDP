using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using ShimanoRDP.App.Info;
using ShimanoRDP.App.Initialization;
using ShimanoRDP.App.Update;
using ShimanoRDP.Config.Connections;
using ShimanoRDP.Config.Connections.Multiuser;
using ShimanoRDP.Connection;
using ShimanoRDP.Messages;
using ShimanoRDP.Properties;
using ShimanoRDP.Tools;
using ShimanoRDP.Tools.Cmdline;
using ShimanoRDP.UI;
using ShimanoRDP.UI.Forms;


namespace ShimanoRDP.App
{
    public class Startup
    {
        private AppUpdater _appUpdate;
        private readonly ConnectionIconLoader _connectionIconLoader;
        private readonly FrmMain _frmMain = FrmMain.Default;

        public static Startup Instance { get; } = new Startup();

        private Startup()
        {
            _appUpdate = new AppUpdater();
            _connectionIconLoader = new ConnectionIconLoader(GeneralAppInfo.HomePath + "\\Icons\\");
        }

        static Startup()
        {
        }

        public void InitializeProgram(MessageCollector messageCollector)
        {
            Debug.Print("---------------------------" + Environment.NewLine + "[START] - " +
                        Convert.ToString(DateTime.Now, CultureInfo.InvariantCulture));
            var startupLogger = new StartupDataLogger(messageCollector);
            startupLogger.LogStartupData();
            CompatibilityChecker.CheckCompatibility(messageCollector);
            ParseCommandLineArgs(messageCollector);
            IeBrowserEmulation.Register();
            _connectionIconLoader.GetConnectionIcons();
            DefaultConnectionInfo.Instance.LoadFrom(Settings.Default, a => "ConDefault" + a);
            DefaultConnectionInheritance.Instance.LoadFrom(Settings.Default, a => "InhDefault" + a);
        }

        private static void ParseCommandLineArgs(MessageCollector messageCollector)
        {
            var interpreter = new StartupArgumentsInterpreter(messageCollector);
            interpreter.ParseArguments(Environment.GetCommandLineArgs());
        }

        public void CreateConnectionsProvider(MessageCollector messageCollector)
        {
            messageCollector.AddMessage(MessageClass.DebugMsg, "Determining if we need a database syncronizer");
            if (!Settings.Default.UseSQLServer) return;
            messageCollector.AddMessage(MessageClass.DebugMsg, "Creating database syncronizer");
            Runtime.ConnectionsService.RemoteConnectionsSyncronizer =
                new RemoteConnectionsSyncronizer(new SqlConnectionsUpdateChecker());
            Runtime.ConnectionsService.RemoteConnectionsSyncronizer.Enable();
        }

        public void CheckForUpdate()
        {
            if (_appUpdate == null)
            {
                _appUpdate = new AppUpdater();
            }
            else if (_appUpdate.IsGetUpdateInfoRunning)
            {
                return;
            }

            var nextUpdateCheck =
                Convert.ToDateTime(Settings.Default.CheckForUpdatesLastCheck.Add(
                                                                                 TimeSpan
                                                                                     .FromDays(Convert.ToDouble(Settings
                                                                                                                .Default
                                                                                                                .CheckForUpdatesFrequencyDays))));
            if (!Settings.Default.UpdatePending && DateTime.UtcNow < nextUpdateCheck)
            {
                return;
            }

            _appUpdate.GetUpdateInfoCompletedEvent += GetUpdateInfoCompleted;
            _appUpdate.GetUpdateInfoAsync();
        }

        private void GetUpdateInfoCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (_frmMain.InvokeRequired)
            {
                _frmMain.Invoke(new AsyncCompletedEventHandler(GetUpdateInfoCompleted), sender, e);
                return;
            }

            try
            {
                _appUpdate.GetUpdateInfoCompletedEvent -= GetUpdateInfoCompleted;

                if (e.Cancelled)
                {
                    return;
                }

                if (e.Error != null)
                {
                    throw e.Error;
                }

                if (_appUpdate.IsUpdateAvailable())
                {
                    Windows.Show(WindowType.Update);
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionMessage("GetUpdateInfoCompleted() failed.", ex);
            }
        }
    }
}