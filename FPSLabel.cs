using Godot;
using System;

public class FPSLabel : Label
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
		Text = Engine.GetFramesPerSecond() + " : " + GetParent().GetChild(1).GetChildCount();      
		Modulate = ((Colors)(GetParent().GetChild(1).GetChild(0))).GetCurrentColor();
  }
}
