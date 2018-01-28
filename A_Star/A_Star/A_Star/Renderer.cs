//作成者：柏
//作成日：2016.12.29
//内　容：描画用クラス
//最後修正者：
//修　正　日：
//修正内容：

using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace A_Star
{
    class Renderer
    {
        private ContentManager contentManager;
        private SpriteBatch spriteBatch;
        private Dictionary<string, Texture2D> textures;
        private SpriteFont font;

        public Renderer(ContentManager content, GraphicsDevice graphics) {
            contentManager = content;
            spriteBatch = new SpriteBatch(graphics);
            textures = new Dictionary<string, Texture2D>();
        }

        public void Initialize() {
            LoadTexture("wall");
            LoadTexture("player");
            LoadTexture("calc_cussor");
            LoadTexture("cussor");
            LoadTexture("target");
            LoadTexture("grid");
            LoadTexture("calc_arrow");
            LoadTexture("arrow");
            font = contentManager.Load<SpriteFont>("./SpriteFont1");
        }

        public void LoadTexture(string name,string filePath = "./") {
            textures.Add(name, contentManager.Load<Texture2D>(filePath + name));
        }
        public void LoadTexture(string name, Texture2D texture) {
            textures.Add(name, texture);
        }
        
        public void DrawTexture(string name,Vector2 position,float alpha = 1.0f) {
            spriteBatch.Draw(textures[name], position, Color.White * alpha);
        }

        public void DrawTexture(string name, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            spriteBatch.Draw(textures[name], position, rect, Color.White * alpha);
        }

        public void DrawString(string text, Vector2 position) {
            spriteBatch.DrawString(font, text, position, Color.White);
        }

        public void UnloadContent() {
            textures.Clear();
        }

        public void Begin() {
            spriteBatch.Begin();
        }
        public void End() {
            spriteBatch.End();
        }

    }
}
