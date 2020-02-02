using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GlmSharp;
using SpreadRuntime.LowLevel.Wrappers;
using SpreadRuntime.LowLevel.Rendering;

namespace SpreadRuntime.HighLevel.Graphics2D
{
    public class Sprite : Quad
    {
        public enum BodyType
        {
            Static, Dynamic
        };

        PhysicsLayer.PhysicsBody body;

        public void SetupPhysics(ref PhysicsLayer.PhysicsContext ctx, BodyType type)
        {
            if (type == BodyType.Dynamic) body = PhysicsLayer.newDynamicBody(ctx, offset, size);
            else if (type == BodyType.Static) body = PhysicsLayer.newGroundBody(ctx, offset, size);
        }

        public void UpdatePhysics()
        {
            offset = body.GetPosition();
        }

        public override void Move(vec2 offset)
        {
            base.Move(offset);
            // body.SetPosition(offset);
        }
    }
}
