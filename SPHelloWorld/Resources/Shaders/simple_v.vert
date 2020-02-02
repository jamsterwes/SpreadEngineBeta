#version 430 core

layout (location = 0) in vec2 position;
layout (location = 1) in vec2 uv;

// uniform mat4 mvp;

out vec2 vertexUV;
void main()
{
    vertexUV = uv;
//    gl_Position = mvp * vec4(position, 0.0, 1.0);
    gl_Position = vec4(position * 0.5, 0.0, 1.0);
}