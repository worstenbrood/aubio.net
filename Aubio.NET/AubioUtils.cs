using Aubio.Interop;
using System.Runtime.InteropServices;

namespace Aubio.NET
{
    public static class AubioUtils
    {
        public static void Cleanup()
        {
            aubio_cleanup();
        }

        [DllImport(NativeMethods.DllName, CallingConvention = CallingConvention.Cdecl)]
        private static extern void aubio_cleanup();
    }
}