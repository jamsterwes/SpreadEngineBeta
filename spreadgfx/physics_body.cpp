#include "physics_body.hpp"
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