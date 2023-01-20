using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace Assignment01Question04
{
    class BitPlane
    {
        IplImage src, grayImage, resultImage;
        IplImage src5, src6, src7, grayImage5, grayImage6, grayImage7;

        public void LoadOriginalImage(string fname)
        {
            src = Cv.LoadImage(fname, LoadMode.Color);
            Cv.SaveImage(fname, src);
        }

        public void LoadGrayScaleImage()
        {
            grayImage = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, grayImage, ColorConversion.RgbToGray);
            Cv.SaveImage("gray.jpg", grayImage);
        }

        public Bitmap GetBitPlane(Bitmap bitmap, int bitPlaneIndex)
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color currColor = bitmap.GetPixel(i, j);

                    int bit1 = GetBit(currColor.R, bitPlaneIndex);
                    int bit2 = GetBit(currColor.G, bitPlaneIndex);
                    int bit3 = GetBit(currColor.B, bitPlaneIndex);

                    //Color newColor = Color.FromArgb(255, 255 * bit, 255 * bit); - get red image
                    //Color newColor = Color.FromArgb(255 * bit, 255, 255 * bit); - get green image
                    //Color newColor = Color.FromArgb(255 * bit, 255 * bit, 255); - get blue image
                    Color newColor = Color.FromArgb(255 * bit1, 255 * bit2, 255 * bit3);

                    newBitmap.SetPixel(i, j, newColor);
                }
            }
            return newBitmap;
        }

        public Bitmap ConstructBitPlane(Bitmap bitmap)
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color currColor = bitmap.GetPixel(i, j);

                    int bit1 = (GetBit(currColor.R, 5) + GetBit(currColor.R, 6) + GetBit(currColor.R, 7))/3;
                    int bit2 = (GetBit(currColor.G, 5) + GetBit(currColor.G, 6) + GetBit(currColor.G, 7))/3;
                    int bit3 = (GetBit(currColor.B, 5) + GetBit(currColor.B, 6) + GetBit(currColor.B, 7))/3;

                    //Color newColor = Color.FromArgb(255, 255 * bit, 255 * bit); - get red image
                    //Color newColor = Color.FromArgb(255 * bit, 255, 255 * bit); - get green image
                    //Color newColor = Color.FromArgb(255 * bit, 255 * bit, 255); - get blue image
                    Color newColor = Color.FromArgb(255 * bit1, 255 * bit2, 255 * bit3);

                    newBitmap.SetPixel(i, j, newColor);
                }
            }
            return newBitmap;
        }

        private static int GetBit(byte b, int bitIndex)
        {
            return (b >> bitIndex) & 0x01;
        }

        public void ConstructNearlyOriginalImage()
        {
            LoadGrayScaleImage();

            src5 = Cv.LoadImage("bitPlane5.jpg", LoadMode.Color);
            grayImage5 = Cv.CreateImage(src5.Size, BitDepth.U8, 1);
            Cv.CvtColor(src5, grayImage5, ColorConversion.RgbToGray);

            src6 = Cv.LoadImage("bitPlane6.jpg", LoadMode.Color);
            grayImage6 = Cv.CreateImage(src6.Size, BitDepth.U8, 1);
            Cv.CvtColor(src6, grayImage6, ColorConversion.RgbToGray);

            src7 = Cv.LoadImage("bitPlane7.jpg", LoadMode.Color);
            grayImage7 = Cv.CreateImage(src7.Size, BitDepth.U8, 1);
            Cv.CvtColor(src7, grayImage7, ColorConversion.RgbToGray);

            resultImage = Cv.CreateImage(grayImage.Size, BitDepth.U8, 1);

            for (int y = 1; y < resultImage.Height; y++)
            {
                for (int x = 1; x < resultImage.Width; x++)
                {
                    double getPixelValue = 0;
                    getPixelValue = Cv.GetReal2D(grayImage5, y, x) + Cv.GetReal2D(grayImage6, y, x) + Cv.GetReal2D(grayImage7, y, x);
                    Cv.SetReal2D(resultImage, y, x, getPixelValue);
                }
            }
            Cv.SaveImage("result.jpg", resultImage);
        }

    }
}