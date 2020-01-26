#pragma once
#include "include/GLFW/glfw3.h"

#ifdef SPREADGFX_EXPORTS
#define SPREAD_API extern "C" __declspec(dllexport)
#else
#define SPREAD_API extern "C" __declspec(dllimport)
#endif

#pragma pack(push, 8)
struct TextureInfo
{
	int width, height;
	GLuint texID;
};
#pragma pack(pop)

SPREAD_API TextureInfo loadTexture(const char* filePath, bool linear);
SPREAD_API TextureInfo loadTextureArray(const char** filePaths, int fileCount);
SPREAD_API void bindTexture(TextureInfo info, int slot);
SPREAD_API void bindTextureArray(TextureInfo info, int slot);