using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAbächerli_Pruefungsarbeit_Game
{
    public partial class Form_helpScreen : Form
    {
        public Form_helpScreen()
        {
            InitializeComponent();
        }


        private void click_btn_return(object sender, EventArgs e)
        {
            //Schliessen dieses Fensters
            this.Close();
        }

        private void click_btn_exitgame(object sender, EventArgs e)
        {
            //Erstellen und aufrufen des Exitreminders
            Form_exitReminder inexitreminder = new Form_exitReminder();
            inexitreminder.Show();
        }
    }
}
