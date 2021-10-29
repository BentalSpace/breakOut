using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace breakOut {
    class Manager {
        Player player;
        Ball ball;
        Brick brick;
        Map map;
        Label lblTest;

        string s;

        Image red, orange, yellow, green, blue, indigo, purple;
        public Manager(Player player, Ball ball, Brick brick, Label lblTest) {
            this.player = player;
            this.ball = ball;
            this.brick = brick;
            this.lblTest = lblTest;
            map = new Map();
        }

        public void Drawbackground(Graphics g) {
            Image background;
            background = Image.FromFile(Application.StartupPath + @"\images\background.png");
            g.DrawImage(background, 0, 50);
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
        public void startGame(KeyEventArgs e) {
            if(e.KeyCode == Keys.Space && ball.startNow) {
                ball.moveY = 1;
                ball.startNow = false;
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
        public void ballMove() {
            if (ball.posX <= 20 || ball.posX >= 795)
                ball.moveX *= -1;
            if (ball.posY <= 70)
                ball.moveY *= -1;
            ball.posX += ball.moveX;
            ball.posY += ball.moveY;

            lblTest.Text = player.PosX.ToString() + ", " + (ball.posX+16).ToString();
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

                        if(ball.posY >= 100 + (30 * (i + 1)) + 17 && ball.posY <= 100 + (30 * (i + 1)) + 20){ // 벽돌 아래 부분
                            if(ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50
                                || ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 50) {
                                map.brickMap[i, j] = 0;
                                ball.moveY *= -1;
                            }
                        }
                        else if(ball.posY + 16 >= 100 + (30 * (i + 1)) && ball.posY + 16 <= 100 + (30 * (i + 1)) + 3) { // 벽돌 위 부분
                            if (ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50
                                || ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 50) {
                                map.brickMap[i, j] = 0;
                                ball.moveY *= -1;
                            }
                        }
                        else if(ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 3) { // 벽돌 왼쪽 부분
                            if(ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20
                                || ball.posY + 16 >= 100 + (30 * (i + 1)) && ball.posY + 16 <= 100 + (30 * (i + 1)) + 20) {
                                map.brickMap[i, j] = 0;
                                ball.moveX *= -1;
                            }
                        }
                        else if(ball.posX >= (30 + (60 * (j - 1))) + 47 && ball.posX <= (30 + (60 * (j - 1))) + 50) { // 벽돌 오른쪽 부분
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
        public void ballPlayerTouch() {
            if (ball.posY + 16 >= player.PosY && ball.posY <= player.PosY - 3) {
                if(ball.posX >= player.PosX && ball.posX+16 <= player.PosX + 100) {
                    float temp = ball.posX - player.PosX;

                    if(temp <= 42) {
                        if(temp/8 <= 1.5f)
                            ball.moveX = -3.5f;
                        else if(temp/8 >= 3.5f)
                            ball.moveX = -1.5f;
                        else 
                            ball.moveX = (temp / 10) * -1;
                        ball.moveY = (5 - (ball.moveX * -1)) * -1;
                    }
                    else {
                        temp -= 42;
                        if (temp/8 >= 3.5f)
                            ball.moveX = 3.5f;
                        else if (temp/8 <= 1.5f)
                            ball.moveX = 1.5f;
                        else
                            ball.moveX = temp / 10;
                        ball.moveY = (5 - ball.moveX) * -1;
                    }
                }
            }
            //lblTest.Text = ball.moveX + ", " + ball.moveY;
        }
    }
}
