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

        bool bShowAlphaButtons = false;

        bool bShowLogicalButtonNumberBadge = false;

        const int MIN_ALPHA_BUTTON_HEIGHT = 20;     // Based on font used for this button.

        Color[] badgeColors;

        public string LastSelectedButtonText
        {
            get
            {
                if (currentLogicalButton >= 0)
                {
                    return logicalButtons[currentLogicalButton].text;
                }
                else
                {
                    return null;
                }
            }
        }

        public object LastSelectedButtonTag
        {
            get
            {
                if (currentLogicalButton >= 0)
                {
                    return logicalButtons[currentLogicalButton].tag;
                }
                else
                {
                    return null;
                }
            }
        }

        public MultiButtonColControl()
        {
            InitializeComponent();

            badgeColors = new Color[3];
            badgeColors[0] = Color.Yellow;
            badgeColors[1] = Color.DeepPink;
            badgeColors[2] = Color.LawnGreen;

            scrollbar.Minimum = scrollbar.Maximum = 0;

            btnAlpha.Height = (flpAlphaButtons.Height - (flpAlphaButtons.Margin.Top + flpAlphaButtons.Margin.Bottom)) / 27;
            btnAlpha.Visible = false;

            for (char c = 'A'; c <= 'Z'; ++c)
            {
                // Clone the designed zero button
                Button b = new Button();
                b.Visible = false;
                b.BackColor = btnAlpha.BackColor;
                b.ForeColor = btnAlpha.ForeColor;
                b.Text = new string(c, 1);
                b.Width = btnAlpha.Width;
                b.Height = btnAlpha.Height;
                b.Padding = new Padding(0);
                b.Margin = new Padding(0);
                b.Font = new Font(btnAlpha.Font, FontStyle.Regular);
                b.Click += new System.EventHandler(this.btnAlpha_Click);

                flpAlphaButtons.Controls.Add(b);
            }

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
            if (this.bShowLogicalButtonNumberBadge) {
                Adorner.AddBadgeTo(b, (logicalButtonIndex + 1).ToString(), badgeColors[logicalButtonIndex % badgeColors.Length]);
            }

            b.BackColor = buttonBackColor;
            int buttonMarginWidth = b.Margin.Top + b.Margin.Bottom;
            int containerInnerHeight = (flpInner.Height - (flpInner.Margin.Top + flpInner.Margin.Bottom));

            int nPhysicalButtonsProposed = physicalButtons.Count + 1;
            int newButtonHeight = (containerInnerHeight / nPhysicalButtonsProposed) - buttonMarginWidth;
            if (newButtonHeight >= minButtonHeight || nPhysicalButtonsProposed == 1)
            {
                b.Text = text;
                b.Tag = tag;
                b.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                b.Click += new System.EventHandler(this.button_Click);
                physicalButtons.Add(b);
                flpInner.Controls.Add(b);

                foreach (Button b2 in physicalButtons)
                {
                    b2.Height = newButtonHeight;
                    b2.Width = flpInner.Width - (flpInner.Margin.Left + flpInner.Margin.Right);
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

            // Make alpha nav button for this button visibles
            string firstChar = text.Substring(0, 1).ToUpper();
            string alphaButtonText = (firstChar.CompareTo("A") <= 0 || firstChar.CompareTo("Z") >= 0) ? "0" : firstChar;
            foreach (Control ctl in flpAlphaButtons.Controls)
            {
                // if typeof(ctl) != Button then next...

                Button b2 = (Button)ctl;    
                if (b2.Text == alphaButtonText)
                {
                    b2.Visible = true;
                    break;
                }
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
                Adorner.RemoveBadgeFrom(b);
            }

            physicalButtons.Clear();

            currentLogicalButton = -1;

            topmostLogicalButton = 0;

            logicalButtonIndex = 0;

            foreach (Control ctl in flpAlphaButtons.Controls)
            {
                // if typeof(ctl) != Button then next...

                Button b2 = (Button)ctl;
                b2.Visible = true;
            }
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
                int logicalButton = i + topmostLogicalButton;
                var physicalButton = physicalButtons[i];
                physicalButton.Text = logicalButtons[logicalButton].text;
                physicalButton.Tag = logicalButtons[logicalButton].tag;
                physicalButton.BackColor = (currentLogicalButton >= 0 && currentLogicalButton == logicalButton) ? SystemColors.Highlight : buttonBackColor;
                Adorner.SetBadgeText(physicalButtons[i], (logicalButton + 1).ToString());
                Adorner.SetBadgeColor(physicalButtons[i], badgeColors[logicalButton % badgeColors.Length]);
                physicalButton.Refresh();
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

            if (currentLogicalButton >= 0 && currentLogicalButton >= topmostLogicalButton + nProposedButtons)
            {
                topmostLogicalButton = currentLogicalButton - nProposedButtons;
            }

            if (topmostLogicalButton + nProposedButtons > logicalButtons.Count)
            {
                topmostLogicalButton = logicalButtons.Count - nProposedButtons;
            }
            scrollbar.Value = topmostLogicalButton;

            for (int i = 0; i < nProposedButtons; ++i)
            {
                b = new Button();
                if (this.bShowLogicalButtonNumberBadge)
                {
                    Adorner.AddBadgeTo(b, (i + topmostLogicalButton + 1).ToString(), badgeColors[(i + topmostLogicalButton) % badgeColors.Length]);
                }
                b.BackColor = buttonBackColor;
                b.Text = logicalButtons[i + topmostLogicalButton].text;
                b.Tag = logicalButtons[i + topmostLogicalButton].tag;
                b.Font = new System.Drawing.Font("Microsoft Sans Serif", fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                b.Height = proposedButtonHeight;
                b.Width = flpInner.Width - (flpInner.Margin.Left + flpInner.Margin.Right);
                b.Click += new System.EventHandler(this.button_Click);
                physicalButtons.Add(b);
                flpInner.Controls.Add(b);
                if (currentLogicalButton >= 0 && i + topmostLogicalButton == currentLogicalButton)
                {
                    b.BackColor = SystemColors.Highlight;
                }
            }

            scrollbar.LargeChange = physicalButtons.Count;
            scrollbar.Maximum = logicalButtons.Count - 1 /* - physicalButtons.Count.  See https://stackoverflow.com/questions/2882789/net-vertical-scrollbar-not-respecting-maximum-property */;

            // Resize alpha buttons
            int alphaButtonHeight = (flpAlphaButtons.Height - (flpAlphaButtons.Margin.Top + flpAlphaButtons.Margin.Bottom)) / 27;
            if (alphaButtonHeight < MIN_ALPHA_BUTTON_HEIGHT)
            {
                alphaButtonHeight = MIN_ALPHA_BUTTON_HEIGHT;
            }

            foreach (Control ctl in flpAlphaButtons.Controls)
            {
                // if typeof(ctl) != Button then next...

                Button b2 = (Button)ctl;
                b2.Height = alphaButtonHeight;
            }

        }

        private void button_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (Button b in physicalButtons)
            {
                if (b == (Button)sender)
                {
                    b.BackColor = SystemColors.Highlight;
                    currentLogicalButton = i + topmostLogicalButton;
                }
                else
                {
                    b.BackColor = buttonBackColor;
                }
                ++i;
            }

            if (Click != null)
            {
                Click(sender, e);
            }
        }

        public void selectLogicalButton(int index, bool bThrowClickEvent, bool bTryToMakeTopButton)
        {
            if (index >= 0 && index < logicalButtons.Count)
            {

                // Set the button
                currentLogicalButton = index;

                if (physicalButtons.Count == 0)
                {
                    return;
                }

                // Force the button visible
                if (currentLogicalButton < topmostLogicalButton || bTryToMakeTopButton)
                {
                    topmostLogicalButton = currentLogicalButton;
                }
                if (topmostLogicalButton > logicalButtons.Count - physicalButtons.Count)
                {
                    topmostLogicalButton = logicalButtons.Count - physicalButtons.Count;
                }
                if (currentLogicalButton >= topmostLogicalButton + physicalButtons.Count)
                {
                    topmostLogicalButton = (currentLogicalButton + 1) - physicalButtons.Count;
                }
                // Else it's already visible


                scrollbar.Value = topmostLogicalButton;

                // Update the highlights on all buttons 
                refreshButtons();

                // Force a click event for this button.
                Button buttonClicked = physicalButtons[currentLogicalButton - topmostLogicalButton];
                if (bThrowClickEvent == true && Click != null)
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
                selectLogicalButton(currentLogicalButton + 1, throwClickEvent, false);
            }
        }

        public void selectPrevLogicalButton(bool throwClickEvent)
        {
            if (currentLogicalButton > 0)
            {
                selectLogicalButton(currentLogicalButton - 1, throwClickEvent, false);
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
                return bShowArrowButtons;
            }
            // Stores the selected value in the private variable colBColor, and 
            // updates the backcolor of the label control lblDisplay.
            set
            {
                bShowArrowButtons = value;
                redoLayout();
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
                redoLayout();
            }
        }
        [Browsable(true)]
        [Category("Layout")]
        [Description("Enables/disables the presence of the alphabet buttons")]
        public bool ShowAlphaButtons
        {
            get
            {
                return bShowAlphaButtons;
            }
            // Stores the selected value in the private variable colBColor, and 
            // updates the backcolor of the label control lblDisplay.
            set
            {
                bShowAlphaButtons = value;
                redoLayout();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Enables/disables the display of a logical button number badge on the button")]
        public bool ShowLogicalButtonNumberBadge
        {
            get
            {
                return bShowLogicalButtonNumberBadge;
            }
            // Stores the selected value in the private variable colBColor, and 
            // updates the backcolor of the label control lblDisplay.
            set
            {
                bShowLogicalButtonNumberBadge = value;
                redoLayout();
            }
        }

        private void redoLayout()
        {
            int mainCol = 0;
            int mainRow = 0;
            int mainColSpan = 1;
            int mainRowSpan = 1;

            for (int i = tlpOuter.Controls.Count-1; i >=0; --i)
            {
                tlpOuter.Controls.Remove(tlpOuter.Controls[i]);
            }

            if (bShowScrollbar)
            {
                tlpOuter.Controls.Add(scrollbar, 0, 1);
                mainCol = 1;
            }
            else
            {
                mainCol = 0;
                mainColSpan = 2;
            }

            if (bShowArrowButtons)
            {
                int arrowCol = (bShowScrollbar) ? 1 : 0;
                int arrowSpan = (bShowScrollbar) ? 1 : 2;
                tlpOuter.Controls.Add(btnUp, arrowCol, 0);
                tlpOuter.Controls.Add(btnDown, arrowCol, 2);
                if (!bShowAlphaButtons)
                {
                    ++arrowSpan;
                }
                tlpOuter.SetColumnSpan(btnUp, arrowSpan);
                tlpOuter.SetColumnSpan(btnDown, arrowSpan);

                mainRow = 1;
            }
            else
            {
                mainRowSpan = 0;
                mainRowSpan = 3;
            }

            if (bShowAlphaButtons)
            {
                tlpOuter.Controls.Add(flpAlphaButtons, 2, 0);
                tlpOuter.SetRowSpan(flpAlphaButtons, 3);
            }
            else
            {
                mainColSpan += 1;
            }

            System.Diagnostics.Debug.WriteLine("mainCol: " + mainCol);
            System.Diagnostics.Debug.WriteLine("mainRow: " + mainRow);
            System.Diagnostics.Debug.WriteLine("mainColSpan: " + mainColSpan);
            System.Diagnostics.Debug.WriteLine("mainRowSpan: " + mainRowSpan);
            
            tlpOuter.Controls.Add(flpInner, mainCol, mainRow);
            tlpOuter.SetColumnSpan(flpInner, mainColSpan);
            tlpOuter.SetRowSpan(flpInner, mainRowSpan);

            MultiButtonColControl_Resize(null, null);
        }

        private void btnAlpha_Click(object sender, EventArgs e)
        {
            Button alphaButton = (Button)sender;
            string letter = alphaButton.Text.ToUpper();
            selectByName(letter);
            if (letter.CompareTo("A") < 0 || letter.CompareTo("Z") > 0)
            {
                letter = "";
            }
            selectByName(letter);
        }

        public void selectByName(string name)
        {
            if (name == "")
            {
                selectLogicalButton(0, false, true);
                return;
            }

            for (int i = 0; i < logicalButtons.Count; ++i)
            {
                if (logicalButtons[i].text.ToUpper().StartsWith(name.ToUpper()))
                {
                    selectLogicalButton(i, false, true);
                    return;
                }
            }

        }
    }
}