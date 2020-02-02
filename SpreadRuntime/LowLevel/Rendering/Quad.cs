using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GlmSharp;
using SpreadRuntime.LowLevel.Wrappers;

namespace SpreadRuntime.LowLevel.Rendering
{
    public class Quad
    {
        private static readonly float[] VERTS = new float[] {
            -1.0f, 1.0f, 0.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 1.0f,
            1.0f, -1.0f, 1.0f, 0.0f,
            -1.0f, -1.0f, 0.0f, 0.0f
        };

        private static readonly uint[] ELEMS = new uint[]
        {
            0, 1, 2,
            0, 2, 3
        };

        private uint vao, vbo, ebo;

        public vec2 size;
        public vec2 offset;
        public vec2 rotation;

        public Quad()
        {
            size = vec2.Ones;
            offset = vec2.Zero;
            rotation = vec2.Zero;
        }

        public Quad(vec2 size)
        {
            this.size = size;
            offset = vec2.Zero;
            rotation = vec2.Zero;
        }

        public Quad(vec2 size, vec2 offset)
        {
            this.size = size;
            this.offset = offset;
            rotation = vec2.Zero;
        }

        public Quad(vec2 size, vec2 offset, vec2 rotation)
        {
            this.size = size;
            this.offset = offset;
            this.rotation = rotation;
        }

        public void BufferToGPU()
        {
            // Gen buffers
            vao = GraphicsLayer.genAndBindVAO();
            vbo = GraphicsLayer.genBuffer();
            ebo = GraphicsLayer.genBuffer();
            // Populate VBO
            uint vertexFloats = 4;
            var vboData = GraphicsLayer.VBOData.FromVertices(VERTS, (int)vertexFloats);
            GraphicsLayer.bufferVBO(vbo, vboData);
            // Populate EBO
            var eboData = GraphicsLayer.EBOData.FromElements(ELEMS);
            GraphicsLayer.bufferEBO(ebo, eboData);
            // Populate VAO / Vertex format
            GraphicsLayer.vertexAttrib(0, 2, vertexFloats, 0);  // in vec2 pos
            GraphicsLayer.vertexAttrib(1, 2, vertexFloats, 2);  // in vec2 uv
        }

        public void Draw(Shader shader)
        {
            shader.Use();
            shader.SetVec2("size", size);
            shader.SetVec2("offset", offset);
            shader.SetVec2("rotation", rotation);
            GraphicsLayer.drawElements(vbo, ebo, (uint)ELEMS.Length);
        }

        public virtual void Move(vec2 offset)
        {
            this.offset += offset;
        }

        public virtual void Rotate(float angle)
        {
            this.rotation.x += angle;
        }
    }
}
