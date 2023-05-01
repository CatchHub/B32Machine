namespace B32Machine
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            b32Screen1 = new B32Screen();
            msMainMenu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            pnlRegisters = new Panel();
            openFileDialog1 = new OpenFileDialog();
            lblRegisters = new Label();
            msMainMenu.SuspendLayout();
            SuspendLayout();
            // 
            // b32Screen1
            // 
            b32Screen1.BackColor = Color.Black;
            b32Screen1.Dock = DockStyle.Fill;
            b32Screen1.Location = new Point(0, 24);
            b32Screen1.Margin = new Padding(3, 2, 3, 2);
            b32Screen1.Name = "b32Screen1";
            b32Screen1.ScreenMemoryLocation = (ushort)40960;
            b32Screen1.Size = new Size(562, 236);
            b32Screen1.TabIndex = 0;
            // 
            // msMainMenu
            // 
            msMainMenu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            msMainMenu.Location = new Point(0, 0);
            msMainMenu.Name = "msMainMenu";
            msMainMenu.Size = new Size(562, 24);
            msMainMenu.TabIndex = 1;
            msMainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(180, 22);
            openToolStripMenuItem.Text = "Open...";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // pnlRegisters
            // 
            pnlRegisters.Dock = DockStyle.Bottom;
            pnlRegisters.Location = new Point(0, 206);
            pnlRegisters.Name = "pnlRegisters";
            pnlRegisters.Size = new Size(562, 54);
            pnlRegisters.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "B32";
            openFileDialog1.Filter = "B32 Files | *.B32";
            // 
            // lblRegisters
            // 
            lblRegisters.Dock = DockStyle.Fill;
            lblRegisters.Font = new Font("Courier New", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblRegisters.Location = new Point(0, 24);
            lblRegisters.Name = "lblRegisters";
            lblRegisters.Size = new Size(562, 182);
            lblRegisters.TabIndex = 3;
            lblRegisters.Text = "label1";
            lblRegisters.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 260);
            Controls.Add(lblRegisters);
            Controls.Add(pnlRegisters);
            Controls.Add(b32Screen1);
            Controls.Add(msMainMenu);
            MainMenuStrip = msMainMenu;
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            msMainMenu.ResumeLayout(false);
            msMainMenu.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private B32Screen b32Screen1;
        private MenuStrip msMainMenu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private Panel pnlRegisters;
        private OpenFileDialog openFileDialog1;
        private Label lblRegisters;
    }
}