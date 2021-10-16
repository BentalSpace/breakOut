using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace breakOut {
    class Player {
        int posX = 355;
        const int posY = 790;
        int speed = 4;
        public void playerMove(MouseEventArgs e) {
            posX = e.X - 50;
        }
        public void drawPlayer(Graphics g) {
            Image player;
            player = Image.FromFile(Application.StartupPath + @"\images\player.png");
            g.DrawImage(player, posX, posY);

        }
    }
}
 