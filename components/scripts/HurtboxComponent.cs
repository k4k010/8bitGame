using Godot;
using System;

public partial class HurtboxComponent : Area2D
{
	[Export]
	public HealthComponent healthComponent;
	
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered;
	}
	
	public void OnAreaEntered(Area2D area)
	{
		if (area is HitboxComponent hitbox)
		{
			healthComponent.TakeDamage(hitbox.damage);
		}
	}
}
