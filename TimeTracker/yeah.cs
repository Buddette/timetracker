using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTracker
{
    public partial class yeah : Form
    {
        public yeah()
        {
            InitializeComponent();

            // this makes it so that when button1 is clicked it calls the function called TrackingButtonGotPressedOrSomething
            // just ignore it, its magic
            button1.Click += (_, _) => TrackingButtonGotPressedOrSomething();
            button2.Click += (_, _) => UnTrackingButtonGotPressedOrSomething();
        }

        public void TrackingButtonGotPressedOrSomething()
        {
            // this will be called when it gets pressed
            Console.Beep(1500, 500);
        }

        public void UnTrackingButtonGotPressedOrSomething()
        {
            // this will be called when it gets pressed also my ball itch
            Console.Beep(500, 500);
        }
    }
}
