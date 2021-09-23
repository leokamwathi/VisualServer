using Godot;
using System;

public class ShapeNode : Node
{
	[Export]
   	public string KeyBind = "PIXELS";
	[Export]
	public int CooldownMax = 20;
	[Export]
	public Texture MainTexture;
	[Export]
	public Texture FillTexture;
	[Export]
	public Texture SpreadTexture;

	[Export]
	public float ScaleBy = 1;	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}



//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
