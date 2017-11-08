using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AscensionGame
{
    public class character : livingEntity
    {
        //spriteSheet characterSpriteSheet = new spriteSheet(Game1.testTexture, 1, 4, new Rectangle(10, 10, 10, 10));

        public static Vector2 currentRespawnPos = new Vector2(250, 0);

        public static int miningRange = 10;

        public static Rectangle currentWeaponRectangle;

        public static Vector2 currentCharacterVelocity;
        public static Vector2 currentCharacterAcceleration;

        public static List<bullet> bullets = new List<bullet>();

        static bool isFacingRight = true;

        #region Stats

        public static int stamina { get; set; } = 10;
        public static int strength { get; set; } = 10;
        public static int intelligence { get; set; } = 10;
        public static int lore { get; set; } = 10;
        public static int agility { get; set; } = 10;
        public static int dexterity { get; set; } = 10;
        public static int alchemy { get; set; } = 10;

        #endregion

        public string playerName { get; set; }
        public static string playerTitle { get; set; } = "the Novice";

        public int currentKills;

        public static float walkingSpeed = 0.1f;
        public static float runningSpeed = agility / 30f;

        //Energy is related to stamina
        public int energy;

        //A dictionary which converts equipped item number to the item
        Dictionary<int, item> numberToItem;

        bool canMine = false;

        static float pistolAngle;

        public item equippedItem = new swordItem();

        #region Health

        public static int currentMaxHP = 100;
        public static int currentHP = currentMaxHP;

        #endregion

        #region Mana

        public static int currentMaxMana = 100;
        public static int currentMana = currentMaxMana;

        #endregion

        public static int miningDamage = 25;
        public static int damage = 10;

        static int jumpSpeed = -10;
        static bool isJumping = false;

        static float weaponAngle;

        public character()
        {

        }

        public void Initialize()
        {
            equippedItem.itemTexture = Game1.sword;
            currentRectangle = new Rectangle((Game1.screenWidth - Game1.human.Width) / 2, (Game1.screenHeight - Game1.human.Height) / 2 - 50, Game1.human.Width, Game1.human.Height);
        }

        public static bool isWithinMiningRange(tile Tile)
        {
            if(Vector2.Distance(Tile.currentRectangle.Location.ToVector2(), gameWorld.myCharacter.currentRectangle.Location.ToVector2()) < miningRange)
            {
                return true;
            }
            return false;
        }

        void applyNewton()
        {
            currentCharacterVelocity += currentCharacterAcceleration;
            currentRectangle.Location += currentCharacterVelocity.ToPoint();

            currentCharacterVelocity = Vector2.Zero;
            currentCharacterAcceleration = Vector2.Zero;
        }

        bool isTooFarLeft()
        {
            if(currentRectangle.Location.X + gameScreen.currentCameraPos.X < gameWorld.worldBorderTiles * tile.tileSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool isTooFarRight()
        {
            if (currentRectangle.Location.X + gameScreen.currentCameraPos.X > gameWorld.tileArray.GetLength(0) * tile.tileSize - gameWorld.worldBorderTiles * tile.tileSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool isCollidingLeft()
        {
            foreach (tile Tile in nearbyTiles)
            {
                if (currentRectangle.Left <= Tile.currentRectangle.Right && currentRectangle.Right > Tile.currentRectangle.Left && currentRectangle.Top < Tile.currentRectangle.Bottom && currentRectangle.Bottom > Tile.currentRectangle.Top && Tile.isSolid)
                {
                    return true;
                }
            }
            return false;
        }

        bool isCollidingRight()
        {
            foreach (tile Tile in nearbyTiles)
            {
                if (currentRectangle.Right >= Tile.currentRectangle.Left && currentRectangle.Left < Tile.currentRectangle.Right && currentRectangle.Top < Tile.currentRectangle.Bottom && currentRectangle.Bottom > Tile.currentRectangle.Top && Tile.isSolid)
                {
                    return true;
                }
            }
            return false;
        }

        bool isCollidingDown()
        {
            foreach (tile Tile in nearbyTiles)
            {
                if (currentRectangle.Bottom >= Tile.currentRectangle.Top && currentRectangle.Top < Tile.currentRectangle.Bottom && currentRectangle.Right > Tile.currentRectangle.Left && currentRectangle.Left < Tile.currentRectangle.Right && Tile.isSolid)
                {
                    return true;
                }
            }
            return false;
        }

        bool isCollidingUp()
        {
            foreach (tile Tile in nearbyTiles)
            {
                if (currentRectangle.Top <= Tile.currentRectangle.Bottom && currentRectangle.Top > Tile.currentRectangle.Top && currentRectangle.Right > Tile.currentRectangle.Left && currentRectangle.Left < Tile.currentRectangle.Right && Tile.isSolid)
                {
                    return true;
                }
            }
            return false;
        }

        void moveLeft()
        {
            if (Game1.currentKeyboardState.IsKeyDown(Keys.Q))
            {                
                if (!isCollidingLeft() && !isTooFarLeft())
                {
                    if (Game1.currentKeyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        gameScreen.currentCameraPos.X -= runningSpeed * Game1.elapsedGameTime;
                    }
                    else
                    {
                        gameScreen.currentCameraPos.X -= walkingSpeed * Game1.elapsedGameTime;
                    }
                }
            }
        }

        void moveRight()
        {
            if (Game1.currentKeyboardState.IsKeyDown(Keys.D))
            {                
                if (!isCollidingRight() && !isTooFarRight())
                {
                    if (Game1.currentKeyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        gameScreen.currentCameraPos.X += runningSpeed * Game1.elapsedGameTime;
                    }
                    else
                    {
                        gameScreen.currentCameraPos.X += walkingSpeed * Game1.elapsedGameTime;
                    }
                }
            }
        }

        void moveUp()
        {
            if (Game1.currentKeyboardState.IsKeyDown(Keys.Z))
            {
                if (!isCollidingUp())
                {
                    gameScreen.currentCameraPos.Y--;
                }
            }
        }

        void moveDown()
        {
            if (Game1.currentKeyboardState.IsKeyDown(Keys.S))
            {
                if (!isCollidingDown())
                {
                    gameScreen.currentCameraPos.Y++;
                }
            }
        }

        void move()
        {
            applyNewton();

            //Check movement for all directions
            moveRight();
            moveLeft();
            moveDown();
            //moveUp();

            //Gravity effect            
            if (!isCollidingDown() && !isJumping)
            {
                gameScreen.currentCameraPos.Y += gameWorld.gravityConstant;
            }
        }

        public void jump()
        {
            if (Game1.currentKeyboardState.IsKeyDown(Keys.Space) && Game1.previousKeyboardState.IsKeyUp(Keys.Space))
            {
                gameScreen.currentCameraPos.Y -= 50;
            }

            /*if (Game1.currentKeyboardState.IsKeyDown(Keys.Space) && Game1.previousKeyboardState.IsKeyUp(Keys.Space))
            {
                isJumping = true;
            }

            if (jumpSpeed <= 10 && isJumping)
            {
                if(jumpSpeed <= 0)
                {
                    if(!isCollidingUp())
                    {
                        gameScreen.currentCameraPos.Y += jumpSpeed;
                    }
                }
                else
                {
                    if(!isCollidingDown())
                    {
                        gameScreen.currentCameraPos.Y += jumpSpeed;
                    }
                }
                jumpSpeed++;           
            }
            else if (jumpSpeed > 10)
            {
                isJumping = false;
                jumpSpeed = -10;
            }*/
        }

        public void attack()
        {
            if(Game1.currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if(weaponAngle < 2f)
                {
                    weaponAngle += 0.5f;
                }
                foreach (creature Creature in creatureManager.aliveCreatures)
                {
                    if (currentRectangle.Intersects(Creature.drawRectangle) && Game1.previousMouseState.LeftButton == ButtonState.Released)
                    {
                        Creature.currentHP -= damage;
                    }
                }
            }
            else if(Game1.currentMouseState.LeftButton == ButtonState.Released)
            {
                weaponAngle = 0f;
            }
        }

        public void LoadCharacter(string PlayerName)
        {
            playerName = PlayerName;
        }

        public void SaveCharacter(string PlayerName)
        {
            File.WriteAllText("C:/AscensionGameFiles/Characters/" + PlayerName + ".txt", playerName + ", " + playerTitle + "; " + "Current HP: " + currentHP + "/" + currentMaxHP);
        }

        public void Respawn()
        {
            currentHP = currentMaxHP;
            gameScreen.currentCameraPos = Vector2.Zero;
        }

        public void Shoot()
        {
            bullets.Add(new bullet());
        }

        public void populateNearbyTiles()
        {
            for (int x = xTile - 1; x <= xTile + 2; x++)
            {
                for (int y = yTile; y < yTile + 8; y++)
                {
                    nearbyTiles.Add(gameWorld.tileArray[x, y]);
                }
            }
        }

        public override void Update()
        {
            //Only check nearby tiles for collision detection
            base.Update();

            //characterSpriteSheet.Update();

            nearbyTiles.Clear();

            xTile = (int)(currentRectangle.Location.X + gameScreen.currentCameraPos.X) / tile.tileSize;
            yTile = (int)(currentRectangle.Location.Y + gameScreen.currentCameraPos.Y) / tile.tileSize;

            populateNearbyTiles();

            if (Game1.currentMouseState.LeftButton == ButtonState.Pressed && Game1.previousMouseState.LeftButton == ButtonState.Released)
            {
                Shoot();
            }

            Vector2 gunDirection = Game1.currentMouseState.Position.ToVector2() - currentRectangle.Location.ToVector2();

            pistolAngle = (float)Math.Atan2(gunDirection.Y, gunDirection.X);

            move();

            attack();

            jump();

            if (currentHP <= 0)
            {
                Respawn();
            }
        }

        public override void Draw()
        {
            base.Draw();

            //characterSpriteSheet.Draw();

            if(isFacingRight)
            {
                Game1.spriteBatch.Draw(Game1.human, currentRectangle, Color.White);
            }
            else
            {
                Game1.spriteBatch.Draw(Game1.human, currentRectangle, Color.White);
            }

            Game1.spriteBatch.Draw(equippedItem.itemTexture, new Rectangle(currentRectangle.X + 10, currentRectangle.Y + 10, equippedItem.itemTexture.Width, equippedItem.itemTexture.Height), new Rectangle(0, 0, equippedItem.itemTexture.Width, equippedItem.itemTexture.Height), Color.White, weaponAngle, new Vector2(equippedItem.itemTexture.Width / 2, equippedItem.itemTexture.Height), SpriteEffects.None, 1);
            Game1.spriteBatch.DrawString(Game1.defaultFont, playerName, new Vector2(currentRectangle.Location.X - Game1.defaultFont.MeasureString(playerName).X / 2, currentRectangle.Location.Y - Game1.defaultFont.MeasureString(playerName).Y - 10), Color.Black);

            //Game1.spriteBatch.Draw(Game1.pistol, new Rectangle(currentCharacterRectangle.Location, new Point(Game1.pistol.Width, Game1.pistol.Height)), null, Color.White, pistolAngle, new Vector2(Game1.pistol.Width / 2, Game1.pistol.Height / 2), SpriteEffects.None, 0);

            //switch(activeItem)
        }
    }
}
