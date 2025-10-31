using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace _4U_Topic_2_Graphics_Assignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState prevMouseState, mouseState;
        KeyboardState prevKeyboardState, keyboardState;

        List<Rectangle> ghostRectangles = new List<Rectangle>();
        List<Texture2D> ghostTextures = new List<Texture2D>();
        List<Texture2D> ghostDrawTextures = new List<Texture2D>();
        

        Rectangle windowRectangle;
        Texture2D backgroundTexture;

        Random generator = new Random();

        float secondTimer, respawnTime;
        int score;

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
            score = 0;
            base.Initialize();
            GenerateGhosts();

        }

        public void GenerateGhosts()
        {
            Rectangle tempGhost;
            for (int i = 0; i < generator.Next(20, 200); i++)
            {
                bool done = false;
                do
                {
                    done = true;

                    tempGhost = new Rectangle(generator.Next(windowRectangle.Width - 51),
                    generator.Next(windowRectangle.Height - 51), generator.Next(20, 51), generator.Next(20, 51));



                    for (int j = 0; j < ghostRectangles.Count; j++)
                    {
                        if (tempGhost.Intersects(ghostRectangles[j]))
                        {
                            done = false;

                        }
                    }

                } while (!done);
                ghostRectangles.Add(tempGhost);
                ghostDrawTextures.Add(ghostTextures[generator.Next(ghostTextures.Count)]);

            }
        }


        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 1; i <= 4; i++)
            {
                ghostTextures.Add(Content.Load<Texture2D>($"Images/ghost{i}"));
            }
            
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (prevKeyboardState.IsKeyUp(Keys.Space) && keyboardState.IsKeyDown(Keys.Space))
            {
                ghostRectangles.Clear();
                ghostDrawTextures.Clear();
                GenerateGhosts();
            }

            //secondTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //if (secondTimer > respawnTime)
            //{
            //    ghostRectangles.Clear();
            //    ghostDrawTextures.Clear();
            //    GenerateGhosts();
            //    secondTimer = 0f;
            //}


            if (prevMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                for (int i = ghostRectangles.Count - 1; i >= 0; i--)
                {
                    if (ghostRectangles[i].Contains(mouseState.Position))
                    {
                        ghostRectangles.RemoveAt(i);
                        ghostDrawTextures.RemoveAt(i);
                        i--;
                        score++;
                    }
                }
            }

            //float offsetY = 100f;
            //offsetY++;

            //ghostRectangles.ForEach(r => r.Location = new Point(r.Location.X, (int)(r.Location.Y + offsetY * (float)gameTime.ElapsedGameTime.TotalSeconds)));

            this.Window.Title = $"Score: {score}";



            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            for (int i = 0; i < ghostRectangles.Count; i++)
            {
                _spriteBatch.Draw(ghostDrawTextures[i], ghostRectangles[i], Color.White);
            }


            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
