using Godot;
using System;

public partial class RunState : State
{
	[Export]
	public State FallState;
	[Export]
	public State IdleState;
	[Export]
	public State JumpState;

	public override void enter()
	{
		base.enter();
		parent.velocity.Y = 0;
	}

	public override State processInput(InputEvent @event)
	{
		if (Input.IsActionJustPressed("Jump") && parent.IsOnFloor())
		{
			return JumpState;
		}
		return null;
	}

	public override State processPhysics(double delta)
	{	
		if (!parent.IsOnFloor())
		{
			return FallState;
		}
		
		float direction = Input.GetAxis("Left", "Right");

		if (parent.IsOnFloor()){
			if (direction != 0)
			{
				parent.animatedSprite.SetFlipH(parent.shootDirection < 0);
				parent.velocity.X = direction * moveSpeed;
			}
			if (direction == 0)
			{
				parent.velocity.X = Mathf.MoveToward(parent.velocity.X, 0, moveSpeed/2 * (float)delta);
				return IdleState;
			}
		}
	
		parent.Velocity = parent.velocity;
		parent.MoveAndSlide();
		return null;
	}
	
}
