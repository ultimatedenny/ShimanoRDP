using System.IO;
using ShimanoRDP.App;
using ShimanoRDP.Config.Putty;
using ShimanoRDP.Connection;
using ShimanoRDP.Container;
using ShimanoRDPTests.Properties;
using ShimanoRDPTests.TestHelpers;
using NUnit.Framework;

namespace ShimanoRDPTests.App
{
	public class ImportTests
	{
		[Test]
		public void ErrorHandlerCalledWhenUnsupportedFileExtensionFound()
		{
			using (FileTestHelpers.DisposableTempFile(out var file, ".blah"))
			{
				var conService = new ConnectionsService(PuttySessionsManager.Instance);
				var container = new ContainerInfo();
				var exceptionOccurred = false;

				Import.HeadlessFileImport(new []{file}, container, conService, s => exceptionOccurred = true);

				Assert.That(exceptionOccurred);
			}
		}

		[Test]
		public void AnErrorInOneFileDoNotPreventOtherFilesFromProcessing()
		{
			using (FileTestHelpers.DisposableTempFile(out var badFile, ".blah"))
			using (FileTestHelpers.DisposableTempFile(out var rdpFile, ".rdp"))
			{
				File.AppendAllText(rdpFile, Resources.test_remotedesktopconnection_rdp);
				var conService = new ConnectionsService(PuttySessionsManager.Instance);
				var container = new ContainerInfo();
				var exceptionCount = 0;
				
				Import.HeadlessFileImport(new[] { badFile, rdpFile }, container, conService, s => exceptionCount++);

				Assert.That(exceptionCount, Is.EqualTo(1));
				Assert.That(container.Children, Has.One.Items);
			}
		}
	}
}
