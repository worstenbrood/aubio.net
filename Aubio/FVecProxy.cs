using System;
using System.Runtime.InteropServices;

namespace Aubio
{
    public class FVecProxy: FVec
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

        private GCHandle? _gchData;
        private GCHandle? _gchVec;

        public FVecProxy(float[] data, int length)
        {
            _gchData = GCHandle.Alloc(data, GCHandleType.Pinned);
            
            // Allocate native FVec
            var vec = new FVec__(_gchData.Value.AddrOfPinnedObject(), (uint)length);
            _gchVec = GCHandle.Alloc(vec, GCHandleType.Pinned);
            
            Handle = _gchVec.Value.AddrOfPinnedObject();
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
                _gchData?.Free();
                _gchVec?.Free();
            }
            base.Dispose(disposing);
        }
    }
}
