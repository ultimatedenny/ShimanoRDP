using System.IO;
using ShimanoRDP.Config.Connections;
using ShimanoRDP.Properties;

namespace ShimanoRDP.App.Initialization
{
    public class CredsAndConsSetup
    {
        public void LoadCredsAndCons()
        {
            new SaveConnectionsOnEdit(Runtime.ConnectionsService);

            if (Settings.Default.FirstStart && !Settings.Default.LoadConsFromCustomLocation &&
                !File.Exists(Runtime.ConnectionsService.GetStartupConnectionFileName()))
                Runtime.ConnectionsService.NewConnectionsFile(Runtime.ConnectionsService
                                                                     .GetStartupConnectionFileName());

            Runtime.LoadConnections();
        }
    }
}