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
    public class Bombas:RectanguloAnimacion
    {

        public Bombas()
        {
            Image = Game1.TheGame.Content.Load<Texture2D>("Images/nube");
            Rectangle = new Rectangle(Game1.TheGame.GraphicsDevice.Viewport.Width,
            random.Next(Game1.TheGame.GraphicsDevice.Viewport.Height - 80),
            75, 50); // Forma de las Nubes: Ancho y Alto 
            Vida = 100; // Vida de las Nubes
            var w = Image.Width / 1; // Movimiento o Intermitencia de la Nube (distinto de 1)
            for (int i = 0; i < 2; i++)
            {
                rectangulos.Add(new Rectangle(w * i, 0, w, Image.Height));
            }

        }

        TimeSpan lasttime, frametime;
        public override void Update(GameTime gameTime)
        { 
            if (gameTime.TotalGameTime.Subtract(frametime).Milliseconds > 500) // Velocidad que se mueve la nube
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
                }             foreach (var item in Game1.TheGame.sprites)
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
