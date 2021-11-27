using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using WMPLib;

namespace breakOut {
    class Brick {
        private bool ballBrickCalcStart = false;
        int hitNum;
        int hitDelay;
        int startDelay;
        int saveI, saveJ;
        /*
         * 0 : 공 갯수 증가
         * 1 : 플레이어 크기 증가
         * 2 : 플레이어 크기 감소
         */
        public bool[] itemSpawn;
        /*
         * x, 0 : X축
         * x, 1 : Y축
         */
        public float[,] itemPos;

        Random random;

        Map map;
        Ball ball;

        Image red, orange, yellow, green, blue, indigo, purple, gray, gray1, gray2, gray3;
        Image bigSize, smallSize, ballMany;

        private WindowsMediaPlayer wmp;

        public Brick(Ball ball) {
            this.ball = ball;
            map = new Map();
            random = new Random();

            hitNum = 0;
            hitDelay = 2 ;
            startDelay = 5;
            itemSpawn = new bool[3];
            itemPos = new float[3, 2];

            wmp = new WindowsMediaPlayer();
            wmp.URL = Application.StartupPath + @"\sounds\hit.mp3";
            wmp.controls.stop();
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
                    else if (map.brickMap[i, j] >= 8)
                        g.DrawImage(gray, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                }
            }
            if(saveI != 0) {
                if (startDelay >= hitDelay) {
                    startDelay = 0;
                    hitNum++;
                    switch (hitNum) {
                        case 1:
                            g.DrawImage(gray1, (30 + (60 * (saveJ - 1))), 100 + ((saveI + 1) * 30));
                            break;
                        case 2:
                            g.DrawImage(gray2, (30 + (60 * (saveJ - 1))), 100 + ((saveI + 1) * 30));
                            break;
                        case 3:
                            g.DrawImage(gray3, (30 + (60 * (saveJ - 1))), 100 + ((saveI + 1) * 30));
                            break;
                    }
                }
                startDelay++;
                if(hitNum >= 3) {
                    saveI = saveJ = 0;
                    startDelay = 5;
                    hitNum = 0;
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
            gray = Image.FromFile(Application.StartupPath + @"\images\gray.png");
            gray1 = Image.FromFile(Application.StartupPath + @"\images\gray1.png");
            gray2 = Image.FromFile(Application.StartupPath + @"\images\gray2.png");
            gray3 = Image.FromFile(Application.StartupPath + @"\images\gray3.png");
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

                        if (ball.posY >= 100 + (30 * (i + 1)) + 17 && ball.posY <= 100 + (30 * (i + 1)) + 20) { // 벽돌 아래 부분
                            if (ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50
                                || ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 50) {
                                map.brickMap[i, j] = 0;
                                ball.moveY *= -1;
                            }
                        }
                        else if (ball.posY + 16 >= 100 + (30 * (i + 1)) && ball.posY + 16 <= 100 + (30 * (i + 1)) + 3) { // 벽돌 위 부분
                            if (ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50
                                || ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 50) {
                                map.brickMap[i, j] = 0;
                                ball.moveY *= -1;
                            }
                        }
                        else if (ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 3) { // 벽돌 왼쪽 부분
                            if (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20
                                || ball.posY + 16 >= 100 + (30 * (i + 1)) && ball.posY + 16 <= 100 + (30 * (i + 1)) + 20) {
                                map.brickMap[i, j] = 0;
                                ball.moveX *= -1;
                            }
                        }
                        else if (ball.posX >= (30 + (60 * (j - 1))) + 47 && ball.posX <= (30 + (60 * (j - 1))) + 50) { // 벽돌 오른쪽 부분
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
        public void ballBrickCalc() {
            for (int i = 0; i < map.brickMap.GetLength(0); i++) {
                for (int j = 0; j < map.brickMap.GetLength(1); j++) {
                    if (map.brickMap[i, j] != 0) {
                        if ((ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50)
                            && (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20)) { // 공의 왼쪽 위
                            ballBrickCalcStart = true;
                        }
                        else if (((ball.posX + 16) >= (30 + (60 * (j - 1))) && (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50) // 오른쪽 위
                            && (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20)) {
                            ballBrickCalcStart = true;
                        }
                        else if ((ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50) // 왼쪽 아래
                            && ((ball.posY + 16) >= 100 + (30 * (i + 1)) && (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20)) {
                            ballBrickCalcStart = true;
                        }
                        else if (((ball.posX + 16) >= (30 + (60 * (j - 1))) && (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50) // 오른쪽 아래
                            && ((ball.posY + 16) >= 100 + (30 * (i + 1)) && (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20)) {
                            ballBrickCalcStart = true;
                        }
                        if (ballBrickCalcStart) {
                            // 한칸씩 당겨서 경계에 확인
                            while (true) {
                                // 한칸씩 땡기기
                                if (ball.moveX >= 1) {
                                    ball.calcPosX += (ball.moveX / 10) * -1;//-1;
                                }
                                else if (ball.moveX <= -1) {
                                    ball.calcPosX += (ball.moveX / 10) * -1;//1;
                                }
                                if (ball.moveY >= 1) {
                                    ball.calcPosY += (ball.moveY / 10) * -1;//-1;
                                }
                                else if (ball.moveY <= -1) {
                                    ball.calcPosY += (ball.moveY / 10) * -1;//1;
                                }
                                //if (!ballBrickCalcStart)
                                //    return;
                                // 경계에 맞나 확인
                                if ((ball.calcPosY + 16) <= 100 + (30 * (i + 1)) && (ball.posY + 16) >= 100 + (30 * (i + 1)) - 1
                                    /*&& (ball.calcPosX >= (30 + (60 * (j - 1))) || (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50)*/) { // 벽돌 위쪽
                                    //위쪽에 부딪힌 판정
                                    ball.dir = "U";
                                    if (map.brickMap[i, j] >= 8) {
                                        map.brickMap[i, j]--;
                                        saveI = i;
                                        saveJ = j;
                                    }
                                    if (map.brickMap[i, j] < 8) {
                                        map.brickMap[i, j] = 0;
                                        saveI = 0;
                                        saveJ = 0;
                                    }
                                    wmp.controls.play();
                                    ballBrickCalcStart = false;
                                    itemDrop(i, j);
                                    return;
                                }
                                else if (ball.calcPosY >= 100 + (30 * (i + 1)) + 20 && ball.calcPosY <= 100 + (30 * (i + 1)) + 20 + 1
                                    /*&& (ball.calcPosX >= (30 + (60 * (j - 1))) || (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50)*/) { // 벽돌 아래쪽
                                    //아래쪽에 부딪힌 판정
                                    ball.dir = "D";
                                    if (map.brickMap[i, j] >= 8) {
                                        map.brickMap[i, j]--;
                                        saveI = i;
                                        saveJ = j;
                                    }
                                    if (map.brickMap[i, j] < 8) {
                                        map.brickMap[i, j] = 0;
                                        saveI = 0;
                                        saveJ = 0;
                                    }
                                    wmp.controls.play();
                                    ballBrickCalcStart = false;
                                    itemDrop(i, j);
                                    return;
                                }
                                else if ((ball.calcPosX + 16) <= (30 + (60 * (j - 1))) && (ball.calcPosX + 16) >= (30 + (60 * (j - 1))) - 1
                                    /*&& (ball.posY >= 100 + (30 * (i + 1)) || (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20)*/) { // 벽돌 왼쪽
                                    //왼쪽에 부딪힌 판정    
                                    ball.dir = "L";
                                    if (map.brickMap[i, j] >= 8) {
                                        map.brickMap[i, j]--;
                                        saveI = i;
                                        saveJ = j;
                                    }
                                    if (map.brickMap[i, j] < 8) {
                                        map.brickMap[i, j] = 0;
                                        saveI = 0;
                                        saveJ = 0;
                                    }
                                    wmp.controls.play();
                                    ballBrickCalcStart = false;
                                    itemDrop(i, j);
                                    return;
                                }
                                else if (ball.calcPosX >= (30 + (60 * (j - 1))) + 50 && ball.calcPosX <= (30 + (60 * (j - 1))) + 50 + 1
                                   /* && (ball.posY >= 100 + (30 * (i + 1)) || (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20)*/) { // 벽돌 오른쪽
                                    //오른쪽에 부딪힌 판정
                                    ball.dir = "R";
                                    if (map.brickMap[i, j] >= 8) {
                                        map.brickMap[i, j]--;
                                        saveI = i;
                                        saveJ = j;
                                    }
                                    if (map.brickMap[i, j] < 8) {
                                        map.brickMap[i, j] = 0;
                                        saveI = 0;
                                        saveJ = 0;
                                    }
                                    wmp.controls.play();
                                    ballBrickCalcStart = false;
                                    itemDrop(i, j);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void itemDrop(int i, int j) {
            int tempRand = 4;//random.Next(99);
            if(tempRand < 3) {
                if (itemSpawn[0])
                    return;
                //아이템 드랍
                itemSpawn[0] = true;
                itemPos[0, 0] = 30 + (60 * (j - 1)) + 5;
                itemPos[0, 1] = 100 + (30 * (i + 1));
            }
            else if(tempRand < 6) {
                if (itemSpawn[1])
                    return;
                //아이템 드랍
                itemSpawn[1] = true;
                itemPos[1, 0] = 30 + (60 * (j - 1)) + 5;
                itemPos[1, 1] = 100 + (30 * (i + 1))+1;
            }
            else if(tempRand < 9) {
                if (itemSpawn[2])
                    return;
                //아이템 드랍
                itemSpawn[2] = true;
                itemPos[2, 0] = 30 + (60 * (j - 1)) + 5;
                itemPos[2, 1] = 100 + (30 * (i + 1));
            }
            bigSize = Image.FromFile(Application.StartupPath + @"\images\bigSizeItem.png");
        }
        public void drawItem(Graphics g) {
            if (!itemSpawn[0] && !itemSpawn[1] && !itemSpawn[2])
                return;
            g.DrawImage(bigSize, itemPos[1, 0], itemPos[1, 1]);
            itemPos[1, 1] += 3;
        }
    }
}
