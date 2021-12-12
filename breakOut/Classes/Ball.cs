using System.Drawing;
using System.Windows.Forms;

namespace breakOut {
    class Ball {
        public int ballCount = 1;
        public int changeBallIndex;
        public int speedTick;
        public float[] posX;// = 405;
        public float[] posY;// = 790 - 17;
        public float[] moveX; //-1.5f;                    //   25 34 43 52 
        public float[] moveY; //3.5f;
        public float[] calcPosX; // = 405;
        public float[] calcPosY; // = 790 - 17;

        public bool startNow = true;
        public string dir = null;

        Player player;
        public Label lblGameover;

        Image ball;
        public Ball(Player player, Label lblGameover) {
            this.player = player;
            this.lblGameover = lblGameover;

            posX = new float[3];
            posY = new float[3];
            moveX = new float[3];
            moveY = new float[3];
            calcPosX = new float[3];
            calcPosY = new float[3];

            calcPosY[0] = 600 - 17;

            ball = Image.FromFile(Application.StartupPath + @"\images\ball.png");
        }

        public void drawBall(Graphics g) {
            if (startNow) {
                calcPosX[0] = player.PosX + 42;

                g.DrawImage(ball, posX[0], posY[0]);
            }
            else {
                // int ballNum = 0;
                for (int ballNum = 0; ballNum < ballCount; ballNum++)
                    //while (ballNum < ballCount)
                    g.DrawImage(ball, posX[ballNum], posY[ballNum]);
                speedTick = speedTick >= 901 ? 900 : speedTick + 1;
            }
        }
        public void ballDeath() {
            for (int ballNum = 0; ballNum < ballCount; ballNum++) {
                if (posY[ballNum] >= 1000) {
                    //죽고, 숫자 땡겨
                    //3번(마지막 번호)이 죽으면 상관 없는데 2번,1번이 죽으면 ballcount가 꼬이니까 한칸씩 땡겨줘야 한다.
                    if (ballNum < ballCount - 1) {
                        int temp = ballCount - 1 - ballNum;
                        posX[ballNum] = posX[ballNum + temp];
                        posY[ballNum] = posY[ballNum + temp];
                        moveX[ballNum] = moveX[ballNum + temp];
                        moveY[ballNum] = moveY[ballNum + temp];
                        calcPosX[ballNum] = calcPosX[ballNum + temp];
                        calcPosY[ballNum] = calcPosY[ballNum + temp];
                    }
                    ballCount--;
                }
            }
            if (ballCount <= 0) {
                lblGameover.Visible = true;
            }
        }
        public void timeBallSpeed(int ballNum) {
            int timePlusMove = speedTick / 300;
            moveX[ballNum] = moveX[ballNum] > 0 ? moveX[ballNum] + timePlusMove : moveX[ballNum] - timePlusMove;
            moveY[ballNum] = moveY[ballNum] > 0 ? moveY[ballNum] + timePlusMove : moveY[ballNum] - timePlusMove;
        }
        public void ballCalcMove() {
            for (int ballNum = 0; ballNum < ballCount; ballNum++) {
                if (posX[ballNum] >= 795 && moveX[ballNum] > 0) { // 옆쪽 벽
                    moveX[ballNum] *= -1;
                }
                if (posX[ballNum] <= 20 && moveX[ballNum] < 0) {
                    moveX[ballNum] *= -1;
                }
                if (posY[ballNum] <= 70 && moveY[ballNum] < 0) { // 위쪽 벽
                    moveY[ballNum] *= -1;
                }

                if (dir == "UD") {
                    moveY[changeBallIndex] *= -1;
                    dir = null;
                    changeBallIndex = 999;
                }
                else if (dir == "LR") {
                    moveX[changeBallIndex] *= -1;
                    dir = null;
                    changeBallIndex = 999;
                }

                calcPosX[ballNum] += moveX[ballNum];
                calcPosY[ballNum] += moveY[ballNum];
            }
        }
        public void ballRealMove() { // 벽돌과의 계산 후 호출
            for (int ballNum = 0; ballNum < ballCount; ballNum++) {
                posX[ballNum] = calcPosX[ballNum];
                posY[ballNum] = calcPosY[ballNum];
            }
        }
    }
}
