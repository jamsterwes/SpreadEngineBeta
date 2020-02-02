using System;
using SpreadRuntime;
using SpreadRuntime.Utilities;

using SpreadRuntime.LowLevel.Wrappers;
using SpreadRuntime.LowLevel.Rendering;

using SpreadRuntime.HighLevel;
using SpreadRuntime.HighLevel.Graphics2D;

using GlmSharp;

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

        Color quadColor = new Color("#FFFFFF");
        vec2 size = vec2.Ones * 0.125f;
        vec2 offset = vec2.Zero;
        vec2 rotation = vec2.Zero;
        public override void Update()
        {
            // Handle Input
            if (WindowLayer.GetKeyDown(ctx, '`')) showDebugConsole = !showDebugConsole;
            if (WindowLayer.GetKeyDown(ctx, 'R')) RandomColor();

            if (WindowLayer.GetKey(ctx, 'W')) offset.y += deltaTime;
            if (WindowLayer.GetKey(ctx, 'S')) offset.y -= deltaTime;
            if (WindowLayer.GetKey(ctx, 'A')) offset.x -= deltaTime;
            if (WindowLayer.GetKey(ctx, 'D')) offset.x += deltaTime;
            if (WindowLayer.GetKey(ctx, 'Q')) rotation.x += deltaTime * (float)Math.PI;
            if (WindowLayer.GetKey(ctx, 'E')) rotation.x -= deltaTime * (float)Math.PI;

            // Draw Test Quad
            shader.Use();
            shader.SetColor("color", quadColor);
            shader.SetVec2("size", size);
            shader.SetVec2("offset", offset);
            shader.SetVec2("rotation", rotation);
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
            UILayer.UIColorPicker3("Background Color", ref camera2D.clearColor);
            UILayer.UIColorPicker3("Quad Color", ref quadColor);
            UILayer.UISeparator();
            UILayer.UIVector2("Quad Size", ref size, 0.01f, 0.0f, 2.0f);
            UILayer.UIVector2("Quad Offset", ref offset, 0.01f, -1.0f, 1.0f);
            UILayer.UIVector2("Quad Rotation (Radians)", ref rotation, 0.1f, -2.0f * (float)Math.PI, 2.0f * (float)Math.PI);
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
