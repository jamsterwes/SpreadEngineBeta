using System.Windows.Forms;
using System.IO;

using SpreadRuntime;
using SpreadRuntime.Wrappers;

namespace SPHelloWorld
{
    public class Application : ISpreadApplication
    {
        public void Run()
        {
            Windowing.Context ctx = Windowing.CreateWindowContext(new Windowing.Options(
                1280, 720, 4, 4, false, "SPHelloWorld"
            ));

            while (Windowing.ShouldRender(ctx))
            {
                Windowing.EnterRenderLoop(ctx);
                // ...
                Windowing.ExitRenderLoop(ctx);
            }
        }
    }
}
