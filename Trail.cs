using Godot;
using System;

public class Trail : Node2D
{

	[Export]
	public int MaxPoints = 360;
	public int MaxPoints2 = 396;

	
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//GlobalPosition = new Vector2(0, 0);
		//GlobalRotation = 0;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
		
		GlobalPosition = new Vector2(0, 0);
		GlobalRotation = 0;



		Vector2 ParentPos = ((Player)GetParent()).Position;
		AddTrail(ParentPos, (Line2D)GetChild(0));
		AddTrail(ParentPos, (Line2D)GetChild(1),true);
		
		//Color color = ((Player)(GetParent().GetParent().GetChild(0))).currentColor;
		Color color = ((Player)(GetParent())).offsetColor;
		((Line2D)GetChild(0)).Modulate = color;
		((Line2D)GetChild(1)).Modulate = color;


	}

  public void AddTrail(Vector2 pos, Line2D trail, bool extend = false)
	{
		int maxPoints = extend? MaxPoints2 : MaxPoints;
		if(trail.GetPointCount() > maxPoints)
		{
			trail.RemovePoint(0);
		}
		trail.AddPoint(pos);
	}
}
