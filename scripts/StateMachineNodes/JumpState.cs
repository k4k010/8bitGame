using Godot;
using System;

public partial class JumpState : State
{
	[Export]
	public State IdleState;
	[Export]
	public State RunState;
	[Export]
	public State FallState;
	
	[Export]
	public float jumpForce = 200.0f;
	
	public override void enter()
	{
		base.enter();
		parent.velocity.Y = -jumpForce;
	}
	
	public override State processInput(InputEvent @event)
	{
		if (Input.IsActionJustReleased("Jump")){
			parent.velocity.Y = Math.Max(parent.velocity.Y, -jumpForce/4);
		}
		return null;
	}
	
	public override State processPhysics(double delta)
	{
		parent.velocity.Y += parent.Gravity * (float)delta;
		
		float direction = Input.GetAxis("Left", "Right");
		
		parent.animatedSprite.SetFlipH(parent.shootDirection < 0);
		parent.velocity.X = direction * moveSpeed/1.5f;
		parent.Velocity = parent.velocity;
		parent.MoveAndSlide();
		
		if (parent.velocity.Y >= 0)
		{
			return FallState;
		}
		
		if (parent.IsOnFloor())
		{
			if (direction != 0)
			{
				return RunState;
			}
			return IdleState;
		}
		return null;
	}
}
