﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalNinja
{
    public class Tile
    {
        Vector2 position;
        Rectangle sourceRect;
        string state;//can player go through

        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect, string state)
        {
            this.position= position;
            this.sourceRect = sourceRect;
            this.state = state;
        }
        public void UnloadContent()
        {
        }
        public void Update(GameTime gameTime, ref Player player)
        {
            if (state == "Solid")
            {
                Rectangle tileRect = new Rectangle((int)Position.X, (int)Position.Y, sourceRect.Width, sourceRect.Height);
                Rectangle playerRect = new Rectangle((int)player.Image.Position.X, (int)player.Image.Position.Y, player.Image.SourceRect.Width, player.Image.SourceRect.Height);

                if (playerRect.Intersects(tileRect))
                {
                    if (player.Velocity.Y < 0)
                        player.Image.Position.Y = tileRect.Bottom;
                    else if (player.Velocity.Y > 0)
                    {
                        player.Image.Position.Y = tileRect.Top - player.Image.SourceRect.Height;
                    }
                    else if (player.Velocity.X < 0)
                        player.Image.Position.X = tileRect.Right;
                    else if (player.Velocity.X > 0)
                        player.Image.Position.X = tileRect.Left - player.Image.SourceRect.Width;//x coord is left side of player, so when colliding on the left side of the obstacle, need to offset the player


                    player.Velocity = Vector2.Zero;
                }
                else if(player.Velocity.Y==0)
                    if (player.Velocity.Y < 3)
                        player.Velocity.Y += player.MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
        }

    }
}
