
namespace breakOut {
    partial class BreakOut {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            this.LblTest = new System.Windows.Forms.Label();
            this.lblGameover = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 60;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.Black;
            this.lblScore.Font = new System.Drawing.Font("맑은 고딕", 20F);
            this.lblScore.ForeColor = System.Drawing.Color.White;
            this.lblScore.Location = new System.Drawing.Point(0, 0);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(139, 37);
            this.lblScore.TabIndex = 0;
            this.lblScore.Text = "SCORE : 0";
            // 
            // LblTest
            // 
            this.LblTest.AutoSize = true;
            this.LblTest.Font = new System.Drawing.Font("굴림", 14F);
            this.LblTest.ForeColor = System.Drawing.Color.White;
            this.LblTest.Location = new System.Drawing.Point(175, 21);
            this.LblTest.Name = "LblTest";
            this.LblTest.Size = new System.Drawing.Size(51, 19);
            this.LblTest.TabIndex = 1;
            this.LblTest.Text = "Label";
            // 
            // lblGameover
            // 
            this.lblGameover.AutoSize = true;
            this.lblGameover.BackColor = System.Drawing.Color.Black;
            this.lblGameover.Font = new System.Drawing.Font("굴림", 40F);
            this.lblGameover.ForeColor = System.Drawing.Color.White;
            this.lblGameover.Location = new System.Drawing.Point(251, 399);
            this.lblGameover.Name = "lblGameover";
            this.lblGameover.Size = new System.Drawing.Size(282, 54);
            this.lblGameover.TabIndex = 2;
            this.lblGameover.Text = "Gameover";
            this.lblGameover.Visible = false;
            // 
            // BreakOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(829, 861);
            this.Controls.Add(this.lblGameover);
            this.Controls.Add(this.LblTest);
            this.Controls.Add(this.lblScore);
            this.DoubleBuffered = true;
            this.Name = "BreakOut";
            this.Text = "break out";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label LblTest;
        private System.Windows.Forms.Label lblGameover;
    }
}

