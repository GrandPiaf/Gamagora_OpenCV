using System;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace TP1_EmguCV {
    class Program {
        static void Main(string[] args) {

            String window = "TP1";
            CvInvoke.NamedWindow(window);

            Mat matWebcam = new Mat("..\\..\\..\\..\\images\\crochet.jpg"); //Because it is located in directory "netcoreapp3.1"

            Image<Bgr, Byte> img = matWebcam.ToImage<Bgr, Byte>();
            Image<Bgr, Byte> resultImage = new Image<Bgr, byte>(img.Width * 2, img.Height);

            CopyToImage(ref img, ref resultImage, 0, 0);
            CopyToImage(ref img, ref resultImage, img.Width, 0);

            Mat matRes = resultImage.Mat;

            CvInvoke.Imshow(window, matRes);
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(window); //Destroy the window if key is pressed

        }

        static void CopyToImage(ref Image<Bgr, Byte> inputImg, ref Image<Bgr, Byte> outputImg, int offsetX, int offsetY) {

            for (int i = 0; i < inputImg.Height; i++) {
                for (int j = 0; j < inputImg.Width; j++) {

                    outputImg.Data[i + offsetY, j + offsetX, 0] = inputImg.Data[i, j, 0];
                    outputImg.Data[i + offsetY, j + offsetX, 1] = inputImg.Data[i, j, 1];
                    outputImg.Data[i + offsetY, j + offsetX, 2] = inputImg.Data[i, j, 2];

                }
            }

        }

        static void CopyToImage(ref Image<Hsv, Byte> inputImg, ref Image<Bgr, Byte> outputImg, int offsetX, int offsetY, int channel) {

            for (int i = 0; i < inputImg.Height; i++) {
                for (int j = 0; j < inputImg.Width; j++) {

                    outputImg.Data[i + offsetX, j + offsetY, 0] = inputImg.Data[i, j, channel];
                    outputImg.Data[i + offsetX, j + offsetY, 1] = inputImg.Data[i, j, channel];
                    outputImg.Data[i + offsetX, j + offsetY, 2] = inputImg.Data[i, j, channel];

                }
            }

        }

    }
}
