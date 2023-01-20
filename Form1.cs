using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment01Question04
{
    public partial class Form1 : Form
    {

        BitPlane bpObj = new BitPlane();
        OpenFileDialog ofd = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Title = "Select Image File";
            ofd.Filter = "Images (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|" + "All files (*.*)|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                bpObj.LoadOriginalImage(ofd.FileName);
                string picPath = ofd.FileName.ToString();
                pictureBox1.ImageLocation = picPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = ofd.FileName.ToString();
            string ext = System.IO.Path.GetExtension(filePath);

            Bitmap bitmap = new Bitmap(filePath);
            ImageFormat imageFormat = bitmap.RawFormat;

            for (int i = 0; i < 8; i++)
            {
                Bitmap newBitmap = bpObj.GetBitPlane(bitmap, i);
                newBitmap.Save("bitPlane" + i + ext, imageFormat);
            }

            pictureBox2.ImageLocation = "bitPlane0.jpg";
            pictureBox3.ImageLocation = "bitPlane1.jpg";
            pictureBox4.ImageLocation = "bitPlane2.jpg";
            pictureBox5.ImageLocation = "bitPlane3.jpg";
            pictureBox6.ImageLocation = "bitPlane4.jpg";
            pictureBox7.ImageLocation = "bitPlane5.jpg";
            pictureBox8.ImageLocation = "bitPlane6.jpg";
            pictureBox9.ImageLocation = "bitPlane7.jpg";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bpObj.ConstructNearlyOriginalImage();
            pictureBox10.ImageLocation = "result.jpg";
        }
    }
}