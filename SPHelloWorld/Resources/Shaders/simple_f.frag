#version 430 core

in vec2 vertexUV;
out vec4 outColor;

uniform vec4 color;

void main()
{
    outColor = vec4(color.rgb, 1.0);
}