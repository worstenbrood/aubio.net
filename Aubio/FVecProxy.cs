using System;
using System.Runtime.InteropServices;

namespace Aubio
{
    public class FVecProxy<T> : FVec
    {
        internal struct FVec__
        {
            public readonly uint Length;
            public readonly IntPtr Data;

            public FVec__(IntPtr data, uint length)
            {
                Data = data;
                Length = length;
            }
        }

        private readonly T[] _data;
        private readonly GCHandle _gchData;

        private readonly FVec__ _vec;
        private readonly GCHandle _gchVec;

        // Create new T[]
        public FVecProxy(int length) : this(new T[length], length)
        {
        }

        /// <summary>
        /// Use existing T[]
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        public FVecProxy(T[] data, int length)
        {
            _data = data;
            _gchData = GCHandle.Alloc(data, GCHandleType.Pinned);

            // Allocate native FVec
            _vec = new FVec__(_gchData.AddrOfPinnedObject(), (uint)length);
            _gchVec = GCHandle.Alloc(_vec, GCHandleType.Pinned);

            Handle = _gchVec.AddrOfPinnedObject();
            Length = length;
        }

        protected override void DisposeNative()
        {
            // Ignore
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _gchData.Free();
                _gchVec.Free();
            }
            base.Dispose(disposing);
        }

        public static implicit operator T[](FVecProxy<T> p) => p._data;
    }

    public class FVecProxy : FVecProxy<float>
    {
        public FVecProxy(int length) : base(length)
        {
        }
    }

}
