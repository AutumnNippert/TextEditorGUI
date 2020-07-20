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

namespace TextEditorGUI
{
    public partial class Form1 : Form
    {
        bool hasChanges = false;
        Content content;
        string fileTitle = "untitled";
        string filePath = "";

        public Form1()
        {
            InitializeComponent();
            content = new Content();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            content.text = textBox.Text;
            content.textChanged(textBox.Text);
            if (!hasChanges)
            {
                hasChanges = true;
                this.Text = this.Text + "*";
                this.Font = new Font(this.Font, FontStyle.Italic);
                    //normal Malgun Gothic, 10.125pt
            }

        }

        private void updateFont(Font font)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            content.save(filePath);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = content.undo();
            textBox.Select(textBox.Text.Length, 0);
            textBox.Focus();
            textBox.ScrollToCaret();
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (filePath != "")
            {
                var file = new FileInfo(filePath);
                File.WriteAllText(file.FullName, textBox.Text);
                this.Text = fileTitle;
                hasChanges = false;
            }
            else
            {
                string savedFile = "";

                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                saveFileDialog.Title = "Save a Text File";
                saveFileDialog.FileName = "";

                saveFileDialog.Filter = "Text Files|*.txt|All FIles|*.*";

                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                {
                    savedFile = saveFileDialog.FileName;
                    var file = new FileInfo(savedFile);
                    File.WriteAllText(file.FullName, textBox.Text);
                    fileTitle = file.Name;
                    filePath = file.FullName;
                    this.Text = fileTitle;
                    hasChanges = false;
                }
            }

            this.Font = new Font(this.Font, FontStyle.Regular);

        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Text = content.redo();
            textBox.Select(textBox.Text.Length, 0);
            textBox.Focus();
            textBox.ScrollToCaret();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
