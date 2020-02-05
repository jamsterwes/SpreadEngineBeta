using System;
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

        public PhysicsLayer.PhysicsBody body;
        public vec2 velocity = vec2.Zero;

        public Sprite(vec2 size, vec2 offset) : base(size, offset) { }

        public void SetupPhysics(ref PhysicsLayer.PhysicsContext ctx, BodyType type)
        {
            if (type == BodyType.Dynamic) body = PhysicsLayer.PhysicsBody.DynamicBody(ctx, offset, size);
            else if (type == BodyType.Static) body = PhysicsLayer.PhysicsBody.GroundBody(ctx, offset, size);
        }

        public void UpdatePhysics()
        {
            offset = body.GetPosition();
            velocity = body.GetVelocity();
        }

        public override void Move(vec2 offset)
        {
            base.Move(offset);
            body.SetPosition(this.offset);
        }
    }
}
