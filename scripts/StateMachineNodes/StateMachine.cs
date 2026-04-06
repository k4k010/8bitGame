using Godot;
using System;

public partial class StateMachine : Node
{

	[Export]
	public State startingState;
	public State currentState;

	public virtual void init(Player parent)
	{
		foreach(State child in GetChildren())
		{
			child.parent = parent;
		}

		changeState(startingState);
	}

	public void changeState(State newState)
	{
		if (currentState != null){
			currentState.exit();
		}

		currentState = newState;
		currentState.enter();
	}

	public virtual void processInput(InputEvent @event)
	{
		State newState = currentState.processInput(@event);
		if (newState != null)
		{
			changeState(newState);
		}
	}

	public virtual void processPhysics(double delta)
	{
		State newState = currentState.processPhysics(delta);
		if (newState != null)
		{
			changeState(newState);
		}
	}

	public virtual void processFrame(double delta)
	{
		State newState = currentState.processFrame(delta);
		if (newState != null)
		{
			changeState(newState);
		}   
	}
}
