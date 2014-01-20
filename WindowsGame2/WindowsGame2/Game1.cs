using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D texture1px;
        private int cols;
        private int rows;
        private float gridSize;
        private int gridWidth;
        private int gridHeight;
        private float centerGridX;
        private float centerGridY;
        private MouseState mouse;
        private KeyboardState keyboard;
        private bool mPressed;
        private bool mirrorMode;
        private Texture2D textureTrans;

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

            generateTextures();

            gridSize = 30;

            mirrorMode = false;
            mPressed = false;

            Window.AllowUserResizing = true;

            base.Initialize();
        }

        private void generateTextures()
        {
            texture1px = new Texture2D(graphics.GraphicsDevice, 1, 1);
            texture1px.SetData(new Color[] { Color.White });

            Byte transparency_amount = 180; //0 transparent; 255 opaque
            textureTrans = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            Color[] c = new Color[1];
            c[0] = Color.FromNonPremultiplied(255, 255, 255, transparency_amount);
            textureTrans.SetData<Color>(c);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.M))
            {
                if (!mPressed)
                {
                    mirrorMode = !mirrorMode;
                    mPressed = true;
                }
            }
            else
            {
                mPressed = false;
            }

            updateGridInfo();

            base.Update(gameTime);
        }

        private void updateGridInfo()
        {
            cols = (int)(this.GraphicsDevice.Viewport.Width / gridSize);
            rows = (int)(this.GraphicsDevice.Viewport.Height / gridSize);
            gridWidth = this.GraphicsDevice.Viewport.Width;
            gridHeight = this.GraphicsDevice.Viewport.Height;
            centerGridX = 0;
            centerGridY = 0;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            if (mirrorMode)
            {
                drawMirrorOverlay();
            }


            // TODO: Add your drawing code here
            drawGridLines();
            drawCurrentBlock();

            

            base.Draw(gameTime);
        }

        private void drawMirrorOverlay()
        {
            spriteBatch.Begin();
            Rectangle rect = new Rectangle((int)(cols/2 * gridSize),0 , (int)(cols/2 * gridSize), (int)(rows * gridSize));
            spriteBatch.Draw(textureTrans, rect, Color.Gold);
            spriteBatch.End();
        }

        private void drawCurrentBlock()
        {
            spriteBatch.Begin();
            float x = (int)((float)mouse.X / gridSize);
            float y = (int)((float)mouse.Y / gridSize);

            Rectangle rectangle = new Rectangle((int)(x * gridSize)-5,(int)(y * gridSize)-5, 10, 10);
            spriteBatch.Draw(texture1px, rectangle, Color.Red);
            spriteBatch.End();
        }

        private void drawGridLines()
        {
            spriteBatch.Begin();
            for (float x = 0; x < cols+1; x++)
            {
                Rectangle rectangle = new Rectangle((int)(centerGridX + x * gridSize), 0, 1, (int)(rows * gridSize));
                spriteBatch.Draw(texture1px, rectangle, Color.Blue);
            }
            for (float y = 0 ; y < rows+1; y++)
            {
                Rectangle rectangle = new Rectangle(0, (int)(centerGridY + y * gridSize), (int)(cols * gridSize), 1);
                spriteBatch.Draw(texture1px, rectangle, Color.Blue);
            }
            spriteBatch.End();
        }
    }
}
