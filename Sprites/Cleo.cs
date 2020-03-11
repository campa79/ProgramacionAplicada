using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleopatra.Sprites
{
    public class Cleo : RectanguloAnimacion
    {
        public enum Estados
        {
            Volando, Daño, Muerte, Destruir
        }
        public int estado = (int)Estados.Volando;

        TimeSpan lasttime,  frametime;

        public int Score { get; internal set; }

        public Cleo()
        
        {
            Image = Game1.TheGame.Content.Load<Texture2D>("Images/Cleos");
            Rectangle = new Rectangle(50, 50, 80, 80); // Forma de imagen para Cleo
            Vida = 30;
            var w = Image.Width / 5;
            for (int i = 0; i < 5; i++)
            {
                rectangulos.Add(new Rectangle(w * i,
                    0,
                    w,
                    Image.Height));
            }
            selectedRectangle = 3;
        }

        public override void Update(GameTime gameTime)
        {
            int x, y;
            x = Rectangle.X;
            y = Rectangle.Y;
            if (estado == (int)Estados.Volando)
            {
                if (gameTime.TotalGameTime.Subtract(frametime).Milliseconds > 250)
                {
                    frametime = gameTime.TotalGameTime;
                    selectedRectangle++;
                    if (selectedRectangle > 4)
                        selectedRectangle = 3;
                }
            }
            if (estado == (int)Estados.Daño)
            {
                selectedRectangle = 0;
                estado = (int)Estados.Volando;

            }
            if (Vida <= 0) // Si vida es menor o igual a 0 = Defino Game Over
            {
                estado = (int)Estados.Muerte;
                frametime = gameTime.TotalGameTime;
                selectedRectangle++;
                if (selectedRectangle > 2)
                    selectedRectangle = 0;
                y += 5;
                
                if (estado != (int)Estados.Destruir) 
                  Game1.TheGame.Actualizaciones.Add(this);
            }
            #region Coordenadas
            if (estado == (int)Estados.Volando)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    x -= 5;
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    x += 5;
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    y -= 5;
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    y += 5;

                if (x > Game1.TheGame.GraphicsDevice.Viewport.Width - Rectangle.Width)
                    x = Game1.TheGame.GraphicsDevice.Viewport.Width - Rectangle.Width;
                else if (x < 0)
                    x = 0;

                if (y > Game1.TheGame.GraphicsDevice.Viewport.Height - Rectangle.Height)
                    y = Game1.TheGame.GraphicsDevice.Viewport.Height - Rectangle.Height;
                else if (y < 0)
                    y = 0;

                #endregion

                if (
                    Keyboard.GetState().IsKeyDown(Keys.Space) &&
                    gameTime.TotalGameTime.Subtract(lasttime).Milliseconds > 300
                    )
                {
                    lasttime = gameTime.TotalGameTime;
                    Misil misil = new Misil(this, (Rectangle.X + Rectangle.Width) - 10, Rectangle.Y + 15);
                    Game1.TheGame.Actualizaciones.Add(misil);

                }
            }
            Rectangle = new Rectangle(x, y,
                            Rectangle.Width,
                            Rectangle.Height);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Game1.TheGame.spriteBatch.DrawString(
                Game1.TheGame.Fonts[Game1.Fuentes.Estadisticas],"Presiona SPACE para tirarle pelotas a las tormentas! - Puntos:"+Score.ToString(), new Vector2(20, 20), Color.White);
            if (Vida <= 0)
            {
                Game1.TheGame.spriteBatch.DrawString(Game1.TheGame.Fonts[Game1.Fuentes.BigFont],"Perdiste Cleo!",new Vector2(50, 150),
                                                     Color.Blue);
                Game1.TheGame.spriteBatch.DrawString(Game1.TheGame.Fonts[Game1.Fuentes.Verdana],
                                                     "Presiona enter para volver a empezar.",
                                                     new Vector2(40, 300),
                                                     Color.Yellow);
                Game1.TheGame.IsGameOver = true;
            }
        }
    }
}

