using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Runtime.Serialization.Formatters;

namespace Animation_Part_1
{
    public class Game1 : Game
    {
        enum Screen
        {
            Intro,
            TribbleYard
        }
        Screen screen; // This variable will keep track of what screen/level we are on

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        MouseState mouseState;
        Texture2D tribbleBrownTexture, tribbleCreamTexture, tribbleGreyTexture, tribbleOrangeTexture, space1Texture, space2Texture, introBCKGDTexture, explosionTexture;
        SoundEffect tribbleCooSFX, suspenseSFX, explosionSFX;
        SoundEffectInstance explosionSFXInstance;
        Song cinematicSong;
        SpriteFont font;
        Rectangle tribbleBrownRect, tribbleCreamRect, tribbleGreyRect, tribbleOrangeRect, background, explosionRect; 
        Vector2 tribbleBrownSpeed, tribbleCreamSpeed, tribbleGreySpeed, tribbleOrangeSpeed;
        Random generator = new Random();
        int collisions = 0, suspenseSFXPlayed = 1, cinematicSongPlayed = 1, explosionSFXPlayed = 1;
        bool explosionTime = false, tribblesMoving = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Intro;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            background = new Rectangle(0, 0, 800, 600);
            explosionRect = new Rectangle(0, 0, 800, 600);

            tribbleBrownRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
            tribbleBrownSpeed = new Vector2(3, 0);

            tribbleCreamRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
            tribbleCreamSpeed = new Vector2(3, 2);

            tribbleGreyRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
            tribbleGreySpeed = new Vector2(3, 2); // setting the initial speed of the object
            // this vector2 stores 2 values that represent the speed of an object broken into x & y components

            tribbleOrangeRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
            tribbleOrangeSpeed = new Vector2(0, 4);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            space1Texture = Content.Load<Texture2D>("space1");
            space2Texture = Content.Load<Texture2D>("space2");
            explosionTexture = Content.Load<Texture2D>("explosionIMG");

            tribbleBrownTexture = Content.Load<Texture2D>("tribbleBrown");
            tribbleCreamTexture = Content.Load<Texture2D>("tribbleCream");
            tribbleGreyTexture = Content.Load<Texture2D>("tribbleGrey");
            tribbleOrangeTexture = Content.Load<Texture2D>("tribbleOrange");
            introBCKGDTexture = Content.Load<Texture2D>("introBackground");

            font = Content.Load<SpriteFont>("Text");

            tribbleCooSFX = Content.Load<SoundEffect>("tribble_coo");
            suspenseSFX = Content.Load<SoundEffect>("Suspense1");
            explosionSFX = Content.Load<SoundEffect>("explosion");
            cinematicSong = Content.Load<Song>("CinematicIntro");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mouseState = Mouse.GetState();
            if (screen == Screen.Intro)
            {
                if (suspenseSFXPlayed == 1)
                {
                    suspenseSFX.Play();
                    suspenseSFXPlayed = 0;
                }
                if (mouseState.LeftButton == ButtonState.Pressed && background.Contains(mouseState.X, mouseState.Y))
                {
                    screen = Screen.TribbleYard;
                }
            }
            else if (screen == Screen.TribbleYard)
            {
                if (cinematicSongPlayed == 1)
                {
                    MediaPlayer.Play(cinematicSong);
                    cinematicSongPlayed = 0;
                }
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    tribbleBrownSpeed.X = 0; tribbleBrownSpeed.Y = 0;
                    tribbleCreamSpeed.X = 0; tribbleCreamSpeed.Y = 0;
                    tribbleGreySpeed.X = 0; tribbleGreySpeed.Y = 0;
                    tribbleOrangeSpeed.X = 0; tribbleOrangeSpeed.Y = 0;


                    explosionTime = true;
                    tribblesMoving = false;
                    explosionSFXInstance = explosionSFX.CreateInstance();
                    if (explosionSFXPlayed == 1)
                    {
                        explosionSFXInstance.Play();
                        explosionSFXPlayed = 0;
                    }
                    if (explosionSFXPlayed == 0 && explosionSFXInstance.State == SoundState.Stopped)
                    {
                        Exit();
                    }
                }
                if (tribblesMoving == true)
                {
                    // Brown Tribble Motion Updates:
                    tribbleBrownRect.X += (int)tribbleBrownSpeed.X;
                    tribbleBrownRect.Y += (int)tribbleBrownSpeed.Y;

                    if (tribbleBrownRect.Right > _graphics.PreferredBackBufferWidth || tribbleBrownRect.Left < 0)
                    {
                        tribbleBrownSpeed.X *= -1;
                        tribbleBrownRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
                        collisions++;
                        tribbleCooSFX.Play();
                    }

                    if (tribbleBrownRect.Bottom + 100 > _graphics.PreferredBackBufferHeight || tribbleBrownRect.Top < 0)
                    {
                        tribbleBrownSpeed.Y *= -1;
                        tribbleBrownRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
                        collisions++;
                        tribbleCooSFX.Play();
                    }

                    // Cream Tribble Motion Updates:
                    tribbleCreamRect.X += (int)tribbleCreamSpeed.X;
                    tribbleCreamRect.Y += (int)tribbleCreamSpeed.Y;

                    if (tribbleCreamRect.Right > _graphics.PreferredBackBufferWidth || tribbleCreamRect.Left < 0)
                    {
                        tribbleCreamSpeed.X *= -1;
                        tribbleCreamRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
                        collisions++;
                        tribbleCooSFX.Play();
                    }
                    if (tribbleCreamRect.Bottom + 100 > _graphics.PreferredBackBufferHeight || tribbleCreamRect.Top < 0)
                    {
                        tribbleCreamSpeed.Y *= -1;
                        tribbleCreamRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
                        collisions++;
                        tribbleCooSFX.Play();
                    }
                    // Gray Tribble Motion Updates:
                    tribbleGreyRect.X += (int)tribbleGreySpeed.X;
                    tribbleGreyRect.Y += (int)tribbleGreySpeed.Y;

                    if (tribbleGreyRect.Right > _graphics.PreferredBackBufferWidth || tribbleGreyRect.Left < 0)
                    {
                        tribbleGreySpeed.X *= -1;
                        tribbleGreyRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
                        collisions++;
                        tribbleCooSFX.Play();
                    }
                    if (tribbleGreyRect.Bottom + 100 > _graphics.PreferredBackBufferHeight || tribbleGreyRect.Top < 0)
                    {
                        tribbleGreySpeed.Y *= -1;
                        tribbleGreyRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
                        collisions++;
                        tribbleCooSFX.Play();
                    }

                    // Orange Tribble Motion Updates:
                    tribbleOrangeRect.X += (int)tribbleOrangeSpeed.X;
                    tribbleOrangeRect.Y += (int)tribbleOrangeSpeed.Y;

                    if (tribbleOrangeRect.Right > _graphics.PreferredBackBufferWidth || tribbleOrangeRect.Left < 0)
                    {
                        tribbleOrangeSpeed.X *= -1;
                        tribbleOrangeRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
                        collisions++;
                        tribbleCooSFX.Play();
                    }
                    if (tribbleOrangeRect.Bottom + 100 > _graphics.PreferredBackBufferHeight || tribbleOrangeRect.Top < 0)
                    {
                        tribbleOrangeSpeed.Y *= -1;
                        tribbleOrangeRect = new Rectangle(generator.Next(200, 601), generator.Next(200, 201), 100, 100);
                        collisions++;
                        tribbleCooSFX.Play();
                    }
                }
                
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            _spriteBatch.Begin();

            if (screen == Screen.TribbleYard)
            {
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

                if (explosionTime == true)
                {
                    _spriteBatch.Draw(explosionTexture, explosionRect, Color.White);
                }
            }
           else if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introBCKGDTexture, background, Color.White);
                _spriteBatch.DrawString(font, "CLICK ANYWHERE TO BEGIN", new Vector2(0, 0), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}