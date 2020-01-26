using System;
using System.Runtime.InteropServices;
using SpreadRuntime.Utilities;
using GlmSharp;

namespace SpreadRuntime.Wrappers
{
    public static class GraphicsLayer
    {
        [DllImport("spreadgfx.dll")]
        extern static void ClearColor(float r, float g, float b);

        public static void ClearColor(Color c)
        {
            ClearColor(c.r, c.g, c.b);
        }

        public static void ClearColor(string hex)
        {
            ClearColor(new Color(hex));
        }

        public enum GLShaderType : uint
        {
            FragmentShader = 35632u,
            VertexShader = 35633u,
            GeometryShader = 0x8DD9u
        }

        [DllImport("spreadgfx.dll")]
        public extern static uint compileShader(GLShaderType shaderType, [MarshalAs(UnmanagedType.LPStr)] string shaderSource);

        [DllImport("spreadgfx.dll")]
        public extern static uint createVisualShader(uint vertexShader, uint fragmentShader);

        [DllImport("spreadgfx.dll")]
        public extern static uint createVisualShaderAndGeometry(uint vertexShader, uint fragmentShader, uint geometryShader);

        [DllImport("spreadgfx.dll")]
        public extern static void linkVisualShader(uint shaderID);

        [DllImport("spreadgfx.dll")]
        public extern static void useVisualShader(uint shaderID);

        [DllImport("spreadgfx.dll")]
        public extern static uint genAndBindVAO();

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct VBOData
        {
            public IntPtr vertices;
            public int vertexCount;
            public int floatPerVertex;

            public static VBOData FromVertices(float[] vertices, int floatPerVertex)
            {
                VBOData data = allocVBOData(vertices.Length / floatPerVertex, floatPerVertex);
                Marshal.Copy(vertices, 0, data.vertices, vertices.Length);
                return data;
            }
        }

        [DllImport("kernel32.dll", EntryPoint = "RtlCopyMemory", SetLastError = false)]
        static extern void CopyMemory(IntPtr Destination, IntPtr Source, uint Length);

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct EBOData
        {
            public IntPtr elemPtr;
            public uint count;

            public static unsafe void Copy(uint[] source, IntPtr dest, uint elemCount)
            {
                fixed (uint* ptrSrc = &source[0])
                {
                    CopyMemory(dest, (IntPtr)ptrSrc, elemCount * 4);
                }
            }

            public static EBOData FromElements(uint[] elems)
            {
                EBOData data = allocEBOData((uint)elems.Length);
                Copy(elems, data.elemPtr, (uint)elems.Length);
                return data;
            }
        }

        [DllImport("spreadgfx.dll")]
        public extern static uint genBuffer();

        [DllImport("spreadgfx.dll")]
        public extern static VBOData allocVBOData(int vertexCount, int floatPerVertex);
        [DllImport("spreadgfx.dll")]
        public extern static void freeVBOData(VBOData data);
        [DllImport("spreadgfx.dll")]
        public extern static void bufferVBO(uint vbo, VBOData data);
        [DllImport("spreadgfx.dll")]
        public extern static EBOData allocEBOData(uint elemCount);
        [DllImport("spreadgfx.dll")]
        public extern static void bufferEBO(uint ebo, EBOData data);

        [DllImport("spreadgfx.dll")]
        public extern static void drawArrays(uint vbo, uint start, uint count, bool points = false);
        [DllImport("spreadgfx.dll")]
        public extern static void drawElements(uint vbo, uint ebo, uint count);

        [DllImport("spreadgfx.dll")]
        public extern static void vertexAttrib(uint idx, uint amt, uint vertexSize, uint offset);

        [DllImport("spreadgfx.dll")]
        public extern static uint attribLocation(uint shaderID, [MarshalAs(UnmanagedType.LPStr)] string attribName);

        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct Matrix4
        {
            public IntPtr values;

            public static Matrix4 FromGLM(mat4 imat)
            {
                Matrix4 mat = allocMatrix4();
                Marshal.Copy(imat.Values1D, 0, mat.values, 16);
                return mat;
            }

            public void Free()
            {
                freeMatrix4(this);
            }
        }

        [DllImport("spreadgfx.dll")]
        public extern static void setAttribInt(uint attrib, int f);
        [DllImport("spreadgfx.dll")]
        public extern static void setAttribFloat(uint attrib, float f);
        [DllImport("spreadgfx.dll")]
        public extern static void setAttribColor(uint attrib, float r, float g, float b, float a);
        [DllImport("spreadgfx.dll")]
        public extern static void setAttribMatrix4(uint attrib, Matrix4 f);

        [DllImport("spreadgfx.dll")]
        public extern static Matrix4 allocMatrix4();
        [DllImport("spreadgfx.dll")]
        public extern static void freeMatrix4(Matrix4 mat);
    }
}
