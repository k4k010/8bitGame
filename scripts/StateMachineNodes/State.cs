using Godot;
using System;

public partial class State : Node
{
	[Export]
	public string animationName;
	[Export]
	public float moveSpeed = 160.0f;
	public Player parent;

	public virtual void enter()
	{
		parent.animatedSprite.Play(animationName);
	}

	public virtual void exit()
	{
		   
	}

	public virtual State processInput(InputEvent @event)
	{
		return null;
	}

	public virtual State processPhysics(double delta)
	{
		return null;
	}

	public virtual State processFrame(double delta)
	{
		return null;
	}
}
