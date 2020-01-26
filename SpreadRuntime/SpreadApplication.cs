using SpreadRuntime.Wrappers;

namespace SpreadRuntime
{
    public class SpreadApplication
    {
        public WindowLayer.Context ctx;
        public float deltaTime, time;

        public SpreadApplication(WindowLayer.Options opts)
        {
            ctx = WindowLayer.CreateWindowContext(opts);
            UILayer.InitializeUI(ctx, "#version 440 core", "res/Roboto-Regular.ttf");
        }
        public virtual void Update() { }
    }
}
