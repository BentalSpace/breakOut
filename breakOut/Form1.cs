using System;
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
            brick = new Brick();
            ball = new Ball();
            manager = new Manager(player, ball, brick);
        }
        private void Form1_Load(object sender, EventArgs e) {
            gameTimer.Interval = 1000 / 60;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e) {
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e) {
            player.playerMove(e);
        }
        private void gameTimer_Tick(object sender, EventArgs e) {
            manager.ballMove();
            manager.ballBrickTouch();

            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            manager.Drawbackground(e.Graphics);
            manager.drawBrick(e.Graphics);
            player.drawPlayer(e.Graphics);
            ball.drawBall(e.Graphics);
        }
    }
}
