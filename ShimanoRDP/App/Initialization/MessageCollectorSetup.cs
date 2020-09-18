using System.Collections.Generic;
using System.Linq;
using ShimanoRDP.Messages;
using ShimanoRDP.Messages.MessageFilteringOptions;
using ShimanoRDP.Messages.MessageWriters;
using ShimanoRDP.Messages.WriterDecorators;

namespace ShimanoRDP.App.Initialization
{
    public class MessageCollectorSetup
    {
        public static void SetupMessageCollector(MessageCollector messageCollector,
                                                 IList<IMessageWriter> messageWriterList)
        {
            messageCollector.CollectionChanged += (o, args) =>
            {
                var messages = args.NewItems.Cast<IMessage>().ToArray();
                foreach (var printer in messageWriterList)
                    foreach (var message in messages)
                        printer.Write(message);
            };
        }

        public static void BuildMessageWritersFromSettings(IList<IMessageWriter> messageWriterList)
        {
#if DEBUG
            messageWriterList.Add(BuildDebugConsoleWriter());
#endif
            messageWriterList.Add(BuildTextLogMessageWriter());
            messageWriterList.Add(BuildNotificationPanelMessageWriter());
            messageWriterList.Add(BuildPopupMessageWriter());
        }

        private static IMessageWriter BuildDebugConsoleWriter()
        {
            return new DebugConsoleMessageWriter();
        }

        private static IMessageWriter BuildTextLogMessageWriter()
        {
            return new MessageTypeFilterDecorator(
                                                  new LogMessageTypeFilteringOptions(),
                                                  new TextLogMessageWriter(Logger.Instance)
                                                 );
        }

        private static IMessageWriter BuildNotificationPanelMessageWriter()
        {
            return new OnlyLogMessageFilter(
                                            new MessageTypeFilterDecorator(
                                                                           new
                                                                               NotificationPanelMessageFilteringOptions(),
                                                                           new MessageFocusDecorator(
                                                                                                     Windows.ErrorsForm,
                                                                                                     new
                                                                                                         NotificationPanelSwitchOnMessageFilteringOptions(),
                                                                                                     new
                                                                                                         NotificationPanelMessageWriter(Windows
                                                                                                                                            .ErrorsForm)
                                                                                                    )
                                                                          )
                                           );
        }

        private static IMessageWriter BuildPopupMessageWriter()
        {
            return new OnlyLogMessageFilter(
                                            new MessageTypeFilterDecorator(
                                                                           new PopupMessageFilteringOptions(),
                                                                           new PopupMessageWriter()
                                                                          )
                                           );
        }
    }
}