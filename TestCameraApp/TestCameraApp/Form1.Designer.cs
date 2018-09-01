namespace TestCameraApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageBoxCamera = new Emgu.CV.UI.ImageBox();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBoxCamera
            // 
            this.imageBoxCamera.Location = new System.Drawing.Point(15, 12);
            this.imageBoxCamera.Name = "imageBoxCamera";
            this.imageBoxCamera.Size = new System.Drawing.Size(643, 316);
            this.imageBoxCamera.TabIndex = 2;
            this.imageBoxCamera.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(294, 335);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 27);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save Face";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 374);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.imageBoxCamera);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxCamera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Emgu.CV.UI.ImageBox imageBoxCamera;
        private System.Windows.Forms.Button btnSave;
    }
}

