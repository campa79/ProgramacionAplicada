using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Cleopatra.Sprites
{
    public class ColeccionNubes : Actualizable // Nubes de fondo
    {
        protected static Random random;
        protected int time;
        TimeSpan nubetime;
        public ColeccionNubes()
        {
            if (random == null)
                random = new Random();
            time = random.Next(0, 150);
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Subtract(nubetime).Milliseconds >=time)
            {
                nubetime = gameTime.TotalGameTime;
                Game1.TheGame.Actualizaciones.Add(new Nubes());
                time = random.Next(100, 900); // Cantidad de nubes quiero que vayan saliendo
            }
        }
    }
}
