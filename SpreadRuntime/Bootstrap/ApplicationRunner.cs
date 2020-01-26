using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadRuntime.Wrappers;

namespace SpreadRuntime.Bootstrap
{
    public class ApplicationRunner
    {
        static DateTime startTime;
        static float GetTime()
        {
            TimeSpan ts = DateTime.Now - startTime;
            float time = (float)ts.TotalSeconds;
            return time;
        }

        public static void RunApplication(SpreadApplication app)
        {
            startTime = DateTime.Now;

            float lastFrameTime = GetTime();
            while (WindowLayer.ShouldRender(app.ctx))
            {
                WindowLayer.EnterRenderLoop(app.ctx);
                UILayer.EnterUIFrame();

                app.Update();

                UILayer.ExitUIFrame();
                WindowLayer.ExitRenderLoop(app.ctx);

                app.time = GetTime();
                app.deltaTime = GetTime() - lastFrameTime;
                lastFrameTime = GetTime();
            }
        }
    }
}
