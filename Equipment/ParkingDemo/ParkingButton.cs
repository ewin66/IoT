using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkingDemo
{
    class ParkingButton:System.Windows.Forms.Button
    {
        private Point location;
        private Button btn;

        public ParkingButton()
        {
            this.MouseDown += BtnMouseDown;
            this.MouseMove += BtnMouseMove;
        }
        private void BtnMouseDown(object sender, MouseEventArgs e)
        {
            location = e.Location;
            btn = sender as Button;
        }

        private void BtnMouseMove(object sender, MouseEventArgs e)
        {
            int pos_x, pos_y;

            if (e.Button == MouseButtons.Left)
            {
                pos_x = btn.Location.X + (e.X - location.X);
                pos_y = btn.Location.Y + (e.Y - location.Y);
                btn.Location = new Point(pos_x, pos_y);
            }
        }

        public string State
        {
            set
            {
                SetParkingState(value);
            }
        }

        public void SetParkingState(string state)
        {
            if (state == "1")
            {
                this.BackColor = Color.Green;
            }
            else if (state == "2")
            {
                this.BackColor = Color.Red;
            }
            else
            {
                this.BackColor = System.Drawing.SystemColors.Control;
            }
        }
    }
}
