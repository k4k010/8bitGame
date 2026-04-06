using Godot;
using System;

public partial class Bullet : Node2D
{
	[Export]
	public float SPEED = 300.0f;
	public Vector2 direction = new Vector2(0.0f, 0.0f);
	[Export]
	public float lifetime = 2.5f;
	[Export]
	public float damage = 20.0f;
	
	public Timer bulletTimer;
	public Area2D impactDetector;
	//public Area2D hitBox;
	
	public override void _Ready()
	{
		SetAsTopLevel(true);
		LookAt(Position + direction);
		
		bulletTimer = GetNode<Timer>("BulletTimer");
		impactDetector = GetNode<Area2D>("ImpactDetector");
		//hitBox = GetNode<Area2D>("HitBox");
		
		//hitBox.AreaEntered += OnHitBoxEntered;
		impactDetector.AreaEntered += OnImpactDetectorEntered;
		bulletTimer.Timeout += () => QueueFree();
		bulletTimer.Start(lifetime);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Position += direction * SPEED * (float)delta;
	}
	
	public void OnImpactDetectorEntered(Area2D area) 
	{
		QueueFree();
	}
	
	//public void OnHitBoxEntered(Area2D area)
	//{
		//if (area is Area2D Hurtbox)
		//{
			//var healthComponent = GetNode<HealthComponent>("HealthComponent");
			//healthComponent.TakeDamage(damage);
		//}
	//}
	
	//public void OnTimerTimeout()
	//{
		//QueueFree();
	//}
}
