#version 430 core
layout (points) in;
layout (triangle_strip, max_vertices = 24) out;


in vec3 occPositive[];
in vec3 occNegative[];
in vec3 layers[];

uniform mat4 mvp;

out vec3 blockPos;
out vec2 vertexUV;
out vec3 vertexNormal;
out vec2 atlasOff;
out float texLayer;
out float ao;
out float skyDiff;

uniform float time;

vec4 translate(vec4 pt)
{
	blockPos = gl_in[0].gl_Position.xyz;
	return mvp * pt;
}

float calculateAO(float occSide1, float occSide2, float corner)
{
	return 1.0 - ((occSide1 + occSide2 + corner) / 3.0);
}

void calculateLighting(vec3 normal)
{
	vertexNormal = normal;
	vec3 norm = normalize(normal);
	vec3 lightDir = normalize(vec3(64.0, -110.0, 64.0));
	float k = min(max(dot(norm, -lightDir), 0.0), 1.0);
	float timeOfDay = 1.0 + sin(time * 0.1) * 0.5; // between 0.0 and 1.0
	float contrast = 0.3;  // clamps lighting between timeOfDay and timeOfDay - contrast
	skyDiff = k * contrast + (timeOfDay - contrast);
}

void frontFace();
void backFace();
void topFace();
void bottomFace();
void leftFace();
void rightFace();

void main() {
	if (occPositive[0].z < 1.0f) frontFace();
	if (occNegative[0].z < 1.0f) backFace();
	if (occPositive[0].y < 1.0f) topFace();
	if (occNegative[0].y < 1.0f) bottomFace();
	if (occNegative[0].x < 1.0f) leftFace();
	if (occPositive[0].x < 1.0f) rightFace();
}

in vec4 xAO[];  // -Y-Z, -Y+Z, +Y-Z, +Y+Z
in vec4 yAO[];  // -X-Z, -X+Z, +X-Z, +X+Z
in vec4 zAO[];  // -X-Y, -X+Y, +X-Y, +X+Y
in vec4 nxAO[];  // -X-Y-Z, -X-Y+Z, -X+Y-Z, -X+Y+Z
in vec4 pxAO[];  // +X-Y-Z, +X-Y+Z, +X+Y-Z, +X+Y+Z

// +ZPLANE
void frontFace()
{
	vec3 faceNormal = vec3(0.0, 0.0, 1.0);

	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 0.0, 1.0, 0.0));
	ao = calculateAO(xAO[0].y, yAO[0].y, nxAO[0].y);  // -X-Y+Z
	texLayer = layers[0].y;
	vertexUV = vec2(0.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();

	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 0.0, 1.0, 0.0));
	// X:-Y+Z, Y:+X+Z, +X-Y+Z
	ao = calculateAO(xAO[0].y, yAO[0].w, pxAO[0].y);  // +X-Y+Z
	texLayer = layers[0].y;
	vertexUV = vec2(1.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();

	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 1.0, 1.0, 0.0));
	ao = calculateAO(xAO[0].w, yAO[0].y, nxAO[0].w);  // -X+Y+Z
	texLayer = layers[0].y;
	vertexUV = vec2(0.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 1.0, 1.0, 0.0));
	// Y:+X+Z, X:+Y+Z, +X+Y+Z
	ao = calculateAO(xAO[0].w, yAO[0].w, pxAO[0].w);  // +X+Y+Z
	texLayer = layers[0].y;
	vertexUV = vec2(1.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();

	EndPrimitive();
}

// -ZPLANE
void backFace()
{
	vec3 faceNormal = vec3(0.0, 0.0, -1.0);

	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 0.0, 0.0, 0.0));
	ao = calculateAO(xAO[0].x, yAO[0].z, pxAO[0].x);  // +X-Y-Z
	texLayer = layers[0].y;
	vertexUV = vec2(0.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();

	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 0.0, 0.0, 0.0));
	ao = calculateAO(xAO[0].x, yAO[0].x, nxAO[0].x);  // -X-Y-Z
	texLayer = layers[0].y;
	vertexUV = vec2(1.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();

	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 1.0, 0.0, 0.0));
	ao = calculateAO(xAO[0].z, yAO[0].z, pxAO[0].z);  // +X+Y-Z
	texLayer = layers[0].y;
	vertexUV = vec2(0.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();

	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 1.0, 0.0, 0.0));
	ao = calculateAO(xAO[0].z, yAO[0].x, nxAO[0].z);  // -X+Y-Z
	texLayer = layers[0].y;
	vertexUV = vec2(1.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();

	EndPrimitive();
}

//in vec4 xAO[];  // -Y-Z, -Y+Z, +Y-Z, +Y+Z
//in vec4 yAO[];  // -X-Z, -X+Z, +X-Z, +X+Z
//in vec4 zAO[];  // -X-Y, -X+Y, +X-Y, +X+Y
//in vec4 nxAO[];  // -X-Y-Z, -X-Y+Z, -X+Y-Z, -X+Y+Z
//in vec4 pxAO[];  // +X-Y-Z, +X-Y+Z, +X+Y-Z, +X+Y+Z

// +YPLANE
void topFace()
{
	vec3 faceNormal = vec3(0.0, 1.0, 0.0);

	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 1.0, 1.0, 0.0));
	ao = calculateAO(xAO[0].w, zAO[0].y, nxAO[0].w);  // -X+Y+Z
	texLayer = layers[0].x;
	vertexUV = vec2(0.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 1.0, 1.0, 0.0));
	ao = calculateAO(xAO[0].w, zAO[0].w, pxAO[0].w);  // +X+Y+Z
	texLayer = layers[0].x;
	vertexUV = vec2(1.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 1.0, 0.0, 0.0));
	ao = calculateAO(xAO[0].z, zAO[0].y, nxAO[0].z);  // -X+Y-Z
	texLayer = layers[0].x;
	vertexUV = vec2(0.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 1.0, 0.0, 0.0));
	ao = calculateAO(xAO[0].z, zAO[0].w, pxAO[0].z);  // +X+Y-Z
	texLayer = layers[0].x;
	vertexUV = vec2(1.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();

	EndPrimitive();
}

// -YPLANE
void bottomFace()
{
	vec3 faceNormal = vec3(0.0, -1.0, 0.0);

	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 0.0, 0.0, 0.0));
	ao = calculateAO(xAO[0].x, zAO[0].x, nxAO[0].x);  // -X-Y-Z
	texLayer = layers[0].z;
	vertexUV = vec2(0.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 0.0, 0.0, 0.0));
	ao = calculateAO(xAO[0].x, zAO[0].z, pxAO[0].x);  // +X-Y-Z
	texLayer = layers[0].z;
	vertexUV = vec2(1.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();

	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 0.0, 1.0, 0.0));
	ao = calculateAO(xAO[0].y, zAO[0].x, nxAO[0].y);  // -X-Y+Z
	texLayer = layers[0].z;
	vertexUV = vec2(0.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 0.0, 1.0, 0.0));
	ao = calculateAO(xAO[0].y, zAO[0].z, pxAO[0].y);  // +X-Y+Z
	texLayer = layers[0].z;
	vertexUV = vec2(1.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();

	EndPrimitive();
}

// -XPLANE
void leftFace()
{
	vec3 faceNormal = vec3(-1.0, 0.0, 0.0);

	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 0.0, 0.0, 0.0));
	ao = calculateAO(yAO[0].x, zAO[0].x, nxAO[0].x);  // -X-Y-Z
	texLayer = layers[0].y;
	vertexUV = vec2(0.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 0.0, 1.0, 0.0));
	ao = calculateAO(yAO[0].y, zAO[0].x, nxAO[0].y);  // -X-Y+Z
	texLayer = layers[0].y;
	vertexUV = vec2(1.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 1.0, 0.0, 0.0));
	ao = calculateAO(yAO[0].x, zAO[0].y, nxAO[0].z);  // -X+Y-Z
	texLayer = layers[0].y;
	vertexUV = vec2(0.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(0.0, 1.0, 1.0, 0.0));
	ao = calculateAO(yAO[0].y, zAO[0].y, nxAO[0].w);  // -X+Y+Z
	texLayer = layers[0].y;
	vertexUV = vec2(1.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();

	EndPrimitive();
}

// +XPLANE
void rightFace()
{
	vec3 faceNormal = vec3(1.0, 0.0, 0.0);
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 0.0, 1.0, 0.0));
	ao = calculateAO(yAO[0].w, zAO[0].z, pxAO[0].y);  // +X-Y+Z
	texLayer = layers[0].y;
	vertexUV = vec2(0.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();

	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 0.0, 0.0, 0.0));
	ao = calculateAO(yAO[0].z, zAO[0].z, pxAO[0].x);  // +X-Y-Z
	texLayer = layers[0].y;
	vertexUV = vec2(1.0, 0.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 1.0, 1.0, 0.0));
	ao = calculateAO(yAO[0].w, zAO[0].w, pxAO[0].w);  // +X+Y+Z
	texLayer = layers[0].y;
	vertexUV = vec2(0.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();
	
	gl_Position = translate(gl_in[0].gl_Position + vec4(1.0, 1.0, 0.0, 0.0));
	ao = calculateAO(yAO[0].z, zAO[0].w, pxAO[0].z);  // +X+Y-Z
	texLayer = layers[0].y;
	vertexUV = vec2(1.0, 1.0);
	calculateLighting(faceNormal);
	EmitVertex();

	EndPrimitive();
}