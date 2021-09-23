using Godot;
using System;

public class Player : Node2D
{
	// Declare member variables here. Examples:
	[Export]
	private int speed = 200;
	private int ORBITS = 1;
	private int MyOrbit = 1;
	public string MODE = "";
	private string[] MODES = {"RESONANCE","MIRRORX","MIRRORY","MIRRORXY"};
	private int currentMode = 0;
	
	public float currentRotation = 0.0f;
	
	private bool isRotateLeft = false;
	private bool isRotateRight = false;
	
	[Export]
	private float colorCoolDownMax = 2;
	private float colorCoolDown;
	public Color currentColor;
	private float  offsetColorCoolDownMax = 2;
	private float  offsetColorCoolDown;
	public Color offsetColor;
	
	
	
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MODE = MODES[currentMode];
		if(GetIndex() == 0 ) Position = GetParent().GetParent().GetParent().GetViewport().Size/2;
		if(GetIndex() > 0 )	Position = GetModePos(GetIndex()-1);
	}

	public Color GetOffsetColor(int offset)
	{
		return((Colors)(GetParent().GetParent().GetChild(0))).GetOffsetColor(offset);
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
	try{
		float RotatationAmount = (float)((speed/10) * (Math.PI/180));
		Vector2 screenSize  = GetParent().GetParent().GetParent().GetViewport().Size;
		if (Input.IsActionJustPressed("ESCAPE"))
		{
			if(GetIndex() > 0 )	Free();
			if(GetIndex() == 0 )	Position = screenSize/2;
			return;
		}

		if (Input.IsActionJustPressed("POSITION-CENTER"))
		{
			if(GetIndex() == 0 )	Position = new Vector2(screenSize.x/2,screenSize.y/2);
		}
		
		if (Input.IsActionJustPressed("POSITION-LEFT"))
		{
			if(GetIndex() == 0 )	Position = new Vector2(screenSize.x/4,screenSize.y/2);
		}

		if (Input.IsActionJustPressed("POSITION-RIGHT"))
		{
			if(GetIndex() == 0 )	Position = new Vector2(screenSize.x*3/4,screenSize.y/2);
		}
		
		if (Input.IsActionJustPressed("POSITION-TOP"))
		{
			if(GetIndex() == 0 )	Position = new Vector2(screenSize.x/2,screenSize.y/4);
		}
		
		if (Input.IsActionJustPressed("POSITION-BOTTOM"))
		{
			if(GetIndex() == 0 )	Position = new Vector2(screenSize.x/2,screenSize.y*3/4);
		}
		
		if (Input.IsActionJustPressed("ROTATE-LEFT"))
		{
			if(GetIndex() == 0 )
			{
				isRotateRight = false;
				isRotateLeft = !isRotateLeft;
			}	
			
		}
		
		if (Input.IsActionJustPressed("ROTATE-RIGHT"))
		{
			if(GetIndex() == 0 )
			{
				isRotateRight = !isRotateRight;
				isRotateLeft = false;
			}	
			
		}
		

		if (Input.IsActionJustPressed("ROTATE-ZERO"))
		{
			if(GetIndex() == 0 )
			{
				isRotateRight = false;
				isRotateLeft = false;
				currentRotation = 0;
			}	
		}
		
		if(GetIndex() == 0 && isRotateLeft )	currentRotation -= RotatationAmount*delta;
		if(GetIndex() == 0 && isRotateRight)	currentRotation += RotatationAmount*delta;
		
		/*
		if(GetIndex() == 0 &&  colorCoolDown <= 0){
			//currentColor = ((Colors)(GetParent().GetParent().GetChild(0))).GetCurrentColor();
			offsetColor = ((Colors)(GetParent().GetParent().GetChild(0))).GetOffsetColor(MyOrbit+1);
			colorCoolDown = colorCoolDownMax;
		}else{
			colorCoolDown -= delta;
		}
		*/
		
		if(colorCoolDown <= 0){
			//currentColor = ((Colors)(GetParent().GetParent().GetChild(0))).GetCurrentColor();
			offsetColor = ((Colors)(GetParent().GetParent().GetChild(0))).GetOffsetColor(MyOrbit);
			
			if(GetIndex() == 0 ){
				colorCoolDown = colorCoolDownMax;
			}else{
				colorCoolDown =  ((Player)GetParent().GetChild(0)).colorCoolDown;
			}
		}else{
			colorCoolDown -= delta;
		}

			
		if (Input.IsActionJustPressed("SPEED-ADD"))
		{
			if (speed < 2000) speed += 20;
		}

		if (Input.IsActionJustPressed("SPEED-SUB"))
		{
			if (speed > 10) speed -= 10;
		}

		if (GetIndex() == 0)
		{
			
			if (Input.IsActionJustPressed("ORBIT-ADD"))
			{
				if (ORBITS < 100) ORBITS += 1;
			}
			
			if (Input.IsActionJustPressed("ORBIT-REMOVE"))
			{
					if (ORBITS > 1) ORBITS -= 1;
			}

			if (Input.IsActionJustPressed("ADD-PLAYER"))
			{
				Player player = (Player)Duplicate();
				player.Position = GetModePos(GetParent().GetChildCount()-1);
				GetParent().AddChild(player);
				player.Visible = true;
				//((Sprite)player.GetChild(0)).Visible = true;
				//Console.WriteLine("CHILD ADDED");
			}

			if (Input.IsActionJustPressed("REMOVE-PLAYER"))
			{
				if (GetParent().GetChildCount() <= 1) return;
				GetParent().GetChild((GetParent().GetChildCount() - 1)).Free();

				if (GetParent().GetChildCount() <= 1)
				{
					Vector2 ScreenCenter = new Vector2(GetParent().GetViewport().Size / 2);
					Position = ScreenCenter;
				}

				//Player player = (Player)GetParent().GetChild((GetParent().GetChildCount() - 1));
				//player.Free();
				//GetParent().RemoveChild(player);
				//player.Visible = false;
				//player.Free();
			}

			if (GetParent().GetChildCount() <= 1) return;
			float moveAmount = delta * speed;
			Vector2 pos = Position;
			
			if (Input.IsActionJustPressed("MODE"))
			{
				if(currentMode >= MODES.Length-1 ) {
					currentMode = 0;
				}else{
					currentMode ++;
				}
				MODE = MODES[currentMode];
			}
		
			if (Input.IsActionPressed("UP"))
			{
				pos.y -= moveAmount;
				//Console.WriteLine("UP");
			}

			if (Input.IsActionPressed("RIGHT"))
			{
				pos.x += moveAmount;
			}

			if (Input.IsActionPressed("DOWN"))
			{
				pos.y += moveAmount;
			}

			if (Input.IsActionPressed("LEFT"))
			{
				pos.x -= moveAmount;
			}
			Position = pos;
			
		}

		if (Visible == false) return;

		if (GetIndex() > 0)
		{
			Position = GetModePos(GetIndex()-1);
		}
		}catch{}
	}

	public Vector2 GetModePos(int Index,string Mode = "")
	{
		Mode = ((Player)GetParent().GetChild(0)).MODE;
		
		Vector2 pos = ((Player)GetParent().GetChild(0)).Position;
		int TotalChildren = GetParent().GetChildCount() - 1;
		if(TotalChildren == 0) return ((Player)GetParent().GetChild(0)).Position; 
		//float AnglePerSeg = (float)(360 / TotalChildren);

		Vector2 ScreenSize = new Vector2(GetParent().GetViewport().Size);
		Vector2 ScreenCenter = new Vector2(ScreenSize / 2);
		int Orbits = ((Player)GetParent().GetChild(0)).ORBITS;
		int ChildrenPerOrbit = (int)(TotalChildren / Orbits);
		
		int Orbit = ((Index) / ChildrenPerOrbit)+1;
		MyOrbit = Orbit;
		
		float RotationRadians = (float)((Math.PI / (180 * ChildrenPerOrbit)) * 360.0f);
		float CurrentRotation = ((Player)GetParent().GetChild(0)).currentRotation;
		float CurrentDistnace = pos.DistanceTo(ScreenCenter);  
		
		float distRatio = (float)Orbit / (float)Orbits;
		float Distance =  (CurrentDistnace * distRatio);
		
		Vector2 diff = pos - ScreenCenter;
		
		if (Mode == "RESONANCE"){
			//if(Index = TotalChildren){
			//	Console.WriteLine(Index + " | " + 180 * RotationRadians*Index/Math.PI);
			//}
			//diff = diff.Rotated(CurrentRotation+RotationRadians*Index)/(Distance); //+ point2
			return diff.Rotated(CurrentRotation + RotationRadians * Index) * distRatio + ScreenCenter ; //+ point2

			//diff *= distRatio;
			//diff += ScreenCenter;
			//diff.MoveToward(ScreenCenter, (CurrentDistnace - Distance));
			
			
			
			//Console.WriteLine(Orbits + " | " + Orbit + " | "+ CurrentDistnace + " | " + distRatio + " >|<" +  Distance + " | " + diff);
			
			return diff;
		}
		if (Mode == "MIRRORX"){
			if(Index%2 == 0){
				return diff.Rotated(CurrentRotation+RotationRadians*Index) * distRatio + ScreenCenter; 
			}else{
				pos = ((Player)GetParent().GetChild(Index)).Position;
				diff = new Vector2 (ScreenSize.x - pos.x,pos.y);
				return diff;//.Rotated(CurrentRotation);
			}
		}
		if (Mode == "MIRRORY"){
			if(Index%2 == 0){
				return diff.Rotated(RotationRadians+RotationRadians*Index) * distRatio  + ScreenCenter; 
			}else{
				pos = ((Player)GetParent().GetChild(Index)).Position;
				diff = new Vector2 (pos.x,ScreenSize.y - pos.y);
				return diff; //.Rotated(CurrentRotation);
			}
		}
		if (Mode == "MIRRORXY"){
			if(Index%2 == 0){
				return diff.Rotated(CurrentRotation+RotationRadians*Index*0.5f) * distRatio  + ScreenCenter; 
			}else{
				pos = ((Player)GetParent().GetChild(Index)).Position;
				//diff = new Vector2 (ScreenSize.x - pos.x,pos.y);
				//diff = new Vector2 (pos.x,ScreenSize.y - pos.y);
				diff = new Vector2 (ScreenSize.x - pos.x,ScreenSize.y - pos.y);
				return diff;//.Rotated(CurrentRotation);
			}
		}
		return ((Player)GetParent().GetChild(0)).Position;
		
	}

	float Deg2Rad(float degrees)
	{
		return (float)(degrees * Math.PI/180);
	}
}
