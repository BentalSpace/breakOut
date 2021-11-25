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
            if(e.KeyCode == Keys.Space && ball.startNow) {
                ball.moveY = 1;
                ball.startNow = false;
            }
        }
        
        public void ballPlayerTouch() {
            if (ball.posY + 16 >= player.PosY && ball.posY <= player.PosY - 3) {
                if(ball.posX >= player.PosX && ball.posX+16 <= player.PosX + 100) {
                    float temp = ball.posX - player.PosX;

                    if(temp <= 42) { // 왼쪽에 부딪힌거
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
                        if(temp >= 34) {
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
    }
}
