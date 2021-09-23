using Godot;
using System;

public class WorldEnvironment : Godot.WorldEnvironment
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("BLOOM"))
		{
			Environment.GlowIntensity += 0.5f;
			if (Environment.GlowIntensity >= 8) { Environment.GlowIntensity = 0; }
		}
	}
}
