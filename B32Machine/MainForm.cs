namespace B32Machine
{
	public partial class MainForm : Form
	{
		private byte[] B32Memory;
		private ushort StartAddr;
		private ushort ExecAddr;
		private ushort InstructionPointer;
		private byte Register_A;
		private byte Register_B;
		private ushort Register_X;
		private ushort Register_Y;
		private ushort Register_D;
		private byte CompareFlag;
		private ushort SpeedMS;

		private System.Threading.Thread? prog;
		private System.Threading.ManualResetEvent PauseEvent;

		delegate void SetTextCallback(string text);
		delegate void PokeCallBack(ushort addr, byte value);


		public MainForm()
		{
			InitializeComponent();

			prog = null;
			CompareFlag = 0;
			SpeedMS = 0;
			realTimeToolStripMenuItem.Checked = true;
			resumeToolStripMenuItem.Enabled = false;
			pauseToolStripMenuItem.Enabled = true;
			B32Memory = new byte[65535];
			StartAddr = 0;
			ExecAddr = 0;
			Register_A = 0;
			Register_B = 0;
			Register_D = 0;
			Register_X = 0;
			Register_Y = 0;
			UpdateRegisterStatus();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{


		}
		private void UpdateRegisterStatus()
		{
			string strRegisters = "";
			strRegisters = "Register A = $" +
			Register_A.ToString("X").PadLeft(2, '0');
			strRegisters += " Register B = $" +
			Register_B.ToString("X").PadLeft(2, '0');
			strRegisters += " Register D = $" +
			Register_D.ToString("X").PadLeft(4, '0');
			strRegisters += "\nRegister X = $" +
			Register_X.ToString("X").PadLeft(4, '0');
			strRegisters += " Register Y = $" +
			Register_Y.ToString("X").PadLeft(4, '0');
			strRegisters += " Instruction Pointer = $" +
			InstructionPointer.ToString("X").PadLeft(4, '0');
			strRegisters += "\nCompare Flag = $" + CompareFlag.ToString("X").PadLeft(2, '0');
			//this.lblRegisters.Text = strRegisters;
			if (lblRegisters.InvokeRequired)
			{
				SetTextCallback z = new SetTextCallback(SetRegisterText);
				this.Invoke(z, new object[] { strRegisters });
			}
			else
			{
				SetRegisterText(strRegisters);
			}

		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			byte Magic1;
			byte Magic2;
			byte Magic3;

			DialogResult dr;
			dr = openFileDialog1.ShowDialog();
			if (dr == DialogResult.Cancel) return;
			lock (b32Screen1)
			{
				b32Screen1.Reset();
			}

			System.IO.BinaryReader br;
			System.IO.FileStream fs = new
			System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);
			br = new System.IO.BinaryReader(fs);
			Magic1 = br.ReadByte();
			Magic2 = br.ReadByte();
			Magic3 = br.ReadByte();
			if (Magic1 != 'B' && Magic2 != '3' && Magic3 != '2')
			{
				MessageBox.Show("This is not a valid B32 file!", "Error!",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			StartAddr = br.ReadUInt16();
			ExecAddr = br.ReadUInt16();
			ushort Counter = 0;
			while ((br.PeekChar() != -1))
			{
				B32Memory[(StartAddr + Counter)] = br.ReadByte();
				Counter++;
			}
			br.Close();
			fs.Close();
			InstructionPointer = ExecAddr;

			//ExecuteProgram(ExecAddr, Counter);
			//System.Threading.Thread prog = new
			//System.Threading.Thread(delegate () { ExecuteProgram(ExecAddr, Counter); });
			prog = new System.Threading.Thread(delegate ()
			{
				ExecuteProgram(ExecAddr, Counter);
			});
			PauseEvent = new System.Threading.ManualResetEvent(true);
			prog.Start();
		}

		private void ThreadPoke(ushort Addr, byte value)
		{
			if (b32Screen1.InvokeRequired)
			{
				PokeCallBack pcb = new PokeCallBack(Poke);
				this.Invoke(pcb, new object[] { Addr, value });
			}
			else
			{
				Poke(Addr, value);
			}
		}
		private void Poke(ushort Addr, byte value)
		{
			lock (b32Screen1)
			{
				b32Screen1.Poke(Addr, value);
			}
		}
		private void SetRegisterText(string text)
		{
			lblRegisters.Text = text;
		}

		private void ExecuteProgram(ushort ExecAddr, ushort ProgLength)
		{
			ProgLength = 64000;
			while (ProgLength > 0)
			{
				byte Instruction = B32Memory[InstructionPointer];
				ProgLength--;
				System.Threading.Thread.Sleep(SpeedMS);
				PauseEvent.WaitOne(System.Threading.Timeout.Infinite);

				if (Instruction == 0x02) // LDX #<value>
				{
					Register_X = (ushort)((B32Memory[(InstructionPointer +
					2)]) << 8);
					Register_X += B32Memory[(InstructionPointer + 1)];
					ProgLength -= 2;
					InstructionPointer += 3;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x01) // LDA #<value>
				{
					Register_A = B32Memory[(InstructionPointer + 1)];
					SetRegisterD();
					ProgLength -= 1;
					InstructionPointer += 2;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x03) // STA ,X
				{
					B32Memory[Register_X] = Register_A;
					//b32Screen1.Poke(Register_X, Register_A);
					ThreadPoke(Register_X, Register_A);
					InstructionPointer++;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x04) // END
				{
					InstructionPointer++;
					UpdateRegisterStatus();
					break;
				}
				if (Instruction == 0x05) // CMPA
				{
					byte CompValue = B32Memory[InstructionPointer + 1];
					CompareFlag = 0;
					if (Register_A == CompValue) CompareFlag =
					(byte)(CompareFlag | 1);
					if (Register_A != CompValue) CompareFlag =
					(byte)(CompareFlag | 2);
					if (Register_A < CompValue) CompareFlag =
					(byte)(CompareFlag | 4);
					if (Register_A > CompValue) CompareFlag =
					(byte)(CompareFlag | 8);
					InstructionPointer += 2;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x06) // CMPB
				{
					byte CompValue = B32Memory[InstructionPointer + 1];
					CompareFlag = 0;
					if (Register_B == CompValue) CompareFlag =
					(byte)(CompareFlag | 1);
					if (Register_B != CompValue) CompareFlag =
					(byte)(CompareFlag | 2);
					if (Register_B < CompValue) CompareFlag =
					(byte)(CompareFlag | 4);
					if (Register_B > CompValue) CompareFlag =
					(byte)(CompareFlag | 8);
					InstructionPointer += 2;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x07) //CMPX
				{
					ushort CompValue =
					(ushort)((B32Memory[(InstructionPointer + 2)]) << 8);
					CompValue += B32Memory[(InstructionPointer + 1)];
					CompareFlag = 0;
					if (Register_X == CompValue) CompareFlag =
					(byte)(CompareFlag | 1);
					if (Register_X != CompValue) CompareFlag =
					(byte)(CompareFlag | 2);
					if (Register_X < CompValue) CompareFlag =
					(byte)(CompareFlag | 4);
					if (Register_X > CompValue) CompareFlag =
					(byte)(CompareFlag | 8);
					InstructionPointer += 3;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x08) //CMPY
				{
					ushort CompValue =
					(ushort)((B32Memory[(InstructionPointer + 2)]) << 8);
					CompValue += B32Memory[(InstructionPointer + 1)];
					CompareFlag = 0;
					if (Register_Y == CompValue) CompareFlag =
					(byte)(CompareFlag | 1);
					if (Register_Y != CompValue) CompareFlag =
					(byte)(CompareFlag | 2);
					if (Register_Y < CompValue) CompareFlag =
					(byte)(CompareFlag | 4);
					if (Register_Y > CompValue) CompareFlag =
					(byte)(CompareFlag | 8);
					InstructionPointer += 3;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x09) //CMPD
				{
					ushort CompValue =
					(ushort)((B32Memory[(InstructionPointer + 2)]) << 8);
					CompValue += B32Memory[(InstructionPointer + 1)];
					CompareFlag = 0;
					if (Register_D == CompValue) CompareFlag =
(byte)(CompareFlag | 1);
					if (Register_D != CompValue) CompareFlag =
					(byte)(CompareFlag | 2);
					if (Register_D < CompValue) CompareFlag =
					(byte)(CompareFlag | 4);
					if (Register_D > CompValue) CompareFlag =
					(byte)(CompareFlag | 8);
					InstructionPointer += 3;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x0A) // JMP
				{
					ushort JmpValue = (ushort)((B32Memory[(InstructionPointer
					+ 2)]) << 8);
					JmpValue += B32Memory[(InstructionPointer + 1)];
					InstructionPointer = JmpValue;
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x0B) // JEQ
				{
					ushort JmpValue = (ushort)((B32Memory[(InstructionPointer
					+ 2)]) << 8);
					JmpValue += B32Memory[(InstructionPointer + 1)];
					if ((CompareFlag & 1) == 1)
					{
						InstructionPointer = JmpValue;
					}
					else
					{
						InstructionPointer += 3;
					}
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x0C) // JNE
				{
					ushort JmpValue = (ushort)((B32Memory[(InstructionPointer
					+ 2)]) << 8);
					JmpValue += B32Memory[(InstructionPointer + 1)];
					if ((CompareFlag & 2) == 2)
					{
						InstructionPointer = JmpValue;
					}
					else
					{
						InstructionPointer += 3;
					}
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x0D) // JGT
				{
					ushort JmpValue = (ushort)((B32Memory[(InstructionPointer
					+ 2)]) << 8);
					JmpValue += B32Memory[(InstructionPointer + 1)];
					if ((CompareFlag & 4) == 4)
					{
						InstructionPointer = JmpValue;
					}
					else
					{
						InstructionPointer += 3;
					}
					UpdateRegisterStatus();
					continue;
				}
				if (Instruction == 0x0E) // JLT
				{
					ushort JmpValue = (ushort)((B32Memory[(InstructionPointer
					+ 2)]) << 8);
					JmpValue += B32Memory[(InstructionPointer + 1)];
					if ((CompareFlag & 8) == 8)
					{
						InstructionPointer = JmpValue;
					}
					else
					{
						InstructionPointer += 3;
					}
					UpdateRegisterStatus();
					continue;
				}
			}

		}
		private void SetRegisterD()
		{
			Register_D = (ushort)(Register_A << 8 + Register_B);
		}

		private void mS14SecondsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UncheckAll();
			((ToolStripMenuItem)sender).Checked = true;
			SpeedMS = 250;
		}

		private void mS12SecondToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UncheckAll();
			((ToolStripMenuItem)sender).Checked = true;
			SpeedMS = 500;
		}

		private void mS1SecondToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UncheckAll();
			((ToolStripMenuItem)sender).Checked = true;
			SpeedMS = 1000;
		}

		private void mS2SecondToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UncheckAll();
			((ToolStripMenuItem)sender).Checked = true;
			SpeedMS = 2000;
		}

		private void mS3SecondToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UncheckAll();
			((ToolStripMenuItem)sender).Checked = true;
			SpeedMS = 3000;
		}

		private void mS4SecondToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UncheckAll();
			((ToolStripMenuItem)sender).Checked = true;
			SpeedMS = 4000;
		}

		private void mS5SecondToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UncheckAll();
			((ToolStripMenuItem)sender).Checked = true;
			SpeedMS = 5000;
		}

		private void realTimeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UncheckAll();
			((ToolStripMenuItem)sender).Checked = true;
			SpeedMS = 0;
		}

		private void UncheckAll()
		{
			mS14SecondsToolStripMenuItem.Checked = false; // 1/4 second
			mS12SecondToolStripMenuItem.Checked = false; // 1/2 second
			mS1SecondToolStripMenuItem.Checked = false; // 1 second
			mS2SecondToolStripMenuItem.Checked = false; // 2 seconds
			mS3SecondToolStripMenuItem.Checked = false; // 3 seconds
			mS4SecondToolStripMenuItem.Checked = false; // 4 seconds
			mS5SecondToolStripMenuItem.Checked = false; // 5 seconds
			realTimeToolStripMenuItem.Checked = false; // real time
		}
		private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			resumeToolStripMenuItem.Enabled = true;
			pauseToolStripMenuItem.Enabled = false;
			PauseEvent.Reset();
		}

		private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			resumeToolStripMenuItem.Enabled = false;
			pauseToolStripMenuItem.Enabled = true;
			PauseEvent.Set();
		}

		private void restartToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (prog != null)
			{
				prog.Interrupt(); //.net in guncelinde abort kaldirildigindan interrupt kullanmak zorunda kaldik, restart fonksiyonu istendigi sekilde calismiyor olabilir
				prog = null;
			}
			InstructionPointer = ExecAddr;
			resumeToolStripMenuItem.Enabled = false;
			pauseToolStripMenuItem.Enabled = true;
			prog = new System.Threading.Thread(delegate () {
				ExecuteProgram(ExecAddr, 64000);
			});
			PauseEvent = new System.Threading.ManualResetEvent(true);
			b32Screen1.Reset();
			prog.Start();
		}

	}
}
