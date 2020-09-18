using CefSharp;
using ShimanoRDP.Connection.Protocol.Http;
using System;
using WeifenLuo.WinFormsUI.Docking;


namespace ShimanoRDP.UI.Window
{
    public partial class HelpWindow : BaseWindow
    {
        public HelpWindow()
        {
            WindowType = WindowType.Help;
            DockPnl = new DockContent();
            InitializeComponent();
        }

        private void HelpWindow_Load(object sender, EventArgs e)
        {
            cefBrwoser.RequestHandler = new RequestHandler();
            cefBrwoser.Load($@"{Cef.CefCommitHash}://help/");
        }
    }
}