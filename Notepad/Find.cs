using System;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Find : Form
    {
        public Find()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Notepad.MatchCase = true;
            }
            else
            {
                Notepad.MatchCase = false;
            }

            Notepad.FindText = textBox1.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Notepad.FindText = "";
            this.Close();
        }

    }

}
