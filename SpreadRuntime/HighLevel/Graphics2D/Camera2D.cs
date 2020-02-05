using SpreadRuntime.Utilities;
using SpreadRuntime.Bootstrap.EventHooks;
using SpreadRuntime.LowLevel.Wrappers;
using GlmSharp;

namespace SpreadRuntime.HighLevel.Graphics2D
{
    public class Camera2D
    {
        public Color clearColor = new Color(0, 0, 0, 1);
        public vec2 position = vec2.Zero;

        [HookUpdate]
        public void Update(SpreadApplication app)
        {
            GraphicsLayer.ClearColor(clearColor);
        }
    }
}
