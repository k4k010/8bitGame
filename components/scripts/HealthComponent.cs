using Godot;
using System;

public partial class HealthComponent : Node
{
	[Export]
	private float _maxHealth;
	private float _health;

	[Export]
	public TextureProgressBar healthBar;
	
	[Signal]
	public delegate void DeathEventHandler();
	
	public override void _Ready()
	{	
		_health = _maxHealth;
		healthBar.Value = _health;
		Death += OnDeath;
	}

	public void TakeDamage(float amount)
	{
		_health -= amount;
		healthBar.Value = _health;
		GD.Print($"Current health: {_health}");
		if (_health <= 0)
		{
			EmitSignal(SignalName.Death);
		}
	}
	
	private void OnDeath()
	{
		if( GetParent() is Player player)
		{
			GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);	
		}
		else
		{
			GetParent().QueueFree();
		}
	}
}
