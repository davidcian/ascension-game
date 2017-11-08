using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AscensionGame
{
    public abstract class livingEntity
    {
        public Rectangle currentRectangle;

        //Implement random starting positions for all spawning creatures.
        public Vector2 spawnPos;

        public static List<tile> nearbyTiles = new List<tile>();

        //The x tile and the y tile of the character's position
        public static int xTile, yTile;

        public virtual void Update()
        {
            nearbyTiles.Clear();

            xTile = (int)(currentRectangle.Location.X + gameScreen.currentCameraPos.X) / tile.tileSize;
            yTile = (int)(currentRectangle.Location.Y + gameScreen.currentCameraPos.Y) / tile.tileSize;
        }

        public virtual void Draw()
        {

        }
    }
}
