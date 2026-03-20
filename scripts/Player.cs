using Godot;
using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{
	public enum States {IdleState, RunState, JumpState, FallState}
	public const float Speed = 300.0f;
	public const float JumpVelocity = -320.0f;
	public const float Gravity = 1000.0f;
	public States State = States.IdleState;
	private AnimatedSprite2D _animatedSprite;
	
	public override void _Ready(){
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");	
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
			_animatedSprite.FlipH = true;
		}
		else if (direction.X > 0)
		{
			_animatedSprite.FlipH = false;
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
					if (Input.IsActionJustReleased("Jump") && !IsOnFloor())
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
						velocity.Y += (float)(Gravity*1.5) * (float)delta;
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
