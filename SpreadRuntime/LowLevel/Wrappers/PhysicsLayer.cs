using System;
using System.Runtime.InteropServices;
using GlmSharp;

namespace SpreadRuntime.LowLevel.Wrappers
{
    public class PhysicsLayer
    {
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
        };

        [DllImport("spreadgfx.dll")]
        static extern PhysicsContext createPhysicsContext(float gravity);
        [DllImport("spreadgfx.dll")]
        static extern float physicsContext_getGravity(PhysicsContext ctx);

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct PhysicsBody
        {
            IntPtr body;
            IntPtr box;

            public vec2 GetPosition()
            {
                return new vec2(physicsBody_GetPositionX(this), physicsBody_GetPositionY(this));
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
    }
}
