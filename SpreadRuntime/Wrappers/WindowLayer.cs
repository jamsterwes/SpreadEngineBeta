using System;
using System.Runtime.InteropServices;

namespace SpreadRuntime.Wrappers
{
    public static class WindowLayer
    {
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct Context
        {
            IntPtr windowRef;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct Options
        {
            int width, height;
            int glMajor, glMinor;
            bool resizable;
            string windowTitle;
            bool vsync;

            public Options(int width, int height, int glMajor, int glMinor, bool resizable, string windowTitle, bool vsync)
            {
                this.width = width;
                this.height = height;
                this.glMajor = glMajor;
                this.glMinor = glMinor;
                this.resizable = resizable;
                this.windowTitle = windowTitle;
                this.vsync = vsync;
            }
        }

        // Initialization
        [DllImport("spreadgfx.dll")]
        public extern static Context CreateWindowContext(Options opt);

        // Control Flow
        [DllImport("spreadgfx.dll")]
        public extern static bool ShouldRender(Context ctx);
        [DllImport("spreadgfx.dll")]
        public extern static void EnterRenderLoop(Context ctx);
        [DllImport("spreadgfx.dll")]
        public extern static void ExitRenderLoop(Context ctx);

        // TODO: Input Handling
        [DllImport("spreadgfx.dll")]
        public extern static bool GetKeyDown(Context ctx, int keyCode);
    }
}
