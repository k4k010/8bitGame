using Godot;
using System;

public partial class Killzone : Area2D
{
	private Timer _timer;
	
	public override void _Ready()
	{
		base._Ready();
		_timer = GetNode<Timer>("DeathCooldown");
		BodyEntered += OnBodyEntered;
		_timer.Timeout += OnTimerTimeout;
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if(body is Player player)
		{
			GD.Print("You Died!");
			_timer.Start();
		}
	}
	
	public void OnTimerTimeout(){
		GetTree().ReloadCurrentScene();
	}
}
