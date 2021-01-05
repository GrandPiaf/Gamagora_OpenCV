using System;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using System.Drawing;

namespace TP1_EmguCV {
    class Program {
        static void Main(string[] args) {

            Exercice6();

        }

        static void Exercice6() {

            String window = "TP1 - Exercice 6";
            CvInvoke.NamedWindow(window);

            /* Reading the base image */
            Mat matBase = new Mat("..\\..\\..\\..\\images\\crochet.jpg"); //Because it is located in directory "netcoreapp3.1"

            /* Applying treatment */

            // Convert base image to HSV
            Mat matHsv = new Mat(matBase.Width, matBase.Height, DepthType.Cv8U, 2);
            CvInvoke.CvtColor(matBase, matHsv, ColorConversion.Bgr2Hsv);
            Image<Hsv, Byte> imgHsv = matHsv.ToImage<Hsv, Byte>();

            // Thresholding the image
            Hsv borneInf = new Hsv(50, 0, 0);
            Hsv borneSup = new Hsv(75, 255, 255);

            Image<Gray, Byte> imgThreshold = imgHsv.InRange(borneInf, borneSup);

            // Erode then Dilate ('open' the image)
            Point anchor = new Point(-1, -1);
            Mat structuringElement = CvInvoke.GetStructuringElement(ElementShape.Ellipse, new System.Drawing.Size(5, 5), anchor);
            int nbIterations = 5;

            CvInvoke.Erode(imgThreshold, imgThreshold, structuringElement, anchor, nbIterations, BorderType.Constant, new MCvScalar(0));
            CvInvoke.Dilate(imgThreshold, imgThreshold, structuringElement, anchor, nbIterations, BorderType.Constant, new MCvScalar(0));


            /* Writing the result image */
            Image<Bgr, Byte> resultImage = new Image<Bgr, byte>(matBase.Width, matBase.Height);

            CopyToImage(ref imgThreshold, ref resultImage, 0, 0);

            resultImage.Save("..\\..\\..\\..\\resultImages\\resultEx6.jpg");


            CvInvoke.Imshow(window, resultImage.Mat);
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(window); //Destroy the window if key is pressed

        }

        static void Exercice5() {

            String window = "TP1 - Exercice 5";
            CvInvoke.NamedWindow(window);

            /* Reading the base image */
            Mat matBase = new Mat("..\\..\\..\\..\\images\\crochet.jpg"); //Because it is located in directory "netcoreapp3.1"

            /* Applying treatment */

            // Convert base image to HSV
            Mat matHsv = new Mat(matBase.Width, matBase.Height, DepthType.Cv8U, 2);
            CvInvoke.CvtColor(matBase, matHsv, ColorConversion.Bgr2Hsv);
            Image<Hsv, Byte> imgHsv = matHsv.ToImage<Hsv, Byte>();

            // Thresholding the image
            Hsv borneInf = new Hsv(50, 0, 0);
            Hsv borneSup = new Hsv(75, 255, 255);

            Image<Gray, Byte> imgThreshold = imgHsv.InRange(borneInf, borneSup);

            /* Writing the result image */
            Image<Bgr, Byte> resultImage = new Image<Bgr, byte>(matBase.Width * 3, matBase.Height * 2);

            Image<Bgr, Byte> imgBase = matBase.ToImage<Bgr, Byte>();

            CopyToImage(ref imgBase, ref resultImage, 0, 0);
            CopyToImage(ref imgHsv, ref resultImage, imgBase.Width, 0);
            CopyToImage(ref imgThreshold, ref resultImage, imgBase.Width * 2, 0);
            GrayCopyChannelToImage(ref imgHsv, ref resultImage, 0, imgBase.Height, 0);
            GrayCopyChannelToImage(ref imgHsv, ref resultImage, imgBase.Width, imgBase.Height, 1);
            GrayCopyChannelToImage(ref imgHsv, ref resultImage, imgBase.Width * 2, imgBase.Height, 2);

            resultImage.Save("..\\..\\..\\..\\resultImages\\resultEx5.jpg");


            CvInvoke.Imshow(window, resultImage.Mat);
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(window); //Destroy the window if key is pressed

        }

        static void Exercice4() {

            String window = "TP1 - Exercice 4";
            CvInvoke.NamedWindow(window);

            Mat matBase = new Mat("..\\..\\..\\..\\images\\crochet.jpg"); //Because it is located in directory "netcoreapp3.1"

            Mat matGrey = new Mat(matBase.Width, matBase.Height, DepthType.Cv8U, 0);

            CvInvoke.CvtColor(matBase, matGrey, ColorConversion.Bgr2Gray);

            Mat matFlip = new Mat(matBase.Width, matBase.Height, DepthType.Cv8U, 2);

            CvInvoke.Flip(matBase, matFlip, FlipType.Vertical);



            Image<Bgr, Byte> resultImage = new Image<Bgr, byte>(matBase.Width * 3, matBase.Height);

            Image<Bgr, Byte> imgBase = matBase.ToImage<Bgr, Byte>();
            Image<Bgr, Byte> imgFlip = matFlip.ToImage<Bgr, Byte>();
            Image<Gray, Byte> imgGray = matGrey.ToImage<Gray, Byte>();

            CopyToImage(ref imgBase, ref resultImage, 0, 0);
            CopyToImage(ref imgFlip, ref resultImage, imgBase.Width, 0);
            CopyToImage(ref imgGray, ref resultImage, imgBase.Width * 2, 0);

            resultImage.Save("..\\..\\..\\..\\resultImages\\resultEx4.jpg");


            CvInvoke.Imshow(window, resultImage.Mat);
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(window); //Destroy the window if key is pressed

        }

        static void Exercice3() {

            String window = "TP1 - Exercice 3";
            CvInvoke.NamedWindow(window);

            Mat matWebcam = new Mat("..\\..\\..\\..\\images\\crochet.jpg"); //Because it is located in directory "netcoreapp3.1"

            Image<Bgr, Byte> baseImg = matWebcam.ToImage<Bgr, Byte>();
            Image<Gray, Byte> grayScaleBaseImg = matWebcam.ToImage<Gray, Byte>();

            Image<Bgr, Byte> resultImage = new Image<Bgr, byte>(baseImg.Width * 3, baseImg.Height);

            CopyToImage(ref baseImg, ref resultImage, 0, 0);
            CopyToImage(ref grayScaleBaseImg, ref resultImage, baseImg.Width, 0);
            GrayCopyChannelToImage(ref baseImg, ref resultImage, baseImg.Width * 2, 0, 0);

            Mat matRes = resultImage.Mat;

            CvInvoke.Imshow(window, matRes);
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(window); //Destroy the window if key is pressed

        }

        // BGR to BGR
        static void CopyToImage(ref Image<Bgr, Byte> inputImg, ref Image<Bgr, Byte> outputImg, int offsetX, int offsetY) {

            for (int i = 0; i < inputImg.Height; i++) {
                for (int j = 0; j < inputImg.Width; j++) {

                    outputImg.Data[i + offsetY, j + offsetX, 0] = inputImg.Data[i, j, 0];
                    outputImg.Data[i + offsetY, j + offsetX, 1] = inputImg.Data[i, j, 1];
                    outputImg.Data[i + offsetY, j + offsetX, 2] = inputImg.Data[i, j, 2];

                }
            }

        }

        // HSV to BGR
        static void CopyToImage(ref Image<Hsv, Byte> inputImg, ref Image<Bgr, Byte> outputImg, int offsetX, int offsetY) {

            for (int i = 0; i < inputImg.Height; i++) {
                for (int j = 0; j < inputImg.Width; j++) {

                    outputImg.Data[i + offsetY, j + offsetX, 0] = inputImg.Data[i, j, 0];
                    outputImg.Data[i + offsetY, j + offsetX, 1] = inputImg.Data[i, j, 1];
                    outputImg.Data[i + offsetY, j + offsetX, 2] = inputImg.Data[i, j, 2];

                }
            }

        }

        // Gray to BGR
        static void CopyToImage(ref Image<Gray, Byte> inputImg, ref Image<Bgr, Byte> outputImg, int offsetX, int offsetY) {

            for (int i = 0; i < inputImg.Height; i++) {
                for (int j = 0; j < inputImg.Width; j++) {

                    outputImg.Data[i + offsetY, j + offsetX, 0] = inputImg.Data[i, j, 0];
                    outputImg.Data[i + offsetY, j + offsetX, 1] = inputImg.Data[i, j, 0];
                    outputImg.Data[i + offsetY, j + offsetX, 2] = inputImg.Data[i, j, 0];

                }
            }

        }

        // BGR to BGR single channel
        static void GrayCopyChannelToImage(ref Image<Bgr, Byte> inputImg, ref Image<Bgr, Byte> outputImg, int offsetX, int offsetY, int channel) {

            for (int i = 0; i < inputImg.Height; i++) {
                for (int j = 0; j < inputImg.Width; j++) {

                    outputImg.Data[i + offsetY, j + offsetX, 0] = inputImg.Data[i, j, channel];
                    outputImg.Data[i + offsetY, j + offsetX, 1] = inputImg.Data[i, j, channel];
                    outputImg.Data[i + offsetY, j + offsetX, 2] = inputImg.Data[i, j, channel];

                }
            }

        }

        // Hsv to BGR single channel
        static void GrayCopyChannelToImage(ref Image<Hsv, Byte> inputImg, ref Image<Bgr, Byte> outputImg, int offsetX, int offsetY, int channel) {

            for (int i = 0; i < inputImg.Height; i++) {
                for (int j = 0; j < inputImg.Width; j++) {

                    outputImg.Data[i + offsetY, j + offsetX, 0] = inputImg.Data[i, j, channel];
                    outputImg.Data[i + offsetY, j + offsetX, 1] = inputImg.Data[i, j, channel];
                    outputImg.Data[i + offsetY, j + offsetX, 2] = inputImg.Data[i, j, channel];

                }
            }

        }

    }
}
