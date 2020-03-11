using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Cleopatra.Sprites
{
    public class Fondo : Sprite
    {
        public Fondo()
        {
            Image = Game1.TheGame.Content.Load<Texture2D>("Images/FondoA");
            Rectangle = Game1.TheGame.GraphicsDevice.Viewport.Bounds;
            Color = Color.White;
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.TheGame.spriteBatch.Draw(Image, Rectangle, Color);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
