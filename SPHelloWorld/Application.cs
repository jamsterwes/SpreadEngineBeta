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
        bool key_h = false;

        public Application() : base(new WindowLayer.Options(1280, 720, 4, 4, false, "SPHelloWorld", false))
        {
            clearColor = Color.FromHSV(hue, 1.0f, 0.5f);
        }

        public override void Update()
        {
            // Setup clear color
            hue = (hue + 360.0f * deltaTime / 5.0f) % 360.0f;
            clearColor = Color.FromHSV(hue, 1.0f, 0.5f);
            GraphicsLayer.ClearColor(clearColor);

            // Draw UI
            DrawTestWindow();

            // Check for H key
            key_h = WindowLayer.GetKeyDown(ctx, 'H');
        }

        // --

        public void DrawTestWindow()
        {
            UILayer.EnterUIWindow("Example Window");
            UILayer.UIText($"Hue: {hue}");
            UILayer.UIText($"H Key: {key_h}");
            UILayer.UIColorPicker3("Current Color", ref clearColor);
            UILayer.ExitUIWindow();
        }
    }
}
