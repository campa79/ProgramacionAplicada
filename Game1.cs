using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using Cleopatra.Sprites;

namespace Cleopatra
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public enum Fuentes
        {
            Estadisticas, BigFont, Verdana
        }
        public enum Sonidos
        {
            Explosion, Disparo,Grito
        }
        public Dictionary<Fuentes, SpriteFont> Fonts { get; private set; }
        internal GraphicsDeviceManager graphics { get; private set; }
        internal SpriteBatch spriteBatch { get; private set; }
        internal static Game1 TheGame { get; private set; }
        internal List<Actualizable> sprites { get; private set; }
        internal List<Sprite> Actualizaciones { get; private set; }
        public Dictionary<Sonidos, SoundEffect> Sounds { get; private set; }
        public bool IsGameOver { get; internal set; } = false;

        Song miCancion;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TheGame = this;
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
            Fonts = new Dictionary<Fuentes, SpriteFont>();
            Actualizaciones = new List<Sprite>();
            sprites = new List<Actualizable>();
            Sounds = new Dictionary<Sonidos, SoundEffect>();

            miCancion = Content.Load<Song>("Audio/musicaS");

            Sounds.Add(Sonidos.Explosion,
                        Content.Load<SoundEffect>("Audio/explosionS"));
            Sounds.Add(Sonidos.Grito,
                        Content.Load<SoundEffect>("Audio/gritoS"));
            Sounds.Add(Sonidos.Disparo,
                         Content.Load<SoundEffect>("Audio/Laser"));
            Fonts.Add(Fuentes.BigFont,
            Content.Load<SpriteFont>("Fonts/BigFont"));
            Fonts.Add(Fuentes.Estadisticas,
                        Content.Load<SpriteFont>("Fonts/Stats"));
            Fonts.Add(Fuentes.Verdana,
             Content.Load<SpriteFont>("Fonts/Verdana"));

            sprites.Add(new Fondo());
            sprites.Add(new Cleo());
            sprites.Add(new ColeccionNubes());
            sprites.Add(new ColecccionBombas());
            
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

        bool firstRun = true;
        protected override void Update(GameTime gameTime)
        {
            if (firstRun)
            {
               MediaPlayer.Play(miCancion);
                firstRun = false;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!IsGameOver)
            {
                foreach (var item in sprites)
                {
                    item.Update(gameTime);
                }
                foreach (var item in Actualizaciones)
                {
                    if (sprites.Contains(item))
                        sprites.Remove(item);
                    else
                        sprites.Add(item);
                }
                Actualizaciones.Clear();
                base.Update(gameTime);
            }
            else if (IsGameOver)
            {
                   foreach (var item in Game1.TheGame.sprites)
                {
                    if (item is Cleo)
                    {
                        item.Update(gameTime);
                        break;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    IsGameOver = false;
                    LoadContent();
                }
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (var item in sprites)
            {
                if (item is Sprite)
                    ((Sprite)item).Draw(gameTime);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
