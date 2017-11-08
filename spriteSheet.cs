using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AscensionGame
{
    class spriteSheet
    {
        public Rectangle sourceRectangle, destinationRectangle;
        Texture2D spriteSheetTexture;
        int rows, columns, currentFrame, totalFrames, frameSizeX, frameSizeY;
        float animationSpeed;

        public spriteSheet(Texture2D SpriteSheetTexture, int Rows, int Columns, Rectangle startingRectangle)
        {
            spriteSheetTexture = SpriteSheetTexture;
            rows = Rows;
            columns = Columns;
            totalFrames = rows * columns;
            frameSizeX = spriteSheetTexture.Width / columns;
            frameSizeY = spriteSheetTexture.Height / rows;
            destinationRectangle = startingRectangle;
        }

        public void Update()
        {
            currentFrame = (currentFrame == totalFrames) ? 0 : currentFrame + 1;
            sourceRectangle = new Rectangle((currentFrame % columns) * frameSizeX, (currentFrame / columns) * frameSizeY, frameSizeX, frameSizeY);
        }

        public void Draw()
        {
            Game1.spriteBatch.Draw(spriteSheetTexture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
