#pragma once
#include <GLFW/glfw3.h>

#ifdef SPREADGFX_EXPORTS
#define SPREAD_API extern "C" __declspec(dllexport)
#else
#define SPREAD_API extern "C" __declspec(dllimport)
#endif

#pragma pack(push, 8)
struct VBOData
{
	float* vertices;
	int vertexCount;
	int floatPerVertex;
};
#pragma pack(pop)

#pragma pack(push, 8)
struct EBOData
{
	unsigned int* elements;
	int elementCount;
};
#pragma pack(pop)

SPREAD_API GLuint genBuffer();

SPREAD_API VBOData allocVBOData(int vertexCount, int floatPerVertex);
SPREAD_API void freeVBOData(VBOData data);
SPREAD_API void bufferVBO(GLuint vbo, VBOData data);

SPREAD_API EBOData allocEBOData(unsigned int elemCount);
SPREAD_API void bufferEBO(GLuint ebo, EBOData data);

SPREAD_API GLuint genAndBindVAO();

SPREAD_API void drawArrays(GLuint vbo, GLuint start, GLuint count, bool points);

SPREAD_API void drawElements(GLuint vbo, GLuint ebo, GLuint count);

SPREAD_API void vertexAttrib(GLuint idx, GLuint amt, GLuint vertexSize, GLuint offset);