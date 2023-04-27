using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Animation_Part_1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D tribbleBrownTexture, tribbleCreamTexture, tribbleGreyTexture, tribbleOrangeTexture;
        Rectangle tribbleGreyRect;
        Vector2 tribbleGreySpeed;

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
            tribbleGreyRect = new Rectangle(300, 10, 100, 100);

            tribbleGreySpeed = new Vector2(0, 2); // setting the initial speed of the object
            // this vector2 stores 2 values that represent the speed of an object broken into x & y components

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            tribbleGreyRect.X += (int)tribbleGreySpeed.X;
            tribbleGreyRect.Y += (int)tribbleGreySpeed.Y; 
        
            if (tribbleGreyRect.Right > _graphics.PreferredBackBufferWidth || tribbleGreyRect.Left < 0)
                tribbleGreySpeed.X *= -1;

            if (tribbleGreyRect.Bottom > _graphics.PreferredBackBufferHeight || tribbleGreyRect.Top < 0)
                tribbleGreyRect.Y *= -1;

                
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            _spriteBatch.Begin();

            _spriteBatch.Draw(tribbleGreyTexture, tribbleGreyRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}