#pragma once
#include "box2d/b2_world.h"

#ifdef SPREADGFX_EXPORTS
#define SPREAD_API extern "C" __declspec(dllexport)
#else
#define SPREAD_API extern "C" __declspec(dllimport)
#endif

#pragma pack(push, 8)
struct physics_context
{
	b2World* world;
};
#pragma pack(pop)

SPREAD_API physics_context createPhysicsContext(float gravity);
SPREAD_API float physicsContext_getGravity(physics_context ctx);
SPREAD_API void physicsContext_step(physics_context ctx, float timestep);