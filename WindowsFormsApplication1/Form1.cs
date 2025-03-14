﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int buttonNo = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = ((char)(65 + buttonNo)).ToString();
            multiButtonRowControl1.addButton(String.Format("{0} {1}", s, buttonNo++), String.Format("Tag {0}", buttonNo));
            multiButtonColControl1.addButton(String.Format("{0} {1}", s, buttonNo++), String.Format("Tag {0}", buttonNo));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            multiButtonRowControl1.clearButtons();
            multiButtonColControl1.clearButtons();
        }

        private void multiButtonRowControl1_Click(object sender, EventArgs e)
        {
            lblClicked.Text = ((Button)sender).Text;
            lblTag.Text = (String)((Button)sender).Tag;
        }

        private void multiButtonColControl1_Click(object sender, EventArgs e)
        {
            lblClicked2.Text = ((Button)sender).Text;
            lblTag2.Text = (String)((Button)sender).Tag;

        }

        private void btnSelectZero_Click(object sender, EventArgs e)
        {
            multiButtonColControl1.selectLogicalButton(0, true, false);
            multiButtonRowControl1.selectLogicalButton(0, true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            multiButtonColControl1.ShowArrowButtons = !multiButtonColControl1.ShowArrowButtons;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            multiButtonColControl1.ShowScrollbar = !multiButtonColControl1.ShowScrollbar;
        }

        private void btnToggleAlpha_Click(object sender, EventArgs e)
        {
            multiButtonColControl1.ShowAlphaButtons = !multiButtonColControl1.ShowAlphaButtons;
        }

        private void btnAdd100_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                multiButtonRowControl1.addButton(String.Format("{0}", buttonNo++), String.Format("Tag {0}", buttonNo));
                multiButtonColControl1.addButton(String.Format("{0}", buttonNo++), String.Format("Tag {0}", buttonNo));
            }
        }

        private void multiButtonColControl1_RightClick(object sender, EventArgs e)
        {
            MessageBox.Show("Right click: " + ((Button)sender).Text);
        }
    }
}
