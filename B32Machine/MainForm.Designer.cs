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
			resumeToolStripMenuItem = new ToolStripMenuItem();
			pauseToolStripMenuItem = new ToolStripMenuItem();
			speedToolStripMenuItem = new ToolStripMenuItem();
			mS14SecondsToolStripMenuItem = new ToolStripMenuItem();
			mS12SecondToolStripMenuItem = new ToolStripMenuItem();
			mS1SecondToolStripMenuItem = new ToolStripMenuItem();
			mS2SecondToolStripMenuItem = new ToolStripMenuItem();
			mS3SecondToolStripMenuItem = new ToolStripMenuItem();
			mS4SecondToolStripMenuItem = new ToolStripMenuItem();
			mS5SecondToolStripMenuItem = new ToolStripMenuItem();
			realTimeToolStripMenuItem = new ToolStripMenuItem();
			pnlRegisters = new Panel();
			openFileDialog1 = new OpenFileDialog();
			lblRegisters = new Label();
			restartToolStripMenuItem = new ToolStripMenuItem();
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
			fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, restartToolStripMenuItem, resumeToolStripMenuItem, pauseToolStripMenuItem, speedToolStripMenuItem });
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
			// resumeToolStripMenuItem
			// 
			resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
			resumeToolStripMenuItem.Size = new Size(180, 22);
			resumeToolStripMenuItem.Text = "Resume";
			resumeToolStripMenuItem.Click += resumeToolStripMenuItem_Click;
			// 
			// pauseToolStripMenuItem
			// 
			pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
			pauseToolStripMenuItem.Size = new Size(180, 22);
			pauseToolStripMenuItem.Text = "Pause";
			pauseToolStripMenuItem.Click += pauseToolStripMenuItem_Click;
			// 
			// speedToolStripMenuItem
			// 
			speedToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mS14SecondsToolStripMenuItem, mS12SecondToolStripMenuItem, mS1SecondToolStripMenuItem, mS2SecondToolStripMenuItem, mS3SecondToolStripMenuItem, mS4SecondToolStripMenuItem, mS5SecondToolStripMenuItem, realTimeToolStripMenuItem });
			speedToolStripMenuItem.Name = "speedToolStripMenuItem";
			speedToolStripMenuItem.Size = new Size(180, 22);
			speedToolStripMenuItem.Text = "Speed";
			// 
			// mS14SecondsToolStripMenuItem
			// 
			mS14SecondsToolStripMenuItem.Name = "mS14SecondsToolStripMenuItem";
			mS14SecondsToolStripMenuItem.Size = new Size(184, 22);
			mS14SecondsToolStripMenuItem.Text = "250 MS (1/4 Second)";
			mS14SecondsToolStripMenuItem.Click += mS14SecondsToolStripMenuItem_Click;
			// 
			// mS12SecondToolStripMenuItem
			// 
			mS12SecondToolStripMenuItem.Name = "mS12SecondToolStripMenuItem";
			mS12SecondToolStripMenuItem.Size = new Size(184, 22);
			mS12SecondToolStripMenuItem.Text = "500 MS (1/2 Second)";
			mS12SecondToolStripMenuItem.Click += mS12SecondToolStripMenuItem_Click;
			// 
			// mS1SecondToolStripMenuItem
			// 
			mS1SecondToolStripMenuItem.Name = "mS1SecondToolStripMenuItem";
			mS1SecondToolStripMenuItem.Size = new Size(184, 22);
			mS1SecondToolStripMenuItem.Text = "1000 MS (1 Second)";
			mS1SecondToolStripMenuItem.Click += mS1SecondToolStripMenuItem_Click;
			// 
			// mS2SecondToolStripMenuItem
			// 
			mS2SecondToolStripMenuItem.Name = "mS2SecondToolStripMenuItem";
			mS2SecondToolStripMenuItem.Size = new Size(184, 22);
			mS2SecondToolStripMenuItem.Text = "2000 MS (2 Second)";
			mS2SecondToolStripMenuItem.Click += mS2SecondToolStripMenuItem_Click;
			// 
			// mS3SecondToolStripMenuItem
			// 
			mS3SecondToolStripMenuItem.Name = "mS3SecondToolStripMenuItem";
			mS3SecondToolStripMenuItem.Size = new Size(184, 22);
			mS3SecondToolStripMenuItem.Text = "3000 MS (3 Second)";
			mS3SecondToolStripMenuItem.Click += mS3SecondToolStripMenuItem_Click;
			// 
			// mS4SecondToolStripMenuItem
			// 
			mS4SecondToolStripMenuItem.Name = "mS4SecondToolStripMenuItem";
			mS4SecondToolStripMenuItem.Size = new Size(184, 22);
			mS4SecondToolStripMenuItem.Text = "4000 MS (4 Second)";
			mS4SecondToolStripMenuItem.Click += mS4SecondToolStripMenuItem_Click;
			// 
			// mS5SecondToolStripMenuItem
			// 
			mS5SecondToolStripMenuItem.Name = "mS5SecondToolStripMenuItem";
			mS5SecondToolStripMenuItem.Size = new Size(184, 22);
			mS5SecondToolStripMenuItem.Text = "5000 MS (5 Second)";
			mS5SecondToolStripMenuItem.Click += mS5SecondToolStripMenuItem_Click;
			// 
			// realTimeToolStripMenuItem
			// 
			realTimeToolStripMenuItem.Name = "realTimeToolStripMenuItem";
			realTimeToolStripMenuItem.Size = new Size(184, 22);
			realTimeToolStripMenuItem.Text = "Real Time (No Delay)";
			realTimeToolStripMenuItem.Click += realTimeToolStripMenuItem_Click;
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
			// restartToolStripMenuItem
			// 
			restartToolStripMenuItem.Name = "restartToolStripMenuItem";
			restartToolStripMenuItem.Size = new Size(180, 22);
			restartToolStripMenuItem.Text = "Restart";
			restartToolStripMenuItem.Click += restartToolStripMenuItem_Click;
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
		private ToolStripMenuItem speedToolStripMenuItem;
		private ToolStripMenuItem mS14SecondsToolStripMenuItem;
		private ToolStripMenuItem mS12SecondToolStripMenuItem;
		private ToolStripMenuItem mS1SecondToolStripMenuItem;
		private ToolStripMenuItem mS2SecondToolStripMenuItem;
		private ToolStripMenuItem mS3SecondToolStripMenuItem;
		private ToolStripMenuItem mS4SecondToolStripMenuItem;
		private ToolStripMenuItem mS5SecondToolStripMenuItem;
		private ToolStripMenuItem realTimeToolStripMenuItem;
		private ToolStripMenuItem resumeToolStripMenuItem;
		private ToolStripMenuItem pauseToolStripMenuItem;
		private ToolStripMenuItem restartToolStripMenuItem;
	}
}