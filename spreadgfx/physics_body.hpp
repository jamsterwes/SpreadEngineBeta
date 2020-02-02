#pragma once
#include "box2d/b2_body.h"
#include "physics_context.hpp"

#ifdef SPREADGFX_EXPORTS
#define SPREAD_API extern "C" __declspec(dllexport)
#else
#define SPREAD_API extern "C" __declspec(dllimport)
#endif

#pragma pack(push, 8)
struct physics_body
{
	b2Body* body;
	b2PolygonShape* box;
};
#pragma pack(pop)

SPREAD_API physics_body newGroundBody(physics_context ctx, b2Vec2 intl_pos, b2Vec2 intl_size);