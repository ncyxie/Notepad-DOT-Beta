using System;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Replace : Form
    {
        public Notepad Notepad;
        public Replace()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Notepad.FindText = textBox1.Text;
            Notepad.ReplaceText = textBox2.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Notepad.FindText = "";
            Notepad.ReplaceText = "";
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Notepad.FindText = textBox1.Text;
            Notepad.ReplaceText = textBox2.Text;

            this.Close();
        }
    }
}
