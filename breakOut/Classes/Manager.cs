using System.Drawing;
using System.Windows.Forms;

namespace breakOut {
    class Manager {

        Player player;
        Ball ball;
        Brick brick;
        Map map;
        Label lblTest;

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
        public void startGame(KeyEventArgs e) {
            if (e.KeyCode == Keys.Space && ball.startNow) {
                ball.moveY = 1;
                ball.startNow = false;
            }
        }

        public void ballPlayerTouch() {
            if (player.playerSize == "M")
                if (ball.posY + 16 >= player.PosY && ball.posY <= player.PosY - 3) {
                    if (ball.posX >= player.PosX && ball.posX + 16 <= player.PosX + 100) {
                        float temp = ball.posX - player.PosX;

                        if (temp <= 42) { // 왼쪽에 부딪힌거
                            if (temp <= 8) {
                                ball.moveX = -5;
                                ball.moveY = -2;
                            }
                            else {
                                ball.moveX = -4;
                                ball.moveY = -3;
                            }
                        }
                        else {
                            temp -= 42;
                            if (temp >= 34) {
                                ball.moveX = 5;
                                ball.moveY = -2;
                            }
                            else {
                                ball.moveX = 4;
                                ball.moveY = -3;
                            }
                        }
                    }
                }

        }
        public void itemPlayerTouch() {
            if (!brick.itemSpawn[0] && !brick.itemSpawn[1] && !brick.itemSpawn[2])
                return;
            if (brick.itemPos[1, 1] + 18 >= player.PosY && brick.itemPos[1, 1] + 18 <= player.PosY + 20) {
                if (brick.itemPos[1, 0] + 30 >= player.PosX && brick.itemPos[1, 0] <= player.PosX + 90) {
                    brick.itemSpawn[1] = false;
                    player.player = Image.FromFile(Application.StartupPath + @"\images\playerBig.png");
                    player.playerSize = "B";
                }
            }
        }
    }
}
