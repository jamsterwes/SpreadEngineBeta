using SpreadRuntime;
using SpreadRuntime.Utilities;
using SpreadRuntime.Wrappers;
using System;
using System.Windows.Forms;

namespace SPHelloWorld
{
    public class Application : SpreadApplication
    {
        Color clearColor;
        float hue = 0.0f;

        public Application() : base(new WindowLayer.Options(1280, 720, 4, 4, false, "SPHelloWorld", false))
        {
            clearColor = Color.FromHSV(hue, 1.0f, 0.5f);
        }

        public override void Update()
        {
            clearColor = Color.FromHSV(hue, 1.0f, 0.5f);
            GraphicsLayer.ClearColor(clearColor);

            DrawTestWindow();
            hue = (hue + 360.0f * deltaTime / 5.0f) % 360.0f;
        }

        // --

        public void DrawTestWindow()
        {
            UILayer.EnterUIWindow("Example Window");
            UILayer.UIText($"Hue: {hue}");
            UILayer.UIColorPicker3("Current Color", ref clearColor);
            UILayer.ExitUIWindow();
        }
    }
}
