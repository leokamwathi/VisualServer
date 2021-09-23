using Godot;
using System;

public class Triangle : Particles2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private float lifeTime;
	[Export]
	public float cooldownMax = 8;
	private float cooldown = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lifeTime = Lifetime;
		Vector2 ScreenCenter = new Vector2(GetParent().GetViewport().Size / 2);
		Position = ScreenCenter;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
			if (Visible == true) return;
		if (@event is InputEventKey eventKey)
		{
			/*
			if (OS.GetScancodeString(eventKey.Scancode) == "Q" || OS.GetScancodeString(eventKey.Scancode) == "q")
			{
				Particles2D heart = (Particles2D)Duplicate();
				Vector2 ScreenCenter = new Vector2(GetParent().GetViewport().Size / 2);
				heart.Position = ScreenCenter;
				heart.Lifetime = Lifetime;
				GetParent().AddChild(heart);
				heart.OneShot = true;
				heart.Emitting = true;
				heart.Visible = true;
			}
			*/
			if (OS.GetScancodeString(eventKey.Scancode) == "Home")
			{

				Lifetime += 0.1f;
				Console.WriteLine("]" + Lifetime);
			}
			if (OS.GetScancodeString(eventKey.Scancode) == "End")
			{
				if (Lifetime > 0) Lifetime -= 0.1f;
				Console.WriteLine("[" + Lifetime);
			}
		}
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Visible == false && cooldown ==0 && Input.IsActionPressed("TRIANGLE") && GetParent().GetChildCount() < 1000)
		{
			Particles2D heart = (Particles2D)Duplicate();
			Vector2 ScreenCenter = new Vector2(GetParent().GetViewport().Size / 2);
			heart.Position = ScreenCenter;
			heart.Lifetime = Lifetime;
			GetParent().AddChild(heart);
			heart.OneShot = true;
			heart.Emitting = true;
			heart.Visible = true;
			cooldown = cooldownMax;
		}
		if(cooldown>0) cooldown--;
		if (Visible == false) return; 
		if (lifeTime < 0) Free();
		lifeTime -= delta;
	}
}
