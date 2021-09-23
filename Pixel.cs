using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;


    class Pixel
    {
        public Vector2 Position; // { get; set; }
        public Vector2 Speed; // { get; set; }
        public Vector2 Bounding; // { get; set;}
        public Vector2 Size; // { get; set;}
        public bool IsAlive = false; // { get; set; }
        public string Mode; // { get; set; }
        public Color MyColor;

        public Pixel(Vector2 position, Vector2 speed, Vector2 bounding, Vector2 size, Color color, bool isAlive = false, string mode = "BOUNCE")
        {
            Position = position;
            Speed = speed;
            Bounding = bounding;
            Size = size;
            MyColor = color;
            IsAlive = isAlive;
            Mode = mode;
        }

        public void Update()
        {
        //if (!IsAlive) return;
            Position += Speed*2;
            BoundingCheck();
        } 

        public void BoundingCheck()
        {
            if (Mode == "BOUNCE")
            {
                if (Position.x > Bounding.x) Position.x = 0;
                if (Position.x < 0) Position.x = Bounding.x;
                if (Position.y > Bounding.y) Position.y = 0;
                if (Position.y < 0) Position.y = Bounding.y;
            }

        if (Mode == "WARP")
        {
            if (Position.x > Bounding.x) Speed.x = -1;
            if (Position.x < 0) Position.x = Speed.x = 1;
            if (Position.y > Bounding.y) Speed.y = -1;
            if (Position.y < 0) Position.y = Speed.y = 1;
        }

        if (Mode == "DEACTIVATE")
            {
                if (Position.x > Bounding.x || 
                    Position.y > Bounding.y ||
                    Position.x < 0 ||
                    Position.y < 0) IsAlive = false;
            }

        }

        

    }

