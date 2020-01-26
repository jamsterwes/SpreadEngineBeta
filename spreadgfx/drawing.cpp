#include <GL/glew.h>
#include "drawing.h"
#include <cstdlib>

GLuint genBuffer()
{
	GLuint vbo;
	glGenBuffers(1, &vbo);
	glBindBuffer(GL_ARRAY_BUFFER, vbo);
	return vbo;
}

VBOData allocVBOData(int vertexCount, int floatPerVertex)
{
	VBOData data;
	data.vertexCount = vertexCount;
	data.floatPerVertex = floatPerVertex;
	data.vertices = (float*)calloc((size_t)vertexCount * floatPerVertex, sizeof(float));
	return data;
}

void freeVBOData(VBOData data)
{
	free(data.vertices);
}

void bufferVBO(GLuint vbo, VBOData data)
{
	glBindBuffer(GL_ARRAY_BUFFER, vbo);
	glBufferData(GL_ARRAY_BUFFER, sizeof(float) * data.floatPerVertex * data.vertexCount, data.vertices, GL_STATIC_DRAW);
}

EBOData allocEBOData(unsigned int elemCount)
{
	EBOData data;
	data.elementCount = elemCount;
	data.elements = (unsigned int*)calloc((size_t)elemCount, sizeof(unsigned int));
	return data;
}

void bufferEBO(GLuint ebo, EBOData data)
{
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ebo);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, data.elementCount * sizeof(unsigned int), data.elements, GL_STATIC_DRAW);
}

GLuint genAndBindVAO()
{
	GLuint vao;
	glGenVertexArrays(1, &vao);
	glBindVertexArray(vao);
	return vao;
}

void drawArrays(GLuint vbo, GLuint start, GLuint count, bool points)
{
	glBindBuffer(GL_ARRAY_BUFFER, vbo);
	glPointSize(10.0f);
	glDrawArrays(points ? GL_POINTS : GL_TRIANGLES, start, count);
}

void drawElements(GLuint vbo, GLuint ebo, GLuint count)
{
	glBindBuffer(GL_ARRAY_BUFFER, vbo);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ebo);
	glDrawElements(GL_TRIANGLES, count, GL_UNSIGNED_INT, 0);
}

void vertexAttrib(GLuint idx, GLuint amt, GLuint vertexAmount, GLuint offset)
{
	glVertexAttribPointer(idx, amt, GL_FLOAT, GL_FALSE, vertexAmount * sizeof(float), (void*)(sizeof(float) * offset));
	glEnableVertexAttribArray(idx);
}