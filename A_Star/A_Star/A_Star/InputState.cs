//作成者：柏
//作成日：2016.12.29
//内　容：入力管理用クラス
//最後修正者：
//修　正　日：
//修正内容：

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace A_Star
{
    class InputState
    {
        Vector2 mousePosition;
        MouseState mouseState;
        
        public InputState() {
            mousePosition = -Vector2.One;
        }

        public void Update() {
            mouseState = Mouse.GetState();
        }

        public bool IsClickLeft() {
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                mousePosition = Method.ToMapchip(new Vector2(mouseState.X, mouseState.Y));
                return true;
            }
            return false;
        }

        public bool IsClickRight() {
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                mousePosition = -Vector2.One;
                return true;
            }
            return false;
        }


        public Vector2 MousePosition {
            get { return mousePosition; }
        }
    }
}
