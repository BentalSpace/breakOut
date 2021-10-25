using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace breakOut {
    class Ball {
        public float posX = 405;
        public float posY = 600;
        public float moveX = -1.5f;
        public float moveY = 3.5f;

        public void drawBall(Graphics g) {
            Image ball;
            ball = Image.FromFile(Application.StartupPath + @"\images\ball.png");
            g.DrawImage(ball, posX, posY);
        }
    }
}
