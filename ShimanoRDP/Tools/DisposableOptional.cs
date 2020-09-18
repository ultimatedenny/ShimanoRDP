using System;
using System.Linq;

namespace ShimanoRDP.Tools
{
    public class DisposableOptional<T> : Optional<T>, IDisposable
        where T : IDisposable
    {
        public DisposableOptional(T value)
            : base(value)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || !this.Any())
                return;

            this.First().Dispose();
        }
    }
}