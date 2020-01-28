using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using SpreadRuntime.LowLevel.Wrappers;
using System.Windows.Forms;
using SpreadRuntime.Bootstrap.EventHooks;
using SpreadRuntime.HighLevel;

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

                InternalRunners<HookUpdateAttribute>(app);
                app.Update();

                UILayer.ExitUIFrame();
                WindowLayer.ExitRenderLoop(app.ctx);

                app.time = GetTime();
                app.deltaTime = GetTime() - lastFrameTime;
                lastFrameTime = GetTime();
            }
        }

        public static void InternalRunners<T>(SpreadApplication app) where T : Attribute
        {
            foreach (var field in app.GetType().GetFields())
            {
                foreach (var method in field.FieldType.GetMethods())
                {
                    if (method.GetCustomAttribute<T>() != null)
                    {
                        method.Invoke(field.GetValue(app), new object[] { app });
                    }
                }
            }
        }
    }
}
