using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatSaberInstaller
{
    public partial class Form1 : Form
    {
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\bsaberinstaller";
        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragOver += new DragEventHandler(Form1_DragLeave);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

        }
        public void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        public void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (!(files.Length > 1))
            {
                if (files[0].Contains(".zip"))
                {
                    Form3 frm3 = new Form3();
                    frm3.filepath = files[0];
                    frm3.filename = Path.GetFileName(files[0]);
                    frm3.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Only .zip files are supported!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You can't install more than one map at a time!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void Form1_DragLeave(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.None;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(docpath))
            {
                if (File.Exists(docpath + @"\path.log"))
                {
                    if (File.ReadAllText(docpath + @"\path.log") == string.Empty)
                    {
                        BeginInvoke(new MethodInvoker(delegate
                        {
                            Hide();
                        }));
                        Form2 frm2 = new Form2();
                        frm2.Show();
                        return;
                    }
                }
                else
                {
                    BeginInvoke(new MethodInvoker(delegate
                    {
                        Hide();
                    }));
                    var logFile = File.Create(docpath + @"\path.log");
                    logFile.Close();
                    Form2 frm2 = new Form2();
                    frm2.Show();
                    return;
                }
            }
            else
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    Hide();
                }));
                Directory.CreateDirectory(docpath);
                var logFile = File.Create(docpath + @"\path.log");
                logFile.Close();
                Form2 frm2 = new Form2();
                frm2.Show();
                return;
            }
            using (StreamReader reader = new StreamReader(docpath + @"\path.log"))
            {
                string line = reader.ReadLine();
                if (Directory.Exists(line))
                {
                    if (File.Exists(line + @"\Beat Saber.exe"))
                    {
                        //do nothing
                    }
                    else
                    {
                        MessageBox.Show("Game path is invalid!\nPlease rechoose it!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Game path is invalid!\nPlease rechoose it!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult diagres = MessageBox.Show("Are you sure you want to rechoose the game path?", "Rechoose Game Path",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (diagres == DialogResult.Yes)
            {
                File.WriteAllText(docpath + @"\path.log", "");
                Application.Restart();
            }
        }
    }
}
