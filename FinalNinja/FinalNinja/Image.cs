using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalNinja
{
    public class Image
    {
        public float Alpha;
        public string Text, FontName, Path;
        public Rectangle SourceRect;
        public bool IsActive;

        public Texture2D Texture;
        public Vector2 Position, Scale;
        Vector2 origin;
        ContentManager content;//used to load everything
        RenderTarget2D renderTarget;
        SpriteFont font;
        Dictionary<string, ImageEffect> effectList;//contains effect, and parent instance.
        public string Effects;//comes from XML

        public FadeEffect FadeEffect;//inherits from imageEffect. Cant modify specific effects from dictionary, since imageeffect is parent class, and cant access derived members.

        public SpriteSheetEffect SpriteSheetEffect;

        void setEffect<T>(ref T effect)//take reference, able to modify "parent" value in effectList.
        {
            if (effect == null)//
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);//obj is Image
            }
            effectList.Add(effect.GetType().ToString().Replace("FinalNinja.", ""), (effect as ImageEffect));//effect as ImageEffect is value.
        }

        public void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
            }
        }

        public void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }

        public void StoreEffects()
        {
            Effects=String.Empty;
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                    Effects += effect.Key + ":";
            }
            if(Effects!=String.Empty)
                Effects.Remove(Effects.Length - 1);//removes last colon
            
        }

        public void RestoreEffects()
        {
            foreach (var effect in effectList)
            {
                DeactivateEffect(effect.Key);
            }
            string[] split = Effects.Split(':');
            foreach (string s in split)
                ActivateEffect(s);
        }

        public Image()
        {
            Path=Text= Effects =String.Empty;
            FontName = "Fonts/Calibri";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            effectList = new Dictionary<string, ImageEffect>();
        }

        public void LoadContent()
        {
            content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (Path != String.Empty)
                Texture = content.Load<Texture2D>(Path);
            font=content.Load<SpriteFont>(FontName);
            
            Vector2 dimensions = Vector2.Zero;

            if(Texture != null)
                dimensions.X += Texture.Width;
            dimensions.X += font.MeasureString(Text).X;

            if(Texture != null)
                dimensions.Y=Math.Max(Texture.Height, font.MeasureString(Text).Y);
            else
                dimensions.Y=font.MeasureString(Text).Y;
            
            if(SourceRect==Rectangle.Empty)
            {
                SourceRect = new Rectangle(0,0, (int)dimensions.X, (int)dimensions.Y);
            }
            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)dimensions.X, (int)dimensions.Y);
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();
            if (Texture != null)
                ScreenManager.Instance.SpriteBatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.DrawString(font, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.SpriteBatch.End();

            Texture = renderTarget;//sets text and image to one image
            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

            setEffect<FadeEffect>(ref FadeEffect);
            setEffect<SpriteSheetEffect>(ref SpriteSheetEffect);

            if (Effects != String.Empty)//loop through effects and activate them 
            {
                string[] split = Effects.Split(':');
                foreach (string item in split)
                {
                    ActivateEffect(item);
                }
            }
         }

        public void UnloadContent()
        {
            content.Unload();
            foreach (var effect in effectList)
            {
                DeactivateEffect(effect.Key);

            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                if(effect.Value.IsActive)
                    effect.Value.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);
            spriteBatch.Draw(Texture, Position + origin, SourceRect, Color.White * Alpha, 
                0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
