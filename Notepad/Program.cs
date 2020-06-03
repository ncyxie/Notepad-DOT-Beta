using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notepad
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Notepad notepad = new Notepad();
            if (args.Length > 0)
            {
                string path = args[0];
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        Task<string> text = sr.ReadToEndAsync();
                        notepad.textBox.Text = text.Result;
                        notepad.textBox.SelectionStart = notepad.textBox.Text.Length;
                        notepad.textBox.SelectionLength = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            Application.Run(notepad);


        }
    }
}
