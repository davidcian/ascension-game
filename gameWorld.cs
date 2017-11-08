using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AscensionGame
{
    public class gameWorld
    {
        //Update all tiles, but only draw them if in view of the player

        public const int seaLevelWorldHeight = 300;
        public const int gravityConstant = 2;
        public const int worldBorderTiles = 65;

        static int maxMountainHeight = 5;
        static int maxCaveLength = 200;
        static int maxVeinLength = 100;

        static int oreVeinNumber = 10;
        static int caveNumber = Game1.randomGenerator.Next(20);

        public static character myCharacter = new character();
        creatureManager myCreatureManager = new creatureManager();

        merchantNPC testMerchant = new merchantNPC();

        //static fetidWormCreature testWorm = new fetidWormCreature();

        //Choose [width, height]
        public static tile[,] tileArray = new tile[300, 100];

        public static string gameWorldName;

        public gameWorld()
        {

        }

        public void Initialize()
        {
            myCharacter.Initialize();
        }

        public static void InitializeTileArray()
        {
            for (int i = 0; i < tileArray.GetLength(0); i++)
            {
                for (int j = 0; j < tileArray.GetLength(1); j++)
                {
                    if(j * 6 >= seaLevelWorldHeight)
                    {
                        tileArray[i, j] = new earthTile(new Vector2(i * tile.tileSize, j * tile.tileSize));
                    }
                    else
                    {
                        tileArray[i, j] = new airTile(new Vector2(i * tile.tileSize, j * tile.tileSize));
                    }
                }
            }

            generateTerrain();
        }

        public static void generateTerrain()
        {
            generateMountains();

            generateCaves();

            generateOres();

            generateDungeons();

            //starting quest

            generateWaters();

            generateTrees();
        }

        public static void generateMountains()
        {
            for(int i = 0; i < tileArray.GetLength(0); i++)
            {
                int heightAboveSea = Game1.randomGenerator.Next(maxMountainHeight);
                for(int j = seaLevelWorldHeight / tile.tileSize - heightAboveSea; j < seaLevelWorldHeight / tile.tileSize; j++)
                {
                    tileArray[i, j] = new earthTile(new Vector2(i * tile.tileSize, j * tile.tileSize));
                }
            }
        }

        public static void generateCave()
        {
            int caveLength = Game1.randomGenerator.Next(maxCaveLength);
            /*int startX = Game1.randomGenerator.Next(tileArray.GetLength(0));
            int startY = Game1.randomGenerator.Next(tileArray.GetLength(1) - seaLevelWorldHeight / tile.tileSize) + seaLevelWorldHeight / tile.tileSize;*/
            int startX = Game1.randomGenerator.Next(tileArray.GetLength(0));
            int startY = Game1.randomGenerator.Next(10) + seaLevelWorldHeight / tile.tileSize;
            for (int i = 0; i < caveLength; i++)
            {
                if (startX > 0 && startY > seaLevelWorldHeight / tile.tileSize && startX < tileArray.GetLength(0) && startY < tileArray.GetLength(1))
                {
                    tileArray[startX, startY] = new airTile(new Vector2(startX * tile.tileSize, startY * tile.tileSize));
                }

                int direction = Game1.randomGenerator.Next(4);
                switch (direction)
                {
                    case 0:
                        startY--;
                        break;
                    case 1:
                        startX++;
                        break;
                    case 2:
                        startY++;
                        break;
                    case 3:
                        startX--;
                        break;
                    default:
                        break;
                }
            }
        }

        public static void generateCaves()
        {
            for(int i = 0; i < caveNumber; i++)
            {
                generateCave();
            }
        }

        public static void generateWater()
        {
            /*int caveLength = Game1.randomGenerator.Next(maxCaveLength);
            int startX = Game1.randomGenerator.Next(tileArray.GetLength(0));
            int startY = Game1.randomGenerator.Next(tileArray.GetLength(1) - seaLevelWorldHeight / tile.tileSize) + seaLevelWorldHeight / tile.tileSize;*/
            int caveLength = 10;
            int startX = Game1.randomGenerator.Next(tileArray.GetLength(0));
            int startY = Game1.randomGenerator.Next(10) + seaLevelWorldHeight / tile.tileSize;
            for (int i = 0; i < caveLength; i++)
            {
                if (startX > 0 && startY > seaLevelWorldHeight / tile.tileSize && startX < tileArray.GetLength(0) && startY < tileArray.GetLength(1))
                {
                    tileArray[startX, startY] = new waterTile(new Vector2(startX * tile.tileSize, startY * tile.tileSize));
                }

                int direction = Game1.randomGenerator.Next(4);
                switch (direction)
                {
                    case 0:
                        startY--;
                        break;
                    case 1:
                        startX++;
                        break;
                    case 2:
                        startY++;
                        break;
                    case 3:
                        startX--;
                        break;
                    default:
                        break;
                }
            }
        }

        public static void generateWaters()
        {
            for (int i = 0; i < 10; i++)
            {
                generateWater();
            }
        }

        public static void generateTree()
        {

        }

        public static void generateTrees()
        {

        }

        public static void generateDungeons()
        {
            for(int i = 0; i < 10; i++)
            {
                //Generate dungeon hall
            }
        }

        public static void generateOreVein()
        {
            int veinLength = Game1.randomGenerator.Next(maxVeinLength);
            int startX = Game1.randomGenerator.Next(tileArray.GetLength(0));
            int startY = Game1.randomGenerator.Next(10) + seaLevelWorldHeight / tile.tileSize;
            for (int i = 0; i < veinLength; i++)
            {
                if (startX > 0 && startY > seaLevelWorldHeight / tile.tileSize && startX < tileArray.GetLength(0) && startY < tileArray.GetLength(1))
                {
                    tileArray[startX, startY] = new goldTile(new Vector2(startX * tile.tileSize, startY * tile.tileSize));
                }

                int direction = Game1.randomGenerator.Next(4);
                switch (direction)
                {
                    case 0:
                        startY--;
                        break;
                    case 1:
                        startX++;
                        break;
                    case 2:
                        startY++;
                        break;
                    case 3:
                        startX--;
                        break;
                    default:
                        break;
                }
            }
        }

        public static void generateOres()
        {
            for (int i = 0; i < oreVeinNumber; i++)
            {
                generateOreVein();
            }
        }

        public static void LoadCreatures()
        {

        }

        public static void LoadGameWorld(string GameWorldName)
        {
            gameWorldName = GameWorldName;
            string fileText = File.ReadAllText("C:/AscensionGameFiles/Worlds/" + gameWorldName + ".txt");
            int fileLength = (fileText.Length <= tileArray.GetLength(0) * tileArray.GetLength(1)) ? fileText.Length : tileArray.GetLength(0) * tileArray.GetLength(1);
            for (int n = 0; n < fileLength; n++)
            {
                int i = n / tileArray.GetLength(1);
                int j = n % tileArray.GetLength(1);
                if (fileText[n] == 'E')
                {
                    tileArray[i, j] = new earthTile(new Vector2(i * tile.tileSize, j * tile.tileSize));
                }
                else if (fileText[n] == 'A')
                {
                    tileArray[i, j] = new airTile(new Vector2(i * tile.tileSize, j * tile.tileSize));
                }
                else if (fileText[n] == 'G')
                {
                    tileArray[i, j] = new goldTile(new Vector2(i * tile.tileSize, j * tile.tileSize));
                }
                else if (fileText[n] == 'W')
                {
                    tileArray[i, j] = new waterTile(new Vector2(i * tile.tileSize, j * tile.tileSize));
                }
            }
        }

        public static void SaveGameWorld(string GameWorldName)
        {
            for (int i = 0; i < tileArray.GetLength(0); i++)
            {
                string terrainLine = "";
                for(int j = 0; j < tileArray.GetLength(1); j++)
                {
                    if (tileArray[i, j].GetType() == typeof(earthTile))
                    {
                        terrainLine += "E";
                    }
                    else if(tileArray[i,j].GetType() == typeof(airTile))
                    {
                        terrainLine += "A";
                    }
                    else if(tileArray[i, j].GetType() == typeof(goldTile))
                    {
                        terrainLine += "G";
                    }
                    else if(tileArray[i, j].GetType() == typeof(waterTile))
                    {
                        terrainLine += "W";
                    }
                }
                File.WriteAllText("C:/AscensionGameFiles/Worlds/" + GameWorldName + ".txt", terrainLine);
            }
        }

        public void Update()
        {
            myCharacter.Update();
            myCreatureManager.Update();

            for(int i = 0; i < tileArray.GetLength(0); i++)
            {
                for(int j = 0; j < tileArray.GetLength(1); j++)
                {
                    if (i >= 0 && j >= 0 && i < tileArray.GetLength(0) && j < tileArray.GetLength(1))
                    {
                        if (tileArray[i, j].currentHealth <= 0)
                        {
                            tileArray[i, j] = new airTile(tileArray[i, j].startRectangle.Location.ToVector2());
                        }
                        tileArray[i, j].Update();
                    }
                }
            }

            //testMerchant.Update();
            //testWorm.Update();
        }

        public void Draw()
        {
            for (int i = (int)(gameScreen.currentCameraPos.X / tile.tileSize) - 5; i < (int)(gameScreen.currentCameraPos.X / tile.tileSize) + Game1.screenWidth / tile.tileSize + 5; i++)
            {
                for(int j = (int)(gameScreen.currentCameraPos.Y / tile.tileSize) - 5; j < (int)(gameScreen.currentCameraPos.Y / tile.tileSize) + Game1.screenHeight / tile.tileSize + 5; j++)
                {
                    if(i >= 0 && j >= 0 && i < tileArray.GetLength(0) && j < tileArray.GetLength(1))
                    {
                        tileArray[i, j].Draw();
                    }
                }
            }

            //testMerchant.Draw();
            //testWorm.Draw();

            Game1.spriteBatch.DrawString(Game1.defaultFont, gameWorldName, Vector2.Zero, Color.Black);

            myCharacter.Draw();

            myCreatureManager.Draw();
        }
    }
}
