using Godot;
using System;

public class Colors : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";



	[Export]
	public Color[] ColorArray = { new Color(.39f, .68f, .21f), new Color(1, 1, .32f), new Color(.98f, .6f, .07f), new Color(1, .15f, .07f), new Color(.51f, 0, .65f), new Color(.07f, .27f, .98f) };

	[Export]
	Godot.Collections.Array<Godot.Collections.Array<Color>> ColorCollection = new Godot.Collections.Array<Godot.Collections.Array<Color>>();

	//[Export]
	//Godot.Collections.Array<Godot.Collections.Dictionary<string,Color>> ColorCollectionArray = new Godot.Collections.Array<Godot.Collections.Dictionary<string, Color>>();

	[Export]
	private float ColorCooldown = 60;
	private float Cooldown = 0;
	private int currentColorIndex = 0;
	private int currentColorWheel = 0;

	private static Random rand = new Random();

		/*
	export(Color) var playerColor
export var color_array = [Color(.39, .68, .21), Color(1, 1, .32), Color(.98, .6, .07), Color(1, .15, .07), Color(.51, 0, .65), Color(.07, .27, .98)]
	export var color_array2 = [Color(1, 1, 1)]
	export var color_array3 = [Color(1, 1, 1)]
	export var color_array4 = [Color(1, 1, 1)]
	export var color_array5 = [Color(1, 1, 1)]
	export var color_array6 = [Color(1, 1, 1)]
	export var colorWheel = [[Color(.39, .68, .21), Color(1, 1, .32), Color(.98, .6, .07), Color(1, .15, .07), Color(.51, 0, .65), Color(.07, .27, .98)],[Color(1, 1, 1)]]
export var changeSpeed := int (100)
*/

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public Color GetCurrentColor()
	{
		return ColorCollection[currentColorWheel][currentColorIndex];
	}

	public Color GetRandomColor()
	{
		return ColorCollection[currentColorWheel][rand.Next(ColorArray.Length -1)];
	}

	public Color GetOffsetColor(int offset)
	{
		int colorPosition  = offset%currentColorIndex;
		return ColorCollection[currentColorWheel][colorPosition];
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (Input.IsActionJustReleased("COLOR"))
		{
			currentColorIndex = 0;
			currentColorWheel++;
			if (currentColorWheel >= ColorCollection.Count) currentColorWheel = 0;
			
			//if (currentColorIndex >= ColorCollection[currentColorWheel].Count) currentColorIndex = 0;
		}

		if (Cooldown <= 0)
		{
			currentColorIndex += 1;
			if (currentColorIndex >= ColorCollection[currentColorWheel].Count) currentColorIndex = 0;
			Cooldown = ColorCooldown / 1000f;
		}
		Cooldown -= delta;
	}
}
