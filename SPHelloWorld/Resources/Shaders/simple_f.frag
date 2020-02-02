#version 430 core

in vec2 vertexUV;
out vec4 outColor;

void main()
{
    outColor = vec4(vertexUV, 0.0, 1.0);
}