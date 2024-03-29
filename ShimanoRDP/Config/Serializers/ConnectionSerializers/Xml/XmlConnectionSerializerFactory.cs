﻿using System.Linq;
using ShimanoRDP.Connection;
using ShimanoRDP.Security;
using ShimanoRDP.Tree;
using ShimanoRDP.Tree.Root;

namespace ShimanoRDP.Config.Serializers.ConnectionSerializers.Xml
{
    public class XmlConnectionSerializerFactory
    {
        public ISerializer<ConnectionInfo, string> Build(
            ICryptographyProvider cryptographyProvider,
            ConnectionTreeModel connectionTreeModel,
            SaveFilter saveFilter = null,
            bool useFullEncryption = false)
        {
            var encryptionKey = connectionTreeModel
                .RootNodes.OfType<RootNodeInfo>()
                .First().PasswordString
                .ConvertToSecureString();

            var connectionNodeSerializer = new XmlConnectionNodeSerializer27(
                cryptographyProvider,
                encryptionKey,
                saveFilter ?? new SaveFilter());

            return new XmlConnectionsSerializer(cryptographyProvider, connectionNodeSerializer)
            {
                UseFullEncryption = useFullEncryption
            };
        }
    }
}
