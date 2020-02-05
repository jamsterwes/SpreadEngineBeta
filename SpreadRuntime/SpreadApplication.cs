using System.Resources;
using System.Text;
using SpreadRuntime.LowLevel.Wrappers;

namespace SpreadRuntime
{
    public class SpreadApplication
    {
        public static ResourceManager res;
        public PhysicsLayer.PhysicsContext physicsContext;
        public WindowLayer.Context ctx;
        public float deltaTime, time;

        public SpreadApplication(WindowLayer.Options opts, ResourceManager res, float gravity = -9.8f)
        {
            // Initialize Window/Resource contexts
            ctx = WindowLayer.CreateWindowContext(opts);
            SpreadApplication.res = res;

            // Initialize UI
            UILayer.InitializeUI(ctx, "#version 440 core", "res/Roboto-Regular.ttf");

            // Initialize Physics
            physicsContext = PhysicsLayer.PhysicsContext.NewContext(gravity);
        }
        public virtual void Update() { }
        public virtual void DrawUI() { }

        // Special stuff
        public static string LoadTextFileResource(string name)
        {
            return Encoding.UTF8.GetString(res.GetObject(name) as byte[]);
        }
    }
}
