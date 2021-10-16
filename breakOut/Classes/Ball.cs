using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace breakOut {
    class Ball {
        public int posX = 405;
        public int posY = 704;
        public int moveX = -5;
        public int moveY = -5;

        public void drawBall(Graphics g) {
            Image ball;
            ball = Image.FromFile(Application.StartupPath + @"\images\ball.png");
            g.DrawImage(ball, posX, posY);
        }
    }
}
