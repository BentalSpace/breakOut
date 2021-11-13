using System.Drawing;
using System.Windows.Forms;

namespace breakOut {
    class Ball {
        public float posX = 405;
        public float posY = 790 - 17;
        public float moveX = 0; //-1.5f;                    //   25 34 43 52 
        public float moveY = 0; //3.5f;
        public float calcPosX = 405;
        public float calcPosY = 790 - 17;

        public bool startNow = true;
        public string dir = null;

        Player player;

        public Ball(Player player) {
            this.player = player;
        }

        public void drawBall(Graphics g) {
            Image ball;
            ball = Image.FromFile(Application.StartupPath + @"\images\ball.png");

            if (startNow) {
                calcPosX = player.PosX + 42;
                g.DrawImage(ball, posX, posY);
            }
            else
                g.DrawImage(ball, posX, posY);
        }
        public void ballCalcMove() {
            if (posX <= 20 || posX >= 795) // 옆쪽 벽
                moveX *= -1;
            if (posY <= 70) // 위쪽 벽
                moveY *= -1;

            if (dir == "U" || dir == "D") {
                moveY *= -1;
                dir = null;
            }
            else if(dir == "L" || dir == "R") {
                moveX *= -1;
                dir = null;
            }

            calcPosX += moveX;
            calcPosY += moveY;
        }
        public void ballRealMove() { // 벽돌과의 계산 후 호출
            posX = calcPosX;
            posY = calcPosY;
        }
    }
}
