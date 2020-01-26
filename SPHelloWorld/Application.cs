using SpreadRuntime;
using SpreadRuntime.Utilities;
using SpreadRuntime.Wrappers;
namespace SPHelloWorld
{
    public class Application : SpreadApplication
    {
        Color clearColor;
        float hue = 0.0f;

        // Initialize all engine variables within constructor
        public Application() : base(new WindowLayer.Options(1280, 720, 4, 4, false, "SPHelloWorld", false), Properties.Resources.ResourceManager)
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
        }

        // --

        public void DrawTestWindow()
        {
            UILayer.EnterUIWindow("Example Window");
            UILayer.UIColorPicker3("Current Color", ref clearColor);
            UILayer.ExitUIWindow();
        }
    }
}
