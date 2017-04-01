using System.Drawing;

namespace EasyJoin
{
    public class ParkingButton : System.Web.UI.WebControls.Button
    {
        public ParkingButton()
        {

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