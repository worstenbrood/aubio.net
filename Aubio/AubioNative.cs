using System;
using Aubio.Interop;
using Aubio.Interop.Win32;
using JetBrains.Annotations;
using NativeMethods = Aubio.Interop.NativeMethods;

namespace Aubio
{
    public sealed class AubioNative : Disposable
    {
        [NotNull] private readonly NativeLibraryLoader _loader;

        public AubioNative(
            [NotNull] string path32 = @"x86\" + NativeMethods.DllName,
            [NotNull] string path64 = @"x64\" + NativeMethods.DllName,
            [NotNull] string patha64 = @"arm64\" + NativeMethods.DllName)
        {
            if (path32 == null)
                throw new ArgumentNullException(nameof(path32));

            if (path64 == null)
                throw new ArgumentNullException(nameof(path64));

            if (path64 == null)
                throw new ArgumentNullException(nameof(patha64));

            _loader = new NativeLibraryLoader(path32, path64, patha64);
        }

        protected override void DisposeManaged()
        {
            _loader.Dispose();
        }

        // Singleton

        private static readonly Lazy<AubioNative> _instance = new Lazy<AubioNative>(() => new AubioNative());

        public static AubioNative Instance
        {
            get 
            {
                return _instance.Value;
            }
        }

    }
}