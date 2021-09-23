using Godot;
using System;

public class ParticlesMain : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	//private float Lifetime = 1;

	//public override void _UnhandledInput(InputEvent @event)
	//{
	//	if (@event is InputEventKey eventKey)
	//	{
	//		if (OS.GetScancodeString(eventKey.Scancode) == "Space")
	//		{
	//			Particles2D diamond = (Particles2D)GetChild(1).Duplicate();
	//			Vector2 ScreenCenter = new Vector2(GetViewportRect().Size / 2);
	//			diamond.Position = ScreenCenter;
	//			diamond.Lifetime = Lifetime;
	//			AddChild(diamond);
	//			diamond.OneShot = true;
	//			diamond.Emitting = true;
	//			diamond.Visible = true;
	//			//OneShot = false;
	//			//OneShot = true;
	//			//Emitting = false;
	//			//Emitting = true;
	//			Console.WriteLine(">>>>>" + GetChildCount());
	//		}



	//		if (OS.GetScancodeString(eventKey.Scancode) == "Home")
	//		{

	//			Lifetime += 0.1f;
	//			Console.WriteLine("]" + Lifetime);
	//		}
	//		if (OS.GetScancodeString(eventKey.Scancode) == "End")
	//		{
	//			if (Lifetime > 0) Lifetime -= 0.1f;
	//			Console.WriteLine("[" + Lifetime);
	//		}
	//	}
	//}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}
	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("FULLSCREEN"))
		{
			OS.WindowFullscreen = !OS.WindowFullscreen;
		}
	}
}
