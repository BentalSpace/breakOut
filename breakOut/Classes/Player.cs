using System.Drawing;
using System.Windows.Forms;

namespace breakOut {
    class Player {
        private int posX = 0;//355;
        private const int posY = 790;

        public string playerSize = "S";

        public Image player;

        public Player() {
            player = Image.FromFile(Application.StartupPath + @"\images\playerSmall.png");
        }
        public int PosX {
            get { return posX; }
        }
        public int PosY {
            get { return posY; }
        }
        public void playerMove(MouseEventArgs e) {
            posX = e.X - 50;
            if (playerSize == "M") {
                if (e.X >= 708 + 50)
                    posX = 708;
                else if (e.X <= 22 + 50)
                    posX = 22;
                else
                    posX = e.X - 50;
            }
            else if (playerSize == "B") {
                if (e.X >= 708 + 50 - 15)
                    posX = 678;
                else if (e.X <= 22 + 50 + 15)
                    posX = 22;
                else
                    posX = e.X - 65;
            }
            else if (playerSize == "S"){
                if (e.X >= 708 + 50 + 20)
                    posX = 748;
                else if (e.X <= 22 + 50 - 20)
                    posX = 22;
                else
                    posX = e.X - 30;
            }
        }
        public void drawPlayer(Graphics g) {
            g.DrawImage(player, posX, posY);

        }
    }
}
