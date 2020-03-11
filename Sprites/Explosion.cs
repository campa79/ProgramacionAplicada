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
    public class Explosion: RectanguloAnimacion
    {
        bool firstRun = true;
        public Explosion(int XE,int YE)
        {
            Image = Game1.TheGame.Content.Load<Texture2D>("Images/explosion");
            Rectangle = new Rectangle(XE,YE,50, 50);
            Vida = 100;
            var w = Image.Width / 7;
            for (int i = 0; i < 7; i++)
            {
                rectangulos.Add(new Rectangle(w * i, 0, w, Image.Height));
            }
        }
        TimeSpan  frametime;
        public override void Update(GameTime gameTime)
        {
            if (firstRun)
            {
                SoundEffect sonido = Game1.TheGame.Sounds[Game1.Sonidos.Explosion];
                sonido.CreateInstance();
                sonido.Play();
                firstRun = false;
            }
            if (gameTime.TotalGameTime.Subtract(frametime).Milliseconds > 200)
            {
                frametime = gameTime.TotalGameTime;
                selectedRectangle++;
                if (selectedRectangle > 6)
                    Game1.TheGame.Actualizaciones.Add(this);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            Game1.TheGame.spriteBatch.Draw(Image, Rectangle, rectangulos[selectedRectangle], Color);
        }

    }
}
