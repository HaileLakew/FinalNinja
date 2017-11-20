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
    //want to serialize a class inside a class
   
    public class SplashScreen : GameScreen
    {
        public Image Image;
        //public Vector2 Position;//must be public for .xml to serialize it. 

        //[XmlElement("Path")]//if the var was not named "Path" in Gamescreen
        //public string Path;//same as tag in .xml. searches at runtime the xml for PAth variable then loads it over (must be public for deserialization)

        //[XmlIgnore]//public but has private setter
        //public ContentManager Content;//couldnt serialize because it is public

        public override void LoadContent()
        {
            base.LoadContent();
            //image = content.Load<Texture2D>(path[0]);//image1, [1] is image2
            Image.LoadContent();
            //Image.FadeEffect.FadeSpeed = 0.5f;overwrites xml value
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Z))
                ScreenManager.Instance.ChangeScreens("TitleScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }
    }
    
}
