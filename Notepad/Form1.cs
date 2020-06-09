using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{
    public partial class Notepad : Form
    {
        public static string FindText = "";
        public static Boolean MatchCase;
        public static string ReplaceText = "";
        int d;

        public frmAbout frmabout;
        public Discord discord;
        public Find find;
        public Replace replace;
        string path;
        public Notepad()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");

            statusBar1.Panels[0].Text = "";
            statusBar1.Panels[1].Text = "";
            statusBar1.Panels[2].Text = "";

            offToolStripMenuItem.Checked = true;
            hourClockToolStripMenuItem.Checked = false;
            hourClockToolStripMenuItem1.Checked = false;
            verticalToolStripMenuItem.Checked = false;
            horizontalToolStripMenuItem.Checked = false;
            bothToolStripMenuItem.Checked = true;
            noneToolStripMenuItem.Checked = false;
            offToolStripMenuItem3.Checked = true;
            onToolStripMenuItem2.Checked = false;
            offToolStripMenuItem4.Checked = true;
            onToolStripMenuItem3.Checked = false;
            statusBar1.Hide();
            timeToolStripMenuItem.Enabled = false;
            wordCounterToolStripMenuItem.Enabled = false;
            characterCounterToolStripMenuItem.Enabled = false;
            fontToolStripMenuItem2.Enabled = false;
            columnsToolStripMenuItem.Enabled = false;

            bothToolStripMenuItem.Checked = true;
            textBox.WordWrap = false;

            statusBar1.AutoSize = true;
            statusBar1.Panels[0].AutoSize = StatusBarPanelAutoSize.Contents;
            statusBar1.Panels[1].AutoSize = StatusBarPanelAutoSize.Contents;
            statusBar1.Panels[2].AutoSize = StatusBarPanelAutoSize.Contents;

            statusBar1.Panels[0].BorderStyle = StatusBarPanelBorderStyle.None;
            statusBar1.Panels[1].BorderStyle = StatusBarPanelBorderStyle.None;
            statusBar1.Panels[2].BorderStyle = StatusBarPanelBorderStyle.None;
        }

        public void GetSettings()
        {
            textBox.Font = Properties.Settings.Default.Font;
            textBox.ForeColor = Properties.Settings.Default.Color;
            textBox.BackColor = Properties.Settings.Default.Mode;
            menuStrip1.BackColor = Properties.Settings.Default.menuStripMode;
            menuStrip1.Font = Properties.Settings.Default.menuStripFont;
            menuStrip1.ForeColor = Properties.Settings.Default.menuStripColor;
            textBox.ScrollBars = Properties.Settings.Default.scrollBars;
            statusBar1.Font = Properties.Settings.Default.statusBarFont;
            statusBarPanel1.BorderStyle = Properties.Settings.Default.statusBarColumns;
            statusBarPanel2.BorderStyle = Properties.Settings.Default.statusBarColumns;
            statusBarPanel3.BorderStyle = Properties.Settings.Default.statusBarColumns;

            onToolStripMenuItem2.Checked = Properties.Settings.Default.statusBar;

            if (onToolStripMenuItem2.Checked == true)
            {
                onToolStripMenuItem2.Checked = true;
                offToolStripMenuItem3.Checked = false;
                statusBar1.Show();
                timeToolStripMenuItem.Enabled = true;
                wordCounterToolStripMenuItem.Enabled = true;
                characterCounterToolStripMenuItem.Enabled = true;
                fontToolStripMenuItem2.Enabled = true;
                columnsToolStripMenuItem.Enabled = true;
            }

            else
            {
                onToolStripMenuItem2.Checked = false;
                offToolStripMenuItem3.Checked = true;
                statusBar1.Hide();
                timeToolStripMenuItem.Enabled = false;
                wordCounterToolStripMenuItem.Enabled = false;
                characterCounterToolStripMenuItem.Enabled = false;
                fontToolStripMenuItem2.Enabled = false;
                columnsToolStripMenuItem.Enabled = false;
            }

            hourClockToolStripMenuItem.Checked = Properties.Settings.Default.twelveHours;
            hourClockToolStripMenuItem1.Checked = Properties.Settings.Default.twentyFourHours;
            offToolStripMenuItem.Checked = Properties.Settings.Default.timerOff;

            if (hourClockToolStripMenuItem.Checked == true)
            {
                hourClockToolStripMenuItem.Checked = true;
                hourClockToolStripMenuItem1.Checked = false;
                offToolStripMenuItem.Checked = false;

                timer1.Start();
            }

            else if (hourClockToolStripMenuItem1.Checked == true)
            {
                hourClockToolStripMenuItem.Checked = false;
                hourClockToolStripMenuItem1.Checked = true;
                offToolStripMenuItem.Checked = false;

                timer1.Start();
            }

            else if (offToolStripMenuItem.Checked == true)
            {
                hourClockToolStripMenuItem.Checked = false;
                hourClockToolStripMenuItem1.Checked = false;
                offToolStripMenuItem.Checked = true;

                timer1.Stop();
            }

            onToolStripMenuItem.Checked = Properties.Settings.Default.statusBarWordCounter;

            if (onToolStripMenuItem.Checked == true)
            {
                onToolStripMenuItem.Checked = true;
                offToolStripMenuItem1.Checked = false;
                textBox.TextChanged += WordCounter;
            }

            else
            {
                onToolStripMenuItem.Checked = false;
                offToolStripMenuItem1.Checked = true;
                textBox.TextChanged -= WordCounter;
            }

            onToolStripMenuItem1.Checked = Properties.Settings.Default.statusBarCharCounter;

            if (onToolStripMenuItem1.Checked == true)
            {
                onToolStripMenuItem1.Checked = true;
                offToolStripMenuItem2.Checked = false;
                textBox.TextChanged += CharCounter;
            }

            else
            {
                onToolStripMenuItem1.Checked = false;
                offToolStripMenuItem2.Checked = true;
                textBox.TextChanged -= CharCounter;
            }

            onToolStripMenuItem3.Checked = Properties.Settings.Default.columnOn;

            if (onToolStripMenuItem3.Checked == true)
            {
                onToolStripMenuItem3.Checked = true;
                offToolStripMenuItem4.Checked = false;
            }

            else
            {
                onToolStripMenuItem3.Checked = false;
                offToolStripMenuItem4.Checked = true;
            }

            verticalToolStripMenuItem.Checked = Properties.Settings.Default.verticalScroll;
            horizontalToolStripMenuItem.Checked = Properties.Settings.Default.horizontalScroll;
            bothToolStripMenuItem.Checked = Properties.Settings.Default.bothScroll;
            noneToolStripMenuItem.Checked = Properties.Settings.Default.noneScroll;

            if (verticalToolStripMenuItem.Checked == true)
            {
                noneToolStripMenuItem.Checked = false;
                bothToolStripMenuItem.Checked = false;
                horizontalToolStripMenuItem.Checked = false;
                verticalToolStripMenuItem.Checked = true;

                textBox.WordWrap = true;
            }
            else if (horizontalToolStripMenuItem.Checked == true)
            {
                noneToolStripMenuItem.Checked = false;
                bothToolStripMenuItem.Checked = false;
                horizontalToolStripMenuItem.Checked = true;
                verticalToolStripMenuItem.Checked = false;

                textBox.WordWrap = false;
            }
            else if (bothToolStripMenuItem.Checked == true)
            {
                noneToolStripMenuItem.Checked = false;
                bothToolStripMenuItem.Checked = true;
                horizontalToolStripMenuItem.Checked = false;
                verticalToolStripMenuItem.Checked = false;

                textBox.WordWrap = false;
            }
            else if (noneToolStripMenuItem.Checked == true)
            {
                noneToolStripMenuItem.Checked = true;
                bothToolStripMenuItem.Checked = false;
                horizontalToolStripMenuItem.Checked = false;
                verticalToolStripMenuItem.Checked = false;

                textBox.WordWrap = true;
            }

            lightModeToolStripMenuItem.Checked = Properties.Settings.Default.lightMode;
            darkModeToolStripMenuItem.Checked = Properties.Settings.Default.darkMode;
            blueModeToolStripMenuItem.Checked = Properties.Settings.Default.blueMode;
            pinkModeToolStripMenuItem.Checked = Properties.Settings.Default.pinkMode;
            oliveModeToolStripMenuItem.Checked = Properties.Settings.Default.oliveMode;
            colorModeToolStripMenuItem.Checked = Properties.Settings.Default.colorMode;
            followStripMenuToolStripMenuItem.Checked = Properties.Settings.Default.followStrip;

            if (lightModeToolStripMenuItem.Checked == true)
            {
                darkModeToolStripMenuItem.Checked = false;
                lightModeToolStripMenuItem.Checked = true;
                blueModeToolStripMenuItem.Checked = false;
                oliveModeToolStripMenuItem.Checked = false;
                pinkModeToolStripMenuItem.Checked = false;
                colorModeToolStripMenuItem.Checked = false;
                followStripMenuToolStripMenuItem.Checked = false;

            }
            else if (darkModeToolStripMenuItem.Checked == true)
            {
                darkModeToolStripMenuItem.Checked = true;
                lightModeToolStripMenuItem.Checked = false;
                blueModeToolStripMenuItem.Checked = false;
                oliveModeToolStripMenuItem.Checked = false;
                pinkModeToolStripMenuItem.Checked = false;
                colorModeToolStripMenuItem.Checked = false;
                followStripMenuToolStripMenuItem.Checked = false;
            }
            else if (blueModeToolStripMenuItem.Checked == true)
            {
                darkModeToolStripMenuItem.Checked = false;
                lightModeToolStripMenuItem.Checked = false;
                blueModeToolStripMenuItem.Checked = true;
                oliveModeToolStripMenuItem.Checked = false;
                pinkModeToolStripMenuItem.Checked = false;
                colorModeToolStripMenuItem.Checked = false;
                followStripMenuToolStripMenuItem.Checked = false;
            }
            else if (pinkModeToolStripMenuItem.Checked == true)
            {
                darkModeToolStripMenuItem.Checked = false;
                lightModeToolStripMenuItem.Checked = false;
                blueModeToolStripMenuItem.Checked = false;
                oliveModeToolStripMenuItem.Checked = false;
                pinkModeToolStripMenuItem.Checked = true;
                colorModeToolStripMenuItem.Checked = false;
                followStripMenuToolStripMenuItem.Checked = false;
            }
            else if (oliveModeToolStripMenuItem.Checked == true)
            {
                darkModeToolStripMenuItem.Checked = false;
                lightModeToolStripMenuItem.Checked = false;
                blueModeToolStripMenuItem.Checked = false;
                oliveModeToolStripMenuItem.Checked = true;
                pinkModeToolStripMenuItem.Checked = false;
                colorModeToolStripMenuItem.Checked = false;
                followStripMenuToolStripMenuItem.Checked = false;
            }
            else if (colorModeToolStripMenuItem.Checked == true)
            {
                darkModeToolStripMenuItem.Checked = false;
                lightModeToolStripMenuItem.Checked = false;
                blueModeToolStripMenuItem.Checked = false;
                oliveModeToolStripMenuItem.Checked = false;
                pinkModeToolStripMenuItem.Checked = false;
                colorModeToolStripMenuItem.Checked = true;
                followStripMenuToolStripMenuItem.Checked = false;
            }
            else if (followStripMenuToolStripMenuItem.Checked == true)
            {
                darkModeToolStripMenuItem.Checked = false;
                lightModeToolStripMenuItem.Checked = false;
                blueModeToolStripMenuItem.Checked = false;
                oliveModeToolStripMenuItem.Checked = false;
                pinkModeToolStripMenuItem.Checked = false;
                colorModeToolStripMenuItem.Checked = false;
                followStripMenuToolStripMenuItem.Checked = true;
            }

            lightModeToolStripMenuItem1.Checked = Properties.Settings.Default.lightStrip;
            darkModeToolStripMenuItem1.Checked = Properties.Settings.Default.darkStrip;
            blueModeToolStripMenuItem1.Checked = Properties.Settings.Default.blueStrip;
            pinkModeToolStripMenuItem1.Checked = Properties.Settings.Default.pinkStrip;
            oliveModeToolStripMenuItem1.Checked = Properties.Settings.Default.oliveStrip;
            colorModeToolStripMenuItem1.Checked = Properties.Settings.Default.colorStrip;
            followTextBoxToolStripMenuItem.Checked = Properties.Settings.Default.followTextBox;

            if (lightModeToolStripMenuItem1.Checked == true)
            {
                darkModeToolStripMenuItem1.Checked = false;
                lightModeToolStripMenuItem1.Checked = true;
                blueModeToolStripMenuItem1.Checked = false;
                oliveModeToolStripMenuItem1.Checked = false;
                pinkModeToolStripMenuItem1.Checked = false;
                colorModeToolStripMenuItem1.Checked = false;
                followTextBoxToolStripMenuItem.Checked = false;
            }
            else if (darkModeToolStripMenuItem1.Checked == true)
            {
                darkModeToolStripMenuItem1.Checked = true;
                lightModeToolStripMenuItem1.Checked = false;
                blueModeToolStripMenuItem1.Checked = false;
                oliveModeToolStripMenuItem1.Checked = false;
                pinkModeToolStripMenuItem1.Checked = false;
                colorModeToolStripMenuItem1.Checked = false;
                followTextBoxToolStripMenuItem.Checked = false;
            }
            else if (blueModeToolStripMenuItem1.Checked == true)
            {
                darkModeToolStripMenuItem1.Checked = false;
                lightModeToolStripMenuItem1.Checked = false;
                blueModeToolStripMenuItem1.Checked = true;
                oliveModeToolStripMenuItem1.Checked = false;
                pinkModeToolStripMenuItem1.Checked = false;
                colorModeToolStripMenuItem1.Checked = false;
                followTextBoxToolStripMenuItem.Checked = false;
            }
            else if (pinkModeToolStripMenuItem1.Checked == true)
            {
                darkModeToolStripMenuItem1.Checked = false;
                lightModeToolStripMenuItem1.Checked = false;
                blueModeToolStripMenuItem1.Checked = false;
                oliveModeToolStripMenuItem1.Checked = false;
                pinkModeToolStripMenuItem1.Checked = true;
                colorModeToolStripMenuItem1.Checked = false;
                followTextBoxToolStripMenuItem.Checked = false;
            }
            else if (oliveModeToolStripMenuItem1.Checked == true)
            {
                darkModeToolStripMenuItem1.Checked = false;
                lightModeToolStripMenuItem1.Checked = false;
                blueModeToolStripMenuItem1.Checked = false;
                oliveModeToolStripMenuItem1.Checked = true;
                pinkModeToolStripMenuItem1.Checked = false;
                colorModeToolStripMenuItem1.Checked = false;
                followTextBoxToolStripMenuItem.Checked = false;
            }
            else if (colorModeToolStripMenuItem1.Checked == true)
            {
                darkModeToolStripMenuItem1.Checked = false;
                lightModeToolStripMenuItem1.Checked = false;
                blueModeToolStripMenuItem1.Checked = false;
                oliveModeToolStripMenuItem1.Checked = false;
                pinkModeToolStripMenuItem1.Checked = false;
                colorModeToolStripMenuItem1.Checked = true;
                followTextBoxToolStripMenuItem.Checked = false;
            }
            else if (followTextBoxToolStripMenuItem.Checked == true)
            {
                darkModeToolStripMenuItem1.Checked = false;
                lightModeToolStripMenuItem1.Checked = false;
                blueModeToolStripMenuItem1.Checked = false;
                oliveModeToolStripMenuItem1.Checked = false;
                pinkModeToolStripMenuItem1.Checked = false;
                colorModeToolStripMenuItem1.Checked = false;
                followTextBoxToolStripMenuItem.Checked = true;
            }


        }

        public void SaveSettings()
        {
            Properties.Settings.Default.Font = textBox.Font;
            Properties.Settings.Default.Color = textBox.ForeColor;
            Properties.Settings.Default.Mode = textBox.BackColor;
            Properties.Settings.Default.menuStripMode = menuStrip1.BackColor;
            Properties.Settings.Default.menuStripFont = menuStrip1.Font;
            Properties.Settings.Default.menuStripColor = menuStrip1.ForeColor;
            Properties.Settings.Default.scrollBars = textBox.ScrollBars;
            Properties.Settings.Default.statusBarFont = statusBar1.Font;
            Properties.Settings.Default.statusBarColumns = statusBarPanel1.BorderStyle;
            Properties.Settings.Default.statusBarColumns = statusBarPanel2.BorderStyle;
            Properties.Settings.Default.statusBarColumns = statusBarPanel3.BorderStyle;

            Properties.Settings.Default.statusBar = onToolStripMenuItem2.Checked;

            Properties.Settings.Default.twelveHours = hourClockToolStripMenuItem.Checked;
            Properties.Settings.Default.twentyFourHours = hourClockToolStripMenuItem1.Checked;
            Properties.Settings.Default.timerOff = offToolStripMenuItem.Checked;

            Properties.Settings.Default.statusBarWordCounter = onToolStripMenuItem.Checked;

            Properties.Settings.Default.statusBarCharCounter = onToolStripMenuItem1.Checked;

            Properties.Settings.Default.columnOn = onToolStripMenuItem3.Checked;

            Properties.Settings.Default.verticalScroll = verticalToolStripMenuItem.Checked;
            Properties.Settings.Default.horizontalScroll = horizontalToolStripMenuItem.Checked;
            Properties.Settings.Default.bothScroll = bothToolStripMenuItem.Checked;
            Properties.Settings.Default.noneScroll = noneToolStripMenuItem.Checked;

            Properties.Settings.Default.lightMode = lightModeToolStripMenuItem.Checked;
            Properties.Settings.Default.darkMode = darkModeToolStripMenuItem.Checked;
            Properties.Settings.Default.blueMode = blueModeToolStripMenuItem.Checked;
            Properties.Settings.Default.pinkMode = pinkModeToolStripMenuItem.Checked;
            Properties.Settings.Default.oliveMode = oliveModeToolStripMenuItem.Checked;
            Properties.Settings.Default.colorMode = colorModeToolStripMenuItem.Checked;
            Properties.Settings.Default.followStrip = followStripMenuToolStripMenuItem.Checked;

            Properties.Settings.Default.lightStrip = lightModeToolStripMenuItem1.Checked;
            Properties.Settings.Default.darkStrip = darkModeToolStripMenuItem1.Checked;
            Properties.Settings.Default.blueStrip = blueModeToolStripMenuItem1.Checked;
            Properties.Settings.Default.pinkStrip = pinkModeToolStripMenuItem1.Checked;
            Properties.Settings.Default.oliveStrip = oliveModeToolStripMenuItem1.Checked;
            Properties.Settings.Default.colorStrip = colorModeToolStripMenuItem1.Checked;
            Properties.Settings.Default.followTextBox = followTextBoxToolStripMenuItem.Checked;

            Properties.Settings.Default.Save();
        }

        private void Notepad_Load(object sender, EventArgs e)
        {
            GetSettings();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            path = string.Empty;
            textBox.Clear();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            path = ofd.FileName;
                            Task<string> text = sr.ReadToEndAsync();
                            textBox.Text = text.Result;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        internal ColorDialog ColorDialog()
        {
            throw new NotImplementedException();
        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path))
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            path = sfd.FileName;
                            using (StreamWriter sw = new StreamWriter(sfd.FileName))
                            {
                                await sw.WriteLineAsync(textBox.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        await sw.WriteLineAsync(textBox.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            await sw.WriteLineAsync(textBox.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmAbout frm = new frmAbout())
            {
                frm.ShowDialog();
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        bool CanApplicationClose = false;

        private void Notepad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CanApplicationClose == false)
            {
                e.Cancel = true;

                DialogResult confirm = MessageBox.Show("Are you sure you want to exit application?", "Exit", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    CanApplicationClose = true;
                    Application.Exit();
                }

                SaveSettings();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Undo();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Cut();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.SelectedText = string.Empty;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = System.DateTime.Now.ToString();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog op = new FontDialog();
            op.Font = textBox.Font;
            if (op.ShowDialog() == DialogResult.OK)
                textBox.Font = op.Font;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog op = new ColorDialog();
            op.Color = textBox.ForeColor;
            if (op.ShowDialog() == DialogResult.OK)
                textBox.ForeColor = op.Color;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(this.textBox.Text, this.textBox.Font, Brushes.Black, 10, 25);
        }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void telegramSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/ncyxie");
        }

        private void discordSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Discord r = new Discord();
            r.ShowDialog();
        }

        private void GithubRepoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ncyxie/Notepad-DOT-Beta");
        }

        private void searchWithGoogleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/search?q=" + textBox.SelectedText);
        }

        private void searchWithBingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.bing.com/search?q=" + textBox.SelectedText);
        }

        private void searchWithDuckDuckGoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://duckduckgo.com/?q=" + textBox.SelectedText);
        }

        private void lightModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem.Checked = false;
            lightModeToolStripMenuItem.Checked = true;
            blueModeToolStripMenuItem.Checked = false;
            oliveModeToolStripMenuItem.Checked = false;
            pinkModeToolStripMenuItem.Checked = false;
            colorModeToolStripMenuItem.Checked = false;
            followStripMenuToolStripMenuItem.Checked = false;

            Properties.Settings.Default.Theme = "light";

            this.textBox.BackColor = Color.White;
            this.textBox.ForeColor = Color.Black;
        }

        private void darkModeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem.Checked = true;
            lightModeToolStripMenuItem.Checked = false;
            blueModeToolStripMenuItem.Checked = false;
            oliveModeToolStripMenuItem.Checked = false;
            pinkModeToolStripMenuItem.Checked = false;
            colorModeToolStripMenuItem.Checked = false;
            followStripMenuToolStripMenuItem.Checked = false;

            Properties.Settings.Default.Theme = "dark";

            this.textBox.BackColor = Color.FromArgb(30, 30, 30);
            this.textBox.ForeColor = Color.White;
        }

        private void blueModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem.Checked = false;
            lightModeToolStripMenuItem.Checked = false;
            blueModeToolStripMenuItem.Checked = true;
            oliveModeToolStripMenuItem.Checked = false;
            pinkModeToolStripMenuItem.Checked = false;
            colorModeToolStripMenuItem.Checked = false;
            followStripMenuToolStripMenuItem.Checked = false;

            Properties.Settings.Default.Theme = "blue";

            this.textBox.BackColor = Color.FromArgb(0, 103, 179);
            this.textBox.ForeColor = Color.White;
        }

        private void oliveModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem.Checked = false;
            lightModeToolStripMenuItem.Checked = false;
            blueModeToolStripMenuItem.Checked = false;
            oliveModeToolStripMenuItem.Checked = true;
            pinkModeToolStripMenuItem.Checked = false;
            colorModeToolStripMenuItem.Checked = false;
            followStripMenuToolStripMenuItem.Checked = false;

            Properties.Settings.Default.Theme = "olive";

            this.textBox.BackColor = Color.FromArgb(107, 142, 35);
            this.textBox.ForeColor = Color.White;
        }

        private void pinkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem.Checked = false;
            lightModeToolStripMenuItem.Checked = false;
            blueModeToolStripMenuItem.Checked = false;
            oliveModeToolStripMenuItem.Checked = false;
            pinkModeToolStripMenuItem.Checked = true;
            colorModeToolStripMenuItem.Checked = false;
            followStripMenuToolStripMenuItem.Checked = false;

            Properties.Settings.Default.Theme = "pink";

            this.textBox.BackColor = Color.FromArgb(255, 192, 203);
            this.textBox.ForeColor = Color.Black;
        }
        private void colorModeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem.Checked = false;
            lightModeToolStripMenuItem.Checked = false;
            blueModeToolStripMenuItem.Checked = false;
            oliveModeToolStripMenuItem.Checked = false;
            pinkModeToolStripMenuItem.Checked = false;
            colorModeToolStripMenuItem.Checked = true;
            followStripMenuToolStripMenuItem.Checked = false;

            Properties.Settings.Default.Theme = "color";

            ColorDialog MyDialog = new ColorDialog();

            MyDialog.Color = textBox.BackColor;

            ColorDialog op = new ColorDialog();

            op.Color = textBox.BackColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
                textBox.BackColor = MyDialog.Color;

        }
        private void followStripMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem.Checked = false;
            lightModeToolStripMenuItem.Checked = false;
            blueModeToolStripMenuItem.Checked = false;
            oliveModeToolStripMenuItem.Checked = false;
            pinkModeToolStripMenuItem.Checked = false;
            colorModeToolStripMenuItem.Checked = false;
            followStripMenuToolStripMenuItem.Checked = true;

            textBox.BackColor = menuStrip1.BackColor;
        }

        private void gitHubReleasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ncyxie/Notepad-DOT-Beta/releases");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hourClockToolStripMenuItem.Checked)
            {
                statusBar1.Panels[0].Text = DateTime.Now.ToString("hh:mm tt");
            }
            else if (hourClockToolStripMenuItem1.Checked)
            {
                statusBar1.Panels[0].Text = DateTime.Now.ToString("HH:mm");
            }
        }

        private void hourClockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hourClockToolStripMenuItem.Checked = true;
            hourClockToolStripMenuItem1.Checked = false;
            offToolStripMenuItem.Checked = false;

            timer1.Start();
        }

        private void hourClockToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            hourClockToolStripMenuItem.Checked = false;
            hourClockToolStripMenuItem1.Checked = true;
            offToolStripMenuItem.Checked = false;

            timer1.Start();
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hourClockToolStripMenuItem.Checked = false;
            hourClockToolStripMenuItem1.Checked = false;
            offToolStripMenuItem.Checked = true;

            timer1.Stop();
            statusBar1.Panels[0].Text = "";
        }

        private void WordCounter(object sender, EventArgs e)
        {
            string txt = textBox.Text;
            char[] separator = { ' ' };

            int wordsCount = txt.Split(separator, StringSplitOptions.RemoveEmptyEntries).Length;

            statusBar1.Panels[1].Text = "Words: " + wordsCount.ToString();
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onToolStripMenuItem.Checked = true;
            offToolStripMenuItem1.Checked = false;
            textBox.TextChanged += WordCounter;
        }

        private void offToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            onToolStripMenuItem.Checked = false;
            offToolStripMenuItem1.Checked = true;
            textBox.TextChanged -= WordCounter;
            statusBar1.Panels[1].Text = "";
        }


        private void CharCounter(object sender, EventArgs e)
        {
            string txt = textBox.Text;

            int charCount = txt.Length;
            statusBar1.Panels[2].Text = "Characters: " + charCount.ToString();
        }

        private void onToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            onToolStripMenuItem1.Checked = true;
            offToolStripMenuItem2.Checked = false;
            textBox.TextChanged += CharCounter;
        }

        private void offToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            onToolStripMenuItem1.Checked = false;
            offToolStripMenuItem2.Checked = true;
            textBox.TextChanged -= CharCounter;
            statusBar1.Panels[2].Text = "";
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = true;
            bothToolStripMenuItem.Checked = false;
            horizontalToolStripMenuItem.Checked = false;
            verticalToolStripMenuItem.Checked = false;

            textBox.ScrollBars = RichTextBoxScrollBars.None;
            textBox.WordWrap = true;
        }

        private void bothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            bothToolStripMenuItem.Checked = true;
            horizontalToolStripMenuItem.Checked = false;
            verticalToolStripMenuItem.Checked = false;

            textBox.ScrollBars = RichTextBoxScrollBars.Both;
            textBox.WordWrap = false;
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            bothToolStripMenuItem.Checked = false;
            horizontalToolStripMenuItem.Checked = true;
            verticalToolStripMenuItem.Checked = false;

            textBox.ScrollBars = RichTextBoxScrollBars.Horizontal;
            textBox.WordWrap = false;
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            noneToolStripMenuItem.Checked = false;
            bothToolStripMenuItem.Checked = false;
            horizontalToolStripMenuItem.Checked = false;
            verticalToolStripMenuItem.Checked = true;

            textBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            textBox.WordWrap = true;
        }

        private void offToolStripMenuItem3_Click_1(object sender, EventArgs e)
        {
            offToolStripMenuItem3.Checked = true;
            onToolStripMenuItem2.Checked = false;
            statusBar1.Hide();
            timeToolStripMenuItem.Enabled = false;
            wordCounterToolStripMenuItem.Enabled = false;
            characterCounterToolStripMenuItem.Enabled = false;
            fontToolStripMenuItem2.Enabled = false;
            columnsToolStripMenuItem.Enabled = false;
        }

        private void onToolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            offToolStripMenuItem3.Checked = false;
            onToolStripMenuItem2.Checked = true;
            statusBar1.Show();
            timeToolStripMenuItem.Enabled = true;
            wordCounterToolStripMenuItem.Enabled = true;
            characterCounterToolStripMenuItem.Enabled = true;
            fontToolStripMenuItem2.Enabled = true;
            columnsToolStripMenuItem.Enabled = true;
        }

        private void timeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void wordCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void characterCounterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fontToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog op = new FontDialog();
            op.Font = menuStrip1.Font;
            op.MinSize = 8;
            op.MaxSize = 24;
            if (op.ShowDialog() == DialogResult.OK)
                menuStrip1.Font = op.Font;
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog op = new ColorDialog();
            op.Color = menuStrip1.ForeColor;
            if (op.ShowDialog() == DialogResult.OK)
                menuStrip1.ForeColor = op.Color;
        }

        private void lightModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem1.Checked = false;
            lightModeToolStripMenuItem1.Checked = true;
            blueModeToolStripMenuItem1.Checked = false;
            oliveModeToolStripMenuItem1.Checked = false;
            pinkModeToolStripMenuItem1.Checked = false;
            colorModeToolStripMenuItem1.Checked = false;
            followTextBoxToolStripMenuItem.Checked = false;

            menuStrip1.BackColor = Color.White;
            menuStrip1.ForeColor = Color.Black;
        }

        private void darkModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem1.Checked = true;
            lightModeToolStripMenuItem1.Checked = false;
            blueModeToolStripMenuItem1.Checked = false;
            oliveModeToolStripMenuItem1.Checked = false;
            pinkModeToolStripMenuItem1.Checked = false;
            colorModeToolStripMenuItem1.Checked = false;
            followTextBoxToolStripMenuItem.Checked = false;

            menuStrip1.BackColor = Color.FromArgb(30, 30, 30);
            menuStrip1.ForeColor = Color.White;
        }

        private void blueModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem1.Checked = false;
            lightModeToolStripMenuItem1.Checked = false;
            blueModeToolStripMenuItem1.Checked = true;
            oliveModeToolStripMenuItem1.Checked = false;
            pinkModeToolStripMenuItem1.Checked = false;
            colorModeToolStripMenuItem1.Checked = false;
            followTextBoxToolStripMenuItem.Checked = false;

            menuStrip1.BackColor = Color.FromArgb(0, 103, 179);
            menuStrip1.ForeColor = Color.White;
        }

        private void pinkModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem1.Checked = false;
            lightModeToolStripMenuItem1.Checked = false;
            blueModeToolStripMenuItem1.Checked = false;
            oliveModeToolStripMenuItem1.Checked = false;
            pinkModeToolStripMenuItem1.Checked = true;
            colorModeToolStripMenuItem1.Checked = false;
            followTextBoxToolStripMenuItem.Checked = false;

            menuStrip1.BackColor = Color.FromArgb(255, 192, 203);
            menuStrip1.ForeColor = Color.Black;
        }

        private void oliveModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem1.Checked = false;
            lightModeToolStripMenuItem1.Checked = false;
            blueModeToolStripMenuItem1.Checked = false;
            oliveModeToolStripMenuItem1.Checked = true;
            pinkModeToolStripMenuItem1.Checked = false;
            colorModeToolStripMenuItem1.Checked = false;
            followTextBoxToolStripMenuItem.Checked = false;

            menuStrip1.BackColor = Color.FromArgb(107, 142, 35);
            menuStrip1.ForeColor = Color.White;
        }

        private void colorModeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem1.Checked = false;
            lightModeToolStripMenuItem1.Checked = false;
            blueModeToolStripMenuItem1.Checked = false;
            oliveModeToolStripMenuItem1.Checked = false;
            pinkModeToolStripMenuItem1.Checked = false;
            colorModeToolStripMenuItem1.Checked = true;
            followTextBoxToolStripMenuItem.Checked = false;

            ColorDialog MyDialog = new ColorDialog();

            MyDialog.Color = menuStrip1.BackColor;

            ColorDialog op = new ColorDialog();

            op.Color = menuStrip1.BackColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
                menuStrip1.BackColor = MyDialog.Color;

        }

        private void followTextBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            darkModeToolStripMenuItem1.Checked = false;
            lightModeToolStripMenuItem1.Checked = false;
            blueModeToolStripMenuItem1.Checked = false;
            oliveModeToolStripMenuItem1.Checked = false;
            pinkModeToolStripMenuItem1.Checked = false;
            colorModeToolStripMenuItem1.Checked = false;
            followTextBoxToolStripMenuItem.Checked = true;

            menuStrip1.BackColor = textBox.BackColor;
        }

        private void fontToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FontDialog op = new FontDialog();
            op.Font = statusBar1.Font;
            op.MinSize = 8;
            op.MaxSize = 16;
            if (op.ShowDialog() == DialogResult.OK)
                statusBar1.Font = op.Font;
        }

        private void columnsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void onToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            onToolStripMenuItem3.Checked = true;
            offToolStripMenuItem4.Checked = false;
            statusBar1.Panels[0].BorderStyle = StatusBarPanelBorderStyle.Sunken;
            statusBar1.Panels[1].BorderStyle = StatusBarPanelBorderStyle.Sunken;
            statusBar1.Panels[2].BorderStyle = StatusBarPanelBorderStyle.Sunken;
        }

        private void offToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            onToolStripMenuItem3.Checked = false;
            offToolStripMenuItem4.Checked = true;
            statusBar1.Panels[0].BorderStyle = StatusBarPanelBorderStyle.None;
            statusBar1.Panels[1].BorderStyle = StatusBarPanelBorderStyle.None;
            statusBar1.Panels[2].BorderStyle = StatusBarPanelBorderStyle.None;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Find r = new Find();
            r.ShowDialog();

            if (FindText != "")
            {
                d = textBox.Find(FindText);
            }

        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindText != "")
            {
                if (MatchCase == true)
                {
                    d = textBox.Find(FindText, (d + 1), textBox.Text.Length, RichTextBoxFinds.MatchCase);

                }
                else
                {
                    d = textBox.Find(FindText, (d + 1), textBox.Text.Length, RichTextBoxFinds.None);
                }

            }

        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replace r = new Replace();
            r.ShowDialog();

            if (MatchCase == true)
            {
                d = textBox.Find(FindText, RichTextBoxFinds.MatchCase);
                textBox.SelectedText = ReplaceText;
            }
            else
            {
                d = textBox.Find(FindText, RichTextBoxFinds.None);
                textBox.SelectedText = ReplaceText;
            }
        }

        private void replaceNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MatchCase == true)
            {
                d = textBox.Find(FindText, (d + 1), textBox.Text.Length, RichTextBoxFinds.MatchCase);
                textBox.SelectedText = ReplaceText;
            }
            else
            {
                d = textBox.Find(FindText, (d + 1), textBox.Text.Length, RichTextBoxFinds.None);
                textBox.SelectedText = ReplaceText;
            }

        }

    }
}

