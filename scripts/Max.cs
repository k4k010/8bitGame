using Godot;
using System;

public partial class Max : CharacterBody2D
{
	private Marker2D _bulletSpawnPos;
	private readonly PackedScene _bulletScene = ResourceLoader.Load<PackedScene>("res://scenes/8bit_bullet.tscn");
	private Timer _shootTimer;
	private bool shooting = false;

	public override void _Ready()
	{
		_bulletSpawnPos = GetNode<Marker2D>("ShootPos");
		_shootTimer = GetNode<Timer>("ShootCooldown");
		_shootTimer.Timeout += OnShootTimeout;
		_shootTimer.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		if(shooting == true)
		{
			var bullet = (Bullet)_bulletScene.Instantiate();
			bullet.GlobalPosition = _bulletSpawnPos.GlobalPosition;
			bullet.direction = new Vector2(-1.0f, 0.0f);
			GetParent().AddChild(bullet);
			shooting = false;
		}
	}

	private void OnShootTimeout()
	{
		shooting = true;
	}
}
