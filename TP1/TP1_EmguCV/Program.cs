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

            CvInvoke.Imshow(window, matWebcam);
            CvInvoke.WaitKey(0);  //Wait for the key pressing event
            CvInvoke.DestroyWindow(window); //Destroy the window if key is pressed

        }
    }
}
