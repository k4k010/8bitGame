using Godot;
using System;

public partial class HealthComponent : Node
{
	private float _maxHealth = 100;
	private float _health;
	
	public TextureProgressBar healthBar;
	
	[Signal]
	public delegate void DeathEventHandler();
	
	public override void _Ready()
	{
		healthBar = GetNode<TextureProgressBar>("../../CanvasLayer/Healthbar");
		
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
		GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
	}
}
