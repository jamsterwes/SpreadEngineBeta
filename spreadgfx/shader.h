#pragma once
#include "include/GLFW/glfw3.h"

#ifdef SPREADGFX_EXPORTS
#define SPREAD_API extern "C" __declspec(dllexport)
#else
#define SPREAD_API extern "C" __declspec(dllimport)
#endif

#pragma pack(push, 8)
struct Matrix4
{
	float* values;
};
#pragma pack(pop)

// shader = individual shader file
SPREAD_API GLuint compileShader(GLenum shaderType, const char* shaderSource);

// shader = entire shader program
SPREAD_API GLuint createVisualShader(GLuint vertexShader, GLuint fragmentShader);
SPREAD_API GLuint createVisualShaderAndGeometry(GLuint vertexShader, GLuint fragmentShader, GLuint geometryShader);
SPREAD_API void linkVisualShader(GLuint shaderID);
SPREAD_API void useVisualShader(GLuint shaderID);

SPREAD_API GLuint attribLocation(GLuint shaderID, const char* attribName);
SPREAD_API void setAttribInt(GLuint attrib, int i);
SPREAD_API void setAttribFloat(GLuint attrib, float f);
SPREAD_API void setAttribColor(GLuint attrib, float r, float g, float b, float a);
SPREAD_API void setAttribMatrix4(GLuint attrib, Matrix4 mat);

SPREAD_API Matrix4 allocMatrix4();
SPREAD_API void freeMatrix4(Matrix4 mat);