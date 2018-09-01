using System;
using System.Drawing;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace TestCameraApp
{
    public partial class Form1 : Form
    {
        #region Fields
        /// <summary>
        /// Bollean for closing the processframe loop
        /// </summary>
        private bool continueStreaming = true;

        /// <summary>
        /// Thread for pulling frames from webcam
        /// </summary>
        private Thread captureThread;

        /// <summary>
        /// The variable that will capture the frames from the webcam
        /// </summary>
        private VideoCapture cameraCapture;

        /// <summary>
        /// Does the algortithm for finding the faces
        /// </summary>
        private CascadeClassifier haarCascade;

        private string saveImageLocation = @"C:\_CapturedFaces";

        private bool saveImage = false;

        private Thread saveImageThread;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor for the form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            this.cameraCapture = new VideoCapture();
            this.captureThread = new Thread(this.ProcessFrame);
            this.captureThread.Start();
        }

        #endregion

        #region Methods and Events

        /// <summary>
        /// Gets a frame from the webcam
        /// Then adds a rectangle around faces
        /// </summary>
        private void ProcessFrame()
        {
            Image<Bgr, byte> capturedImage;
            Image<Bgr, byte> capturedFace;
            while (this.continueStreaming)
            {
                // Get the frame from the webcam
                capturedImage = this.cameraCapture.QueryFrame().ToImage<Bgr, byte>();
                if (capturedImage != null)
                {
                    // Convert the image to a grayscale image
                    // Required for the algorithm since it is easier to compare pixels when in black and white 
                    Image<Gray, byte> grayFrame = capturedImage.Convert<Gray, byte>();
                    // gets an array of rectangles were the function thinks a face is
                    Rectangle[] faces = haarCascade.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);

                    // For every face rectangle we draw it onto the image
                    foreach (Rectangle face in faces)
                    {
                        Rectangle areaOfFace = new Rectangle(new Point(face.X, face.Y), new Size(face.Width + 5, face.Height));
                        // Set the Region of Intrest so we can copy the image to set it into the capturedFace image
                        capturedImage.ROI = areaOfFace;
                        // Copy the image of the face
                        capturedFace = capturedImage.Copy();

                        // Check if thread is alive, if not save the image
                        if (this.saveImageThread == null && this.saveImage)
                        {
                            this.saveImageThread = new Thread(() => this.SaveImage(capturedFace));
                            this.saveImageThread.Start();
                        }
                        // Reset the Region of Intrest to update the image box
                        capturedImage.ROI = Rectangle.Empty;
                        // Draw the rectangle around the the face
                        capturedImage.Draw(areaOfFace, new Bgr(Color.Red), 2);
                    }

                    // Resize the image to the size of the imagebox
                    CvInvoke.Resize(capturedImage, capturedImage, new Size(this.imageBoxCamera.Width, this.imageBoxCamera.Height));
                    // Update the imagebox in the GUI with the image
                    this.UpdateImage(capturedImage.Mat);
                    capturedImage = null;
                }
            }
        }

        /// <summary>
        /// Thread Safe Method for updating the image box
        /// </summary>
        /// <param name="image"> The image to put in the image box </param>
        private void UpdateImage(Mat image)
        {
            // This asks if the caller is from a different thread or the gui thread
            // Since the call is coming from a different thread we need to have it run on the gui frame
            if (InvokeRequired)
            {
                try
                {
                    // we invoke the gui thread to update the image
                    Invoke(new Action(() => this.UpdateImage(image)));
                }
                // Above code will throw an exception, fix later
                catch
                {
                }
            }
            else
            {
                // When called from the gui thread it will run this
                this.imageBoxCamera.Image = image;
            }
        }

        /// <summary>
        /// When the form loads, it calls this function
        /// </summary>
        /// <param name="sender"> The sender of the evnt </param>
        /// <param name="e"> The event </param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // make the cascade object and set the file location
            this.haarCascade = new CascadeClassifier("haarcascade_frontalface_alt2.xml");
            this.HasFolder();
        }

        /// <summary>
        /// Stops the thread gently when the user clooses the program
        /// </summary>
        /// <param name="sender"> The sender of the event </param>
        /// <param name="e"> The event </param>
        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            //Check if the thread is still running
            if (this.captureThread.IsAlive)
            {
                // this will kill the thread without aborting it
                this.continueStreaming = false;
            }

            // Check if there is an instance of the thread, if so kill it
            if (this.saveImageThread != null)
            {
                if (this.saveImageThread.IsAlive)
                {
                    this.saveImageThread.Abort();
                }
            }
        }

        /// <summary>
        /// Check if the folder for where the pitures will be saved exists
        /// If not make the folder
        /// </summary>
        private void HasFolder()
        {
            if (!Directory.Exists(this.saveImageLocation))
            {
                Directory.CreateDirectory(this.saveImageLocation);
            }
        }

        /// <summary>
        /// Saves image to a file
        /// </summary>
        /// <param name="image"> The image to be saved </param>
        private void SaveImage(Image<Bgr, byte> image)
        {
            string filename = $@"{this.saveImageLocation}\1.jpg";            
            /*if (!File.Exists(filename))
            {
                File.Create(filename);
                Thread.Sleep(1000);
            }*/

            Bitmap img = image.ToBitmap();
            img.Save(filename);
            this.saveImage = false;
        }

        /// <summary>
        /// Button Click for starting the save image event
        /// </summary>
        /// <param name="sender"> The sender of the event </param>
        /// <param name="e"> The event </param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.saveImage = true;
        }

        #endregion
    }
}
