using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export]
	public Sprite2D sprite;
	[Export]
	public CollisionShape2D collisionShape;
}
