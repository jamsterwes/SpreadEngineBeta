using SpreadRuntime.Utilities;
using SpreadRuntime.Bootstrap.EventHooks;
using SpreadRuntime.LowLevel.Wrappers;

namespace SpreadRuntime.HighLevel.Graphics2D
{
    public class Camera2D
    {
        public Color clearColor = new Color(0, 0, 0, 1);

        [HookUpdate]
        public void Update(SpreadApplication app)
        {
            GraphicsLayer.ClearColor(clearColor);
        }
    }
}
