using NUnit.Framework;
using ShimanoRDP.UI.Forms;

namespace ShimanoRDPTests.UI.Forms
{
    public class OptionsFormSetupAndTeardown
    {
        protected FrmOptions _optionsForm;

        [OneTimeSetUp]
        public void OnetimeSetup()
        {
        }

        [SetUp]
        public void Setup()
        {
            _optionsForm = new FrmOptions();
            _optionsForm.Show();
        }

        [TearDown]
        public void Teardown()
        {
            _optionsForm.Dispose();
            while (_optionsForm.Disposing) ;
            _optionsForm = null;
        }
    }
}