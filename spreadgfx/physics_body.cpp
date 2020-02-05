#include "physics_body.hpp"
#include "box2d/b2_fixture.h"
#include "box2d/b2_polygon_shape.h"

physics_body newGroundBody(physics_context ctx, b2Vec2 intl_pos, b2Vec2 intl_size)
{
	physics_body body;

	b2BodyDef def;
	def.position.Set(intl_pos.x, intl_pos.y);

	body.body = ctx.world->CreateBody(&def);

	body.box = new b2PolygonShape;
	body.box->SetAsBox(intl_size.x, intl_size.y);

	body.body->CreateFixture(body.box, 0.0f);

	return body;
}

physics_body newDynamicBody(physics_context ctx, b2Vec2 intl_pos, b2Vec2 intl_size)
{
	physics_body body;
	
	b2BodyDef def;
	def.type = b2_dynamicBody;
	def.position.Set(intl_pos.x, intl_pos.y);

	body.body = ctx.world->CreateBody(&def);

	body.box = new b2PolygonShape;
	body.box->SetAsBox(intl_size.x, intl_size.y);

	b2FixtureDef fix;
	fix.shape = body.box;
	fix.density = 1.0f;
	fix.friction = 1.0f;

	body.body->CreateFixture(&fix);

	body.body->SetSleepingAllowed(false);

	return body;
}

float physicsBody_GetPositionX(physics_body body)
{
	return body.body->GetPosition().x;
}

float physicsBody_GetPositionY(physics_body body)
{
	return body.body->GetPosition().y;
}

float physicsBody_GetVelocityX(physics_body body)
{
	return body.body->GetLinearVelocity().x;
}

float physicsBody_GetVelocityY(physics_body body)
{
	return body.body->GetLinearVelocity().y;
}

void physicsBody_SetPosition(physics_body body, b2Vec2 pos)
{
	body.body->SetTransform(pos, 0.0f);
}

void physicsBody_ApplyImpulse(physics_body body, b2Vec2 impulse)
{
	body.body->ApplyLinearImpulseToCenter(impulse, false);
}