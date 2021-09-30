using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatSaberInstaller
{
    public partial class Form3 : Form
    {
        public string filename { get; set; }
        public string filepath { get; set; }
        string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\bsaberinstaller";
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label3.Text = "The file name was: " + filename;
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = File.ReadAllText(docpath + @"\path.log");
            if (Directory.Exists(path + @"\Beat Saber_Data\CustomLevels\" + textBox1.Text))
            {
                DialogResult digres = MessageBox.Show("This name is already taken by another map!\ndo you want to overwrite it?",
                    "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (digres == DialogResult.Yes)
                {
                    Directory.Delete(path + @"\Beat Saber_Data\CustomLevels\" + textBox1.Text, true);
                    Directory.CreateDirectory(path + @"\Beat Saber_Data\CustomLevels\" + textBox1.Text);
                    Cursor.Current = Cursors.WaitCursor;
                    ZipFile.ExtractToDirectory(filepath, path + @"\Beat Saber_Data\CustomLevels\" + textBox1.Text);
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Installation Finished!", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }
            }
            else
            {
                Directory.CreateDirectory(path + @"\Beat Saber_Data\CustomLevels\" + textBox1.Text);
                Cursor.Current = Cursors.WaitCursor;
                ZipFile.ExtractToDirectory(filepath, path + @"\Beat Saber_Data\CustomLevels\" + textBox1.Text);
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Installation Finished!", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            }
        }
    }
}
