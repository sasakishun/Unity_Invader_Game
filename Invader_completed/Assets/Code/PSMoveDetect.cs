//Sugimoto Lab: Interactive Media Lab
//Experiment #2
//Last Update 2014/12/14 by N.T

using UnityEngine;
using System.Collections;
using OpenCvSharp;
using OpenCvSharp.MachineLearning;
using System.Runtime.InteropServices;
using System.Xml;

public class PSMoveDetect : MonoBehaviour
{
    // Setting Width and Height 
    const int CAPTURE_WIDTH = 640;
    const int CAPTURE_HEIGHT = 480;

    // Internal parameters of your camera
	const double fx = 660.5071807875812;
    const double fy = 662.0103568287129;
	const double ux = 399.1575466095347;
    const double uy = 298.9527473279853;

	// Initialization
    public static double Sx = 0;
    public static double Sy = 0;
    public static double Sz = 0;
	const double Range_H = 5;	// (H - Range_H) ~ (H + Range_H)
	const double Range_S = 5;	// (S - Range_S) ~ (S + Range_S)
	const double Range_V = 5;	// (V - Range_V) ~ (V + Range_V)
	const double MAX_G = 255;
	const double MAX_B = 255;
	const double MAX_R = 255;
	const double CTR_para = 0.02;
	const double Sphere_R = 2.25;

    // Preparing for getting images
    private CvCapture capture;
    private CvWindow win;
    private CvScalar pointcolour;
    private CvScalar pointhsv;

    float radius;
    CvPoint2D32f center;
    XmlDocument webxml;

    // Use this for initialization
    void Start()
    {
        // Declaration of using a Web camera
        capture = Cv.CreateCameraCapture(0);
        // Setting frame width and height
        Cv.SetCaptureProperty(capture, CaptureProperty.FrameWidth, CAPTURE_WIDTH);
        Cv.SetCaptureProperty(capture, CaptureProperty.FrameHeight, CAPTURE_HEIGHT);
        // Getting frame from the web camera
        IplImage frame = Cv.QueryFrame(capture);
        // Showing the size of frames
        Debug.Log("width:" + frame.Width + " height:" + frame.Height);
        // Initialization for getting RGB
        win = new CvWindow("GetRGBWindow", frame);
        win.OnMouseCallback += new CvMouseCallback(getclickedpixelrgb);
        pointhsv.Val0 = 0;
        // Name windows
		Cv.NamedWindow("STEP1(Filter)");
		Cv.NamedWindow("STEP2(HSV)");
		Cv.NamedWindow("STEP3(Segmentation)");
		Cv.NamedWindow("STEP4(morphology)");
		Cv.NamedWindow("Result(PSMoveDetect)");
		Cv.NamedWindow("Center");
    }

    // Update is called once per frame
	void Update()
	{
		IplImage frame = Cv.QueryFrame(capture);
		IplImage img = Cv.CloneImage(frame);	//use for first imput 
		IplImage imgResult = img.Clone();		//copy img
		IplImage imgSTEP1 = new IplImage(img.Size, BitDepth.U8, 3);		//Output images of STEP1 
		IplImage imgSTEP2 = new IplImage(img.Size, BitDepth.U8, 3);		//Output images of STEP2
		IplImage imgSTEP3 = new IplImage(img.Size, BitDepth.U8, 1);		//Output images of STEP3 
		IplImage imgSTEP4 = new IplImage(img.Size, BitDepth.U8, 1);		//Output images of STEP4 
		
		// STEP1: Noise Removal (spatial filter)
        Cv.Smooth(img, imgSTEP1, SmoothType.Blur, 1);
        Cv.Smooth(img, imgSTEP1, SmoothType.Gaussian, 1);
        Cv.ShowImage("STEP1(Filter)", imgSTEP1);

        // STEP2: Convert RGB into HSV
        Cv.CvtColor(imgSTEP1, imgSTEP2, ColorConversion.BgrToHsv);
        Cv.ShowImage("STEP2(HSV)", imgSTEP2);

        CvMemStorage storage = new CvMemStorage();
		storage.Clear();
		
		// STEP3: Area Segmentation
        Cv.InRangeS(imgSTEP2,
            new CvScalar(0 - Range_H,
                         255 - Range_S,
                         192 - Range_V),
            new CvScalar(0 + Range_H,
                         255 + Range_S,
                         192 + Range_V),
            imgSTEP3);
        /*        Cv.InRangeS(imgSTEP2 ,
                    new CvScalar((pointhsv.Val0) -  Range_H,
                                 (pointhsv.Val1) -  Range_S,
                                 (pointhsv.Val2) -  Range_V),
                    new CvScalar((pointhsv.Val0) +  Range_H,
                                 (pointhsv.Val1) +  Range_S,
                                 (pointhsv.Val2) +  Range_V),
                    imgSTEP3);
        */		Cv.ShowImage("STEP3(Segmentation)", imgSTEP3);
		
		// STEP4: Noise Removal (Morphology image processing)
		IplImage img_tmp = new IplImage(img.Size, BitDepth.U8, 1); // For using temporary output images

        Cv.Dilate(imgSTEP3,img_tmp);
        Cv.Erode(img_tmp, img_tmp);

        Cv.Erode(img_tmp, img_tmp);
        Cv.Dilate(img_tmp, imgSTEP4);
        Cv.ShowImage("STEP4(morphology)", imgSTEP4);
		
		// STEP5: Detect circles and get positions
        CvSeq<CvPoint> contours;
        Cv.FindContours(imgSTEP4, storage, out contours,
            CvContour.SizeOf, ContourRetrieval.Tree,
            ContourChain.ApproxNone);
        if(contours == null)
        {
            Debug.Log("PSMove is not detected");
        }
        else{
            contours = Cv.ApproxPoly(contours, CvContour.SizeOf,
                storage, ApproxPolyMethod.DP,
                Cv.ContourPerimeter(contours)*CTR_para, true);
            
            Cv.DrawContours(imgResult, contours,
                new CvScalar(MAX_G, 0, 0), new CvScalar(0, MAX_B, 0), 3, -1);
            
            Cv.MinEnclosingCircle(contours, out center, out radius);
            Cv.DrawCircle(img, center, 2, new CvScalar(0, MAX_B, 0));
            Sz = fx * Sphere_R / radius;
            Sx = -((center.X - ux) * Sz) / fx;
            Sy = -((center.Y - uy) * Sz) / fy;
        }

		// STEP6: Show images
		win.ShowImage(frame);
        Cv.ShowImage("Result(PSMoveDetect)", imgResult);
        Cv.ShowImage("Center", img);
	}
    
    private void getclickedpixelrgb(MouseEvent me, int x, int y, MouseEvent me2) //add
    {
        if (me == MouseEvent.LButtonDown)
        {
            using (IplImage framecolor = Cv.RetrieveFrame(capture))
            {
                if (framecolor != null)
                {
                    pointcolour = Cv.Get2D(framecolor, y, x);
                    Cv.CvtColor(framecolor, framecolor, ColorConversion.BgrToHsv);
                    pointhsv = Cv.Get2D(framecolor, y, x);

               //   Debug.Log("R : " + pointcolour.Val2 + " G : " + pointcolour.Val1 + " B : " + pointcolour.Val0);
                 	Debug.Log("H : " + (pointhsv.Val0 * 2) + " S : " + pointhsv.Val1 + " V : " + pointhsv.Val2);
                }
            }
        }
    }

    // Ending process
    void OnDestroy()
    {
        Cv.DestroyAllWindows();
        Cv.ReleaseCapture(capture);
    }
}
