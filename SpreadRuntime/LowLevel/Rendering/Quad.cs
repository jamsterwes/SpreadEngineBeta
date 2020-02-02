using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Draw()
        {
            GraphicsLayer.drawElements(vbo, ebo, (uint)ELEMS.Length);
        }
    }
}
