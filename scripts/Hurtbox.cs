using Godot;
using System;

public partial class Hurtbox : Area2D
{
	public HealthComponent healthComponent;
	
	public override void _Ready()
	{
		healthComponent = GetNode<HealthComponent>("../HealthComponent");
		AreaEntered += OnAreaEntered;
	}
	
	public void OnAreaEntered(Area2D area)
	{
		if (area is HitBox hitbox)
		{
			healthComponent.TakeDamage(hitbox.damage);
		}
	}
}
