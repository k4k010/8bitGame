using Godot;
using System;

public partial class HurtboxComponent : Area2D
{
	public HealthComponent healthComponent;
	
	public override void _Ready()
	{
		healthComponent = GetNode<HealthComponent>("../HealthComponent");
		AreaEntered += OnAreaEntered;
	}
	
	public void OnAreaEntered(Area2D area)
	{
		GD.Print($"Something entered the hurtbox: {area.Name}");
		if (area is HitboxComponent hitbox)
		{
			GD.Print("It was a hitbox! Taking damage...");
			healthComponent.TakeDamage(hitbox.damage);
		}
	}
}
