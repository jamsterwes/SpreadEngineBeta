#include "physics_context.hpp"

physics_context createPhysicsContext(float gravity)
{
	physics_context ctx;
	b2Vec2 gravity_vector;
	gravity_vector.Set(0, gravity);
	ctx.world = new b2World(gravity_vector);
	return ctx;
}

float physicsContext_getGravity(physics_context ctx)
{
	return ctx.world->GetGravity().y;
}

void physicsContext_step(physics_context ctx, float timestep)
{
	ctx.world->Step(timestep, 128, 128);
}