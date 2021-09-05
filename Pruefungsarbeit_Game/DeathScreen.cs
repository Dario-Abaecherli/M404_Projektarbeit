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
    public partial class Form_deathScreen : Form
    {
        public Form_deathScreen(int level, string selectedClass, string charName)
        {
            InitializeComponent();

            lbl_enscreen_class.Text = charName + "/" + selectedClass;
            lbl_enscreen_lvl.Text = "Reached Level:" + level;
        }

        private void Form_deathScreen_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
