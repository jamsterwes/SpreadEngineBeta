using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SpreadRuntime.Wrappers
{
    public class UILayer
    {
        [DllImport("spreadgfx.dll")]
        public static extern void InitializeUI(WindowLayer.Context ctx, [MarshalAs(UnmanagedType.LPStr)] string versionString, [MarshalAs(UnmanagedType.LPStr)] string fontPath);
        [DllImport("spreadgfx.dll")]
        public static extern void EnterUIFrame();
        [DllImport("spreadgfx.dll")]
        public static extern void ExitUIFrame();
        [DllImport("spreadgfx.dll")]
        public static extern void EnterUIWindow([MarshalAs(UnmanagedType.LPStr)] string windowName);
        [DllImport("spreadgfx.dll")]
        public static extern void ExitUIWindow();
        [DllImport("spreadgfx.dll")]
        public static extern void UIText([MarshalAs(UnmanagedType.LPStr)] string text);
        [DllImport("spreadgfx.dll")]
        public static extern void UIColoredText([MarshalAs(UnmanagedType.LPStr)] string text, Utilities.Color color);
        [DllImport("spreadgfx.dll")]
        public static extern bool UIButton([MarshalAs(UnmanagedType.LPStr)] string label);
        [DllImport("spreadgfx.dll")]
        public static extern void UICheckbox([MarshalAs(UnmanagedType.LPStr)] string text, [In, Out] ref bool value);
        [DllImport("spreadgfx.dll")]
        public static extern void UIColorPicker3([MarshalAs(UnmanagedType.LPStr)] string text, [In, Out] ref Utilities.Color color);
    }
}
