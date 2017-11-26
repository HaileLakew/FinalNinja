using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalNinja
{
    class VideoManager
    {
        Video video;
        VideoPlayer player;
        Texture2D videoTexture;

        bool forceStop;

        public VideoManager()
        {

        }

        public void LoadContent()
        {
            ContentManager myManager = new ContentManager(
                    ScreenManager.Instance.Content.ServiceProvider, "Content");

            video = myManager.Load<Video>("video");
            player = new VideoPlayer();

            forceStop = false;
        }

        public void Update(GameTime gameTime)
        {
            if (player.State == MediaState.Stopped && !forceStop)
            {
                player.IsLooped = true;
                player.Play(video);
            }
        }

        public void stopVideo()
        {
            player.Stop();
            forceStop = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Only call GetTexture if a video is playing or paused
            if (player.State != MediaState.Stopped)
                videoTexture = player.GetTexture();

            // Drawing to the rectangle will stretch the 
            // video to fill the screen
            Rectangle screen = new Rectangle(0,
                0,
                900,
                600);

            // Draw the video, if we have a texture to draw.
            if (videoTexture != null)
            {
                spriteBatch.Draw(videoTexture, screen, Color.White);
            }
        }
    }
}
