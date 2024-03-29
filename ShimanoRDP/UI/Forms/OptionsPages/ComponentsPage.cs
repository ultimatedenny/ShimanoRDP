﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using ShimanoRDP.App;
using ShimanoRDP.App.Info;
using ShimanoRDP.Connection.Protocol.RDP;
using ShimanoRDP.Messages;
using ShimanoRDP.Properties;
using ShimanoRDP.Resources.Language;
using ShimanoRDP.Themes;

namespace ShimanoRDP.UI.Forms.OptionsPages
{
    public partial class ComponentsPage
    {
        public ComponentsPage()
        {
            ApplyTheme();
            PageIcon = Properties.Resources.ComponentsCheck_Icon;
            InitializeComponent();
            FontOverrider.FontOverride(this);
            ThemeManager.getInstance().ThemeChanged += ApplyTheme;
        }

        public override string PageName
        {
            get => Language.ComponentsCheck;
            set { }
        }

        #region Form Stuff

        public override void ApplyLanguage()
        {
            base.ApplyLanguage();

            Text = Language.ComponentsCheck;
            btnCheck.Text = Language.Refresh;
        }

        private void BtnCheckAgain_Click(object sender, EventArgs e)
        {
            Runtime.MessageCollector.AddMessage(MessageClass.InformationMsg, "Beginning component check", true);
            CheckRdp();
            CheckVnc();
            CheckPutty();
            Runtime.MessageCollector.AddMessage(MessageClass.InformationMsg, "Finished component check", true);
        }

        #endregion

        private void CheckRdp()
        {
            pnlCheck1.Visible = true;
            var rdpProtocolFactory = new RdpProtocolFactory();
            var supportedVersions = rdpProtocolFactory.GetSupportedVersions();

            if (supportedVersions.Any())
            {
                pbCheck1.Image = Properties.Resources.Good_Symbol;
                lblCheck1.ForeColor = Color.DarkOliveGreen;
                lblCheck1.Text = "RDP (Remote Desktop) " + Language.CheckSucceeded;
                txtCheck1.Text = string.Format(Language.CcRDPOK, string.Join(", ", supportedVersions));
                Runtime.MessageCollector.AddMessage(MessageClass.InformationMsg, "RDP versions installed: "+ string.Join(",", supportedVersions), true);
            }
            else
            {
                pbCheck1.Image = Properties.Resources.Bad_Symbol;
                lblCheck1.ForeColor = Color.Firebrick;
                lblCheck1.Text = "RDP (Remote Desktop) " + Language.CheckFailed;
                txtCheck1.Text = string.Format(Language.CcRDPFailed, GeneralAppInfo.UrlForum);

                Runtime.MessageCollector.AddMessage(MessageClass.WarningMsg,
                                                    "RDP " + Language.CcNotInstalledProperly, true);
            }
        }

        private void CheckVnc()
        {
            pnlCheck2.Visible = true;

            try
            {
                using (var vnc = new VncSharp.RemoteDesktop())
                {
                    vnc.CreateControl();

                    while (!vnc.Created)
                    {
                        Thread.Sleep(10);
                        System.Windows.Forms.Application.DoEvents();
                    }

                    pbCheck2.Image = Properties.Resources.Good_Symbol;
                    lblCheck2.ForeColor = Color.DarkOliveGreen;
                    lblCheck2.Text = "VNC (Virtual Network Computing) " + Language.CheckSucceeded;
                    txtCheck2.Text = string.Format(Language.CcVNCOK, vnc.ProductVersion);
                    Runtime.MessageCollector.AddMessage(MessageClass.InformationMsg, "VNC installed", true);
                }
            }
            catch (Exception)
            {
                pbCheck2.Image = Properties.Resources.Bad_Symbol;
                lblCheck2.ForeColor = Color.Firebrick;
                lblCheck2.Text = "VNC (Virtual Network Computing) " + Language.CheckFailed;
                txtCheck2.Text = string.Format(Language.CcVNCFailed, GeneralAppInfo.UrlForum);

                Runtime.MessageCollector.AddMessage(MessageClass.WarningMsg,
                                                    "VNC " + Language.CcNotInstalledProperly, true);
            }
        }

        private void CheckPutty()
        {
            pnlCheck3.Visible = true;
            string pPath;
            if (Settings.Default.UseCustomPuttyPath == false)
            {
                pPath = GeneralAppInfo.HomePath + "\\PuTTYNG.exe";
            }
            else
            {
                pPath = Settings.Default.CustomPuttyPath;
            }

            if (File.Exists(pPath))
            {
                var versionInfo = FileVersionInfo.GetVersionInfo(pPath);

                pbCheck3.Image = Properties.Resources.Good_Symbol;
                lblCheck3.ForeColor = Color.DarkOliveGreen;
                lblCheck3.Text = "PuTTY (SSH/Telnet/Rlogin/RAW) " + Language.CheckSucceeded;
                txtCheck3.Text =
                    $"{Language.CcPuttyOK}{Environment.NewLine}Version: {versionInfo.ProductName} Release: {versionInfo.FileVersion}";
                Runtime.MessageCollector.AddMessage(MessageClass.InformationMsg, "PuTTY installed", true);
            }
            else
            {
                pbCheck3.Image = Properties.Resources.Bad_Symbol;
                lblCheck3.ForeColor = Color.Firebrick;
                lblCheck3.Text = "PuTTY (SSH/Telnet/Rlogin/RAW) " + Language.CheckFailed;
                txtCheck3.Text = Language.CcPuttyFailed;

                Runtime.MessageCollector.AddMessage(MessageClass.WarningMsg,
                                                    "PuTTY " + Language.CcNotInstalledProperly, true);
                Runtime.MessageCollector.AddMessage(MessageClass.ErrorMsg, "File " + pPath + " does not exist.",
                                                    true);
            }
        }

        
    }
}