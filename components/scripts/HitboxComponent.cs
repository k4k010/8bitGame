using Godot;
using System;

public partial class HitboxComponent : Area2D
{
	[Export]
	public int damage = 25;
	public CollisionShape2D collisionShape;
	
	public override void _Ready()
	{
		collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
	} 
	
	public HitboxComponent()
	{
		this.CollisionMask = 0;
		this.CollisionLayer = 2;
	}
}
