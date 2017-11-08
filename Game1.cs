using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace AscensionGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //You have to earn the right to more than one character

        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public static KeyboardState previousKeyboardState;
        public static KeyboardState currentKeyboardState;

        public static MouseState previousMouseState;
        public static MouseState currentMouseState;

        public static float elapsedGameTime;

        #region Texture2D
        public static Texture2D air_tile;
        public static Texture2D blowgun;
        public static Texture2D button_1;
        public static Texture2D clear_sky_clouds;
        public static Texture2D cursor;
        public static Texture2D dagger;
        public static Texture2D dwarf;
        public static Texture2D earth_tile;
        public static Texture2D egyptian_sword;
        public static Texture2D fetid_worm;
        public static Texture2D feedback_window;
        public static Texture2D flail;
        public static Texture2D forest_elf;
        public static Texture2D glaive;
        public static Texture2D goblin;
        public static Texture2D gold_tile;
        public static Texture2D human;
        public static Texture2D katana;
        public static Texture2D mace;
        public static Texture2D main_menu;
        public static Texture2D overlay_item_container;
        public static Texture2D pistol;
        public static Texture2D play_arrow;
        public static Texture2D sand_tile;
        public static Texture2D slider_bar;
        public static Texture2D slider_button;
        public static Texture2D slime;
        public static Texture2D splash_screen_ascension;
        public static Texture2D stone_tile;
        public static Texture2D sword;
        public static Texture2D tab_container;
        public static Texture2D text_box;
        public static Texture2D trash_bin;
        public static Texture2D trog;
        public static Texture2D war_axe;
        public static Texture2D war_hammer;
        public static Texture2D water_tile;
        public static Texture2D wolf;
        public static Texture2D wooden_staff;
        #endregion

        public static Texture2D testTexture;

        public static int screenWidth;
        public static int screenHeight;

        public static int currentRealTime;

        //One instance of the screen manager class
        public screenManager myScreenManager = new screenManager();

        //Default font to use everywhere
        public static SpriteFont defaultFont;

        //Default random number generator, no seed
        public static Random randomGenerator = new Random();

        //Several catchprases to be displayed as the window title
        private string[] catchphrases = new string[4] { "The elves at it again", "The dwarves are coming", "Ascend to godhood", "Destroy all the blocks!" }; 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            Window.Title = catchphrases[randomGenerator.Next(catchphrases.Length)];

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Texture2D loading
            air_tile = Content.Load<Texture2D>("Graphics/air_tile");
            blowgun = Content.Load<Texture2D>("Graphics/blowgun");
            button_1 = Content.Load<Texture2D>("Graphics/button_1");
            clear_sky_clouds = Content.Load<Texture2D>("Graphics/clear_sky_clouds");
            cursor = Content.Load<Texture2D>("Graphics/cursor");
            dagger = Content.Load<Texture2D>("Graphics/dagger");
            dwarf = Content.Load<Texture2D>("Graphics/dwarf");
            earth_tile = Content.Load<Texture2D>("Graphics/earth_tile");
            egyptian_sword = Content.Load<Texture2D>("Graphics/egyptian_sword");
            feedback_window = Content.Load<Texture2D>("Graphics/feedback_window");
            fetid_worm = Content.Load<Texture2D>("Graphics/fetid_worm");
            flail = Content.Load<Texture2D>("Graphics/flail");
            forest_elf = Content.Load<Texture2D>("Graphics/forest_elf");
            glaive = Content.Load<Texture2D>("Graphics/glaive");
            goblin = Content.Load<Texture2D>("Graphics/goblin");
            gold_tile = Content.Load<Texture2D>("Graphics/gold_tile");
            human = Content.Load<Texture2D>("Graphics/human");
            katana = Content.Load<Texture2D>("Graphics/katana");
            mace = Content.Load<Texture2D>("Graphics/mace");
            main_menu = Content.Load<Texture2D>("Graphics/main_menu");
            overlay_item_container = Content.Load<Texture2D>("Graphics/overlay_item_container");
            pistol = Content.Load<Texture2D>("Graphics/pistol");
            play_arrow = Content.Load<Texture2D>("Graphics/play_arrow");
            sand_tile = Content.Load<Texture2D>("Graphics/sand_tile");
            slider_bar = Content.Load<Texture2D>("Graphics/slider_bar");
            slider_button = Content.Load<Texture2D>("Graphics/slider_button");
            slime = Content.Load<Texture2D>("Graphics/slime");
            splash_screen_ascension = Content.Load<Texture2D>("Graphics/splash_screen_ascension");
            stone_tile = Content.Load<Texture2D>("Graphics/stone_tile");
            sword = Content.Load<Texture2D>("Graphics/sword");
            tab_container = Content.Load<Texture2D>("tab_container");
            text_box = Content.Load<Texture2D>("Graphics/text_box");
            trash_bin = Content.Load<Texture2D>("Graphics/trash_bin");
            trog = Content.Load<Texture2D>("Graphics/trog");
            war_axe = Content.Load<Texture2D>("Graphics/war_axe");
            war_hammer = Content.Load<Texture2D>("Graphics/war_hammer");
            water_tile = Content.Load<Texture2D>("Graphics/water_tile");
            wolf = Content.Load<Texture2D>("Graphics/wolf");
            wooden_staff = Content.Load<Texture2D>("Graphics/wooden_staff");
            #endregion

            testTexture = Content.Load<Texture2D>("Graphics/human_walking_right_sheet");

            defaultFont = Content.Load<SpriteFont>("Fonts/SpriteFont1");

            gameWorld.LoadCreatures();

            myScreenManager.Initialize();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            elapsedGameTime = gameTime.ElapsedGameTime.Milliseconds;

            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();

            myScreenManager.Update();

            currentRealTime = gameTime.TotalGameTime.Seconds;

            if (currentKeyboardState.IsKeyDown(Keys.F) && previousKeyboardState.IsKeyUp(Keys.F) && currentKeyboardState.IsKeyDown(Keys.LeftAlt))
            {
                if(!graphics.IsFullScreen)
                {
                    graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                    graphics.IsFullScreen = true;
                    graphics.ApplyChanges();
                }
                else
                {
                    graphics.PreferredBackBufferWidth = 800;
                    graphics.PreferredBackBufferHeight = 480;
                    graphics.IsFullScreen = false;
                    graphics.ApplyChanges();
                }
            }

            if(mainMenuScreen.exitGame.isCurrentlyClicked)
            {
                Exit();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            myScreenManager.Draw();

            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

            //spriteBatch.DrawString(defaultFont, "FPS : " + frameRate, Vector2.Zero, Color.Green);

            spriteBatch.Draw(cursor, currentMouseState.Position.ToVector2(), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
