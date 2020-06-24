using System;
using System.IO.MemoryMappedFiles;

namespace Dormarch.OpenType.FileHandling
{
    public class MappedFileAccessor : IDataAccessor
    {
        private readonly MemoryMappedViewAccessor _view;
        private bool disposedValue;

        public long Capacity => !disposedValue ? _view.Capacity 
            : throw new InvalidOperationException(Resources.FileHandling_MappedFileAccessor_AccessAfterDisposeError);

        public MappedFileAccessor(MemoryMappedFile file)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            _view = file.CreateViewAccessor(0, 0, MemoryMappedFileAccess.Read);
        }

        public byte ReadByte(long position) => !disposedValue ? _view.ReadByte(position) 
            : throw new InvalidOperationException(Resources.FileHandling_MappedFileAccessor_AccessAfterDisposeError);

        public int ReadArray(long position, byte[] array, int offset, int count) => !disposedValue ? _view.ReadArray(position, array, offset, count)
            : throw new InvalidOperationException(Resources.FileHandling_MappedFileAccessor_AccessAfterDisposeError);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _view.Dispose();
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
