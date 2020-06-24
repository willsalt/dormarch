using System;
using System.Collections.Generic;
using System.Text;

namespace Dormarch.OpenType.FileHandling
{
    public interface IDataAccessor : IDisposable
    {
        long Capacity { get; }

        byte ReadByte(long position);

        int ReadArray(long position, byte[] arr, int offset, int count);
    }
}
