﻿using ShimanoRDP.Properties;
using ShimanoRDP.Tools.Cmdline;

namespace ShimanoRDP.Tools
{
    public class PuttyProcessController : ProcessController
    {
        public bool Start(CommandLineArguments arguments = null)
        {
            var filename = Settings.Default.UseCustomPuttyPath
                ? Settings.Default.CustomPuttyPath
                : App.Info.GeneralAppInfo.PuttyPath;
            return Start(filename, arguments);
        }
    }
}