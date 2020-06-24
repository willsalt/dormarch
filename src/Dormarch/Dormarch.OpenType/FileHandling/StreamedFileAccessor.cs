using System;
using System.IO;

namespace Dormarch.OpenType.FileHandling
{
    public class StreamedFileAccessor : IDataAccessor
    {
        private readonly MemoryStream _stream;
        private bool disposedValue;

        public StreamedFileAccessor(MemoryStream str)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            _stream = str;
        }

        public long Capacity => !disposedValue ? _stream.Length 
            : throw new InvalidOperationException(Resources.FileHandling_StreamedFileAccessor_AccessAfterDisposeError);

        public int ReadArray(long position, byte[] arr, int offset, int count)
        {
            if (disposedValue)
            {
                throw new InvalidOperationException(Resources.FileHandling_StreamedFileAccessor_AccessAfterDisposeError);
            }
            _stream.Seek(position, SeekOrigin.Begin);
            return _stream.Read(arr, offset, count);
        }

        public byte ReadByte(long position)
        {
            if (disposedValue)
            {
                throw new InvalidOperationException(Resources.FileHandling_StreamedFileAccessor_AccessAfterDisposeError);
            }
            _stream.Seek(position, SeekOrigin.Begin);
            return (byte)_stream.ReadByte();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _stream.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
