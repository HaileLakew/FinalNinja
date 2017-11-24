using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace FinalNinja
{
    public class MovingTile
    {
        public Image Image;
        public Vector2 Velocity;
        public float MoveSpeed;
        public Vector2 Coords;

        public MovingTile()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            Velocity = Vector2.Zero;
            Coords = new Vector2(rnd.Next(0, 700), rnd.Next(0, 100));
            MoveSpeed = rnd.Next(80, 150);
        }

        public void LoadContent()
        {
            Image.LoadContent(Coords);
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {

            Image.IsActive = true;
            Random rnd = new Random(DateTime.Now.Millisecond);

            Velocity.Y = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;


            int threshold = rnd.Next(0, 800);

            Image.Update(gameTime);
            Image.Position += Velocity;

            if (this.Image.Position.Y < 0)
            {
                this.Image.Alpha = 1;
                this.Image.Position.Y = rnd.Next(500, 800);
                MoveSpeed = rnd.Next(20, 150);
            }
            else if (this.Image.Position.Y < threshold)
                this.Image.Alpha -= (1 / (float)this.Image.Position.Y);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
}