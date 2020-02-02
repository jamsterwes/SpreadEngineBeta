#include <GL/glew.h>
#include "shader.hpp"
#include <stdlib.h>

GLuint compileShader(GLenum shaderType, const char* shaderSource)
{
	GLuint shaderID = glCreateShader(shaderType);
	glShaderSource(shaderID, 1, &shaderSource, NULL);
	glCompileShader(shaderID);
	return shaderID;
}

GLuint createVisualShader(GLuint vertexShader, GLuint fragmentShader)
{
	GLuint shaderProgram = glCreateProgram();
	glAttachShader(shaderProgram, vertexShader);
	glAttachShader(shaderProgram, fragmentShader);
	glBindFragDataLocation(shaderProgram, 0, "outColor");
	return shaderProgram;
}

GLuint createVisualShaderAndGeometry(GLuint vertexShader, GLuint fragmentShader, GLuint geometryShader)
{
	GLuint shaderProgram = glCreateProgram();
	glAttachShader(shaderProgram, vertexShader);
	glAttachShader(shaderProgram, geometryShader);
	glAttachShader(shaderProgram, fragmentShader);
	glBindFragDataLocation(shaderProgram, 0, "outColor");
	return shaderProgram;
}

void linkVisualShader(GLuint shaderID)
{
	glLinkProgram(shaderID);
}

void useVisualShader(GLuint shaderID)
{
	glUseProgram(shaderID);
}

GLuint attribLocation(GLuint shaderID, const char* attribName)
{
	return glGetUniformLocation(shaderID, attribName);
}

void setAttribInt(GLuint attrib, int i)
{
	glUniform1i(attrib, i);
}

void setAttribFloat(GLuint attrib, float f)
{
	glUniform1f(attrib, f);
}

void setAttribVec2(GLuint attrib, float x, float y)
{
	glUniform2f(attrib, x, y);
}

void setAttribColor(GLuint attrib, float r, float g, float b, float a)
{
	glUniform4f(attrib, r, g, b, a);
}

void setAttribMatrix4(GLuint attrib, Matrix4 mat)
{
	glUniformMatrix4fv(attrib, 1, GL_FALSE, mat.values);
}

Matrix4 allocMatrix4()
{
	Matrix4 mat;
	mat.values = (float*)calloc(16, sizeof(float));
	return mat;
}

void freeMatrix4(Matrix4 mat)
{
	free(mat.values);
}