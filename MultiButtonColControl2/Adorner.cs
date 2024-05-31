using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

// Adapted rom https://stackoverflow.com/questions/29756038/add-a-badge-to-a-c-sharp-winforms-control

namespace MultiButtonColControl2
{
    static class Adorner
    {
        private static List<Control> controls = new List<Control>();

        static public bool AddBadgeTo(Control ctl, string Text)
        {
            return AddBadgeTo(ctl, Text, Color.Tan);
        }

        static public bool AddBadgeTo(Control ctl, string Text, Color backColor)
        {
            if (controls.Contains(ctl)) return false;

            Badge badge = new Badge(backColor);
            badge.AutoSize = true;
            badge.Text = Text;
            badge.BackColor = Color.Transparent;
            controls.Add(ctl);
            ctl.Controls.Add(badge);
            SetPosition(badge, ctl);

            return true;
        }

        static public bool RemoveBadgeFrom(Control ctl)
        {
            Badge badge = GetBadge(ctl);
            if (badge != null)
            {
                ctl.Controls.Remove(badge);
                controls.Remove(ctl);
                return true;
            }
            else return false;
        }

        static public void SetBadgeText(Control ctl, string newText)
        {
            Badge badge = GetBadge(ctl);
            if (badge != null)
            {
                badge.Text = newText;
                // SetPosition(badge, ctl);
                badge.Update();
            }
        }

        static public void SetBadgeColor(Control ctl, Color badgeColor)
        {
            Badge badge = GetBadge(ctl);
            if (badge != null)
            {
                badge.SetBadgeColor(badgeColor);
                // SetPosition(badge, ctl);
                badge.Update();
            }
        }

        static public string GetBadgeText(Control ctl)
        {
            Badge badge = GetBadge(ctl);
            if (badge != null) return badge.Text;
            return "";
        }

        static private void SetPosition(Badge badge, Control ctl)
        {
            badge.Location = new Point(ctl.Left + 2,
                                       ctl.Top + 2);
        }

        static public void SetClickAction(Control ctl, Action<Control> action)
        {
            Badge badge = GetBadge(ctl);
            if (badge != null) badge.ClickEvent = action;
        }

        static Badge GetBadge(Control ctl)
        {
            for (int c = 0; c < ctl.Controls.Count; c++)
                if (ctl.Controls[c] is Badge) return ctl.Controls[c] as Badge;
            return null;
        }


        class Badge : Label
        {
            new Color BackColor = Color.Tan;
            new Color ForeColor = Color.Black;
            Font font = new Font("Sans Serif", 10f);

            public Action<Control> ClickEvent;

            public Badge() { }

            public Badge(Color backColor)
            {
                BackColor = backColor;
            }

            public void SetBadgeColor(Color backColor)
            {
                BackColor = backColor;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.FillEllipse(new SolidBrush(BackColor), this.ClientRectangle);
                e.Graphics.DrawString(Text, font, new SolidBrush(ForeColor), 7, 4);
            }

            protected override void OnClick(EventArgs e)
            {
                ClickEvent(this);
            }

        }
    }
}
