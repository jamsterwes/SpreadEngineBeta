using System;
using System.Runtime.InteropServices;
using GlmSharp;

namespace SpreadRuntime.LowLevel.Wrappers
{
    public class PhysicsLayer
    {
        const float PHYSICS_SCALING_TOPHYS = 100.0f;

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PhysicsContext
        {
            IntPtr world;

            public static PhysicsContext NewContext(float gravity = -9.8f)
            {
                return createPhysicsContext(gravity);
            }

            public float GetGravity()
            {
                return physicsContext_getGravity(this);
            }

            public void Step(float timestep)
            {
                physicsContext_step(this, timestep);
            }
        };

        [DllImport("spreadgfx.dll")]
        static extern PhysicsContext createPhysicsContext(float gravity);
        [DllImport("spreadgfx.dll")]
        static extern float physicsContext_getGravity(PhysicsContext ctx);
        [DllImport("spreadgfx.dll")]
        static extern void physicsContext_step(PhysicsContext ctx, float timestep);

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PhysicsBody
        {
            IntPtr body;
            IntPtr box;

            public vec2 GetPosition()
            {
                return (new vec2(physicsBody_GetPositionX(this), physicsBody_GetPositionY(this))) / PHYSICS_SCALING_TOPHYS;
            }

            public vec2 GetVelocity()
            {
                return (new vec2(physicsBody_GetVelocityX(this), physicsBody_GetVelocityY(this))) / PHYSICS_SCALING_TOPHYS;
            }

            public void SetPosition(vec2 pos)
            {
                physicsBody_SetPosition(this, pos * PHYSICS_SCALING_TOPHYS);
            }

            public void ApplyImpulse(vec2 impulse)
            {
                physicsBody_ApplyImpulse(this, impulse * PHYSICS_SCALING_TOPHYS * PHYSICS_SCALING_TOPHYS);
            }

            public static PhysicsBody DynamicBody(PhysicsContext ctx, vec2 intl_pos, vec2 intl_size)
            {
                return newDynamicBody(ctx, intl_pos * PHYSICS_SCALING_TOPHYS, intl_size * PHYSICS_SCALING_TOPHYS);
            }

            public static PhysicsBody GroundBody(PhysicsContext ctx, vec2 intl_pos, vec2 intl_size)
            {
                return newGroundBody(ctx, intl_pos * PHYSICS_SCALING_TOPHYS, intl_size * PHYSICS_SCALING_TOPHYS);
            }
        };

        [DllImport("spreadgfx.dll")]
        public static extern PhysicsBody newGroundBody(PhysicsContext ctx, vec2 intl_pos, vec2 intl_size);

        [DllImport("spreadgfx.dll")]
        public static extern PhysicsBody newDynamicBody(PhysicsContext ctx, vec2 intl_pos, vec2 intl_size);
    
        [DllImport("spreadgfx.dll")]
        public static extern float physicsBody_GetPositionX(PhysicsBody body);
        [DllImport("spreadgfx.dll")]
        public static extern float physicsBody_GetPositionY(PhysicsBody body);
        [DllImport("spreadgfx.dll")]
        public static extern float physicsBody_GetVelocityX(PhysicsBody body);
        [DllImport("spreadgfx.dll")]
        public static extern float physicsBody_GetVelocityY(PhysicsBody body);
        [DllImport("spreadgfx.dll")]
        public static extern void physicsBody_SetPosition(PhysicsBody body, vec2 pos);
        [DllImport("spreadgfx.dll")]
        public static extern void physicsBody_ApplyImpulse(PhysicsBody body, vec2 impulse);
    }
}
