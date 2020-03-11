using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Cleopatra.Sprites
{
    public abstract class Vivo : Sprite
    {
        public virtual int Vida { get; set; }
        protected Texture2D vida;

        public override void Draw(GameTime gameTime)
        {
            DrawVida(gameTime);

            base.Draw(gameTime);
        }

        protected void DrawVida(GameTime gameTime)
        {
            if (Vida > 0)
            {
                vida = new Texture2D(Game1.TheGame.GraphicsDevice, Vida, 20);
                Color[] data = new Color[Vida * 20];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Color.LightGreen;

                vida.SetData(data);
                Game1.TheGame.spriteBatch.Draw(vida, new Vector2(Rectangle.X, Rectangle.Y), Color.White);
            }
        }
    }
}
