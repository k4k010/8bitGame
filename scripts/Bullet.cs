using Godot;
using System;

public partial class Bullet : CharacterBody2D
{
	public const float SPEED = 300.0f;
	public float vel;
	public const float lifetime= 2.5f; 
	
	public Timer _bulletTimer;
	
	public override void _Ready()
	{
		_bulletTimer = GetNode<Timer>("BulletTimer");
		_bulletTimer.Timeout += OnTimerTimeout;
		_bulletTimer.Start(lifetime);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		MoveLocalX(vel * SPEED * (float)delta);
	}
	
	public void OnTimerTimeout()
	{
		QueueFree();
	}
}
