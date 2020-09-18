using System;
using ShimanoRDP.Connection;


namespace ShimanoRDP.Config.Putty
{
    public class PuttySessionChangedEventArgs : EventArgs
    {
        public PuttySessionInfo Session { get; set; }

        public PuttySessionChangedEventArgs(PuttySessionInfo sessionChanged = null)
        {
            Session = sessionChanged;
        }
    }
}