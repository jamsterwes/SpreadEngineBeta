using System;
using SpreadRuntime;
using SpreadRuntime.Utilities;

using SpreadRuntime.LowLevel.Wrappers;
using SpreadRuntime.LowLevel.Rendering;

using SpreadRuntime.HighLevel;
using SpreadRuntime.HighLevel.Graphics2D;

namespace SPHelloWorld
{
    public class Application : SpreadApplication
    {
        public Camera2D camera2D = new Camera2D();
        public Quad quad = new Quad();
        public Shader shader;  // Don't load until constructor

        // Initialize all engine variables within constructor
        public Application() : base(new WindowLayer.Options(1280, 720, 4, 4, false, "SPHelloWorld", false), Properties.Resources.ResourceManager)
        {
            quad.BufferToGPU();
            shader = new Shader("simple_v", "simple_f");
        }

        bool showDebugConsole = false;
        public override void Update()
        {
            // Handle Input
            if (WindowLayer.GetKeyDown(ctx, '`')) showDebugConsole = !showDebugConsole;
            if (WindowLayer.GetKeyDown(ctx, 'R')) RandomColor();

            // Draw Test Quad
            shader.Use();
            quad.Draw();
        }

        public override void DrawUI()
        {
            // Draw UI
            if (showDebugConsole) DrawDebugConsole();
            if (showDebugConsole) DrawRenderConsole();
        }

        // --

        public void RandomColor()
        {
            var rand = new Random();
            float hue = (float)rand.NextDouble() * 360.0f;
            camera2D.clearColor = Color.FromHSV(hue, 1.0f, 1.0f);
            DebugConsole.Write($"Random color set to <{hue:0.00}, 1, 1>");
        }

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
