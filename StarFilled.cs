using Godot;
using System;

public class StarFilled : Particles2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private float lifeTime;
	public float cooldownMax = 20;
	private float cooldown = 0;
	private string KeyBind = "";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lifeTime = Lifetime;
		Vector2 ScreenCenter = new Vector2(GetParent().GetViewport().Size / 2);
		Position = ScreenCenter;
		KeyBind = ((ShapeNode)GetParent()).KeyBind;
		cooldownMax = ((ShapeNode)GetParent()).CooldownMax;
		Texture = ((ShapeNode)GetParent()).FillTexture;
		Scale = Scale * ((ShapeNode)GetParent()).ScaleBy;
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
		if (Visible == true && Input.IsActionJustPressed("ESCAPE"))
		{
			Free();
			return;
		}
		
		if (Visible == false && cooldown <=0 && Input.IsActionPressed(KeyBind) && GetParent().GetChildCount() < 1000)
		{
			Particles2D heart = (Particles2D)Duplicate();
			Vector2 ScreenCenter = new Vector2(GetParent().GetViewport().Size / 2);
			heart.Position = ScreenCenter;
			heart.Lifetime = 0.25f;
			GetParent().AddChild(heart);
			heart.OneShot = true;
			heart.Emitting = true;
			heart.Visible = true;
			heart.ProcessMaterial = (Material)ProcessMaterial.Duplicate(true);
			((ParticlesMaterial)heart.ProcessMaterial).Color = ((Colors)(GetParent().GetParent().GetChild(0))).GetCurrentColor();
			cooldown = cooldownMax/1000.0f;
		}
		if(cooldown>0) cooldown-= delta;
		if (Visible == false) return; 
		if (lifeTime < 0) Free();
		lifeTime -= delta;
	}
}
