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
    public class Player
    {
        public Image Image;
        public Vector2 Velocity;
        public float MoveSpeed;
        public Vector2 Coords;
        public bool Class;//true=b, false=w

        public bool dead = false;

        public Player()
        {
            Velocity = Vector2.Zero;
            Coords = new Vector2(100, 100);
        }

        public void LoadContent()
        {
            Image.LoadContent(Coords);
        }

        public void UnloadContent()
        {
            Image.UnloadContent();
        }

        private bool attack;

        public void Update(GameTime gameTime, Vector2 pbLoc, Vector2 pwLoc)
        {

            attack = (pbLoc.X - pwLoc.X <= 30 && pbLoc.X - pwLoc.X >= -30)  && (pbLoc.Y - pwLoc.Y <= 30 && pbLoc.Y - pwLoc.Y >= -30);

            Image.IsActive = true;
            if (Class)
            {
                if (InputManager.Instance.KeyDown(Keys.Right))
                {
                    Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                }
                else if (InputManager.Instance.KeyDown(Keys.Left))
                {
                    Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 1;
                }
                else
                    Velocity.X = 0;

                if (InputManager.Instance.KeyDown(Keys.Down))
                {
                    if (attack)
                        dead = true;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 4;
                }

                if (InputManager.Instance.KeyPressed(Keys.Up))
                    Velocity.Y += -16;
                else
                    Velocity.Y += 1;

                if(Velocity.Y < 0)
                    if (Velocity.X < 0)
                        Image.SpriteSheetEffect.CurrentFrame.Y = 3;
                    else
                        Image.SpriteSheetEffect.CurrentFrame.Y = 2;

                if (Velocity.X == 0 && Velocity.Y > 0)
                    Image.IsActive = false;

                Image.Update(gameTime);
                Image.Position += Velocity;
            }
            else//white moveset
            {
                if (InputManager.Instance.KeyDown(Keys.D))
                {
                    Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                }
                else if (InputManager.Instance.KeyDown(Keys.A))
                {
                    Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 1;
                }
                else
                    Velocity.X = 0;

                if (InputManager.Instance.KeyDown(Keys.S))
                {
                    if (attack)
                        dead = true;
                    Image.SpriteSheetEffect.CurrentFrame.Y = 4;
                }

                if (InputManager.Instance.KeyPressed(Keys.W))
                    Velocity.Y += -16;
                else
                    Velocity.Y += 1;

                if (Velocity.Y < 0)
                    if (Velocity.X < 0)
                        Image.SpriteSheetEffect.CurrentFrame.Y = 3;
                    else
                        Image.SpriteSheetEffect.CurrentFrame.Y = 2;

                if (Velocity.X == 0 && Velocity.Y > 0)
                    Image.IsActive = false;

                Image.Update(gameTime);
                Image.Position += Velocity;

            }
            if (this.Image.Position.X <35)
                this.Image.Position.X = 35;
            if (this.Image.Position.X > 730)
                this.Image.Position.X = 730;
        }
            public void Draw(SpriteBatch spriteBatch)
            {
                Image.Draw(spriteBatch);
            }
    }
}