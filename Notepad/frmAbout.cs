using System;
using System.Drawing;
using System.Windows.Forms;

namespace Notepad
{
    public partial class frmAbout : Form
    {
        public Notepad notepad;

        public frmAbout()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Theme == "light")
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                btnOK.ForeColor = Color.Black;
            }
            else if (Properties.Settings.Default.Theme == "dark")
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                this.ForeColor = Color.White;
                btnOK.ForeColor = Color.Black;
            }
            else if (Properties.Settings.Default.Theme == "blue")
            {
                this.BackColor = Color.FromArgb(0, 103, 179);
                this.ForeColor = Color.White;
                btnOK.ForeColor = Color.Black;
            }
            else if (Properties.Settings.Default.Theme == "olive")
            {
                this.BackColor = Color.FromArgb(107, 142, 35);
                this.ForeColor = Color.White;
                btnOK.ForeColor = Color.Black;
            }
            else if (Properties.Settings.Default.Theme == "pink")
            {
                this.BackColor = Color.FromArgb(255, 192, 203);
                this.ForeColor = Color.Black;
                btnOK.ForeColor = Color.Black;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void About_Load(object sender, EventArgs e)
        {

        }

        private void lblProductName_Click(object sender, EventArgs e)
        {

        }
    }
}
