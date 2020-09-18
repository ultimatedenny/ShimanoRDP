using System;
using System.IO;
using ShimanoRDP.App;
using ShimanoRDP.App.Info;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers;
using ShimanoRDP.UI.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ShimanoRDP.Config.Settings
{
    public class DockPanelLayoutSaver
    {
        private readonly ISerializer<DockPanel, string> _dockPanelSerializer;
        private readonly IDataProvider<string> _dataProvider;

        public DockPanelLayoutSaver(ISerializer<DockPanel, string> dockPanelSerializer,
                                    IDataProvider<string> dataProvider)
        {
            if (dockPanelSerializer == null)
                throw new ArgumentNullException(nameof(dockPanelSerializer));
            if (dataProvider == null)
                throw new ArgumentNullException(nameof(dataProvider));

            _dockPanelSerializer = dockPanelSerializer;
            _dataProvider = dataProvider;
        }

        public void Save()
        {
            try
            {
                if (Directory.Exists(SettingsFileInfo.SettingsPath) == false)
                {
                    Directory.CreateDirectory(SettingsFileInfo.SettingsPath);
                }

                var serializedLayout = _dockPanelSerializer.Serialize(FrmMain.Default.pnlDock);
                _dataProvider.Save(serializedLayout);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionStackTrace("SavePanelsToXML failed", ex);
            }
        }
    }
}