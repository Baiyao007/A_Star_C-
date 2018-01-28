//作成者：柏
//作成日：2016.12.29
//内　容：Playerクラス
//最後修正者：
//修　正　日：
//修正内容：

using Microsoft.Xna.Framework;

namespace A_Star
{
    class Player
    {
        private Vector2 position;
        
        public Player(Stage stage) {
            position = stage.GetPlayer();
        }

        public void Update() { }

        public Vector2 Position {
            get { return position; }
        }
        public void Draw(Renderer renderer) {
            renderer.DrawTexture("player", Method.ToMapPosition(position));
        }

    }
}
