using Godot;
using System;

public class DiamondSpread : Particles2D
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
			if (OS.GetScancodeString(eventKey.Scancode) == "W" || OS.GetScancodeString(eventKey.Scancode) == "w")
			{
				Particles2D diamond = (Particles2D)Duplicate();
				Vector2 ScreenCenter = new Vector2(GetParent().GetViewport().Size / 2);
				diamond.Position = ScreenCenter;
				diamond.Lifetime = Lifetime;
				GetParent().AddChild(diamond);
				diamond.OneShot = true;
				diamond.Emitting = true;
				diamond.Visible = true;
			}
			*/

			if (OS.GetScancodeString(eventKey.Scancode) == "Home")
			{
				Lifetime += 0.1f;
			}
			if (OS.GetScancodeString(eventKey.Scancode) == "End")
			{
				if (Lifetime > 0) Lifetime -= 0.1f;
			}
		}
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Visible == false && cooldown<=0 && Input.IsActionPressed("DIAMOND") && GetParent().GetChildCount() < 1000)
		{
			Particles2D diamond = (Particles2D)Duplicate();
			Vector2 ScreenCenter = new Vector2(GetParent().GetViewport().Size / 2);
			diamond.Position = ScreenCenter;
			diamond.Lifetime = Lifetime/2;
			((ParticlesMaterial)diamond.ProcessMaterial).EmissionBoxExtents = new Vector3(GetParent().GetViewport().Size.x*20, GetParent().GetViewport().Size.y*20, 0);
			GetParent().AddChild(diamond);
			diamond.OneShot = true;
			diamond.Emitting = true;
			diamond.Visible = true;
			cooldown = cooldownMax/1000.0f;
		}
		if(cooldown>0) cooldown-=delta;
		if (Visible == false) return; 
		if (lifeTime < 0) Free();
		lifeTime -= delta;
	}
}
