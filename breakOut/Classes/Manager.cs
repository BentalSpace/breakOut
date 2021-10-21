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

        Image red, orange, yellow, green, blue, indigo, purple;
        public Manager(Player player, Ball ball, Brick brick) {
            this.player = player;
            this.ball = ball;
            this.brick = brick;
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
                    else if(map.brickMap[i, j] == 2)
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
        public void ballMove() {
            if (ball.posX <= 20 || ball.posX >= 795)
                ball.moveX *= -1;
            if (ball.posY <= 70)
                ball.moveY *= -1;
            ball.posX += ball.moveX;
            ball.posY += ball.moveY;
        }
        public void ballBrickTouch() {
            for (int i = 0; i < map.brickMap.GetLength(0); i++) {
                for (int j = 0; j < map.brickMap.GetLength(1); j++) {
                    if (map.brickMap[i, j] != 0) {
                        if (ball.posX >= (30 + (60 * (j - 1))) - 16 && ball.posX <= (30 + (60 * (j - 1))) + 50
                            && ball.posY >= 100 + ((i + 1) * 30) && ball.posY <= 100 + ((i + 1) * 30) + 20)
                            map.brickMap[i, j] = 0;
                    }
                }
            }
        }
    }
}
