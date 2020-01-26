using System.Resources;
using System.Text;
using SpreadRuntime.Wrappers;

namespace SpreadRuntime
{
    public class SpreadApplication
    {
        public static ResourceManager res;
        public WindowLayer.Context ctx;
        public float deltaTime, time;

        public SpreadApplication(WindowLayer.Options opts, ResourceManager res)
        {
            ctx = WindowLayer.CreateWindowContext(opts);
            SpreadApplication.res = res;
            UILayer.InitializeUI(ctx, "#version 440 core", "res/Roboto-Regular.ttf");
        }
        public virtual void Update() { }

        // Special stuff
        public static string LoadTextFileResource(string name)
        {
            return Encoding.ASCII.GetString(res.GetObject(name) as byte[]);
        }
    }
}
