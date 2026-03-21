using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	public const float SPEED = 300.0f;
	public float vel; 
	
	public override void _PhysicsProcess(double delta)
	{
		MoveLocalX(vel * SPEED * (float)delta);
	}
}
