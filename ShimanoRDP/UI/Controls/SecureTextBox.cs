using System.Security;
using ShimanoRDP.Security;

namespace ShimanoRDP.UI.Controls
{
    public partial class SecureTextBox : MrngTextBox
    {
        public SecureString SecString { get; private set; } = new SecureString();

        public SecureTextBox()
        {
            InitializeComponent();
            TextChanged += SecureTextBox_TextChanged;
        }

        private void SecureTextBox_TextChanged(object sender, System.EventArgs e)
        {
            SecString = Text.ConvertToSecureString();
        }
    }
}