﻿using System;

namespace ShimanoRDP.Config.Serializers.ConnectionSerializers.MsSql
{
    public class SqlConnectionListMetaData
    {
        public string Name { get; set; }
        public string Protected { get; set; }
        public bool Export { get; set; }
        public Version ConfVersion { get; set; }
    }
}