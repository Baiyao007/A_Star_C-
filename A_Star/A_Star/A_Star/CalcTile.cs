using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A_Star
{
    class CalcTile
    {
        private Vector2 position;
        private int g_Value;
        private int h_Value;
        private bool isCheck;
        CalcTile father;

        public CalcTile(Vector2 position) {
            this.position = position;
            isCheck = false;
        }

        public int G {
            get { return g_Value; }
            set { g_Value = value; }
        }

        public int H {
            get { return h_Value; }
            set { h_Value = value; }
        }

        public int GH {
            get { return G + H; }
        }

        public bool IsCheck {
            get { return isCheck; }
            set { isCheck = value; }
        }

        public Vector2 Position {
            get { return position; }
        }

        public CalcTile Father {
            get { return father; }
            set { father = value; }
        }
        public void Draw(Renderer renderer) {
            renderer.DrawTexture("calc_cussor", Method.ToMapPosition(position));
            Vector2 g_Position = Method.ToMapPosition(position) + Parameter.G_Offset;
            Vector2 h_Position = Method.ToMapPosition(position) + Parameter.H_Offset;
            Vector2 gh_Position = Method.ToMapPosition(position) + Parameter.GH_Offset;
            renderer.DrawString(G.ToString(), g_Position);
            renderer.DrawString(H.ToString(), h_Position);
            renderer.DrawString((G + H).ToString(), gh_Position);
        }
    }
}
