using SpreadRuntime;
using SpreadRuntime.LowLevel.Wrappers;

using SPHelloWorld.Objects;

namespace SPHelloWorld
{
    public class Application : SpreadApplication
    {
        /// In-Game Objects
        BlankObject obj;


        /// Constructor

        public Application() : base(new WindowLayer.Options(1280, 720, 4, 4, false, "SPHelloWorld", true), Properties.Resources.ResourceManager)
        {
            obj = new BlankObject();
        }

        /// Update/Draw Functions

        public override void Update() { }

        public override void DrawUI() { }
    }
}
