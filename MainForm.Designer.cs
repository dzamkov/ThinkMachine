namespace ThinkMachine
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.PaddingContainer = new System.Windows.Forms.TableLayoutPanel();
            this.ImageContainer = new System.Windows.Forms.Panel();
            this.PaddingContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // PaddingContainer
            // 
            resources.ApplyResources(this.PaddingContainer, "PaddingContainer");
            this.PaddingContainer.Controls.Add(this.ImageContainer, 1, 1);
            this.PaddingContainer.Name = "PaddingContainer";
            // 
            // ImageContainer
            // 
            this.ImageContainer.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.ImageContainer, "ImageContainer");
            this.ImageContainer.Name = "ImageContainer";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.PaddingContainer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.PaddingContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel PaddingContainer;
        private System.Windows.Forms.Panel ImageContainer;
    }
}

