using Godot;
using System;

public class Pixels2 : Particles2D
{
// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private float lifeTime;
	[Export]
	public float cooldownMax = 8;
	[Export]
	public int rotationAnglePerSec = 18;
	private float cooldown = 0;
	
	public float rotationVal = 0;
	
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
			if (OS.GetScancodeString(eventKey.Scancode) == "Enter")
			{
				Particles2D pixels = (Particles2D)Duplicate();
				Vector2 ScreenCenter = new Vector2(GetParent().GetViewport().Size / 2);
				pixels.Position = ScreenCenter;
				pixels.Lifetime = Lifetime;
				GetParent().AddChild(pixels);
				pixels.OneShot = true;
				pixels.Emitting = true;
				pixels.Visible = true;
				Console.WriteLine(">>>>>" + GetChildCount());
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
		if (Visible == true && Input.IsActionJustPressed("ESCAPE"))
		{
			Free();
			return;
		}
		
			if (Visible == false && Input.IsActionJustReleased("LASER") && GetParent().GetChildCount() < 1000)
			{
				cooldown = 0;
				rotationVal = 0;
			}
			if (Visible == false && cooldown <=0 && Input.IsActionPressed("LASER") && GetParent().GetChildCount() < 1000)
			{
				Particles2D pixels = new Particles2D();
				pixels = (Particles2D)Duplicate();
				Vector2 ScreenCenter = new Vector2(GetParent().GetViewport().Size / 2);
				int rotateAngle = (GetParent().GetChildCount()/1000 * 360);
				pixels.Rotate(rotationVal * (3.412f / 180.0f));
				pixels.Position = ScreenCenter;
				pixels.Lifetime = Lifetime+2;
				GetParent().AddChild(pixels);
				pixels.OneShot = true;
				pixels.Emitting = true;
				pixels.Visible = true;
			pixels.ProcessMaterial = (Material)ProcessMaterial.Duplicate(true);
			//((ParticlesMaterial)pixels.ProcessMaterial).Color = ((Colors)(GetParent().GetParent().GetChild(0))).GetCurrentColor();
			((ShaderMaterial)pixels.ProcessMaterial).SetShaderParam("color_value", ((Colors)(GetParent().GetParent().GetChild(0))).GetCurrentColor());
			((ShaderMaterial)pixels.ProcessMaterial).SetShaderParam("totalParticles", Amount);
			cooldown = cooldownMax/1000.0f;
			rotationVal += delta * rotationAnglePerSec;
			}
		if(cooldown>0) cooldown -= delta;
		
		if (Visible == false) return; 
		if (lifeTime < 0) Free();
		lifeTime -= delta;
	}
}
