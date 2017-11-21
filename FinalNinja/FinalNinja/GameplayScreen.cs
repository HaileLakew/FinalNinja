using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalNinja
{
    public class GameplayScreen :GameScreen
    {
        Player playerb;
        Player playerw;
        Map map;

        public override void LoadContent()
        {
            base.LoadContent();

            XmlManager<Player> playerLoader = new XmlManager<Player>();
            XmlManager<Player> playerwLoader = new XmlManager<Player>();
            XmlManager<Map> mapLoader = new XmlManager<Map>();
            playerb = playerLoader.Load("Load/Playerb.xml");
            playerw = playerwLoader.Load("Load/Playerw.xml");
            map = mapLoader.Load("Load/Map1.xml");
            playerb.LoadContent();
            playerw.LoadContent();
            map.LoadContent();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
            playerb.UnloadContent();
            playerw.UnloadContent();
            map.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            playerb.Update(gameTime);
            playerw.Update(gameTime);
            
            map.Update(gameTime, ref playerb);
            map.Update(gameTime, ref playerw);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map.Draw(spriteBatch, "Underlay");//under the player
            playerb.Draw(spriteBatch);
            playerw.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");
        }
    }
}
