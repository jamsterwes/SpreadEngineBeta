using System;
using SpreadRuntime;
using SpreadRuntime.Utilities;
using SpreadRuntime.LowLevel.Wrappers;

using SpreadRuntime.HighLevel;
using SpreadRuntime.HighLevel.Graphics2D;

namespace SPHelloWorld
{
    public class Application : SpreadApplication
    {
        public Camera2D camera2D = new Camera2D();

        // Initialize all engine variables within constructor
        public Application() : base(new WindowLayer.Options(1280, 720, 4, 4, false, "SPHelloWorld", false), Properties.Resources.ResourceManager) { }

        bool showDebugConsole = false;
        public override void Update()
        {
            // Handle Input
            if (WindowLayer.GetKeyDown(ctx, '`')) showDebugConsole = !showDebugConsole;

            // Draw UI
            if (showDebugConsole) DrawDebugConsole();
            DrawRenderConsole();
        }

        // --

        public void DrawRenderConsole()
        {
            UILayer.EnterUIWindow("Render Console");
            UILayer.UIColorPicker3("Clear Color", ref camera2D.clearColor);
            UILayer.ExitUIWindow();
        }

        public void DrawDebugConsole()
        {
            UILayer.EnterUIWindow("Debug Console");
            UILayer.UIText(string.Join("\n", DebugConsole.lines));
            UILayer.ExitUIWindow();
        }
    }
}
