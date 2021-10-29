using System.Drawing;
using System.Windows.Forms;

namespace breakOut {
    class Ball {
        public float posX = 405;
        public float posY = 790 - 17;
        public float moveX = 0; //-1.5f;
        public float moveY = 0; //3.5f;
        public bool startNow = true;

        Player player;

        public Ball(Player player) {
            this.player = player;
        }

        public void drawBall(Graphics g) {
            Image ball;
            ball = Image.FromFile(Application.StartupPath + @"\images\ball.png");

            if (startNow) {
                posX = player.PosX + 42;
                g.DrawImage(ball, posX, posY);
            }
            else
                g.DrawImage(ball, posX, posY);
        }
    }
}
