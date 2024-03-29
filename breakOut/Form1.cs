﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace breakOut {
    public partial class BreakOut : Form {
        Player player;
        Brick brick;
        Ball ball;
        Manager manager;
        
        public BreakOut() {
            InitializeComponent();
            player = new Player();
            ball = new Ball(player, lblGameover);
            brick = new Brick(ball, player);
            manager = new Manager(player, ball, brick, lblScore);
        }
        private void Form1_Load(object sender, EventArgs e) {
            gameTimer.Interval = 1000 / 60;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            manager.startGame(e);
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e) {
            player.playerMove(e);
        }
        private void gameTimer_Tick(object sender, EventArgs e) {
            if (brick.gameSet)
                return;
            ball.ballCalcMove();
            brick.ballBrickCalc();
            ball.ballRealMove();
            ball.ballDeath();
            //brick.ballBrickTouch();
            manager.ballPlayerTouch();
            manager.itemPlayerTouch();
            brick.itemDespawn();
            manager.scoreUpdate();

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            manager.Drawbackground(e.Graphics);
            brick.drawBrick(e.Graphics);
            player.drawPlayer(e.Graphics);
            ball.drawBall(e.Graphics);
            brick.drawItem(e.Graphics);
        }
    }
}
