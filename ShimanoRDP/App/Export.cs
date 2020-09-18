using System;
using System.Linq;
using System.Windows.Forms;
using ShimanoRDP.Config.Connections;
using ShimanoRDP.Config.DataProviders;
using ShimanoRDP.Config.Serializers;
using ShimanoRDP.Config.Serializers.ConnectionSerializers.Csv;
using ShimanoRDP.Config.Serializers.ConnectionSerializers.Xml;
using ShimanoRDP.Connection;
using ShimanoRDP.Container;
using ShimanoRDP.Security;
using ShimanoRDP.Security.Factories;
using ShimanoRDP.Tree;
using ShimanoRDP.Tree.Root;
using ShimanoRDP.UI.Forms;


namespace ShimanoRDP.App
{
    public static class Export
    {
        public static void ExportToFile(ConnectionInfo selectedNode, ConnectionTreeModel connectionTreeModel)
        {
            try
            {
                var saveFilter = new SaveFilter();

                using (var exportForm = new FrmExport())
                {
                    if (selectedNode?.GetTreeNodeType() == TreeNodeType.Container)
                        exportForm.SelectedFolder = selectedNode as ContainerInfo;
                    else if (selectedNode?.GetTreeNodeType() == TreeNodeType.Connection)
                    {
                        if (selectedNode.Parent.GetTreeNodeType() == TreeNodeType.Container)
                            exportForm.SelectedFolder = selectedNode.Parent;
                        exportForm.SelectedConnection = selectedNode;
                    }

                    if (exportForm.ShowDialog(FrmMain.Default) != DialogResult.OK)
                        return;

                    ConnectionInfo exportTarget;
                    switch (exportForm.Scope)
                    {
                        case FrmExport.ExportScope.SelectedFolder:
                            exportTarget = exportForm.SelectedFolder;
                            break;
                        case FrmExport.ExportScope.SelectedConnection:
                            exportTarget = exportForm.SelectedConnection;
                            break;
                        default:
                            exportTarget = connectionTreeModel.RootNodes.First(node => node is RootNodeInfo);
                            break;
                    }

                    saveFilter.SaveUsername = exportForm.IncludeUsername;
                    saveFilter.SavePassword = exportForm.IncludePassword;
                    saveFilter.SaveDomain = exportForm.IncludeDomain;
                    saveFilter.SaveInheritance = exportForm.IncludeInheritance;
                    saveFilter.SaveCredentialId = exportForm.IncludeAssignedCredential;

                    SaveExportFile(exportForm.FileName, exportForm.SaveFormat, saveFilter, exportTarget);
                }
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionMessage("App.Export.ExportToFile() failed.", ex);
            }
        }

        private static void SaveExportFile(string fileName,
                                           SaveFormat saveFormat,
                                           SaveFilter saveFilter,
                                           ConnectionInfo exportTarget)
        {
            try
            {
                ISerializer<ConnectionInfo, string> serializer;
                switch (saveFormat)
                {
                    case SaveFormat.mRXML:
                        var cryptographyProvider = new CryptoProviderFactoryFromSettings().Build();
                        var rootNode = exportTarget.GetRootParent() as RootNodeInfo;
                        var connectionNodeSerializer = new XmlConnectionNodeSerializer27(
                                                                                         cryptographyProvider,
                                                                                         rootNode?.PasswordString
                                                                                                 .ConvertToSecureString() ??
                                                                                         new RootNodeInfo(RootNodeType
                                                                                                              .Connection)
                                                                                             .PasswordString
                                                                                             .ConvertToSecureString(),
                                                                                         saveFilter);
                        serializer = new XmlConnectionsSerializer(cryptographyProvider, connectionNodeSerializer);
                        break;
                    case SaveFormat.mRCSV:
                        serializer =
                            new CsvConnectionsSerializerShimanoRDPFormat(saveFilter, Runtime.CredentialProviderCatalog);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(saveFormat), saveFormat, null);
                }

                var serializedData = serializer.Serialize(exportTarget);
                var fileDataProvider = new FileDataProvider(fileName);
                fileDataProvider.Save(serializedData);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionStackTrace($"Export.SaveExportFile(\"{fileName}\") failed.", ex);
            }
            finally
            {
                Runtime.ConnectionsService.RemoteConnectionsSyncronizer?.Enable();
            }
        }
    }
}