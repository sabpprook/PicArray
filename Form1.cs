using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicMatrix
{
    public partial class Form1 : Form
    {
        public Bitmap b1, b2;
        public Form preview = new Preview();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this.Owner) == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                b1 = openBitmap(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this.Owner) == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
                b2 = openBitmap(textBox2.Text);
            }
            ImageMerge();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (preview.BackgroundImage != null)
            {
                if (saveFileDialog1.ShowDialog(this.Owner) == DialogResult.OK)
                    preview.BackgroundImage.Save(saveFileDialog1.FileName);
            }
        }

        private Bitmap openBitmap(string fileName)
        {
            return new Bitmap(new MemoryStream(File.ReadAllBytes(fileName)));
        }

        private void ImagePreview(Image img)
        {
            preview.Size = img.Size;
            preview.BackgroundImage = img;
            preview.Show();
        }

        private void ImageMerge()
        {
            MemoryStream ms = null;

            ms = new MemoryStream();
            b1.Save(ms, ImageFormat.Bmp);
            byte[] b1Array = new byte[ms.Length];
            b1Array = ms.ToArray();

            ms = new MemoryStream();
            b2.Save(ms, ImageFormat.Bmp);
            byte[] b2Array = new byte[ms.Length];
            b2Array = ms.ToArray();

            for (int i=54; i< b1Array.Length; i++)
                b2Array[i] += b1Array[i];

            ms = new MemoryStream(b2Array);
            ImagePreview(Image.FromStream(ms));
        }
    }
}
