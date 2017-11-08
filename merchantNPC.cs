using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AscensionGame
{
    class merchantNPC : UpdateDraw
    {
        Texture2D merchantTexture = Game1.human;
        Vector2 currentPos = Vector2.Zero;

        public merchantNPC()
        {

        }

        public void sayLoud(string Phrase)
        {
            Game1.spriteBatch.DrawString(Game1.defaultFont, Phrase, currentPos, Color.Black);
        }

        public void Update()
        {
            currentPos.X++;
            currentPos.Y++;
        }

        public void Draw()
        {
            Game1.spriteBatch.Draw(merchantTexture, currentPos, null);
            sayLoud("Hey there");
        }
    }
}
