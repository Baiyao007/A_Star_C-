//作成者：柏
//作成日：2016.12.29
//内　容：通用メソッド管理用クラス
//最後修正者：
//修　正　日：
//修正内容：

using Microsoft.Xna.Framework;

namespace A_Star
{
    static class Method
    {
        public static Vector2 ToMapPosition(Vector2 mapchip)
        {
            int tx = (int)mapchip.X * Parameter.WallSize;
            int ty = (int)mapchip.Y * Parameter.WallSize;
            return new Vector2(tx, ty);
        }


        public static Vector2 ToMapchip(Vector2 position)
        {
            int mx = (int)position.X / Parameter.WallSize;
            int my = (int)position.Y / Parameter.WallSize;
            return new Vector2(mx, my);
        }
    }
}
