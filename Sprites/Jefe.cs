using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleopatra.Sprites
{
    public class Jefe : RectanguloAnimacion
    {
        public enum Estados
        {
            Volando, Daño, Muerte, Destruir
        }
        public int estado = (int)Estados.Volando;

        TimeSpan lasttime, frametime;
        public Jefe()
        {
            Image = Game1.TheGame.Content.Load<Texture2D>("Images/Tormenta");
            Rectangle = new Rectangle(50, 50, 80, 80);
            Vida = 30;
            var w = Image.Width / 6;
            for (int i = 0; i < 6; i++)
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
            if (gameTime.TotalGameTime.Subtract(frametime).Milliseconds > 200)
            {
                frametime = gameTime.TotalGameTime;
                selectedRectangle++;
                if (selectedRectangle > 1)
                    selectedRectangle = 0;
            }
            #region coordenadas

            int x = Rectangle.X;

            x -= 2;

            Rectangle = new Rectangle(x, Rectangle.Y,
                Rectangle.Width, Rectangle.Height);

            if (Rectangle.X < -100)
            {
                Game1.TheGame.Actualizaciones.Add(this);
            }

            #endregion

            #region Choque

            if (gameTime.TotalGameTime.Subtract(lasttime).Milliseconds > 500)
            {
                lasttime = gameTime.TotalGameTime;
                Cleo LaCleo = null;
                foreach (var item in Game1.TheGame.sprites)
                {
                    if (item is Cleo)
                    {
                        LaCleo = item as Cleo;
                        break;
                    }
                }
                foreach (var item in Game1.TheGame.sprites)
                {
                    if (item is Cleo)
                    {
                        LaCleo = item as Cleo;
                        break;
                    }
                }
                if (LaCleo == null)
                {
                    throw new NullReferenceException("No esta Cleo???");
                }
                if (Rectangle.Intersects(LaCleo.Rectangle))
                {
                    SoundEffect sonido = Game1.TheGame.Sounds[Game1.Sonidos.Grito];
                    sonido.CreateInstance();
                    sonido.Play();
                    Explosion explosion = new Explosion(Rectangle.Location.X, Rectangle.Location.Y);
                    LaCleo.Vida -= 20;
                    LaCleo.estado = 1;
                    Game1.TheGame.Actualizaciones.Add(this);
                    Game1.TheGame.Actualizaciones.Add(explosion);
                }


            }
            #endregion

            if (Vida <= 0)
            {
                Explosion explosion = new Explosion(Rectangle.Location.X, Rectangle.Location.Y);
                Game1.TheGame.Actualizaciones.Add(this);
                Game1.TheGame.Actualizaciones.Add(explosion);
            }

        }
    }

    }
