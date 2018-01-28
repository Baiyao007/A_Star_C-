//作成者：柏
//作成日：2016.12.29
//内　容：常数管理用クラス
//最後修正者：
//修　正　日：
//修正内容：

using Microsoft.Xna.Framework;

namespace A_Star
{
    static class Parameter
    {
        public const int WallSize = 64;
        public const int StageWidth = 20;
        public const int StageHeigth = 11;
        public const int ScreenWidth = StageWidth * WallSize;
        public const int ScreenHeigth = StageHeigth * WallSize;
        public static readonly Vector2 G_Offset = new Vector2(5, 40);
        public static readonly Vector2 H_Offset = new Vector2(35, 40);
        public static readonly Vector2 GH_Offset = new Vector2(5, 5);
        
    }
}
