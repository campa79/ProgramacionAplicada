using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Cleopatra.Sprites
{
   public class ColecccionBombas : Actualizable
    {
        protected static Random random;
        protected int time;
        TimeSpan bombatime;
        public ColecccionBombas()
        {
            if (random == null)
                random = new Random();
            time = random.Next(0, 1);
            //time = random.Next(0, 5); // Random para la aparición de Nubes Bomba
        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Subtract(bombatime).Seconds >= time)
            {
                bombatime = gameTime.TotalGameTime;
                Game1.TheGame.Actualizaciones.Add(new Bombas());
                time = random.Next(1, 2);
                //time = random.Next(2, 5); // Random para la aparición de Nubes Bomba
            }
        }
    }
}
