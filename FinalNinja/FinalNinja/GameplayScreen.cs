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
    public class GameplayScreen :GameScreen
    {
        private int tileCount = 500;

        Player playerb;
        Player playerw;

        List <MovingTile> blackTileList = new List<MovingTile>();
        List<MovingTile> whiteTileList = new List<MovingTile>();

        Vector2 pbLoc;
        Vector2 pwLoc;

        Map map;

        public override void LoadContent()
        {
            base.LoadContent();

            XmlManager<Player> playerLoader = new XmlManager<Player>();
            XmlManager<Player> playerwLoader = new XmlManager<Player>();

            XmlManager<MovingTile> blackTileLoader = new XmlManager<MovingTile>();

            XmlManager<Map> mapLoader = new XmlManager<Map>();
            playerb = playerLoader.Load("Load/Playerb.xml");
            playerw = playerwLoader.Load("Load/Playerw.xml");

            for (int x = 0; x < tileCount; x++)
                blackTileList.Add(blackTileLoader.Load("Load/BlackTile.xml"));

            for (int x = 0; x < tileCount; x++)
                whiteTileList.Add(blackTileLoader.Load("Load/WhiteTile.xml"));

            map = mapLoader.Load("Load/Map1.xml");
            playerb.LoadContent();
            playerw.LoadContent();

            for (int x = 0; x < tileCount; x++)
            { 
                blackTileList[x].LoadContent();
                whiteTileList[x].LoadContent();
            }

            map.LoadContent();
        }
        public override void UnloadContent()
        {
            base.UnloadContent();
            playerb.UnloadContent();
            playerw.UnloadContent();

            for (int x = 0; x < tileCount; x++)
            {
                blackTileList[x].UnloadContent();
                whiteTileList[x].UnloadContent();
            }

            map.UnloadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            pbLoc = playerb.Image.Position;
            pwLoc = playerw.Image.Position;

            if (!playerw.dead)
                playerb.Update(gameTime, pbLoc, pwLoc);
            if (!playerb.dead)
                playerw.Update(gameTime, pbLoc, pwLoc);

            for (int x = 0; x < tileCount; x++)
            { 
                blackTileList[x].Update(gameTime);
                whiteTileList[x].Update(gameTime);
            }

            map.Update(gameTime, ref playerb);
            map.Update(gameTime, ref playerw);

            if (InputManager.Instance.KeyPressed(Keys.Q))
                ScreenManager.Instance.ChangeScreens("TitleScreen");
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            map.Draw(spriteBatch, "Underlay");//under the player

            for (int x = 0; x < tileCount; x++)
            {
                if (!playerb.dead)
                    blackTileList[x].Draw(spriteBatch);
                if (!playerw.dead)
                    whiteTileList[x].Draw(spriteBatch);
            }
            if (!playerw.dead)
                playerb.Draw(spriteBatch);
            if (!playerb.dead)
                playerw.Draw(spriteBatch);
            map.Draw(spriteBatch, "Overlay");
        }
    }
}
