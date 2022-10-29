using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace TimeTracker
{
    public partial class yeah : Form
    {
        public string numberthing;

        public yeah()
        {
            InitializeComponent();

            // this makes it so that when button1 is clicked it calls the function called TrackingButtonGotPressedOrSomething
            // just ignore it, its magic
            button1.Click += (_, _) => TrackingButtonGotPressedOrSomething();
            button2.Click += (_, _) => UnTrackingButtonGotPressedOrSomething();
            button3.Click += (_, _) => EmailButtonGotPressedOrSomething();
            button4.Click += (_, _) => ProgramButtonGotPressedOrSomething();
        }



        public void TrackingButtonGotPressedOrSomething()
        {
            // this will be called when it gets pressed
            //Program.OldProgram(); OLD BROKEY DONT USE
            label3.Text = "status: running";
            // this makes it run on a separate threadpool thread, which doesn't make the UI thread stop 
            Task.Run(() => Program.NewProgramStart());
        }

        public void UnTrackingButtonGotPressedOrSomething()
        {
            // this will be called when it gets pressed also my ball itch
            label3.Text = "status: not running";
            Program.stop = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text == "eepy")
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"https://cdn.discordapp.com/attachments/537146611692994570/1030755741486809098/Among_Us_-_Venting_Sound_Effect.wav");
                player.Play();
                MessageBox.Show("wrong", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
        }

        public void updatenumberthing(string number)
        {
            MethodInvoker inv = delegate
            {
                label4.Text = number;
            };

            this.Invoke(inv);
        }

        public void ProgramButtonGotPressedOrSomething()
        {
            Program.process_name = textBox2.Text;
            Program.stop = true;
            Program.timer.Reset();
            File.Delete(Program.path);
        }

        //ignore
        #region ignore
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        #endregion

        public void EmailButtonGotPressedOrSomething()
        {
            MessageBox.Show("fuck you you're not allowed to complain", "NO! SUHT UP!!! :c", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
