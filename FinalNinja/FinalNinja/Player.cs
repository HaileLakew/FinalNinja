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
        public bool grounded;
        public bool Class;//true=b, false=w

        public Player()
        {
            Velocity = Vector2.Zero;
            Coords = new Vector2(100, 100);
            grounded = false;
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
            if (Class)
            {
                if (!grounded)//jumping
                {
                    if (Velocity.Y < 3)
                        Velocity.Y += MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //Image.SpriteSheetEffect.CurrentFrame.Y = 2;


                    if (InputManager.Instance.KeyDown(Keys.Right))
                    {
                        Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Image.SpriteSheetEffect.CurrentFrame.Y = 2;
                    }
                    else if (InputManager.Instance.KeyDown(Keys.Left))
                    {
                        Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Image.SpriteSheetEffect.CurrentFrame.Y = 3;
                    }
                    else
                        Velocity.X = 0;
                }
                if (grounded)
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

                    if (InputManager.Instance.KeyDown(Keys.Up) && grounded)
                    {
                        Velocity.Y = -20;//-MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                        grounded = false;
                    }
                    else Velocity.Y = 0;
                }

                if (Velocity.X == 0 && Velocity.Y == 0)
                    Image.IsActive = false;

                Image.Update(gameTime);
                Image.Position += Velocity;

            }
            else//white moveset
            {
                if (!grounded)//jumping
                {
                    if (Velocity.Y < 3)
                        Velocity.Y += MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    //Image.SpriteSheetEffect.CurrentFrame.Y = 2;


                    if (InputManager.Instance.KeyDown(Keys.D))
                    {
                        Velocity.X = MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Image.SpriteSheetEffect.CurrentFrame.Y = 2;
                    }
                    else if (InputManager.Instance.KeyDown(Keys.A))
                    {
                        Velocity.X = -MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Image.SpriteSheetEffect.CurrentFrame.Y = 3;
                    }
                    else
                        Velocity.X = 0;
                }
                if (grounded)
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

                    if (InputManager.Instance.KeyDown(Keys.W) && grounded)
                    {
                        Velocity.Y = -20;//-MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        Image.SpriteSheetEffect.CurrentFrame.Y = 0;
                        grounded = false;
                    }
                    else Velocity.Y = 0;
                }

                if (Velocity.X == 0 && Velocity.Y == 0)
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