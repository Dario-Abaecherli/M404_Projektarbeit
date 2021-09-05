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
    public partial class Form_titleScreen : Form
    {

        public Form_titleScreen()
        {
            InitializeComponent();
        }

        private void click_btn_gameStart(object sender, EventArgs e)
        {
            //Verlinkung der Charakter Creation Forms und ausführung dieser
            Form_charCreation incharcreationscreen = new Form_charCreation();
            incharcreationscreen.Show();
        }

        private void click_btn_help(object sender, EventArgs e)
        {
            //Helpscreen Verlinken und aufrufen (wird hier verlinkt, damit es immer wieder Zugänglich ist)
            Form_helpScreen inhelpscreen = new Form_helpScreen();
            inhelpscreen.Show();
        }

        private void click_btn_exitGame(object sender, EventArgs e)
        {
            //Erstellen und aufrufen des Exitreminders
            Form_exitReminder inexitreminder = new Form_exitReminder();
            inexitreminder.Show();
        }

        private void click_btn_load(object sender, EventArgs e)
        {

        }
    }
}
