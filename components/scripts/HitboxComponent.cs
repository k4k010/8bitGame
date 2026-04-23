using Godot;
using System;

public partial class HitboxComponent : Area2D
{
	[Export]
	public int damage = 25;
	[Export]
	public CollisionShape2D collisionShape;
	
	public override void _Ready()
	{
	} 
	
	public HitboxComponent()
	{
		this.CollisionMask = 0;
		this.CollisionLayer = 2;
	}
}
