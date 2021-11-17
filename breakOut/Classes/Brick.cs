using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace breakOut {
    class Brick {
        private bool ballBrickCalcStart = false;

        Map map;
        Ball ball;

        Image red, orange, yellow, green, blue, indigo, purple;

        public Brick(Ball ball) {
            this.ball = ball;
            map = new Map();
        }
        public void drawBrick(Graphics g) {
            imageFileSet();
            for (int i = 0; i < map.brickMap.GetLength(0); i++) {
                for (int j = 1; j < map.brickMap.GetLength(1); j++) {
                    if (map.brickMap[i, j] == 1)
                        g.DrawImage(red, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[i, j] == 2)
                        g.DrawImage(orange, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[i, j] == 3)
                        g.DrawImage(yellow, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[i, j] == 4)
                        g.DrawImage(green, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[i, j] == 5)
                        g.DrawImage(blue, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[i, j] == 6)
                        g.DrawImage(indigo, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[i, j] == 7)
                        g.DrawImage(purple, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                }
            }
        }
        private void imageFileSet() {
            red = Image.FromFile(Application.StartupPath + @"\images\red.png");
            orange = Image.FromFile(Application.StartupPath + @"\images\orange.png");
            yellow = Image.FromFile(Application.StartupPath + @"\images\yellow.png");
            green = Image.FromFile(Application.StartupPath + @"\images\green.png");
            blue = Image.FromFile(Application.StartupPath + @"\images\blue.png");
            indigo = Image.FromFile(Application.StartupPath + @"\images\indigo.png");
            purple = Image.FromFile(Application.StartupPath + @"\images\purple.png");
        }

        public void ballBrickTouch() {
            for (int i = 0; i < map.brickMap.GetLength(0); i++) {
                for (int j = 0; j < map.brickMap.GetLength(1); j++) {
                    if (map.brickMap[i, j] != 0) {
                        //if ((ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50)
                        //    && (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20)) { // 왼쪽 위
                        //    map.brickMap[i, j] = 0;
                        //    ball.moveX *= -1;
                        //    ball.moveY *= -1;
                        //}
                        //else if (((ball.posX + 16) >= (30 + (60 * (j - 1))) && (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50) // 오른쪽 위
                        //    && (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20))
                        //    map.brickMap[i, j] = 0;
                        //else if ((ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50) // 왼쪽 아래
                        //    && ((ball.posY + 16) >= 100 + (30 * (i + 1)) && (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20))
                        //    map.brickMap[i, j] = 0;
                        //else if (((ball.posX + 16) >= (30 + (60 * (j - 1))) && (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50) // 오른쪽 아래
                        //    && ((ball.posY + 16) >= 100 + (30 * (i + 1)) && (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20))
                        //    map.brickMap[i, j] = 0;

                        if (ball.posY >= 100 + (30 * (i + 1)) + 17 && ball.posY <= 100 + (30 * (i + 1)) + 20) { // 벽돌 아래 부분
                            if (ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50
                                || ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 50) {
                                map.brickMap[i, j] = 0;
                                ball.moveY *= -1;
                            }
                        }
                        else if (ball.posY + 16 >= 100 + (30 * (i + 1)) && ball.posY + 16 <= 100 + (30 * (i + 1)) + 3) { // 벽돌 위 부분
                            if (ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50
                                || ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 50) {
                                map.brickMap[i, j] = 0;
                                ball.moveY *= -1;
                            }
                        }
                        else if (ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 3) { // 벽돌 왼쪽 부분
                            if (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20
                                || ball.posY + 16 >= 100 + (30 * (i + 1)) && ball.posY + 16 <= 100 + (30 * (i + 1)) + 20) {
                                map.brickMap[i, j] = 0;
                                ball.moveX *= -1;
                            }
                        }
                        else if (ball.posX >= (30 + (60 * (j - 1))) + 47 && ball.posX <= (30 + (60 * (j - 1))) + 50) { // 벽돌 오른쪽 부분
                            if (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20
                                || ball.posY + 16 >= 100 + (30 * (i + 1)) && ball.posY + 16 <= 100 + (30 * (i + 1)) + 20) {
                                map.brickMap[i, j] = 0;
                                ball.moveX *= -1;
                            }
                        }
                    }
                }
            }
        }
        public void ballBrickCalc() {
            for (int i = 0; i < map.brickMap.GetLength(0); i++) {
                for (int j = 0; j < map.brickMap.GetLength(1); j++) {
                    if (map.brickMap[i, j] != 0) {
                        if ((ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50)
                            && (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20)) { // 공의 왼쪽 위
                            ballBrickCalcStart = true;
                        }
                        else if (((ball.posX + 16) >= (30 + (60 * (j - 1))) && (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50) // 오른쪽 위
                            && (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20)) {
                            ballBrickCalcStart = true;
                        }
                        else if ((ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50) // 왼쪽 아래
                            && ((ball.posY + 16) >= 100 + (30 * (i + 1)) && (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20)) {
                            ballBrickCalcStart = true;
                        }
                        else if (((ball.posX + 16) >= (30 + (60 * (j - 1))) && (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50) // 오른쪽 아래
                            && ((ball.posY + 16) >= 100 + (30 * (i + 1)) && (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20)) {
                            ballBrickCalcStart = true;
                        }
                        if (ballBrickCalcStart) {
                            // 한칸씩 당겨서 경계에 확인
                            while (true) {
                                // 한칸씩 땡기기
                                if (ball.moveX >= 1) {
                                    ball.calcPosX -= 1;
                                }
                                else if (ball.moveX <= -1) {
                                    ball.calcPosX += 1;
                                }
                                if (ball.moveY >= 1) {
                                    ball.calcPosY -= 1;
                                }
                                else if (ball.moveY <= -1) {
                                    ball.calcPosY += 1;
                                }
                                if (!ballBrickCalcStart)
                                    return;
                                // 경계에 맞나 확인
                                if ((ball.calcPosY + 16) == 100 + (30 * (i + 1))
                                    /*&& (ball.calcPosX >= (30 + (60 * (j - 1))) || (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50)*/) { // 벽돌 위쪽
                                    //위쪽에 부딪힌 판정
                                    ball.dir = "U";
                                    map.brickMap[i, j] = 0;
                                    ballBrickCalcStart = false;
                                }
                                else if (ball.calcPosY == 100 + (30 * (i + 1)) + 20
                                    /*&& (ball.calcPosX >= (30 + (60 * (j - 1))) || (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50)*/) { // 벽돌 아래쪽
                                    //아래쪽에 부딪힌 판정
                                    ball.dir = "D";
                                    map.brickMap[i, j] = 0;
                                    ballBrickCalcStart = false;
                                }
                                else if ((ball.calcPosX + 16) == (30 + (60 * (j - 1)))
                                    /*&& (ball.posY >= 100 + (30 * (i + 1)) || (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20)*/) { // 벽돌 왼쪽
                                    //왼쪽에 부딪힌 판정    
                                    ball.dir = "L";
                                    map.brickMap[i, j] = 0;
                                    ballBrickCalcStart = false;
                                }
                                else if (ball.calcPosX == (30 + (60 * (j - 1))) + 50
                                   /* && (ball.posY >= 100 + (30 * (i + 1)) || (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20)*/) { // 벽돌 오른쪽
                                    //오른쪽에 부딪힌 판정
                                    ball.dir = "R";
                                    map.brickMap[i, j] = 0;
                                    ballBrickCalcStart = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
