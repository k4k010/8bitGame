using Godot;
using System;

public partial class HitBox : Area2D
{
	[Export]
	public int damage = 25;
	public CollisionShape2D collisionShape;
	
	public override void _Ready()
	{
		collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
	} 
	
	private void _init()
	{
		this.CollisionMask = 0;
		this.CollisionLayer = 2;
	}
	
	//public void SetDisabled()
	//{
		//collisionShape.SetDeferred("disabled", true);
	//}
}
