using System.Diagnostics;

namespace ShimanoRDP.Messages.MessageWriters
{
    public class DebugConsoleMessageWriter : IMessageWriter
    {
        public void Write(IMessage message)
        {
            var textToPrint = $"{message.Class}: {message.Text}";
            Debug.Print(textToPrint);
        }
    }
}