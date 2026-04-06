using Godot;
using System;

public partial class GunArm : Node2D
{
	public float yScale;
	public Marker2D bulletSpawnPos;
	public AudioStreamPlayer2D bulletSound;
	
	private readonly PackedScene _bulletScene = ResourceLoader.Load<PackedScene>("res://scenes/8bit_bullet.tscn");
	
	public override void _Ready()
	{
		bulletSpawnPos = GetNode<Marker2D>("BulletSpawnPos");
		bulletSound = GetNode<AudioStreamPlayer2D>("BulletSound");
	}
	
	public override void _Process(double delta)
	{
		LookAt(GetGlobalMousePosition());
		RotationDegrees = Mathf.Wrap(RotationDegrees, 0, 360);
		
		yScale = (RotationDegrees > 90 && RotationDegrees < 270) ? -1 : 1;
		Scale = new Vector2(1.0f, yScale);
		
		if (Input.IsActionJustPressed("Shoot"))
		{
			var bullet = (Bullet)_bulletScene.Instantiate();
			bullet.GlobalPosition = bulletSpawnPos.GlobalPosition;
			bullet.direction = GlobalPosition.DirectionTo(GetGlobalMousePosition());
			GetParent().AddChild(bullet);
			bulletSound.Play();
		}
		//GD.Print(Scale);
	}
}
