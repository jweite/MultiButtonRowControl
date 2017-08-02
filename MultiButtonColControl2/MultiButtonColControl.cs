using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiButtonColControl2
{
    public partial class MultiButtonColControl : UserControl
    {
        private Color buttonBackColor = SystemColors.Control;

        List<Button> physicalButtons = new List<Button>();

        Dictionary<int, LogicalButton> logicalButtons = new Dictionary<int, LogicalButton>();

        int logicalButtonIndex = 0;

        int currentLogicalButton = -1;

        int topmostLogicalButton = 0;

        public int minButtonHeight = 50;

        public float fontSize = 16;

        public event EventHandler Click;

        bool bShowArrowButtons = true;

        bool bShowScrollbar = true;

        public MultiButtonColControl()
        {
            InitializeComponent();
            scrollbar.Minimum =  scrollbar.Maximum = 0;
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
            int buttonMarginWidth = b.Margin.Top + b.Margin.Bottom;
            int containerInnerHeight = (flpInner.Height - (flpInner.Margin.Top + flpInner.Margin.Bottom));

            int nPhysicalButtonsProposed = physicalButtons.Count + 1;
            int newButtonHeight = (containerInnerHeight / nPhysicalButtonsProposed) - buttonMarginWidth;
            if (newButtonHeight >= minButtonHeight || nPhysicalButtonsProposed == 1) {
                b.Text = text;
                b.Tag = tag;
                b.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                b.Click += new System.EventHandler(this.button_Click);
                physicalButtons.Add(b);
                flpInner.Controls.Add(b);

                foreach (Button b2 in physicalButtons) {
                    b2.Height = newButtonHeight;
                    b2.Width = flpInner.Width;
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

            topmostLogicalButton = 0;

            logicalButtonIndex = 0;

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (currentLogicalButton > 0)
            {
                --currentLogicalButton;
                if (currentLogicalButton < topmostLogicalButton)
                {
                    topmostLogicalButton = currentLogicalButton;
                    scrollbar.Value = topmostLogicalButton;
                }
                refreshButtons();

                Button buttonClicked = physicalButtons[currentLogicalButton - topmostLogicalButton];
                if (Click != null)
                {
                    Click(buttonClicked, e);
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (currentLogicalButton < logicalButtons.Count - 1)
            {
                ++currentLogicalButton;
                if (currentLogicalButton > topmostLogicalButton + (physicalButtons.Count - 1))
                {
                    topmostLogicalButton = currentLogicalButton - (physicalButtons.Count - 1);
                    scrollbar.Value = topmostLogicalButton;
                }
                refreshButtons();

                if (currentLogicalButton >= topmostLogicalButton)
                {
                    Button buttonClicked = physicalButtons[currentLogicalButton - topmostLogicalButton];
                    if (Click != null)
                    {
                        Click(buttonClicked, e);
                    }
                }
            }
        }

        private void scrollDown()
        {
            if (topmostLogicalButton + physicalButtons.Count < logicalButtons.Count)
            {
                ++topmostLogicalButton;
                refreshButtons();
            }
        }

        private void scrollUp()
        {
            if (topmostLogicalButton > 0)
            {
                --topmostLogicalButton;
                refreshButtons();
            }
        }

        private void refreshButtons()
        {
            for (int i = 0; i < physicalButtons.Count; ++i)
            {
                physicalButtons[i].Text = logicalButtons[i + topmostLogicalButton].text;
                physicalButtons[i].Tag = logicalButtons[i + topmostLogicalButton].tag;
                physicalButtons[i].BackColor = (currentLogicalButton >= 0 && currentLogicalButton == i + topmostLogicalButton) ? SystemColors.Highlight : buttonBackColor;
            }
        }

        private void scrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            topmostLogicalButton = e.NewValue;
            refreshButtons();
        }

        private void MultiButtonColControl_Resize(object sender, EventArgs e)
        {
            if (logicalButtons.Count == 0) return;

            flpInner.Controls.Clear();
            physicalButtons.Clear();

            Button b = new Button();
            b.BackColor = buttonBackColor;
            int nProposedButtons = logicalButtons.Count;
            int buttonMarginHeight = b.Margin.Left + b.Margin.Right;
            int buttonTotalSize = minButtonHeight + buttonMarginHeight;
            int containerUsableHeight = flpInner.Height - (flpInner.Margin.Top + flpInner.Margin.Bottom);

            if (nProposedButtons == 0)
            {
                return;
            }

            if (containerUsableHeight < 0)
            {
                return;
            }
            
            int proposedButtonHeight = (containerUsableHeight / nProposedButtons) - buttonMarginHeight;

            if (proposedButtonHeight < minButtonHeight)
            {
                // See how many min-sized buttons could fit
                nProposedButtons = containerUsableHeight / (minButtonHeight + buttonMarginHeight);
                if (nProposedButtons == 0)
                {
                    return;
                }
                // And see how big they'd really be with that many.

                proposedButtonHeight = (containerUsableHeight / nProposedButtons) - buttonMarginHeight;
            }

            if (currentLogicalButton >= 0 && currentLogicalButton < topmostLogicalButton)
            {
                topmostLogicalButton = currentLogicalButton;
            }

            if (currentLogicalButton >= 0 && currentLogicalButton >= topmostLogicalButton + nProposedButtons) {
                topmostLogicalButton = (currentLogicalButton - nProposedButtons) + 1;
            }

            if (topmostLogicalButton + nProposedButtons > logicalButtons.Count)
            {
                topmostLogicalButton = logicalButtons.Count - nProposedButtons;
            }
            scrollbar.Value = topmostLogicalButton;

            for (int i = 0; i < nProposedButtons; ++i) {
                b = new Button();
                b.BackColor = buttonBackColor;
                b.Text = logicalButtons[i + topmostLogicalButton].text;
                b.Tag = logicalButtons[i + topmostLogicalButton].tag;
                b.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                b.Height = proposedButtonHeight;
                b.Width = flpInner.Width;
                b.Click += new System.EventHandler(this.button_Click);
                physicalButtons.Add(b);
                flpInner.Controls.Add(b);
                if (currentLogicalButton >= 0 && i + topmostLogicalButton == currentLogicalButton) {
                    b.BackColor = SystemColors.Highlight;
                }
            }

            if (logicalButtonIndex > physicalButtons.Count)
            {
                scrollbar.Maximum = logicalButtonIndex - physicalButtons.Count;
            }

        }

        private void button_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (Button b in physicalButtons)
            {
                if (b == (Button)sender) {
                    b.BackColor = SystemColors.Highlight;
                    currentLogicalButton = i + topmostLogicalButton;
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
            if (index >= 0 && index < logicalButtons.Count)
            {

                // Set the button
                currentLogicalButton = index;

                // Try to force the button visible
                if (physicalButtons.Count == 0)
                {
                    return;
                }
                if (currentLogicalButton < topmostLogicalButton)
                {
                    topmostLogicalButton = currentLogicalButton;
                    scrollbar.Value = topmostLogicalButton;
                }
                if (currentLogicalButton >= topmostLogicalButton + physicalButtons.Count)
                {
                    topmostLogicalButton = (currentLogicalButton + 1) - physicalButtons.Count;
                    scrollbar.Value = topmostLogicalButton;
                }

                // Update the highlights on all buttons 
                refreshButtons();

                // Force a click event for this button.
                Button buttonClicked = physicalButtons[currentLogicalButton - topmostLogicalButton];
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
                btnUp.BackColor = buttonBackColor;
                btnDown.BackColor = buttonBackColor;
                scrollbar.BackColor = buttonBackColor;
                foreach (Button b in physicalButtons)
                {
                    b.BackColor = buttonBackColor;
                }
            }
        }

        // BUG: ShowArrows and ShowScrollbar interact in an unexpected way.  With the tester add buttons, then Toggle Scrollbar,
        //  Toggle Arrows, Toggle Scrollbar, Toggle Arrows: button rows seem offset by one...
        //  For now I only plan to use these as design-time settings.  BUT...

        [Browsable(true)]
        [Category("Layout")]
        [Description("Enables/disables the presence of the two arrow buttons")]
        public bool ShowArrowButtons
        {
            get
            {
                System.Diagnostics.Debug.WriteLine("mbcc ShowArrowButtons get: " + bShowArrowButtons);
                return bShowArrowButtons;
            }
            // Stores the selected value in the private variable colBColor, and 
            // updates the backcolor of the label control lblDisplay.
            set
            {
                bShowArrowButtons = value;
                System.Diagnostics.Debug.WriteLine("mbcc ShowArrowButtons get: " + bShowArrowButtons);
                if (bShowArrowButtons == false)
                {
                    tlpOuter.Controls.Remove(btnUp);
                    tlpOuter.Controls.Remove(btnDown);
                    tlpOuter.SetRow(flpInner, 0);
                    tlpOuter.SetRowSpan(flpInner, 3);
                }
                else
                {
                    tlpOuter.SetRowSpan(flpInner, 1);
                    tlpOuter.SetRow(flpInner, 1);
                    if (bShowScrollbar == true)
                    {
                        tlpOuter.Controls.Add(btnUp, 1, 0);
                        tlpOuter.Controls.Add(btnDown, 1, 2);
                    }
                    else
                    {
                        tlpOuter.Controls.Add(btnUp, 0, 0);
                        tlpOuter.Controls.Add(btnDown, 0, 2);
                        tlpOuter.SetColumnSpan(btnUp, 2);
                        tlpOuter.SetColumnSpan(btnDown, 2);

                    }
                }
            }
        }

        [Browsable(true)]
        [Category("Layout")]
        [Description("Enables/disables the presence of the scrollbar")]
        public bool ShowScrollbar
        {
            get
            {
                return bShowScrollbar;
            }
            // Stores the selected value in the private variable colBColor, and 
            // updates the backcolor of the label control lblDisplay.
            set
            {
                bShowScrollbar = value;
                if (bShowScrollbar == false)
                {
                    tlpOuter.Controls.Remove(scrollbar);
                    if (bShowArrowButtons == true)
                    {
                        tlpOuter.SetColumn(btnUp, 0);
                        tlpOuter.SetColumn(btnDown, 0);
                        tlpOuter.SetColumnSpan(btnUp, 2);
                        tlpOuter.SetColumnSpan(btnDown, 2);
                    }
                    tlpOuter.SetColumn(flpInner, 0);
                    tlpOuter.SetColumnSpan(flpInner, 2);
                }
                else
                {
                    if (bShowArrowButtons == true)
                    {
                        tlpOuter.SetColumnSpan(btnUp, 1);
                        tlpOuter.SetColumnSpan(btnDown, 1);
                        tlpOuter.SetColumn(btnUp, 1);
                        tlpOuter.SetColumn(btnDown, 1);
                    }
                    tlpOuter.SetColumnSpan(flpInner, 1);
                    tlpOuter.SetColumn(flpInner, 1);

                    tlpOuter.Controls.Add(scrollbar, 0, 1);
                }
            }
        }

    }
}
