using SpreadRuntime;
using SpreadRuntime.Wrappers;

namespace SPHelloWorld
{
    public class Application : ISpreadApplication
    {
        public void Run()
        {
            WindowLayer.Context ctx = WindowLayer.CreateWindowContext(new WindowLayer.Options(
                1280, 720, 4, 4, false, "SPHelloWorld", true
            ));

            int i = 0;
            while (WindowLayer.ShouldRender(ctx))
            {
                GraphicsLayer.ClearColor((i >= 60) ? "#FF00FF" : "#00FF00");

                WindowLayer.EnterRenderLoop(ctx);
                // ...
                WindowLayer.ExitRenderLoop(ctx);

                i = (i + 1) % 120;
            }
        }
    }
}
