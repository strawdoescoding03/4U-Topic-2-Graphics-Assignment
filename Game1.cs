using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _4U_Topic_2_Graphics_Assignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState previousMouseState, mouseState;
        KeyboardState previousKeyboardState, keyboardState;

        List<Rectangle> rectangles = new List<Rectangle>();
        List<Texture2D> textures = new List<Texture2D>();
        List<Texture2D> alternateTextures = new List<Texture2D>();

        Rectangle windowRectangle;
        Texture2D backgroundTexture;

        Random generator = new Random();

        float secondTimer, respawnTime;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            windowRectangle = new Rectangle(
                0,
                0,
                _graphics.PreferredBackBufferWidth, 
                _graphics.PreferredBackBufferHeight);

            generator = new Random();

            secondTimer = 0f;
            respawnTime = 2f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        
        public void generateObjects()
        {
            // Method to generate objects
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
