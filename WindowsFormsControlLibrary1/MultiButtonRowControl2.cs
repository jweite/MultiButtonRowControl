using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiButtonRowControl2
{
    public partial class multiButtonRowControl : UserControl
    {
        private Color buttonBackColor = SystemColors.Control;

        List<Button> physicalButtons = new List<Button>();

        Dictionary<int, LogicalButton> logicalButtons = new Dictionary<int, LogicalButton>();

        int logicalButtonIndex = 0;

        int currentLogicalButton = -1;

        int leftmostLogicalButton = 0;

        public int minButtonWidth = 200;

        public float fontSize = 16;

        public event EventHandler Click;

        public multiButtonRowControl()
        {
            InitializeComponent();
            scrollbar.Minimum = scrollbar.Maximum = 0;
        }

        public void addButton(String text)
        {
            addButton(text, null);
        }

        public void addButton(String text, Object tag)
        {
            LogicalButton logicalButton = new LogicalButton();
            logicalButton.text = text;
            logicalButton.tag = tag;
            logicalButtons.Add(logicalButtonIndex, logicalButton);

            Button b = new Button();
            b.BackColor = buttonBackColor;
            int buttonMarginWidth = b.Margin.Left + b.Margin.Right;
            int containerInnerWidth = (flpInner.Width - (flpInner.Margin.Left + flpInner.Margin.Right));

            int nPhysicalButtonsProposed = physicalButtons.Count + 1;
            int newButtonWidth = (containerInnerWidth / nPhysicalButtonsProposed) - buttonMarginWidth;
            if (newButtonWidth >= minButtonWidth || nPhysicalButtonsProposed == 1) {
                b.Text = text;
                b.Tag = tag;
                b.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                b.Click += new System.EventHandler(this.button_Click);                
                physicalButtons.Add(b);
                flpInner.Controls.Add(b);

                foreach (Button b2 in physicalButtons) {
                    b2.Width = newButtonWidth;
                    b2.Height = flpInner.Height;
                }

                if (currentLogicalButton >= 0 && logicalButtonIndex == currentLogicalButton)
                {
                    b.BackColor = SystemColors.Highlight;
                }
            }

            ++logicalButtonIndex;

            if (logicalButtonIndex > physicalButtons.Count)
            {
                scrollbar.Maximum = logicalButtonIndex - physicalButtons.Count;
            }
        }

        public void clearButtons()
        {
            logicalButtons.Clear();

            flpInner.Controls.Clear();

            scrollbar.Minimum = 0;
            scrollbar.Value = 0;
            scrollbar.Maximum = 0;

            foreach (Button b in physicalButtons)
            {
                b.Click -= this.button_Click;
            }

            physicalButtons.Clear();

            currentLogicalButton = -1;

            leftmostLogicalButton = 0;

            logicalButtonIndex = 0;
            
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (currentLogicalButton > 0)
            {
                --currentLogicalButton;
                if (currentLogicalButton < leftmostLogicalButton)
                {
                    leftmostLogicalButton = currentLogicalButton;
                    scrollbar.Value = leftmostLogicalButton;
                }
                refreshButtons();

                Button buttonClicked = physicalButtons[currentLogicalButton - leftmostLogicalButton];
                if (Click != null)
                {
                    Click(buttonClicked, e);
                }
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (currentLogicalButton < logicalButtons.Count-1)
            {
                ++currentLogicalButton;
                if (currentLogicalButton > leftmostLogicalButton + (physicalButtons.Count - 1))
                {
                    leftmostLogicalButton = currentLogicalButton - (physicalButtons.Count - 1);
                    scrollbar.Value = leftmostLogicalButton;
                }
                refreshButtons();

                Button buttonClicked = physicalButtons[currentLogicalButton - leftmostLogicalButton];
                if (Click != null)
                {
                    Click(buttonClicked, e);
                }
            }
        }

        private void scrollRight()
        {
            if (leftmostLogicalButton + physicalButtons.Count < logicalButtons.Count)
            { 
                ++leftmostLogicalButton;
                refreshButtons();
            }        
        }

        private void scrollLeft()
        {
            if (leftmostLogicalButton > 0)
            {
                --leftmostLogicalButton;
                refreshButtons();
            }
        }

        private void refreshButtons()
        {
            for (int i = 0; i < physicalButtons.Count; ++i)
            {
                physicalButtons[i].Text = logicalButtons[i + leftmostLogicalButton].text;
                physicalButtons[i].Tag = logicalButtons[i + leftmostLogicalButton].tag;
                physicalButtons[i].BackColor = (currentLogicalButton >= 0 && currentLogicalButton == i + leftmostLogicalButton) ? SystemColors.Highlight : buttonBackColor;
            }
        }

        private void scrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            leftmostLogicalButton = e.NewValue;
            refreshButtons();
        }

        private void bigButtonBar_Resize(object sender, EventArgs e)
        {
            if (logicalButtons.Count == 0) return;
            
            flpInner.Controls.Clear();
            physicalButtons.Clear();

            Button b = new Button();
            b.BackColor = buttonBackColor;
            int nProposedButtons = logicalButtons.Count;
            int buttonMarginWidth = b.Margin.Left + b.Margin.Right;
            int buttonTotalSize = minButtonWidth + buttonMarginWidth;
            int containerUsableWidth =  flpInner.Width - (flpInner.Margin.Left + flpInner.Margin.Right);

            if (nProposedButtons == 0)
            {
                return;
            }

            int proposedButtonWidth = (containerUsableWidth / nProposedButtons) - buttonMarginWidth;

            if (proposedButtonWidth < minButtonWidth)
            {
                nProposedButtons = containerUsableWidth / (minButtonWidth + buttonMarginWidth);
                if (nProposedButtons == 0)
                {
                    return;
                }
                proposedButtonWidth = (containerUsableWidth / nProposedButtons) - buttonMarginWidth;
            }

            if (nProposedButtons == 5)
            {
                int y = 10;
            }

            if (currentLogicalButton >= 0 && currentLogicalButton < leftmostLogicalButton) {
                leftmostLogicalButton = currentLogicalButton;
            }

            if (currentLogicalButton >= 0 && currentLogicalButton >= leftmostLogicalButton + nProposedButtons) {
                leftmostLogicalButton = currentLogicalButton - nProposedButtons;
            }

            if (leftmostLogicalButton + nProposedButtons > logicalButtons.Count)
            {
                leftmostLogicalButton = logicalButtons.Count - nProposedButtons;
            }
            scrollbar.Value = leftmostLogicalButton;

            for (int i = 0; i < nProposedButtons; ++i) {
                b = new Button();
                b.BackColor = buttonBackColor;
                b.Text = logicalButtons[i + leftmostLogicalButton].text;
                b.Tag = logicalButtons[i + leftmostLogicalButton].tag;
                b.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                b.Width = proposedButtonWidth;
                b.Height = flpInner.Height;
                b.Click += new System.EventHandler(this.button_Click);                
                physicalButtons.Add(b);
                flpInner.Controls.Add(b);
                if (currentLogicalButton >= 0 && i + leftmostLogicalButton == currentLogicalButton) {
                    b.BackColor = SystemColors.Highlight;
                }
            }

            scrollbar.LargeChange = physicalButtons.Count;
            scrollbar.Maximum = logicalButtons.Count - 1 /* - physicalButtons.Count.  See https://stackoverflow.com/questions/2882789/net-vertical-scrollbar-not-respecting-maximum-property */;

        }

        private void button_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (Button b in physicalButtons)
            {
                if (b == (Button)sender) {
                    b.BackColor = SystemColors.Highlight;
                    currentLogicalButton = i + leftmostLogicalButton;
                }
                else {
                    b.BackColor = buttonBackColor;
                }
                ++i;
            }

            if (Click != null)
            {
                Click(sender, e);
            }
        }

        public void selectLogicalButton(int index, bool throwClickEvent) {
            if (index >= 0 && index < logicalButtons.Count) {
                
                // Set the button
                currentLogicalButton = index;

                // Try to force the button visible
                if (physicalButtons.Count == 0)
                {
                    return;
                }
                if (currentLogicalButton < leftmostLogicalButton)
                {
                    leftmostLogicalButton = currentLogicalButton;
                    scrollbar.Value = leftmostLogicalButton;
                }
                if (currentLogicalButton >= leftmostLogicalButton + physicalButtons.Count)
                {
                    leftmostLogicalButton = (currentLogicalButton + 1) - physicalButtons.Count;
                    scrollbar.Value = leftmostLogicalButton;
                }

                // Update the highlights on all buttons 
                refreshButtons();

                // Force a click event for this button.
                Button buttonClicked = physicalButtons[currentLogicalButton - leftmostLogicalButton];
                if (throwClickEvent == true && Click != null)
                {
                    EventArgs e = new EventArgs();
                    Click(buttonClicked, e);
                }
            }
        }

        public void selectNextLogicalButton(bool throwClickEvent)
        {
            if (currentLogicalButton < logicalButtons.Count - 1)
            {
                selectLogicalButton(currentLogicalButton + 1, throwClickEvent);
            }
        }

        public void selectPrevLogicalButton(bool throwClickEvent)
        {
            if (currentLogicalButton > 0)
            {
                selectLogicalButton(currentLogicalButton - 1, throwClickEvent);
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets and sets the background color of the component's buttons")]
        public Color ButtonBackColor
        // Retrieves the value of the private variable colBColor.
        {
            get
            {
                return buttonBackColor;
            }
            // Stores the selected value in the private variable colBColor, and 
            // updates the backcolor of the label control lblDisplay.
            set
            {
                buttonBackColor = value;
                btnLeft.BackColor = buttonBackColor;
                btnRight.BackColor = buttonBackColor;
                scrollbar.BackColor = buttonBackColor;
                foreach (Button b in physicalButtons)
                {
                    b.BackColor = buttonBackColor;
                }
            }
        }
    }
}
