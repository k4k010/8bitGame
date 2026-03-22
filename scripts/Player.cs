using Godot;
using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{
	public enum States {IdleState, RunState, JumpState, FallState}
	public const float Speed = 150.0f;
	public const float JumpVelocity = -320.0f;
	public const float Gravity = 1000.0f;
	public States State = States.IdleState;
	
	private Vector2 tempFlipScale = new Vector2(1.0f, 0.0f);
	private Node2D _flipNode;
	private AnimatedSprite2D _animatedSprite;
	private CharacterBody2D _bullet;
	private Marker2D _bulletSpawnPos;
	private readonly PackedScene _bulletScene = ResourceLoader.Load<PackedScene>("res://scenes/8bit_bullet.tscn");
	
	public override void _Ready(){
		_flipNode = GetNode<Node2D>("Flip");
		_animatedSprite = GetNode<AnimatedSprite2D>("Flip/AnimatedSprite2D");
		_bulletSpawnPos = GetNode<Marker2D>("Flip/BulletSpawnPos");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else if(direction == Vector2.Zero)
		{
			velocity.X = Mathf.MoveToward(velocity.X, 0, Speed/2);	
		}
		
		if(direction.X < 0)
		{
			tempFlipScale = _flipNode.Scale;
			tempFlipScale.X = -1.0f;
			_flipNode.Scale = tempFlipScale;
		}
		else if (direction.X > 0)
		{
			tempFlipScale = _flipNode.Scale;
			tempFlipScale.X = 1.0f;
			_flipNode.Scale = tempFlipScale;
		}
		if(Input.IsActionJustPressed("Shoot"))
		{
			GD.Print("Shooting");
			var bullet = (Bullet)_bulletScene.Instantiate();
			bullet.GlobalPosition = _bulletSpawnPos.GlobalPosition;
			bullet.vel = tempFlipScale.X;
			GetParent().AddChild(bullet);
		}
		if (!IsOnFloor())
		{
			velocity.Y += Gravity * (float)delta;
		}
					
		switch (State)
			{
				case States.IdleState:
				{
					_animatedSprite.Play("Idle");
					if (direction != Vector2.Zero && IsOnFloor())
					{
						State = States.RunState;
					}
					if (Input.IsActionJustPressed("Jump") && IsOnFloor())
					{
						velocity.Y = JumpVelocity;
						State = States.JumpState;
					}
				}
				break;
				case States.RunState:
				{
						_animatedSprite.Play("Run");
						if(direction == Vector2.Zero)
						{
							State = States.IdleState;
						}
						if (Input.IsActionJustPressed("Jump") && IsOnFloor())
						{
							velocity.Y = JumpVelocity;
							State = States.JumpState;
						}
				}
				break;
				case States.JumpState:
				{
					_animatedSprite.Play("Jump");
					velocity.X = direction.X * Speed;
					if (velocity.Y > 0 && !IsOnFloor())
					{
						velocity.Y = Math.Max(velocity.Y, JumpVelocity/4);
						State = States.FallState;
						GD.Print("Fall State");
					}
					if(IsOnFloor()){
						State = States.IdleState;
					}
				}
				break;
				case States.FallState:
				{
					if(velocity.Y > 0 && !IsOnFloor())
					{
						velocity.Y += (float)(Gravity) * (float)delta;
					}
					if (IsOnFloor())
					{
						State = States.IdleState;
					}
				}
					break;
			}
		Velocity = velocity;
		
		MoveAndSlide();
	}
}
