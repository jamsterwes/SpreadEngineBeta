using System.Collections.Generic;
using SpreadRuntime.LowLevel.Wrappers;

namespace SpreadRuntime.LowLevel.Rendering
{
    public class Shader
    {
        public uint program;
        public Shader(string vertexRes, string fragmentRes)
        {
            uint vert = GraphicsLayer.compileShader(GraphicsLayer.GLShaderType.VertexShader, SpreadApplication.LoadTextFileResource(vertexRes));
            uint frag = GraphicsLayer.compileShader(GraphicsLayer.GLShaderType.FragmentShader, SpreadApplication.LoadTextFileResource(fragmentRes));

            program = GraphicsLayer.createVisualShader(vert, frag);
            GraphicsLayer.linkVisualShader(program);
        }

        public uint FindAttribute(string name)
        {
            return GraphicsLayer.attribLocation(program, name);
        }

        public void Use()
        {
            GraphicsLayer.useVisualShader(program);
        }
    }
}
