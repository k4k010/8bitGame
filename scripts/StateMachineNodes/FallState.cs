using Godot;
using System;

public partial class FallState : State
{
	[Export]
	public State IdleState;
	[Export]
	public State RunState;
	[Export]
	public State JumpState;
	
	[Export]
	public float fallMultiplier = 3.0f;
	
	public override void enter(){
		base.enter();
	}
	
	public override State processInput(InputEvent @event)
	{
		return null;
	}
	
	public override State processPhysics(double delta)
	{
		parent.velocity.Y += fallMultiplier * parent.Gravity * (float)delta;
		
		float direction = Input.GetAxis("Left", "Right");
		
		parent.animatedSprite.SetFlipH(parent.shootDirection < 0);
		parent.velocity.X = direction * moveSpeed/1.5f;
		parent.Velocity = parent.velocity;
		parent.MoveAndSlide();
		
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
