using Godot;
using System;

public partial class IdleState : State
{
	[Export]
	public State FallState;
	[Export]
	public State RunState;
	[Export]
	public State JumpState;

	public override void enter()
	{
		base.enter();
		parent.velocity.X = 0;
		parent.velocity.Y = 0;
	}

	public override State processInput(InputEvent @event)
	{
		if (Input.IsActionPressed("Jump") && parent.IsOnFloor()){
			return JumpState;
		}
		if (Input.IsActionPressed("Left") || Input.IsActionPressed("Right") && parent.IsOnFloor()){
			return RunState;
		}
		return null;
	}

	public override State processPhysics(double delta)
	{
		parent.animatedSprite.SetFlipH(parent.shootDirection < 0);
		
		if (!parent.IsOnFloor())
		{
			return FallState;
		}
		parent.Velocity = parent.velocity;
		parent.MoveAndSlide();
		return null;
	}
}
