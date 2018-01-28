//作成者：柏
//作成日：2016.12.29
//内　容：ステージ管理用クラス
//最後修正者：
//修　正　日：
//修正内容：

using Microsoft.Xna.Framework;

namespace A_Star
{
    enum MapDataNo
    {
        None,
        Wall,
        Player = 9,
    }
    class Stage
    {
        private int[,] mapData;
        public Stage() {
            mapData = new int[,] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 1},
                { 1, 0, 0, 9, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
            };
        }

        public Vector2 GetPlayer() {
            for (int y = 0; y < mapData.GetLength(0); y++) {
                for (int x = 0; x < mapData.GetLength(1); x++) {
                    if (mapData[y, x] == (int)MapDataNo.Player) {
                        return new Vector2(x, y);
                    }
                }
            }
            return Vector2.Zero;
        }

        public int[,] MapData {
            get { return mapData; }
        }
        public void Draw(Renderer renderer) {
            for (int y = 0; y < mapData.GetLength(0); y++) {
                for (int x = 0; x < mapData.GetLength(1); x++) {
                    Vector2 position = Method.ToMapPosition(new Vector2(x, y));
                    renderer.DrawTexture("grid", position);
                    if(mapData[y, x] == (int)MapDataNo.Wall) {
                        Rectangle rect = new Rectangle(0, 0, Parameter.WallSize, Parameter.WallSize);
                        renderer.DrawTexture("wall", position, rect);
                    }
                }
            }
        }
    }
}
