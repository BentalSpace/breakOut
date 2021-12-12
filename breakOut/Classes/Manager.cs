using System.Drawing;
using System.Windows.Forms;

namespace breakOut {
    class Manager {
        public int stageNum;

        Player player;
        Ball ball;
        Brick brick;
        Map map;
        Label lblScore;

        public Manager(Player player, Ball ball, Brick brick, Label lblScore) {
            this.player = player;
            this.ball = ball;
            this.brick = brick;
            this.lblScore = lblScore;
            map = new Map();
        }
        public Manager() { }
        //public void StageClear(int clearItem, int currentItem) {
        //     if(2 <= currentItem) {
        //        stageNum++;
        //        // ball.startNow = true; //이게 왜 null인데
        //        //brick.attackBrick = 0;
        //    }
        //     if(stageNum >= 4) {
        //        //스테이지 클리어
        //    }
        //}

        public void Drawbackground(Graphics g) {
            Image background;
            background = Image.FromFile(Application.StartupPath + @"\images\background.png");
            g.DrawImage(background, 0, 50);
        }
        public void startGame(KeyEventArgs e) {
            if (e.KeyCode == Keys.Space && ball.startNow) {
                ball.moveY[0] = 1;
                ball.startNow = false;
            }
        }

        public void ballPlayerTouch() {
            for (int ballNum = 0; ballNum < ball.ballCount; ballNum++)
                if (ball.posY[ballNum] + 16 >= player.PosY && ball.posY[ballNum] <= player.PosY - 3) {
                    if (player.playerSize.Equals("M")) {
                        if (ball.posX[ballNum] >= player.PosX && ball.posX[ballNum] + 16 <= player.PosX + 100) {
                            float temp = ball.posX[ballNum] - player.PosX;

                            if (temp <= 42) { // 왼쪽에 부딪힌거
                                if (temp <= 8) {
                                    ball.moveX[ballNum] = -5;
                                    ball.moveY[ballNum] = -2;
                                }
                                else {
                                    ball.moveX[ballNum] = -4;
                                    ball.moveY[ballNum] = -3;
                                }
                            }
                            else {
                                temp -= 42;
                                if (temp >= 34) {
                                    ball.moveX[ballNum] = 5;
                                    ball.moveY[ballNum] = -2;
                                }
                                else {
                                    ball.moveX[ballNum] = 4;
                                    ball.moveY[ballNum] = -3;
                                }
                            }
                        }
                        ball.timeBallSpeed(ballNum);
                    }
                    else if (player.playerSize.Equals("B")) {
                        if (ball.posX[ballNum] >= player.PosX && ball.posX[ballNum] + 16 <= player.PosX + 130) {
                            float temp = ball.posX[ballNum] - player.PosX; // 130-16 114 -> 57

                            if (temp <= 57) {
                                if (temp <= 8) {
                                    ball.moveX[ballNum] = -5;
                                    ball.moveY[ballNum] = -2;
                                }
                                else {
                                    ball.moveX[ballNum] = -4;
                                    ball.moveY[ballNum] = -3;
                                }
                            }
                            else {
                                temp -= 57;
                                if (temp >= 49) {
                                    ball.moveX[ballNum] = 5;
                                    ball.moveY[ballNum] = -2;
                                }
                                else {
                                    ball.moveX[ballNum] = 4;
                                    ball.moveY[ballNum] = -3;
                                }
                            }
                        }
                        ball.timeBallSpeed(ballNum);
                    }
                    else if (player.playerSize.Equals("S")) {
                        if (ball.posX[ballNum] >= player.PosX && ball.posX[ballNum] + 16 <= player.PosX + 60) {
                            float temp = ball.posX[ballNum] - player.PosX; // 60-16 44 -> 22
                            if (temp <= 22) {
                                if (temp <= 8) {
                                    ball.moveX[ballNum] = -5;
                                    ball.moveY[ballNum] = -2;
                                }
                                else {
                                    ball.moveX[ballNum] = -4;
                                    ball.moveY[ballNum] = -3;
                                }
                            }
                            else {
                                temp -= 22;
                                if (temp >= 14) {
                                    ball.moveX[ballNum] = 5;
                                    ball.moveY[ballNum] = -2;
                                }
                                else {
                                    ball.moveX[ballNum] = 4;
                                    ball.moveY[ballNum] = -3;
                                }
                            }
                        }
                        ball.timeBallSpeed(ballNum);
                    }
                }
        }
        public void ballItemCatch() {
            brick.itemSpawn[0] = false;
            if (ball.ballCount == 3)
                return;
            if (ball.ballCount == 1) {
                ball.posX[1] = ball.posX[2] = ball.posX[0];
                ball.posY[1] = ball.posY[2] = ball.posY[0];

                ball.moveX[1] = ball.moveX[0] * -1;
                ball.moveY[1] = ball.moveY[0];

                ball.moveX[2] = ball.moveX[0] + 1;
                ball.moveY[2] = ball.moveY[0] - 1;
            }
            else if (ball.ballCount == 2) {
                ball.posX[2] = ball.posX[0];
                ball.posY[2] = ball.posY[0];
                ball.moveX[2] = ball.moveX[0] * -1;
                ball.moveY[2] = ball.moveY[0];
            }
            ball.ballCount = 3;
            ball.calcPosX[1] = ball.posX[1];
            ball.calcPosY[1] = ball.posY[1];
            ball.calcPosX[2] = ball.posX[2];
            ball.calcPosY[2] = ball.posY[2];
        }
        public void scoreUpdate() {
            lblScore.Text = "SCORE : " + brick.score.ToString();
        }
        public void itemPlayerTouch() {
            if (!brick.itemSpawn[0] && !brick.itemSpawn[1] && !brick.itemSpawn[2])
                return;
            if (brick.itemSpawn[0]) {
                if (brick.itemPos[0, 1] + 18 >= player.PosY && brick.itemPos[0, 1] + 18 <= player.PosY + 20) {
                    if (player.playerSize.Equals("M")) {
                        if (brick.itemPos[0, 0] + 35 >= player.PosX && brick.itemPos[0, 0] + 5 <= player.PosX + 100) {
                            ballItemCatch();
                            brick.score += 60;
                        }
                    }
                    else if (player.playerSize.Equals("B")) {
                        if (brick.itemPos[0, 0] + 35 >= player.PosX && brick.itemPos[0, 0] + 5 <= player.PosX + 130) {
                            ballItemCatch();
                            brick.score += 30;
                        }
                    }
                    else if (player.playerSize.Equals("S")) {
                        if (brick.itemPos[0, 0] + 35 >= player.PosX && brick.itemPos[0, 0] + 5 <= player.PosX + 60) {
                            ballItemCatch();
                            brick.score += 90;
                        }
                    }
                }
            }
            if (brick.itemSpawn[1]) {
                if (brick.itemPos[1, 1] + 18 >= player.PosY && brick.itemPos[1, 1] + 18 <= player.PosY + 20) {
                    if (player.playerSize.Equals("M")) {
                        if (brick.itemPos[1, 0] + 35 >= player.PosX && brick.itemPos[1, 0] + 5 <= player.PosX + 100) {
                            brick.itemSpawn[1] = false;
                            player.player = Image.FromFile(Application.StartupPath + @"\images\playerBig.png");
                            player.playerSize = "B";
                            brick.score += 60;
                        }
                    }
                    else if (player.playerSize.Equals("B")) {
                        if (brick.itemPos[1, 0] + 35 >= player.PosX && brick.itemPos[1, 0] + 5 <= player.PosX + 130) {
                            brick.itemSpawn[1] = false;
                            brick.score += 30;
                        }
                    }
                    else if (player.playerSize.Equals("S")) {
                        if (brick.itemPos[1, 0] + 35 >= player.PosX && brick.itemPos[1, 0] + 5 <= player.PosX + 60) {
                            brick.itemSpawn[1] = false;
                            player.player = Image.FromFile(Application.StartupPath + @"\images\playerNormal.png");
                            player.playerSize = "M";
                            brick.score += 90;
                        }
                    }
                }
            }
            if (brick.itemSpawn[2]) {
                if (brick.itemPos[2, 1] + 18 >= player.PosY && brick.itemPos[2, 1] + 18 <= player.PosY + 20) {
                    if (player.playerSize.Equals("M")) {
                        if (brick.itemPos[2, 0] + 35 >= player.PosX && brick.itemPos[2, 0] + 5 <= player.PosX + 100) {
                            brick.itemSpawn[2] = false;
                            player.player = Image.FromFile(Application.StartupPath + @"\images\playerSmall.png");
                            player.playerSize = "S";
                            brick.score += 60;
                        }
                    }
                    else if (player.playerSize.Equals("B")) {
                        if (brick.itemPos[2, 0] + 35 >= player.PosX && brick.itemPos[2, 0] + 5 <= player.PosX + 130) {
                            brick.itemSpawn[2] = false;
                            player.player = Image.FromFile(Application.StartupPath + @"\images\playerNormal.png");
                            player.playerSize = "M";
                            brick.score += 30;
                        }
                    }
                    else if (player.playerSize.Equals("S")) {
                        if (brick.itemPos[2, 0] + 35 >= player.PosX && brick.itemPos[2, 0] + 5 <= player.PosX + 60) {
                            brick.itemSpawn[2] = false;
                            brick.score += 90;
                        }
                    }
                }
            }
        }
    }
}
