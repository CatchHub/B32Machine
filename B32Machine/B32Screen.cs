using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B32Machine
{
    public partial class B32Screen : UserControl
    {
        private ushort m_ScreenMemoryLocation;
        private byte[] m_ScreenMemory;
        public ushort ScreenMemoryLocation
        {
            get
            {
                return this.m_ScreenMemoryLocation;
            }
            set
            {
                this.m_ScreenMemoryLocation = value;
            }
        }
        public B32Screen()
        {
            InitializeComponent();
            this.m_ScreenMemory = new byte[4000];
            this.m_ScreenMemoryLocation = 0xA000;
            for (int i = 0; i < 4000; i += 2)
            {
                this.m_ScreenMemory[i + 1] = 7; //produce gray text on the black background
                this.m_ScreenMemory[i] = 32;
            }
        }

        private void B32Screen_Load(object sender, EventArgs e)
        {

        }

        //Poke method first tries to create an offset and returns if there is an error.
        //then it checks to make sure it’s in range, then “pokes” to value into the memory location.
        //Finally, the control refreshes itself to update.
        public void Poke(ushort Address, byte Value)
        {
            ushort MemLoc;
            try
            {
                MemLoc = (ushort)(Address - m_ScreenMemoryLocation);
            }
            catch (Exception)
            {
                return;
            }
            if (MemLoc < 0 || MemLoc > 3999)
                return;
            m_ScreenMemory[MemLoc] = Value;
            Refresh();
        }
        //Peek will return the byte value stored in any video memory location.
        //The method first tries to create an offset, validates the range, then returns the appropriate byte.
        public byte Peek(ushort Address)
        {
            ushort MemLoc;
            try
            {
                MemLoc = (ushort)(Address - m_ScreenMemoryLocation);
            }
            catch (Exception)
            {
                return (byte)0;
            }
            if (MemLoc < 0 || MemLoc > 3999)
                return (byte)0;
            return m_ScreenMemory[MemLoc];
        }

        private void B32Screen_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics bmpGraphics = Graphics.FromImage(bmp);
            Font f = new Font("Courier New", 10f, FontStyle.Bold);
            int xLoc = 0;
            int yLoc = 0;
            for (int i = 0; i < 4000; i += 2)
            {
                SolidBrush bgBrush = null;
                SolidBrush fgBrush = null;
                if ((m_ScreenMemory[i + 1] & 112) == 112)
                {
                    bgBrush = new SolidBrush(Color.Gray);
                }
                if ((m_ScreenMemory[i + 1] & 112) == 96)
                {
                    bgBrush = new SolidBrush(Color.Brown);
                }
                if ((m_ScreenMemory[i + 1] & 112) == 80)
                {
                    bgBrush = new SolidBrush(Color.Magenta);
                }
                if ((m_ScreenMemory[i + 1] & 112) == 64)
                {
                    bgBrush = new SolidBrush(Color.Red);
                }
                if ((m_ScreenMemory[i + 1] & 112) == 48)
                {
                    bgBrush = new SolidBrush(Color.Cyan);
                }
                if ((m_ScreenMemory[i + 1] & 112) == 32)
                {
                    bgBrush = new SolidBrush(Color.Green);
                }
                if ((m_ScreenMemory[i + 1] & 112) == 16)
                {
                    bgBrush = new SolidBrush(Color.Blue);
                }
                if ((m_ScreenMemory[i + 1] & 112) == 0)
                {
                    bgBrush = new SolidBrush(Color.Black);
                }
                if ((m_ScreenMemory[i + 1] & 7) == 0)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.Gray);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Black);
                    }
                }
                if ((m_ScreenMemory[i + 1] & 7) == 1)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.LightBlue);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Blue);
                    }
                }
                if ((m_ScreenMemory[i + 1] & 7) == 2)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.LightGreen);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Green);
                    }
                }
                if ((m_ScreenMemory[i + 1] & 7) == 3)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.LightCyan);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Cyan);
                    }
                }
                if ((m_ScreenMemory[i + 1] & 7) == 4)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.Pink);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Red);
                    }
                }
                if ((m_ScreenMemory[i + 1] & 7) == 5)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.Fuchsia);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Magenta);
                    }
                }
                if ((m_ScreenMemory[i + 1] & 7) == 6)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.Yellow);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Brown);
                    }
                }
                if ((m_ScreenMemory[i + 1] & 7) == 7)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.White);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Gray);
                    }
                }
                if (bgBrush == null)
                    bgBrush = new SolidBrush(Color.Black);
                if (fgBrush == null)
                    fgBrush = new SolidBrush(Color.Gray);
                if (((xLoc % 640) == 0) && (xLoc != 0))
                {
                    yLoc += 11;
                    xLoc = 0;
                }
                string s =
                System.Text.Encoding.ASCII.GetString(m_ScreenMemory, i, 1);
                PointF pf = new PointF(xLoc, yLoc);
                bmpGraphics.FillRectangle(bgBrush, xLoc + 2, yLoc + 2, 8f, 11f);
                bmpGraphics.DrawString(s, f, fgBrush, pf);
                xLoc += 8;
            }
            e.Graphics.DrawImage(bmp, new Point(0, 0));
            bmpGraphics.Dispose();
            bmp.Dispose();
        }
    }
}
