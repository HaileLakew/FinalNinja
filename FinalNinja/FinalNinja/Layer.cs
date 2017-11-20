﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FinalNinja
{
    public class Layer
    {
        public class TileMap
        {
            [XmlElement("Row")]
            public List<string> Row;

            public TileMap()
            {
                Row = new List<string>();
            }
        }
        [XmlElement("TileMap")]
        public TileMap Tile;
        public Image Image;
        public string SolidTiles, OverlayTiles;//which tiles are indeed solid
        List<Tile> underlayTiles, overlayTiles;
        string state;

        public Layer()
        {
            Image = new Image();
            underlayTiles = new List<Tile>();
            overlayTiles = new List<Tile>();
            SolidTiles = OverlayTiles=string.Empty;
        }
        
        public void LoadContent(Vector2 tileDimensions)
        {
            Image.LoadContent();
            Vector2 position = -tileDimensions;//starts at "-1" so, after incrementing, it begins at 0.
            foreach (string row in this.Tile.Row)
            {
                string[] split = row.Split(']');
                position.X = -tileDimensions.X;
                position.Y+= tileDimensions.Y;
                foreach (string s in split)
                {

                    if (s != String.Empty)
                    {
                        position.X += tileDimensions.X;
                        if (!s.Contains("x"))
                        {
                            state = "Passive";
                            Tile tile = new Tile();

                            string str = s.Replace("[", String.Empty);
                            int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                            int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));

                            if (SolidTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                            {
                                state = "Solid";
                            }


                            tile.LoadContent(position, new Rectangle(value1 * (int)tileDimensions.X,
                                value2 * (int)tileDimensions.Y, (int)tileDimensions.X, (int)tileDimensions.Y), state);

                            if (OverlayTiles.Contains("[" + value1.ToString() + ":" + value2.ToString() + "]"))
                            {
                                overlayTiles.Add(tile);
                            }
                            else
                                underlayTiles.Add(tile);
                        }
                    }
                }
            }
        }
        
        public void UnloadContent()
        {
            Image.UnloadContent();
        }
        
        public void Update(GameTime gameTime, ref Player player)
        {
            foreach (Tile tile in underlayTiles)
                tile.Update(gameTime, ref player);

            foreach(Tile tile in overlayTiles)
                tile.Update(gameTime, ref player);
        }
        
        public void Draw(SpriteBatch spriteBatch, string drawType)//comes from tile.cs
        {
            List<Tile> tiles;
            if (drawType == "Underlay")
                tiles = underlayTiles;
            else
                tiles = overlayTiles;

            foreach (Tile tile in tiles)
            {
                Image.Position = tile.Position;
                Image.SourceRect = tile.SourceRect;
                Image.Draw(spriteBatch);
            }
        }
    }
}