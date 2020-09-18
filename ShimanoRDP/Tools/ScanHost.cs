﻿using System;
using System.Collections;
using ShimanoRDP.App;
using ShimanoRDP.Connection.Protocol.Http;
using ShimanoRDP.Connection.Protocol.RDP;
using ShimanoRDP.Connection.Protocol.Rlogin;
using ShimanoRDP.Connection.Protocol.SSH;
using ShimanoRDP.Connection.Protocol.Telnet;
using ShimanoRDP.Connection.Protocol.VNC;
using ShimanoRDP.Messages;
using ShimanoRDP.Resources.Language;


namespace ShimanoRDP.Tools
{
    public class ScanHost
    {
        #region Properties

        public static int SshPort { get; set; } = (int)ProtocolSSH1.Defaults.Port;
        public static int TelnetPort { get; set; } = (int)ProtocolTelnet.Defaults.Port;
        public static int HttpPort { get; set; } = (int)ProtocolHTTP.Defaults.Port;
        public static int HttpsPort { get; set; } = (int)ProtocolHTTPS.Defaults.Port;
        public static int RloginPort { get; set; } = (int)ProtocolRlogin.Defaults.Port;
        public static int RdpPort { get; set; } = (int)RdpProtocol6.Defaults.Port;
        public static int VncPort { get; set; } = (int)ProtocolVNC.Defaults.Port;
        public ArrayList OpenPorts { get; set; }
        public ArrayList ClosedPorts { get; set; }
        public bool Rdp { get; set; }
        public bool Vnc { get; set; }
        public bool Ssh { get; set; }
        public bool Telnet { get; set; }
        public bool Rlogin { get; set; }
        public bool Http { get; set; }
        public bool Https { get; set; }
        public string HostIp { get; set; }
        public string HostName { get; set; } = "";

        public string HostNameWithoutDomain
        {
            get
            {
                if (string.IsNullOrEmpty(HostName) || HostName == HostIp)
                {
                    return HostIp;
                }

                return HostName.Split('.')[0];
            }
        }

        #endregion

        #region Methods

        public ScanHost(string host)
        {
            HostIp = host;
            OpenPorts = new ArrayList();
            ClosedPorts = new ArrayList();
        }

        public override string ToString()
        {
            try
            {
                return "SSH: " + Convert.ToString(Ssh) + " Telnet: " + Convert.ToString(Telnet) + " HTTP: " +
                       Convert.ToString(Http) + " HTTPS: " + Convert.ToString(Https) + " Rlogin: " +
                       Convert.ToString(Rlogin) + " RDP: " + Convert.ToString(Rdp) + " VNC: " + Convert.ToString(Vnc);
            }
            catch (Exception)
            {
                Runtime.MessageCollector.AddMessage(MessageClass.WarningMsg, "ToString failed (Tools.PortScan)", true);
                return "";
            }
        }

        //Adpating to objectlistview instaed of listview
        public string HostIPorName
        {
            get
            {
                if (string.IsNullOrEmpty(HostName))
                    return HostIp;
                else
                    return HostName;
            }
        }

        public string RdpName
        {
            get { return BoolToYesNo(Rdp); }
        }

        public string VncName
        {
            get { return BoolToYesNo(Vnc); }
        }

        public string SshName
        {
            get { return BoolToYesNo(Rdp); }
        }

        public string TelnetName
        {
            get { return BoolToYesNo(Telnet); }
        }

        public string RloginName
        {
            get { return BoolToYesNo(Rlogin); }
        }

        public string HttpName
        {
            get { return BoolToYesNo(Http); }
        }

        public string HttpsName
        {
            get { return BoolToYesNo(Https); }
        }

        public string OpenPortsName
        {
            get
            {
                var strOpen = "";
                foreach (int p in OpenPorts)
                {
                    strOpen += p + ", ";
                }

                return strOpen;
            }
        }

        public string ClosedPortsName
        {
            get
            {
                var strClosed = "";
                foreach (int p in ClosedPorts)
                {
                    strClosed += p + ", ";
                }

                return strClosed;
            }
        }


        private static string BoolToYesNo(bool value)
        {
            return value ? Language.Yes : Language.No;
        }

        public void SetAllProtocols(bool value)
        {
            Vnc = value;
            Telnet = value;
            Ssh = value;
            Rlogin = value;
            Rdp = value;
            Https = value;
            Http = value;
        }

        #endregion
    }
}