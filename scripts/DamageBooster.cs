using Godot;
using System;

public partial class DamageBooster : RigidBody2D
{
	public float direction;
	private float _initialVelocityX = 300.0f;
	private float _initialVelocityY = -600.0f;
	
	public Marker2D bulletSpawnPos;
	private readonly PackedScene _dmgBooster = ResourceLoader.Load<PackedScene>("res://scenes/8bit_damage_booster.tscn");
	
	public override void _Ready()
	{
		SetAsTopLevel(true);
		AngularVelocity = 0.0f;
		LinearVelocity = new Vector2(direction * _initialVelocityX, _initialVelocityY);
	}
}
