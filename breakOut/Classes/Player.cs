using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace breakOut {
    class Player {
        private int posX = 0;//355;
        private const int posY = 790;

        public int PosX {
            get { return posX; }
        }
        public int PosY {
            get { return posY; }
        }
        public void playerMove(MouseEventArgs e) {
            if (e.X <= 22 + 50)
                posX = 22;
            else if (e.X >= 708 + 50)
                posX = 708;
            else
                posX = e.X - 50;
        }
        public void drawPlayer(Graphics g) {
            Image player;
            player = Image.FromFile(Application.StartupPath + @"\images\player.png");
            g.DrawImage(player, posX, posY);

        }
    }
}
 