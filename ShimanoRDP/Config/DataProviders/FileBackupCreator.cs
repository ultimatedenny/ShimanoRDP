using System;
using System.IO;
using ShimanoRDP.App;
using ShimanoRDP.Messages;
using ShimanoRDP.Resources.Language;

namespace ShimanoRDP.Config.DataProviders
{
    public class FileBackupCreator
    {
        public void CreateBackupFile(string fileName)
        {
            try
            {
                if (WeDontNeedToBackup(fileName))
                    return;

                var backupFileName =
                    string.Format(Properties.Settings.Default.BackupFileNameFormat, fileName, DateTime.Now);
                File.Copy(fileName, backupFileName);
            }
            catch (Exception ex)
            {
                Runtime.MessageCollector.AddExceptionMessage(Language.ConnectionsFileBackupFailed, ex,
                                                             MessageClass.WarningMsg);
                throw;
            }
        }

        private bool WeDontNeedToBackup(string filePath)
        {
            return FeatureIsTurnedOff() || FileDoesntExist(filePath);
        }

        private bool FileDoesntExist(string filePath)
        {
            return !File.Exists(filePath);
        }

        private bool FeatureIsTurnedOff()
        {
            return Properties.Settings.Default.BackupFileKeepCount == 0;
        }
    }
}