using Godot;
using System;

public partial class GunArm : Node2D
{
	public float yScale;
	public Marker2D bulletSpawnPos;
	public AudioStreamPlayer2D bulletSound;
	public AnimatedSprite2D gunAnimatedSprite;
	public AnimatedSprite2D bulletSpawnAnimatedSprite;
	public Player player;
	
	private readonly PackedScene _bulletScene = ResourceLoader.Load<PackedScene>("res://scenes/8bit_bullet.tscn");
	private readonly PackedScene _dmgBooster = ResourceLoader.Load<PackedScene>("res://scenes/8bit_damage_booster.tscn");
	
	public override void _Ready()
	{
		player = GetNode<Player>("..");
		bulletSpawnPos = GetNode<Marker2D>("BulletSpawnPos");
		bulletSound = GetNode<AudioStreamPlayer2D>("BulletSound");
		gunAnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		bulletSpawnAnimatedSprite = GetNode<AnimatedSprite2D>("BulletSpawnPos/AnimatedSprite2D");
		
		gunAnimatedSprite.AnimationFinished += OnAnimationFinished;
	}
	
	public override void _Process(double delta)
	{
		LookAt(GetGlobalMousePosition());
		RotationDegrees = Mathf.Wrap(RotationDegrees, 0, 360);
		
		yScale = (RotationDegrees > 90 && RotationDegrees < 270) ? -1 : 1;
		Scale = new Vector2(1.0f, yScale);
		
		if (Input.IsActionJustPressed("Shoot"))
		{
			Shoot();
		}
		
		if (Input.IsActionJustPressed("Ultimate"))
		{
			Ultimate();
		}
	}
	
	private void OnAnimationFinished()
	{
		if (gunAnimatedSprite.Animation == "Shoot")
		{
			gunAnimatedSprite.Play("Idle");
		}
	}
	
	private void Shoot()
	{
		gunAnimatedSprite.Play("Shoot");
		bulletSpawnAnimatedSprite.Play("default");
		var bullet = (Bullet)_bulletScene.Instantiate();
		bullet.GlobalPosition = bulletSpawnPos.GlobalPosition;
		bullet.direction = GlobalPosition.DirectionTo(GetGlobalMousePosition());
		GetParent().AddChild(bullet);
		bulletSound.Play();
	}
	
	private void Ultimate()
	{
		var dmgBooster = (DamageBooster)_dmgBooster.Instantiate();
		dmgBooster.GlobalPosition = bulletSpawnPos.GlobalPosition;
		dmgBooster.direction = player.shootDirection;
		GetParent().AddChild(dmgBooster);
	} 
}
