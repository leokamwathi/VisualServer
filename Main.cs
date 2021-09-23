using Godot;
using System;

public class Main : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private float Phi = (3.142f/180.0f);
	[Export]
	public Texture texture;
	RID ci_rid;
	public static Random rand = new Random();
	private int CoolDown = 0;

	public Texture normalMap;
	public Particles2D particles;
	public bool ShowParticles = false;


	//private Vector2[] Pixels = new Vector2[10000];
	Pixel[] Pixels = new Pixel[10000];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Create Canvas
		ci_rid = VisualServer.CanvasItemCreate();
		particles = (Particles2D)GetChild(1).Duplicate();
		//particles.Emitting = true;
		particles.Texture = texture;

		//Make this node the parent
		VisualServer.CanvasItemSetParent(ci_rid, GetCanvasItem());

		for (int x = 0; x < Pixels.Length; x++)
		{
			Vector2 position = new Vector2(rand.Next((int)GetViewportRect().Size.x), rand.Next((int)GetViewportRect().Size.y));
			Vector2 speed = new Vector2(rand.Next(10)>5?1:-1, rand.Next(10) > 5 ? 1 : -1);
			Vector2 bounding = GetViewportRect().Size;
			Vector2 size = new Vector2(rand.Next(10), rand.Next(10));
			Color color = new Color(rand.Next(255), rand.Next(255), rand.Next(255));
			Pixels[x] = new Pixel(position,speed,bounding,size,color,false,"DEACTIVATE");
		}



		// Draw a texture on it.
		// Remember, keep this reference.
		// texture = ResourceLoader.Load("res://icon.png") as Texture;
		//if (texture is null)
		//{
		//	Console.WriteLine("Texture is NULLLLLLLL");
		//}
		//else
		//{
		//	Console.WriteLine("Texture is NOOOOOOOOOOOOTTTTTTT  NULLLLLLLL+++" + texture.GetRid().ToString());
		//}
		// Add it, centered.
		//VisualServer.canvas_item_add_texture_rect(ci_rid, Rect2(texture.get_size() / 2, texture.get_size()), texture)
		//VisualServer.CanvasItemAddTextureRect(ci_rid, new Rect2(new Vector2(0,0), texture.GetSize()), texture.GetRid());
		//VisualServer.CanvasItemAddRect(ci_rid, new Rect2(new Vector2(0, 0), new Vector2(100,100)), new Color(255,0,0));

		//for (int x = 0; x < 10000; x++)
		//{
		//	VisualServer.CanvasItemAddRect(ci_rid, new Rect2(new Vector2(rand.Next((int)GetViewportRect().Size.x), rand.Next((int)GetViewportRect().Size.y)), new Vector2(rand.Next(10), rand.Next(10))), new Color(rand.Next(255), 0, 0));
		//}

		//Add the item, rotated 45 degrees and translated.
		//var xform = Transform2D().rotated(deg2rad(45)).translated(Vector2(20, 30))
		//var xform = (new Transform2D()).Rotated(45 * Phi).Translated(new Vector2(rand.Next((int)GetViewportRect().Size.x), rand.Next((int)GetViewportRect().Size.y)));
		//VisualServer.CanvasItemSetTransform(ci_rid, xform);

		//var xform = (new Transform2D()).Scaled(new Vector2(2, 2)); // .Rotated(45 * Phi); //.Translated(new Vector2(rand.Next((int)GetViewportRect().Size.x), rand.Next((int)GetViewportRect().Size.y)));
		//VisualServer.CanvasItemSetTransform(ci_rid, new Transform2D(3.1421f,new Vector2(0,0)));


		//Console.WriteLine("Exists = " + ResourceLoader.Exists("res://icon.png"));

		if(texture is null)	texture = ResourceLoader.Load("res://20x20_cyan.png") as Texture;

		//RID r = new RID(texture);


		//Console.WriteLine("Size X = " + texture.GetSize().x);
	   

		//VisualServer.CanvasItemAddTextureRect(ci_rid, new Rect2(new Vector2(100, 100), new Vector2(20, 20)), texture.GetRid(), false, new Color(1, 1, 1, 1), false, texture.GetRid());
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey eventKey)
		{
			ShowParticles = false;
			if (OS.GetScancodeString(eventKey.Scancode) == "Backspace")
			{
				ShowParticles = true;
				Console.WriteLine("Show Particles!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			}

			if (OS.GetScancodeString(eventKey.Scancode) == "Enterx")
			{
				for (int x = 0; x < Pixels.Length; x++)
				{
					Vector2 position = new Vector2(rand.Next((int)GetViewportRect().Size.x), rand.Next((int)GetViewportRect().Size.y));
					Pixels[x].Position = position; 
				}
			}

			if (OS.GetScancodeString(eventKey.Scancode) == "Enter")
			{
				ShowParticles = true;
				Console.WriteLine("Show Particles!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				/*
				if (CoolDown > 0) return;
				CoolDown = Engine.GetFramesPerSecond() > 45 ? 10 : (int)(60 - Engine.GetFramesPerSecond());
				*/
				/*
				float red = rand.Next(10)>5?(float)rand.Next(255) : 0;
				float green = rand.Next(10) > 5 ? (float)rand.Next(255) : 0;
				float blue = rand.Next(10) > 5 ? (float)rand.Next(255) : red+green == 0? (float)rand.Next(255) : 0;
				*/
				/*
				float red = 1;      //(float)(rand.Next(100) * 0.01);
				float green = 1;    //(float)(rand.Next(100) * 0.01);
				float blue = 1;     //(float)(rand.Next(100) * 0.01);

				Color color = new Color(red,green,blue);
				//Console.WriteLine("Color = (" + red + "," + green + "," + blue + ")");
				int count = 0;
				int expected = 250;
				int maxCount = Engine.GetFramesPerSecond() > 60 ? expected : (int)(expected * Engine.GetFramesPerSecond()/60);
				for (int x = 0; x < Pixels.Length; x++)
				{
					if (Pixels[x].IsAlive) continue;
					count++;
					if (count >= maxCount) break;
					Vector2 position = new Vector2(GetViewportRect().Size.x/2, GetViewportRect().Size.y/2);
					int angel = rand.Next(360);
					Vector2 speed = new Vector2((float)Math.Sin(angel), (float)Math.Cos(angel));
					Pixels[x].Speed = speed;
					Pixels[x].MyColor = color;
					Pixels[x].Position = position;
					Pixels[x].Mode = "DEACTIVATE";
					Pixels[x].IsAlive = true;
				}
				*/
			}

			}
	}

	public override void _PhysicsProcess(float delta)
	{

		//base._Process(delta);
		//SetPosition(new Vector2(rand.Next((int)GetViewportRect().Size.x), rand.Next((int)GetViewportRect().Size.y)));
		//var xform = (new Transform2D()).Scaled(new Vector2(2,2)) ; // .Rotated(45 * Phi); //.Translated(new Vector2(rand.Next((int)GetViewportRect().Size.x), rand.Next((int)GetViewportRect().Size.y)));
		//VisualServer.CanvasItemSetTransform(ci_rid, xform);

		
		if (ShowParticles)
		{
			if(GetChildCount() < 3)
			{
				AddChild(particles);
				particles.Position = new Vector2(rand.Next((int)GetViewportRect().Size.x), rand.Next((int)GetViewportRect().Size.y));
				particles.Emitting = true;
				particles.Visible = true;
			}
			//VisualServer.CanvasItemAddParticles(ci_rid, new RID(particles), texture.GetRid(),normalMap.GetRid());
		}
		else
		{
			if (GetChildCount() == 3)
			{
				RemoveChild(particles);
				particles.Emitting = false;
				particles.Visible = false;
			}
			//VisualServer.CanvasItemClear(ci_rid);
		}
		/*
		int maxParticles = Engine.GetFramesPerSecond() < 60f? (int)((Engine.GetFramesPerSecond() / 60f) * Pixels.Length) : Pixels.Length; // (int)((Engine.GetFramesPerSecond() / 100f) * Pixels.Length);
		int AliveParticles = 0;
		for (int x = 0; x < Pixels.Length; x++)
		{
			if (Pixels[x].IsAlive) {
				AliveParticles++;
				//VisualServer.CanvasItemAddRect(ci_rid, new Rect2(Pixels[x].Position, Pixels[x].Size), Pixels[x].MyColor);
				//VisualServer.CanvasItemAddTextureRect(ci_rid, new Rect2(Pixels[x].Position, texture.GetSize()), texture.GetRid(), false, Pixels[x].MyColor, false, texture.GetRid());
				//VisualServer.CanvasItemAddTextureRect(ci_rid, new Rect2(Pixels[x].Position, texture.GetSize()), texture.GetRid(), false, Pixels[x].MyColor, false, texture.GetRid());
				
			}
		}
		UpdatePixels(Pixels.Length);
		if (CoolDown > 0) CoolDown--;

		if (Input.IsActionJustPressed("Enter"))
		{
		}
		}
		UpdatePixels(Pixels.Length);
		if (CoolDown > 0) CoolDown--;

		if (Input.IsActionJustPressed("Enter")){

			CoolDown = 0;
		}
		*/
		//((Label)GetChild(0)).Text = Engine.GetFramesPerSecond().ToString() + " | " + AliveParticles;
	  
	}

	void UpdatePixels(int maxParticles)
	{
		///int totalChecks = Pixels.Length > maxParticles ? maxParticles : Pixels.Length;

		for (int x = 0; x < maxParticles; x++)
		{
			Pixels[x].Bounding = GetViewportRect().Size;
			Pixels[x].Update();
		}
	}
}
