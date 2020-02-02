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
	body.box->SetAsBox(intl_size.x * 0.5f, intl_size.y * 0.5f);

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
	body.box->SetAsBox(intl_size.x * 0.5f, intl_size.y * 0.5f);

	b2FixtureDef fix;
	fix.shape = body.box;
	fix.density = 1.0f;
	fix.friction = 1.0f;

	body.body->CreateFixture(&fix);

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