using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;

namespace breakOut {
    class Brick {
        private bool ballBrickCalcStart = false;
        int hitNum;
        int hitDelay;
        int startDelay;
        int saveI, saveJ;
        int stageNum;
        public int score;
        public int attackBrick;
        int animTick;
        int goldAnimCount;
        public int[] mapGoldCount;
        int goldAnimDelay;
        int goldAnimDelayCount;

        List<int> goldI;
        List<int> goldJ;
        /*
         * 0 : 공 갯수 증가
         * 1 : 플레이어 크기 증가
         * 2 : 플레이어 크기 감소
         */
        public bool[] itemSpawn;
        public bool gameSet = false;
        /*
         * x, 0 : X축
         * x, 1 : Y축
         */
        public float[,] itemPos;

        Random random;

        public Map map;
        Ball ball;
        Manager manager;
        Player player;

        Image red, orange, yellow, green, blue, indigo, purple, gray, gray1, gray2, gray3, gold, gold1, gold2, gold3, gold4;
        Image bigSize, smallSize, ballMany;

        private WindowsMediaPlayer wmp;

        public Brick(Ball ball, Player player) {
            this.ball = ball;
            this.player = player;
            manager = new Manager();
            map = new Map();
            random = new Random();
            goldI = new List<int>();
            goldJ = new List<int>();

            hitNum = 0;
            hitDelay = 5;
            startDelay = 0;
            stageNum = 0;
            itemSpawn = new bool[3];
            itemPos = new float[3, 2];
            saveI = saveJ = 999;
            mapGoldCount = new int[map.brickMap.GetLength(0)];
            goldAnimDelay = 5;

            wmp = new WindowsMediaPlayer();
            wmp.URL = Application.StartupPath + @"\sounds\hit.mp3";
            wmp.controls.stop();

            imageFileSet();

            bigSize = Image.FromFile(Application.StartupPath + @"\images\bigSizeItem.png");
            smallSize = Image.FromFile(Application.StartupPath + @"\images\smallSizeItem.png");
            ballMany = Image.FromFile(Application.StartupPath + @"\images\ballItem.png");
            specialBrick();
        }
        private void specialBrick() {
            int randI = 0;
            int randJ = 0;
            for (int i = 0; i < map.brickMap.GetLength(0); i++) {
                mapGoldCount[i] = (int)(map.mapItemCount[i] * 0.1);
                for (int j = 0; j < mapGoldCount[i]; j++) {
                    while (map.brickMap[i, randI, randJ] == 0
                        || map.brickMap[i, randI, randJ] == 8) {
                        randI = random.Next(0, map.brickMap.GetLength(1));
                        randJ = random.Next(0, map.brickMap.GetLength(2));
                    }
                    map.brickMap[i, randI, randJ] = 8;
                    goldI.Add(randI);
                    goldJ.Add(randJ);
                    randI = randJ = 0;
                }
            }
        }
        public void drawBrick(Graphics g) {
            for (int i = 0; i < map.brickMap.GetLength(1); i++) {
                for (int j = 1; j < map.brickMap.GetLength(2); j++) {
                    if (map.brickMap[stageNum, i, j] == 1)
                        g.DrawImage(red, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[stageNum, i, j] == 2)
                        g.DrawImage(orange, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[stageNum, i, j] == 3)
                        g.DrawImage(yellow, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[stageNum, i, j] == 4)
                        g.DrawImage(green, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[stageNum, i, j] == 5)
                        g.DrawImage(blue, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[stageNum, i, j] == 6)
                        g.DrawImage(indigo, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[stageNum, i, j] == 7)
                        g.DrawImage(purple, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[stageNum, i, j] == 8)
                        g.DrawImage(gold, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                    else if (map.brickMap[stageNum, i, j] >= 9)
                        g.DrawImage(gray, (30 + (60 * (j - 1))), 100 + ((i + 1) * 30));
                }
            }
            goldBrickAnim(g);
            grayBrickAnim(g);
        }
        private void grayBrickAnim(Graphics g) {
            if (saveI == 999)
                return;
            switch (hitNum) {
                case 1:
                    g.DrawImage(gray1, (30 + (60 * (saveJ - 1))), 100 + ((saveI + 1) * 30));
                    break;
                case 2:
                    g.DrawImage(gray2, (30 + (60 * (saveJ - 1))), 100 + ((saveI + 1) * 30));
                    break;
                case 3:
                    g.DrawImage(gray3, (30 + (60 * (saveJ - 1))), 100 + ((saveI + 1) * 30)); // 이동인 도움
                    saveI = saveJ = 999;
                    hitNum = 1;
                    break;
            }
            if(startDelay >= hitDelay) {
                hitNum++;
                startDelay = 0;
            }
            startDelay++;
        }
        private void goldBrickAnim(Graphics g) {
            animTick++;
            if (animTick >= 120) {
                switch (goldAnimCount) {
                    case 0:
                        for (int i = 0; i < mapGoldCount[stageNum]; i++)
                            g.DrawImage(gold1, (30 + (60 * (goldJ[i] - 1))), 100 + ((goldI[i] + 1) * 30));
                        break;
                    case 1:
                        for (int i = 0; i < mapGoldCount[stageNum]; i++)
                            g.DrawImage(gold2, (30 + (60 * (goldJ[i] - 1))), 100 + ((goldI[i] + 1) * 30));
                        break;
                    case 2:
                        for (int i = 0; i < mapGoldCount[stageNum]; i++)
                            g.DrawImage(gold3, (30 + (60 * (goldJ[i] - 1))), 100 + ((goldI[i] + 1) * 30));
                        break;
                    case 3:
                        for (int i = 0; i < mapGoldCount[stageNum]; i++)
                            g.DrawImage(gold4, (30 + (60 * (goldJ[i] - 1))), 100 + ((goldI[i] + 1) * 30));
                        animTick = 0;
                        goldAnimCount = 0;
                        break;
                }
                if (goldAnimDelayCount >= goldAnimDelay) {
                    goldAnimCount++;
                    goldAnimDelayCount = 0;
                }
                goldAnimDelayCount++;
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
            gold = Image.FromFile(Application.StartupPath + @"\images\gold.png");
            gold1 = Image.FromFile(Application.StartupPath + @"\images\gold1.png");
            gold2 = Image.FromFile(Application.StartupPath + @"\images\gold2.png");
            gold3 = Image.FromFile(Application.StartupPath + @"\images\gold3.png");
            gold4 = Image.FromFile(Application.StartupPath + @"\images\gold4.png");

        }

        //public void ballBrickTouch() {
        //    for (int i = 0; i < map.brickMap.GetLength(1); i++) {
        //        for (int j = 0; j < map.brickMap.GetLength(2); j++) {
        //            if (map.brickMap[0, i, j] != 0) {
        //                //if ((ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50)
        //                //    && (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20)) { // 왼쪽 위
        //                //    map.brickMap[0, i, j] = 0;
        //                //    ball.moveX *= -1;
        //                //    ball.moveY *= -1;
        //                //}
        //                //else if (((ball.posX + 16) >= (30 + (60 * (j - 1))) && (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50) // 오른쪽 위
        //                //    && (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20))
        //                //    map.brickMap[0, i, j] = 0;
        //                //else if ((ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50) // 왼쪽 아래
        //                //    && ((ball.posY + 16) >= 100 + (30 * (i + 1)) && (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20))
        //                //    map.brickMap[0, i, j] = 0;
        //                //else if (((ball.posX + 16) >= (30 + (60 * (j - 1))) && (ball.posX + 16) <= (30 + (60 * (j - 1))) + 50) // 오른쪽 아래
        //                //    && ((ball.posY + 16) >= 100 + (30 * (i + 1)) && (ball.posY + 16) <= 100 + (30 * (i + 1)) + 20))
        //                //    map.brickMap[0, i, j] = 0;

        //                if (ball.posY >= 100 + (30 * (i + 1)) + 17 && ball.posY <= 100 + (30 * (i + 1)) + 20) { // 벽돌 아래 부분
        //                    if (ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50
        //                        || ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 50) {
        //                        map.brickMap[0, i, j] = 0;
        //                        ball.moveY *= -1;
        //                    }
        //                }
        //                else if (ball.posY + 16 >= 100 + (30 * (i + 1)) && ball.posY + 16 <= 100 + (30 * (i + 1)) + 3) { // 벽돌 위 부분
        //                    if (ball.posX >= (30 + (60 * (j - 1))) && ball.posX <= (30 + (60 * (j - 1))) + 50
        //                        || ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 50) {
        //                        map.brickMap[0, i, j] = 0;
        //                        ball.moveY *= -1;
        //                    }
        //                }
        //                else if (ball.posX + 16 >= (30 + (60 * (j - 1))) && ball.posX + 16 <= (30 + (60 * (j - 1))) + 3) { // 벽돌 왼쪽 부분
        //                    if (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20
        //                        || ball.posY + 16 >= 100 + (30 * (i + 1)) && ball.posY + 16 <= 100 + (30 * (i + 1)) + 20) {
        //                        map.brickMap[0, i, j] = 0;
        //                        ball.moveX *= -1;
        //                    }
        //                }
        //                else if (ball.posX >= (30 + (60 * (j - 1))) + 47 && ball.posX <= (30 + (60 * (j - 1))) + 50) { // 벽돌 오른쪽 부분
        //                    if (ball.posY >= 100 + (30 * (i + 1)) && ball.posY <= 100 + (30 * (i + 1)) + 20
        //                        || ball.posY + 16 >= 100 + (30 * (i + 1)) && ball.posY + 16 <= 100 + (30 * (i + 1)) + 20) {
        //                        map.brickMap[0, i, j] = 0;
        //                        ball.moveX *= -1;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        public void ballBrickCalc() {
            for (int i = 0; i < map.brickMap.GetLength(1); i++) {
                for (int j = 0; j < map.brickMap.GetLength(2); j++) {
                    if (map.brickMap[stageNum, i, j] != 0) {
                        for (int ballNum = 0; ballNum < ball.ballCount; ballNum++) {
                            if ((ball.posX[ballNum] >= (30 + (60 * (j - 1))) && ball.posX[ballNum] <= (30 + (60 * (j - 1))) + 50)
                                 && (ball.posY[ballNum] >= 100 + (30 * (i + 1)) && ball.posY[ballNum] <= 100 + (30 * (i + 1)) + 20)) { // 공의 왼쪽 위
                                ballBrickCalcStart = true;
                            }
                            else if (((ball.posX[ballNum] + 16) >= (30 + (60 * (j - 1))) && (ball.posX[ballNum] + 16) <= (30 + (60 * (j - 1))) + 50) // 오른쪽 위
                                && (ball.posY[ballNum] >= 100 + (30 * (i + 1)) && ball.posY[ballNum] <= 100 + (30 * (i + 1)) + 20)) {
                                ballBrickCalcStart = true;
                            }
                            else if ((ball.posX[ballNum] >= (30 + (60 * (j - 1))) && ball.posX[ballNum] <= (30 + (60 * (j - 1))) + 50) // 왼쪽 아래
                                && ((ball.posY[ballNum] + 16) >= 100 + (30 * (i + 1)) && (ball.posY[ballNum] + 16) <= 100 + (30 * (i + 1)) + 20)) {
                                ballBrickCalcStart = true;
                            }
                            else if (((ball.posX[ballNum] + 16) >= (30 + (60 * (j - 1))) && (ball.posX[ballNum] + 16) <= (30 + (60 * (j - 1))) + 50) // 오른쪽 아래
                                && ((ball.posY[ballNum] + 16) >= 100 + (30 * (i + 1)) && (ball.posY[ballNum] + 16) <= 100 + (30 * (i + 1)) + 20)) {
                                ballBrickCalcStart = true;
                            }
                            if (ballBrickCalcStart) {
                                // 한칸씩 당겨서 경계에 확인
                                while (true) {
                                    // 한칸씩 땡기기
                                    if (ball.moveX[ballNum] >= 1) {
                                        ball.calcPosX[ballNum] += (ball.moveX[ballNum] / 10) * -1;//-1;
                                    }
                                    else if (ball.moveX[ballNum] <= -1) {
                                        ball.calcPosX[ballNum] += (ball.moveX[ballNum] / 10) * -1;//1;
                                    }
                                    if (ball.moveY[ballNum] >= 1) {
                                        ball.calcPosY[ballNum] += (ball.moveY[ballNum] / 10) * -1;//-1;
                                    }
                                    else if (ball.moveY[ballNum] <= -1) {
                                        ball.calcPosY[ballNum] += (ball.moveY[ballNum] / 10) * -1;//1;
                                    }
                                    //if (!ballBrickCalcStart)
                                    //    return;
                                    // 경계에 맞나 확인
                                    if ((ball.calcPosY[ballNum] + 16) <= 100 + (30 * (i + 1)) && (ball.calcPosY[ballNum] + 16) >= 100 + (30 * (i + 1)) - 1
                                        /*&& (ball.calcPosX[ballNum] >= (30 + (60 * (j - 1))) || (ball.posX[ballNum] + 16) <= (30 + (60 * (j - 1))) + 50)*/) { // 벽돌 위쪽
                                                                                                                                                               //위쪽에 부딪힌 판정
                                        ball.dir = "UD";
                                        calcSatisfy(i, j, ballNum);
                                        return;
                                    }
                                    else if (ball.calcPosY[ballNum] >= 100 + (30 * (i + 1)) + 20 && ball.calcPosY[ballNum] <= 100 + (30 * (i + 1)) + 20 + 1
                                        /*&& (ball.calcPosX[ballNum] >= (30 + (60 * (j - 1))) || (ball.posX[ballNum] + 16) <= (30 + (60 * (j - 1))) + 50)*/) { // 벽돌 아래쪽
                                                                                                                                                               //아래쪽에 부딪힌 판정
                                        ball.dir = "UD";
                                        calcSatisfy(i, j, ballNum);
                                        return;
                                    }
                                    else if ((ball.calcPosX[ballNum] + 16) <= (30 + (60 * (j - 1))) && (ball.calcPosX[ballNum] + 16) >= (30 + (60 * (j - 1))) - 1
                                        /*&& (ball.posY[ballNum] >= 100 + (30 * (i + 1)) || (ball.posY[ballNum] + 16) <= 100 + (30 * (i + 1)) + 20)*/) { // 벽돌 왼쪽
                                                                                                                                                         //왼쪽에 부딪힌 판정    
                                        ball.dir = "LR";
                                        calcSatisfy(i, j, ballNum);
                                        return;
                                    }
                                    else if (ball.calcPosX[ballNum] >= (30 + (60 * (j - 1))) + 50 && ball.calcPosX[ballNum] <= (30 + (60 * (j - 1))) + 50 + 1
                                       /* && (ball.posY[ballNum] >= 100 + (30 * (i + 1)) || (ball.posY[ballNum] + 16) <= 100 + (30 * (i + 1)) + 20)*/) { // 벽돌 오른쪽
                                                                                                                                                         //오른쪽에 부딪힌 판정
                                        ball.dir = "LR";
                                        calcSatisfy(i, j, ballNum);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void calcSatisfy(int i, int j, int ballNum) {
            ball.changeBallIndex = ballNum;
            if (map.brickMap[stageNum, i, j] >= 10) {
                map.brickMap[stageNum, i, j]--;
                saveI = i;
                saveJ = j;
            }
            else if (map.brickMap[stageNum, i, j] == 8) {
                map.brickMap[stageNum, i, j] = 0;
                saveI = 999;
                saveJ = 999;
                attackBrick++;
                score += 300;
                itemDrop(i, j);
                goldI.Remove(i);
                goldJ.Remove(j);
                mapGoldCount[stageNum]--;
            }
            else if (map.brickMap[stageNum, i, j] <= 9) {
                if (map.brickMap[stageNum, i, j] == 9)
                    score += 100;
                else
                    score += 20;
                map.brickMap[stageNum, i, j] = 0;
                saveI = 999;
                saveJ = 999;
                attackBrick++;
                itemDrop(i, j);
            }
            
            wmp.controls.play();
            ballBrickCalcStart = false;
            StageClear(map.mapItemCount[stageNum], attackBrick);
        }
        private void StageClear(int clearItem, int currentItem) {
            if (clearItem * 0.9 <= currentItem) {
                ball.startNow = true;
                ball.moveX[0] = 0;
                ball.moveY[0] = 0;
                ball.calcPosY[0] = 600 - 17;
                ball.ballCount = 1;
                ball.speedTick = 0;
                itemSpawn[0] = itemSpawn[1] = itemSpawn[2] = false;
                attackBrick = 0;
                player.playerSize = "M";
                for (int i = 0; i < mapGoldCount[stageNum]; i++) {
                    goldI.RemoveAt(0);
                    goldJ.RemoveAt(0);
                }
                stageNum++;

            }
            if (stageNum >= 3) {
                //게임 클리어
                ball.lblGameover.Text = "GameClear";
                ball.lblGameover.Visible = true;
                gameSet = true;
                stageNum = 2;
            }
        }
        private void itemDrop(int i, int j) {
            int tempRand = 1;//random.Next(99);
            if (tempRand < 10) {
                if (itemSpawn[0])
                    return;
                //아이템 드랍
                itemSpawn[0] = true;
                itemPos[0, 0] = 30 + (60 * (j - 1)) + 5;
                itemPos[0, 1] = 100 + (30 * (i + 1));
            }
            else if (tempRand < 20) {
                if (itemSpawn[1])
                    return;
                //아이템 드랍
                itemSpawn[1] = true;
                itemPos[1, 0] = 30 + (60 * (j - 1)) + 5;
                itemPos[1, 1] = 100 + (30 * (i + 1));
            }
            else if (tempRand < 30) {
                if (itemSpawn[2])
                    return;
                //아이템 드랍
                itemSpawn[2] = true;
                itemPos[2, 0] = 30 + (60 * (j - 1)) + 5;
                itemPos[2, 1] = 100 + (30 * (i + 1));
            }
        }
        public void drawItem(Graphics g) {
            if (!itemSpawn[0] && !itemSpawn[1] && !itemSpawn[2])
                return;
            if (itemSpawn[0]) {
                g.DrawImage(ballMany, itemPos[0, 0], itemPos[0, 1]);
                itemPos[0, 1] += 2;
            }
            if (itemSpawn[1]) {
                g.DrawImage(bigSize, itemPos[1, 0], itemPos[1, 1]);
                itemPos[1, 1] += 2;
            }
            if (itemSpawn[2]) {
                g.DrawImage(smallSize, itemPos[2, 0], itemPos[2, 1]);
                itemPos[2, 1] += 2;
            }
        }
        public void itemDespawn() {
            if (!itemSpawn[0] && !itemSpawn[1] && !itemSpawn[2])
                return;

            if (itemSpawn[0]) {
                if (itemPos[0, 1] >= 710)
                    itemSpawn[0] = false;
            }
            if (itemSpawn[1]) {
                if (itemPos[1, 1] >= 710)
                    itemSpawn[1] = false;
            }
            if (itemSpawn[2]) {
                if (itemPos[2, 1] >= 710)
                    itemSpawn[2] = false;
            }
        }
    }
}
