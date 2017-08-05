namespace MultiButtonColControl2
{
    partial class MultiButtonColControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpOuter = new System.Windows.Forms.TableLayoutPanel();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.scrollbar = new System.Windows.Forms.VScrollBar();
            this.flpInner = new System.Windows.Forms.FlowLayoutPanel();
            this.flpAlphaButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAlpha = new System.Windows.Forms.Button();
            this.tlpOuter.SuspendLayout();
            this.flpAlphaButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpOuter
            // 
            this.tlpOuter.ColumnCount = 3;
            this.tlpOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpOuter.Controls.Add(this.flpAlphaButtons, 2, 0);
            this.tlpOuter.Controls.Add(this.btnUp, 1, 0);
            this.tlpOuter.Controls.Add(this.btnDown, 1, 2);
            this.tlpOuter.Controls.Add(this.scrollbar, 0, 1);
            this.tlpOuter.Controls.Add(this.flpInner, 1, 1);
            this.tlpOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOuter.Location = new System.Drawing.Point(0, 0);
            this.tlpOuter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlpOuter.Name = "tlpOuter";
            this.tlpOuter.RowCount = 3;
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOuter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlpOuter.Size = new System.Drawing.Size(325, 565);
            this.tlpOuter.TabIndex = 0;
            // 
            // btnUp
            // 
            this.btnUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUp.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnUp.Location = new System.Drawing.Point(31, 4);
            this.btnUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(255, 41);
            this.btnUp.TabIndex = 0;
            this.btnUp.Text = "";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDown.Font = new System.Drawing.Font("Wingdings", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnDown.Location = new System.Drawing.Point(31, 520);
            this.btnDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(255, 41);
            this.btnDown.TabIndex = 1;
            this.btnDown.Text = "";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // scrollbar
            // 
            this.scrollbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollbar.LargeChange = 1;
            this.scrollbar.Location = new System.Drawing.Point(0, 49);
            this.scrollbar.Name = "scrollbar";
            this.scrollbar.Size = new System.Drawing.Size(27, 467);
            this.scrollbar.TabIndex = 2;
            this.scrollbar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollbar_Scroll);
            // 
            // flpInner
            // 
            this.flpInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpInner.Location = new System.Drawing.Point(31, 53);
            this.flpInner.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flpInner.Name = "flpInner";
            this.flpInner.Size = new System.Drawing.Size(255, 459);
            this.flpInner.TabIndex = 3;
            // 
            // flpAlphaButtons
            // 
            this.flpAlphaButtons.Controls.Add(this.btnAlpha);
            this.flpAlphaButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAlphaButtons.Location = new System.Drawing.Point(294, 4);
            this.flpAlphaButtons.Margin = new System.Windows.Forms.Padding(4);
            this.flpAlphaButtons.Name = "flpAlphaButtons";
            this.tlpOuter.SetRowSpan(this.flpAlphaButtons, 3);
            this.flpAlphaButtons.Size = new System.Drawing.Size(27, 557);
            this.flpAlphaButtons.TabIndex = 4;
            // 
            // btnAlpha
            // 
            this.btnAlpha.Location = new System.Drawing.Point(3, 3);
            this.btnAlpha.Name = "btnAlpha";
            this.btnAlpha.Size = new System.Drawing.Size(24, 23);
            this.btnAlpha.TabIndex = 0;
            this.btnAlpha.Text = "A";
            this.btnAlpha.UseVisualStyleBackColor = true;
            // 
            // MultiButtonColControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpOuter);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MultiButtonColControl";
            this.Size = new System.Drawing.Size(325, 565);
            this.Resize += new System.EventHandler(this.MultiButtonColControl_Resize);
            this.tlpOuter.ResumeLayout(false);
            this.flpAlphaButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpOuter;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.VScrollBar scrollbar;
        private System.Windows.Forms.FlowLayoutPanel flpInner;
        private System.Windows.Forms.FlowLayoutPanel flpAlphaButtons;
        private System.Windows.Forms.Button btnAlpha;
    }
}
