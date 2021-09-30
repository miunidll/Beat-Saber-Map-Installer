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
    public partial class Form2 : Form
    {
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\bsaberinstaller";
        public Form2()
        {
            InitializeComponent();
            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                if (Directory.Exists(textBox1.Text))
                {
                    if (File.Exists(textBox1.Text + @"\Beat Saber.exe"))
                    {

                        using (StreamWriter outputFile = new StreamWriter(docpath + @"\path.log"))
                        {
                            outputFile.Write(textBox1.Text);
                        }
                        this.Hide();
                        Form1 frm1 = new Form1();
                        frm1.Show();
                    }
                    else
                    {
                        MessageBox.Show("Please select a valid path.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid path.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }
    }
}
