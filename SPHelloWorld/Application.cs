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
        public PhysicsLayer.PhysicsContext physicsContext;

        public Camera2D camera2D = new Camera2D();
        public Sprite floor = new Sprite(new vec2(20.0f, 0.1f), new vec2(0.0f, -0.9f));
        public Sprite quad = new Sprite(new vec2(0.125f, 0.125f), new vec2(0.0f, 0.5f));
        public Shader shader;  // Don't load until constructor
        public PhysicsLayer.PhysicsBody floor_body, quad_body;

        // Initialize all engine variables within constructor
        public Application() : base(new WindowLayer.Options(1280, 720, 4, 4, false, "SPHelloWorld", true), Properties.Resources.ResourceManager)
        {
            floor.BufferToGPU();
            quad.BufferToGPU();
            shader = new Shader("simple_v", "simple_f");

            // Set background color of sky
            camera2D.clearColor = new Color("#c0e6fc");

            // Physics
            physicsContext = PhysicsLayer.PhysicsContext.NewContext(-98.0f);
            floor.SetupPhysics(ref physicsContext, Sprite.BodyType.Static);
            quad.SetupPhysics(ref physicsContext, Sprite.BodyType.Dynamic);
        }

        bool showDebugConsole = true;

        Color floorColor = new Color("#318a0e");
        Color quadColor = new Color("#000055");
        public override void Update()
        {
            // Handle Input
            if (WindowLayer.GetKeyDown(ctx, '`')) showDebugConsole = !showDebugConsole;
            if (WindowLayer.GetKeyDown(ctx, 'R')) RandomColor();
            if (WindowLayer.GetKeyDown(ctx, 'H')) DebugConsole.Write(quad_body.GetPosition().ToString());
            HandleQuadMovement();

            // Update Physics
            physicsContext.Step(1.0f / 60.0f);

            shader.SetColor("color", floorColor);
            floor.UpdatePhysics();
            floor.Draw(shader);

            shader.SetColor("color", quadColor);
            quad.UpdatePhysics();
            quad.Draw(shader);
        }

        public override void DrawUI()
        {
            // Draw UI
            if (showDebugConsole) DrawDebugConsole();
            if (showDebugConsole) DrawRenderConsole();
        }

        // --

        public void HandleQuadMovement()
        {
            if (WindowLayer.GetKey(ctx, 'W')) quad.Move(vec2.UnitY * deltaTime);
            if (WindowLayer.GetKey(ctx, 'S')) quad.Move(-vec2.UnitY * deltaTime);
            if (WindowLayer.GetKey(ctx, 'A')) quad.Move(-vec2.UnitX * deltaTime);
            if (WindowLayer.GetKey(ctx, 'D')) quad.Move(vec2.UnitX * deltaTime);
            if (WindowLayer.GetKey(ctx, 'Q')) quad.Rotate(deltaTime * (float)Math.PI);
            if (WindowLayer.GetKey(ctx, 'E')) quad.Rotate(-deltaTime * (float)Math.PI);
        }

        public void RandomColor()
        {
            var rand = new Random();
            float hue = (float)rand.NextDouble() * 360.0f;
            camera2D.clearColor = Color.FromHSV(hue, 1.0f, 1.0f);
            DebugConsole.Write($"Random color set to <{hue:0.00}, 1, 1>");
        }

        public void DrawRenderConsole()
        {
            UILayer.EnterUIWindow("Physics Console");
            UILayer.UIText($"Box Position\nX: {quad.offset.x:0.00} Y: {quad.offset.y:0.00}");
            UILayer.UIText($"Box Velocity\nX: {quad.velocity.x:0.00} Y: {quad.velocity.y:0.00}");
            //UILayer.UIColorPicker3("Background Color", ref camera2D.clearColor);
            //UILayer.UIColorPicker3("Quad Color", ref quadColor);
            //UILayer.UISeparator();
            //UILayer.UIVector2("Quad Offset", ref quad.offset, 0.01f, -1.0f, 1.0f);
            //UILayer.UIVector2("Quad Size", ref quad.size, 0.1f, 0.0f, 10.0f);
            //UILayer.UIVector2("Floor Offset", ref floor.offset, 0.01f, -1.0f, 1.0f);
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
