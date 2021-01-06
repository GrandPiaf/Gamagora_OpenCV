using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;


public class Webcam : MonoBehaviour
{

    public RawImage rawImage;

    private VideoCapture webcam;
    private Mat webcamFrame;
    private Texture2D tex;

    private CascadeClassifier frontFaceCascadeClassifier;
    private string AbsPathToFrontCascadeClassidier = @"Assets\haarcascades\haarcascade_frontalface_default.xml";

    private System.Drawing.Rectangle[] frontFaces;
    private int MIN_FACE_SIZE = 50;
    private int MAX_FACE_SIZE = 400;

    // Start is called before the first frame update
    void Start() {
        //webcam = new VideoCapture("D:\\Cours\\Gamagora\\OpenCV\\TP2\\video.mp4");
        webcam = new VideoCapture(@"..\video.mp4");
        webcamFrame = new Mat();

        webcam.ImageGrabbed += new System.EventHandler(HandleWebcamQueryFrame);
        webcam.Start();

        frontFaceCascadeClassifier = new CascadeClassifier(AbsPathToFrontCascadeClassidier);
    }

    // Update is called once per frame
    void Update() {
        if (webcam.IsOpened) {
            bool grabbed = webcam.Grab();

            if (!grabbed) {
                Debug.Log("no more grab");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
                return;
            }
            DisplayFrameOnPlane();
        }

    }

    private void DisplayFrameOnPlane() {
        if (webcamFrame.IsEmpty) return;

        int width = (int)rawImage.rectTransform.rect.width;
        int height = (int)rawImage.rectTransform.rect.height;

        if (tex != null) {
            Destroy(tex);
            tex = null;
        }

        tex = new Texture2D(width, height, TextureFormat.RGBA32, false);

        CvInvoke.Resize(webcamFrame, webcamFrame, new System.Drawing.Size(width, height));
        CvInvoke.CvtColor(webcamFrame, webcamFrame, ColorConversion.Bgr2Rgba);
        CvInvoke.Flip(webcamFrame, webcamFrame, FlipType.Vertical);
        CvInvoke.Flip(webcamFrame, webcamFrame, FlipType.Horizontal);

        tex.LoadRawTextureData(webcamFrame.ToImage<Emgu.CV.Structure.Rgba, byte>().Bytes);
        tex.Apply();

        rawImage.texture = tex;
    }

    private void HandleWebcamQueryFrame(object sender, System.EventArgs e) {
        if (webcam.IsOpened) {
            webcam.Retrieve(webcamFrame);
        }
        Debug.Log(webcamFrame.Rows + " " + webcamFrame.Height);

        lock (webcamFrame) {
            Mat matGrayscale = new Mat(webcam.Width, webcamFrame.Height, DepthType.Cv8U, 1);
            CvInvoke.CvtColor(webcamFrame, matGrayscale, ColorConversion.Bgr2Gray);

            //Face detection
            frontFaces = frontFaceCascadeClassifier.DetectMultiScale(matGrayscale, 1.1, 3,
                                                                    new System.Drawing.Size(MIN_FACE_SIZE, MIN_FACE_SIZE),
                                                                    new System.Drawing.Size(MAX_FACE_SIZE, MAX_FACE_SIZE));

            Debug.Log("Number of detected faces : " + frontFaces.Length);
            for (int i = 0; i < frontFaces.Length; i++) {
                CvInvoke.Rectangle(webcamFrame, frontFaces[i], new MCvScalar(255, 255, 0), 5);
            }
        }
        System.Threading.Thread.Sleep(15);
    }

    private void OnDestroy() {
        Debug.Log("enterging destroy");

        if (webcam != null) {
            Debug.Log("sleeping");
            System.Threading.Thread.Sleep(50);

            webcam.Stop();
            webcam.Dispose();
        }

        Debug.Log("Destroying webcam");
    }

}
