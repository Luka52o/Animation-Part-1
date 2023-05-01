using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Animation_Part_1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D tribbleBrownTexture, tribbleCreamTexture, tribbleGreyTexture, tribbleOrangeTexture, space1Texture, space2Texture;
        Rectangle tribbleBrownRect, tribbleCreamRect, tribbleGreyRect, tribbleOrangeRect; 
        Vector2 tribbleBrownSpeed, tribbleCreamSpeed, tribbleGreySpeed, tribbleOrangeSpeed;
        Random generator = new Random();
        int collisions = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            tribbleBrownRect = new Rectangle(generator.Next(100, 701), generator.Next(100, 401), 100, 100);
            tribbleBrownSpeed = new Vector2(3, 0);

            tribbleCreamRect = new Rectangle(generator.Next(100, 701), generator.Next(100, 401), 100, 100);
            tribbleCreamSpeed = new Vector2(3, 2);

            tribbleGreyRect = new Rectangle(generator.Next(100, 701), generator.Next(100, 401), 100, 100);
            tribbleGreySpeed = new Vector2(3, 2); // setting the initial speed of the object
            // this vector2 stores 2 values that represent the speed of an object broken into x & y components

            tribbleOrangeRect = new Rectangle(generator.Next(100, 701), generator.Next(100, 401), 100, 100);
            tribbleOrangeSpeed = new Vector2(0, 4);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            space1Texture = Content.Load<Texture2D>("space1");
            space2Texture = Content.Load<Texture2D>("space2");

            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // Brown Tribble Motion Updates:
            tribbleBrownRect.X += (int)tribbleBrownSpeed.X;
            tribbleBrownRect.Y += (int)tribbleBrownSpeed.Y;

            if (tribbleBrownRect.Right > _graphics.PreferredBackBufferWidth || tribbleBrownRect.Left < 0)
            {
                tribbleBrownSpeed.X *= -1;
                tribbleBrownRect.Width++;
                tribbleBrownRect.Height++;
                collisions++;
            }

            if (tribbleBrownRect.Bottom + 100 > _graphics.PreferredBackBufferHeight || tribbleBrownRect.Top < 0)
            {
                tribbleBrownSpeed.Y *= -1;
                tribbleBrownRect.Width++;
                tribbleBrownRect.Height++;
                collisions++;
            }

            // Cream Tribble Motion Updates:
            tribbleCreamRect.X += (int)tribbleCreamSpeed.X;
            tribbleCreamRect.Y += (int)tribbleCreamSpeed.Y;

            if (tribbleCreamRect.Right > _graphics.PreferredBackBufferWidth || tribbleCreamRect.Left < 0)
            {
                tribbleCreamSpeed.X *= -1;
                tribbleCreamRect.Width++;
                tribbleCreamRect.Height++;
                collisions++;
            }
            if (tribbleCreamRect.Bottom + 100 > _graphics.PreferredBackBufferHeight || tribbleCreamRect.Top < 0)
            {
                tribbleCreamSpeed.Y *= -1;
                tribbleCreamRect.Width++;
                tribbleCreamRect.Height++;
                collisions++;
            }
            // Gray Tribble Motion Updates:
            tribbleGreyRect.X += (int)tribbleGreySpeed.X;
            tribbleGreyRect.Y += (int)tribbleGreySpeed.Y; 
        
            if (tribbleGreyRect.Right > _graphics.PreferredBackBufferWidth || tribbleGreyRect.Left < 0)
            {
                tribbleGreySpeed.X *= -1;
                tribbleGreyRect.Width++;
                tribbleGreyRect.Height++;
                collisions++;
            }
            if (tribbleGreyRect.Bottom + 100 > _graphics.PreferredBackBufferHeight || tribbleGreyRect.Top < 0)
            {
                tribbleGreySpeed.Y *= -1;
                tribbleGreyRect.Width++;
                tribbleGreyRect.Height++;
                collisions++;
            }

            // Orange Tribble Motion Updates:
            tribbleOrangeRect.X += (int)tribbleOrangeSpeed.X;
            tribbleOrangeRect.Y += (int)tribbleOrangeSpeed.Y;

            if (tribbleOrangeRect.Right > _graphics.PreferredBackBufferWidth || tribbleOrangeRect.Left < 0)
            {
                tribbleOrangeSpeed.X *= -1;
                tribbleOrangeRect.Width++;
                tribbleOrangeRect.Height++;
                collisions++;
            }
            if (tribbleOrangeRect.Bottom + 100 > _graphics.PreferredBackBufferHeight || tribbleOrangeRect.Top < 0)
            {
                tribbleOrangeSpeed.Y *= -1;
                tribbleOrangeRect.Width++;
                tribbleOrangeRect.Height++;
                collisions++;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            _spriteBatch.Begin();
            if (collisions % 2 == 0)
            {
                _spriteBatch.Draw(space1Texture, new Vector2(0, 0), Color.White);
            }
            else if (collisions % 2 != 0)
            {
                _spriteBatch.Draw(space2Texture, new Vector2(0, 0), Color.White);
            }


            _spriteBatch.Draw(tribbleBrownTexture, tribbleBrownRect, Color.White);
            _spriteBatch.Draw(tribbleCreamTexture, tribbleCreamRect, Color.White);
            _spriteBatch.Draw(tribbleGreyTexture, tribbleGreyRect, Color.White);
            _spriteBatch.Draw(tribbleOrangeTexture, tribbleOrangeRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}