﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ShimanoRDP.App;
using ShimanoRDP.Connection;
using ShimanoRDP.Container;
using ShimanoRDP.Tree;


namespace ShimanoRDP.Tools
{
    public class ConnectionsTreeToMenuItemsConverter
    {
        public MouseEventHandler MouseUpEventHandler { get; set; }


        public IEnumerable<ToolStripDropDownItem> CreateToolStripDropDownItems(ConnectionTreeModel connectionTreeModel)
        {
            var rootNodes = connectionTreeModel.RootNodes;
            return CreateToolStripDropDownItems(rootNodes);
        }

        public IEnumerable<ToolStripDropDownItem> CreateToolStripDropDownItems(IEnumerable<ConnectionInfo> nodes)
        {
            var dropDownList = new List<ToolStripDropDownItem>();
            try
            {
                dropDownList.AddRange(nodes.Select(CreateMenuItem));
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionMessage("frmMain.AddNodeToMenu() failed", ex);
            }

            return dropDownList;
        }

        private void AddSubMenuNodes(IEnumerable<ConnectionInfo> nodes, ToolStripDropDownItem toolStripMenuItem)
        {
            foreach (var connectionInfo in nodes)
            {
                var newItem = CreateMenuItem(connectionInfo);
                toolStripMenuItem.DropDownItems.Add(newItem);
            }
        }

        private ToolStripDropDownItem CreateMenuItem(ConnectionInfo node)
        {
            var menuItem = new ToolStripMenuItem
            {
                Text = node.Name,
                Tag = node
            };

            var nodeAsContainer = node as ContainerInfo;
            if (nodeAsContainer != null)
            {
                menuItem.Image = Properties.Resources.Folder;
                menuItem.Tag = nodeAsContainer;
                AddSubMenuNodes(nodeAsContainer.Children, menuItem);
            }
            else if (node.GetTreeNodeType() == TreeNodeType.PuttySession)
            {
                menuItem.Image = Properties.Resources.PuttySessions;
                menuItem.Tag = node;
            }
            else if (node.GetTreeNodeType() == TreeNodeType.Connection)
            {
                menuItem.Image = node.OpenConnections.Count > 0 ? Properties.Resources.Play : Properties.Resources.Pause;
                menuItem.Tag = node;
            }

            menuItem.MouseUp += MouseUpEventHandler;
            return menuItem;
        }
    }
}