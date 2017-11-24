using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalNinja
{
    public class InfoTile
    {
        public Image Image;
        public Vector2 Velocity;
        public float MoveSpeed;
        public Vector2 Coords;

        public InfoTile()
        {
            Velocity = Vector2.Zero;
            Coords = new Vector2(0, 0);
            MoveSpeed = 100;
        }

        public void LoadContent()
        {
            Image.LoadContent(Coords);
            this.Image.Alpha = 0;
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            Image.IsActive = true;
            Random rnd = new Random(DateTime.Now.Millisecond);

            if (this.Image.Position.Y < 50)
                Velocity.Y = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            else
                Velocity.Y = 0;


            Image.Update(gameTime);
            Image.Position += Velocity;
            this.Image.Alpha += ((float)this.Image.Position.Y/ 400);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}
