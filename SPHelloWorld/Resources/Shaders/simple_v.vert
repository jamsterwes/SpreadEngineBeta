#version 430 core

layout (location = 0) in vec2 position;
layout (location = 1) in vec2 uv;

uniform vec2 size, offset, rotation, cameraOffset;

// aspect ratio
float ratio = 16.0 / 9.0;

out vec2 vertexUV;
void main()
{
    vertexUV = uv;
    vec2 rotpos = position * size;
    rotpos = vec2(rotpos.x * cos(rotation.x) - rotpos.y * sin(rotation.x), rotpos.y * cos(rotation.x) + rotpos.x * sin(rotation.x));
    rotpos += offset - cameraOffset;
    rotpos.x = rotpos.x / ratio;
    gl_Position = vec4(rotpos, 0.0, 1.0);
}