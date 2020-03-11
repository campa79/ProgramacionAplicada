using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace Cleopatra.Sprites
{
    public abstract class RectanguloAnimacion : Vivo
    {
        protected List<Rectangle> rectangulos;
        protected int selectedRectangle;

        public RectanguloAnimacion()
        {
            rectangulos = new List<Rectangle>();
        }

        public override void Draw(GameTime gameTime)
        {
            base.DrawVida(gameTime);
            Game1.TheGame.spriteBatch.Draw(Image, Rectangle, rectangulos[selectedRectangle], Color);
        }

    }

}
