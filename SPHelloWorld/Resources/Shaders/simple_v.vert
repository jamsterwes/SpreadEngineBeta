#version 430 core

layout (location = 0) in vec2 position;
layout (location = 1) in vec2 uv;

uniform vec2 size, offset, rotation;

// aspect ratio
float ratio = 16.0 / 9.0;

out vec2 vertexUV;
void main()
{
    vertexUV = uv;
    vec2 rotpos = vec2(position.x * cos(rotation.x) - position.y * sin(rotation.x), position.y * cos(rotation.x) + position.x * sin(rotation.x));
    rotpos.x = rotpos.x / ratio;
    gl_Position = vec4(rotpos * size + offset, 0.0, 1.0);
}