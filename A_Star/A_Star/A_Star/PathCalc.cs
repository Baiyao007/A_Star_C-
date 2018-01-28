//作成者：柏
//作成日：2016.12.29
//内　容：経路探索計算用クラス
//最後修正者：
//修　正　日：
//修正内容：

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace A_Star
{
    enum Director {
        Left,
        Up,
        Right,
        Down,
    }

    class PathCalc
    {
        private Vector2 targetPosition;
        private Vector2 startPosition;
        private List<Vector2> offsets;
        private List<CalcTile> open;
        private List<CalcTile> close;
        private int[,] checkMap;
        private int checkCussor;
        private List<CalcTile> path;

        public PathCalc() {
            targetPosition = -Vector2.One;
            open = new List<CalcTile>();
            close = new List<CalcTile>();
            path = new List<CalcTile>();
        }

        public void Initialize() {
            offsets = new List<Vector2>() {
                new Vector2(-1, 0), //左
                new Vector2( 0,-1), //上
                new Vector2( 1, 0), //右
                new Vector2( 0, 1), //下
            };
        }

        public void ClearMemory() {
            open.Clear();
            close.Clear();
            path.Clear();
        }

        private bool IsClosed(Vector2 position) {
            CalcTile check = close.Find(c => c.Position == position);
            return check != null;
        }

        private bool IsOpen(Vector2 position) {
            CalcTile check = open.Find(c => c.Position == position);
            return check != null;
        }

        public void Calculate() {
            if (!CanMove(targetPosition)) { return; }

            CalcTile player = new CalcTile(startPosition);
            player.Father = player;
            close.Add(player);
            PutInClose(player);

            checkCussor = 0;
            int count = 0;
            while (close[checkCussor].Position != targetPosition) {
                count++;
                UpdateCheck(close[checkCussor]);
                if (count > 1000) { return; }
            }
        }

        public void GetPath() {
            if (!CanMove(targetPosition)) { return; }
            if (close[checkCussor].Position != targetPosition) { return; }
            path.Add(close[checkCussor]);
            while (path[path.Count - 1].Father != path[path.Count - 1]) {
                path.Add(path[path.Count - 1].Father);
            }
        }

        private void PutInClose(CalcTile tile) {
            close.Add(tile);
        }

        private bool CanMove(Vector2 position) {
            return !(checkMap[(int)position.Y, (int)position.X] == -1);
        }

        private void UpdateCheck(CalcTile checkCussor) {
            for (int i = 0; i < offsets.Count; i++) {
                NewTileOne(offsets[i]);
            }
            
            CheckNext();
            
        }

        private void CheckNext() {
            if (open.Count == 0) { return; }
            int minNo = -1;
            for (int i = 0; i < open.Count; i++) {
                if (open[i].Father != close[checkCussor]) { continue; }
                if (minNo == -1) { minNo = i; }
                if (open[minNo].GH < open[i].GH) { continue; }
                if (open[minNo].GH == open[i].GH) {
                    if(open[minNo].G < open[i].G) { continue; }
                }
                minNo = i;
            }

            if (minNo == -1) {
                CalcTile c = close[checkCussor];
                close.RemoveAt(checkCussor);
                close.Insert(0, c);
            }
            else {
                close.Add(open[minNo]);
                open.Remove(open[minNo]);
                checkCussor++;
            }
            
        }


        private void NewTileOne(Vector2 offset) {
            Vector2 tilePosition = close[checkCussor].Position + offset;
            
            if(IsClosed(tilePosition)) { return; }
            if (!CanMove(tilePosition)) { return; }

            if (IsOpen(tilePosition)) {
                CalcTile check = open.Find(x => x.Position == tilePosition);
                int checkG = CalcG(check);
                if (checkG < check.G) {
                    check.G = checkG;
                    check.Father = close[checkCussor];
                }
            } else {
                CalcTile tile = new CalcTile(tilePosition);
                tile.Father = close[checkCussor];
                tile.G = CalcG(tile);
                tile.H = CalcH(tilePosition, targetPosition);
                open.Add(tile);
            }
            
        }

        private int CalcG(CalcTile tile) {
            return (tile.Father.Position == startPosition) ? 10 : tile.Father.G + 10;
        }

        private int CalcH(Vector2 v1,Vector2 v2) {
            int tx = (int)Math.Abs(v1.X - v2.X);
            int ty = (int)Math.Abs(v1.Y - v2.Y);
            return (tx + ty) * 10;
        }



        public void SetCheckMap(int[,] mapData) {
            int width = mapData.GetLength(1);
            int height = mapData.GetLength(0);

            checkMap = new int[height, width];
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    if (mapData[y, x] == (int)MapDataNo.Wall) {
                        checkMap[y, x] = -1;
                    }
                }
            }
        }

        public void SetTarget(Vector2 target) {
            targetPosition = target;
        }
        public void SetStart(Vector2 start) {
            startPosition = start;
        }

        private bool HaveTarget() {
            return targetPosition != -Vector2.One;
        }

        public void Draw(Renderer renderer) {
            if (!HaveTarget()) { return; }
            if (!CanMove(targetPosition)) {
                renderer.DrawTexture("target", Method.ToMapPosition(targetPosition));
                return;
            }
            

            for (int i = 0; i < close.Count; i++) {
                close[i].Draw(renderer);
                renderer.DrawTexture("cussor", Method.ToMapPosition(close[i].Position));
                Rectangle rect = GetArrow(close[i]);
                renderer.DrawTexture("arrow", Method.ToMapPosition(close[i].Position), rect);
            }

            for (int i = 0; i < open.Count; i++) {
                open[i].Draw(renderer);
                renderer.DrawTexture("calc_cussor", Method.ToMapPosition(open[i].Position));
                Rectangle rect = GetArrow(open[i]);
                renderer.DrawTexture("calc_arrow", Method.ToMapPosition(open[i].Position), rect);
            }

            for (int i = 0; i < path.Count; i++) {
                renderer.DrawTexture("target", Method.ToMapPosition(path[i].Position));
            }

        }

        private Rectangle GetArrow(CalcTile tile) {
            int tileSize = Parameter.WallSize;

            Vector2 director = tile.Position - tile.Father.Position;

            if (director == offsets[(int)Director.Left]) {
                return new Rectangle(tileSize * (int)Director.Left , 0, tileSize, tileSize);
            }
            else if (director == offsets[(int)Director.Up]) {
                return new Rectangle(tileSize * (int)Director.Up   , 0, tileSize, tileSize);
            }
            else if (director == offsets[(int)Director.Right]) {
                return new Rectangle(tileSize * (int)Director.Right, 0, tileSize, tileSize);
            }
            else {
                return new Rectangle(tileSize * (int)Director.Down , 0, tileSize, tileSize);
            }
        }

    }
}
