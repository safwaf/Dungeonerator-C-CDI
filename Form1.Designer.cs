using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace GDIgame
{
    partial class Form1
    {
        private BoardManager BoardMgr = new BoardManager();
        private PictureBox Canvas = new PictureBox();
        private Timer Clock = new Timer();
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

        private void Form1_KeyPress(object sender, KeyPressEventArgs k)
        {
            switch (k.KeyChar)
            {
                case 'a':
                    BoardMgr.LeftKey();
                    break;
                case 's':
                    BoardMgr.DownKey();
                    break;
                case 'w':
                    BoardMgr.UpKey();
                    break;
                case 'd':
                    BoardMgr.RightKey();
                    break;
            }
        Invalidate();   //redraw the entire surface. optimize later;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            BoardMgr.DrawProcess(e.Graphics);
            base.OnPaint(e);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 639);
            this.Name = "Form1";
            this.Text = "Dungenerator";
            this.ResumeLayout(false);
            this.KeyPress += new KeyPressEventHandler(this.Form1_KeyPress);

        }

        #endregion
    }
}

