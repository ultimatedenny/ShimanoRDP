using ShimanoRDP.App;
using ShimanoRDP.App.Info;
using ShimanoRDP.UI.Forms;
using ShimanoRDP.UI.Window;
using System;
using System.IO;
using ShimanoRDP.Messages;
using WeifenLuo.WinFormsUI.Docking;

namespace ShimanoRDP.Config.Settings
{
    public class DockPanelLayoutLoader
    {
        private readonly FrmMain _mainForm;
        private readonly MessageCollector _messageCollector;

        public DockPanelLayoutLoader(FrmMain mainForm, MessageCollector messageCollector)
        {
            if (mainForm == null)
                throw new ArgumentNullException(nameof(mainForm));
            if (messageCollector == null)
                throw new ArgumentNullException(nameof(messageCollector));

            _mainForm = mainForm;
            _messageCollector = messageCollector;
        }

        public void LoadPanelsFromXml()
        {
            try
            {
                while (_mainForm.pnlDock.Contents.Count > 0)
                {
                    var dc = (DockContent)_mainForm.pnlDock.Contents[0];
                    dc.Close();
                }

#if !PORTABLE
                var oldPath =
 Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + GeneralAppInfo.ProductName + "\\" + SettingsFileInfo.LayoutFileName;
#endif
                var newPath = SettingsFileInfo.SettingsPath + "\\" + SettingsFileInfo.LayoutFileName;
                if (File.Exists(newPath))
                {
                    _mainForm.pnlDock.LoadFromXml(newPath, GetContentFromPersistString);
#if !PORTABLE
				}
				else if (File.Exists(oldPath))
				{
					_mainForm.pnlDock.LoadFromXml(oldPath, GetContentFromPersistString);
#endif
                }
                else
                {
                    _mainForm.SetDefaultLayout();
                }
            }
            catch (Exception ex)
            {
                _messageCollector.AddExceptionMessage("LoadPanelsFromXML failed", ex);
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            // pnlLayout.xml persistence XML fix for refactoring to ShimanoRDP
            if (persistString.StartsWith("mRemote."))
                persistString = persistString.Replace("mRemote.", "ShimanoRDP.");

            try
            {
                if (persistString == typeof(ConfigWindow).ToString())
                    return Windows.ConfigForm;

                if (persistString == typeof(ConnectionTreeWindow).ToString())
                    return Windows.TreeForm;

                if (persistString == typeof(ErrorAndInfoWindow).ToString())
                    return Windows.ErrorsForm;

                if (persistString == typeof(ScreenshotManagerWindow).ToString())
                    return Windows.ScreenshotForm;
            }
            catch (Exception ex)
            {
                _messageCollector.AddExceptionMessage("GetContentFromPersistString failed", ex);
            }

            return null;
        }
    }
}