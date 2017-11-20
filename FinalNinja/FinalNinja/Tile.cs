using System;
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
                    /*Console.WriteLine("asd");
                    if (playerRect.Left <= tileRect.Right)
                        player.Image.Position.X = tileRect.Right;
                    else if (playerRect.Right >= tileRect.Left)
                        player.Image.Position.X = tileRect.Left - player.Image.SourceRect.Width;
                    else if (playerRect.Top <= tileRect.Bottom)
                        player.Image.Position.Y = tileRect.Bottom;
                    else
                        player.Image.Position.Y = tileRect.Top - player.Image.SourceRect.Height;
                    */
                    
                    if (player.Velocity.X < 0)
                        player.Image.Position.X = tileRect.Right;
                    else if (player.Velocity.X > 0)
                        player.Image.Position.X = tileRect.Left - player.Image.SourceRect.Width;//x coord is left side of player, so when colliding on the left side of the obstacle, need to offset the player
                    else if (player.Velocity.Y < 0)
                        player.Image.Position.Y = tileRect.Bottom;
                    else
                        player.Image.Position.Y = tileRect.Top - player.Image.SourceRect.Height;
                    
                    player.Velocity = Vector2.Zero;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
        }

    }
}
