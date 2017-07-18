namespace WindowsFormsApplication1
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
            this.button1 = new System.Windows.Forms.Button();
            this.multiButtonRowControl1 = new MultiButtonRowControl2.multiButtonRowControl();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblClicked = new System.Windows.Forms.Label();
            this.lblTag = new System.Windows.Forms.Label();
            this.multiButtonColControl1 = new MultiButtonColControl2.MultiButtonColControl();
            this.lblTag2 = new System.Windows.Forms.Label();
            this.lblClicked2 = new System.Windows.Forms.Label();
            this.btnSelectZero = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(324, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // multiButtonRowControl1
            // 
            this.multiButtonRowControl1.ButtonBackColor = System.Drawing.SystemColors.Control;
            this.multiButtonRowControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.multiButtonRowControl1.Location = new System.Drawing.Point(0, 0);
            this.multiButtonRowControl1.Name = "multiButtonRowControl1";
            this.multiButtonRowControl1.Size = new System.Drawing.Size(1285, 75);
            this.multiButtonRowControl1.TabIndex = 2;
            this.multiButtonRowControl1.Click += new System.EventHandler(this.multiButtonRowControl1_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(324, 240);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblClicked
            // 
            this.lblClicked.AutoSize = true;
            this.lblClicked.Location = new System.Drawing.Point(540, 150);
            this.lblClicked.Name = "lblClicked";
            this.lblClicked.Size = new System.Drawing.Size(35, 13);
            this.lblClicked.TabIndex = 4;
            this.lblClicked.Text = "label1";
            // 
            // lblTag
            // 
            this.lblTag.AutoSize = true;
            this.lblTag.Location = new System.Drawing.Point(641, 150);
            this.lblTag.Name = "lblTag";
            this.lblTag.Size = new System.Drawing.Size(35, 13);
            this.lblTag.TabIndex = 5;
            this.lblTag.Text = "label1";
            // 
            // multiButtonColControl1
            // 
            this.multiButtonColControl1.ButtonBackColor = System.Drawing.Color.Maroon;
            this.multiButtonColControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.multiButtonColControl1.Location = new System.Drawing.Point(0, 75);
            this.multiButtonColControl1.Name = "multiButtonColControl1";
            this.multiButtonColControl1.Size = new System.Drawing.Size(244, 447);
            this.multiButtonColControl1.TabIndex = 6;
            this.multiButtonColControl1.Click += new System.EventHandler(this.multiButtonColControl1_Click);
            // 
            // lblTag2
            // 
            this.lblTag2.AutoSize = true;
            this.lblTag2.Location = new System.Drawing.Point(384, 328);
            this.lblTag2.Name = "lblTag2";
            this.lblTag2.Size = new System.Drawing.Size(35, 13);
            this.lblTag2.TabIndex = 8;
            this.lblTag2.Text = "label1";
            // 
            // lblClicked2
            // 
            this.lblClicked2.AutoSize = true;
            this.lblClicked2.Location = new System.Drawing.Point(283, 328);
            this.lblClicked2.Name = "lblClicked2";
            this.lblClicked2.Size = new System.Drawing.Size(35, 13);
            this.lblClicked2.TabIndex = 7;
            this.lblClicked2.Text = "label1";
            // 
            // btnSelectZero
            // 
            this.btnSelectZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectZero.Location = new System.Drawing.Point(324, 194);
            this.btnSelectZero.Name = "btnSelectZero";
            this.btnSelectZero.Size = new System.Drawing.Size(106, 23);
            this.btnSelectZero.TabIndex = 9;
            this.btnSelectZero.Text = "Select Item 0";
            this.btnSelectZero.UseVisualStyleBackColor = true;
            this.btnSelectZero.Click += new System.EventHandler(this.btnSelectZero_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1285, 522);
            this.Controls.Add(this.btnSelectZero);
            this.Controls.Add(this.lblTag2);
            this.Controls.Add(this.lblClicked2);
            this.Controls.Add(this.multiButtonColControl1);
            this.Controls.Add(this.lblTag);
            this.Controls.Add(this.lblClicked);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.multiButtonRowControl1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private MultiButtonRowControl2.multiButtonRowControl multiButtonRowControl1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblClicked;
        private System.Windows.Forms.Label lblTag;
        private MultiButtonColControl2.MultiButtonColControl multiButtonColControl1;
        private System.Windows.Forms.Label lblTag2;
        private System.Windows.Forms.Label lblClicked2;
        private System.Windows.Forms.Button btnSelectZero;
    }
}

