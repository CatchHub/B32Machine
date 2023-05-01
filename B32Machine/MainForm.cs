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
        public MainForm()
        {
            InitializeComponent();
            CompareFlag = 0;
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
            this.lblRegisters.Text = strRegisters;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte Magic1;
            byte Magic2;
            byte Magic3;
            openFileDialog1.ShowDialog();
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
            ExecuteProgram(ExecAddr, Counter);
        }
        private void ExecuteProgram(ushort ExecAddr, ushort ProgLength)
        {
            ProgLength = 64000;
            while (ProgLength > 0)
            {
                byte Instruction = B32Memory[InstructionPointer];
                ProgLength--;
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
                    b32Screen1.Poke(Register_X, Register_A);
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
    }
}