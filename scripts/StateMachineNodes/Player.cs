using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public AnimatedSprite2D animatedSprite;
	public StateMachine stateMachine;
	public Node2D gunArm;
	
	private float _maxHealth;
	public float maxHealth{
		get{ return _maxHealth;}
		set
		{
			_maxHealth = value;
		}
	}
	private float _health;
	public float health{
		get{ return _health;}
		set
		{
			_health = value;
		}
	}
	
	public float shootDirection;
	public Vector2 velocity;
	[Export]
	public float Gravity = 600.0f;

	public override void _Ready()
	{
		maxHealth = 100;
		health = maxHealth;
		
		animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		gunArm = GetNode<Node2D>("GunArm");
		stateMachine = GetNode<StateMachine>("StateMachine");
		stateMachine.init(this);
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		stateMachine.processInput(@event);
		//GD.Print(@event.ToString());
	}

	public override void _PhysicsProcess(double delta)
	{
		stateMachine.processPhysics(delta);
	}

	public override void _Process(double delta)
	{
		stateMachine.processFrame(delta);
		
		if (gunArm != null)
		{
			shootDirection = (gunArm.RotationDegrees > 90 && gunArm.RotationDegrees < 270) ? -1.0f : 1.0f;
			//GD.Print(shootDirection);
		}
	}
}
