#include <GL/glew.h>
#define STB_IMAGE_IMPLEMENTATION
#include <stb_image.h>
#include "texture.hpp"

TextureInfo loadTexture(const char* filePath, bool linear)
{
	TextureInfo info;
	glGenTextures(1, &info.texID);
	glBindTexture(GL_TEXTURE_2D, info.texID);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	
	int channels;
	stbi_set_flip_vertically_on_load(true);
	unsigned char* image = stbi_load(filePath, &info.width, &info.height, &channels, STBI_rgb_alpha);
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, info.width, info.height, 0, GL_RGBA, GL_UNSIGNED_BYTE, image);
	stbi_image_free(image);

	return info;
}

// TODO: STOP HARD-CODING TEXTURE SIZES FOR ARRAYS
TextureInfo loadTextureArray(const char** filePaths, int fileCount)
{
	TextureInfo info;
	glGenTextures(1, &info.texID);
	glBindTexture(GL_TEXTURE_2D_ARRAY, info.texID);

	// Get info on first image
	stbi_set_flip_vertically_on_load(true);

	glTexParameteri(GL_TEXTURE_2D_ARRAY, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D_ARRAY, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D_ARRAY, GL_TEXTURE_MIN_FILTER, GL_NEAREST_MIPMAP_LINEAR);
	glTexParameteri(GL_TEXTURE_2D_ARRAY, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glTexParameterf(GL_TEXTURE_2D_ARRAY, GL_TEXTURE_MAX_ANISOTROPY, 8.0f);

	glTexStorage3D(GL_TEXTURE_2D_ARRAY, 5, GL_RGBA8, 16, 16, fileCount);

	for (int i = 0; i < fileCount; i++)
	{
		int channels;
		// We already loaded layer 1 (i == 0)
		unsigned char* image = stbi_load(filePaths[i], &info.width, &info.height, &channels, STBI_rgb_alpha);
		glTexSubImage3D(GL_TEXTURE_2D_ARRAY, 0, 0, 0, i, 16, 16, 1, GL_RGBA, GL_UNSIGNED_BYTE, image);
		stbi_image_free(image);
	}

	glGenerateMipmap(GL_TEXTURE_2D_ARRAY);

	return info;
}

void bindTexture(TextureInfo info, int slot)
{
	glActiveTexture(GL_TEXTURE0 + slot);
	glBindTexture(GL_TEXTURE_2D, info.texID);
}

void bindTextureArray(TextureInfo info, int slot)
{
	glActiveTexture(GL_TEXTURE0 + slot);
	glBindTexture(GL_TEXTURE_2D_ARRAY, info.texID);
}