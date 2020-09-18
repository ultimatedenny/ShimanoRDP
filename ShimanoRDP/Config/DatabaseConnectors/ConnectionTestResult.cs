namespace ShimanoRDP.Config.DatabaseConnectors
{
    public enum ConnectionTestResult
    {
        ConnectionSucceded,
        ServerNotAccessible,
        UnknownDatabase,
        CredentialsRejected,
        UnknownError
    }
}