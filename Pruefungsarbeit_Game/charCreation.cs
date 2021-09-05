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
    public partial class Form_charCreation : Form
    {
        //Initialiseren der zu Übergebenden Variable in einem Array
        public string[] FcC_selectedClass = new string[6];

        //Normale Variablen
        int selectedClass = 0;
        string charname;
        bool numberName;


        public Form_charCreation()
        {
            InitializeComponent();
        }

        //Class Selection Programm
        //Aussuchen einer von 3 Klassen, die danach in den gameScreen übergeben werden soll
        private void click_select_fighter(object sender, EventArgs e)
        {
            //Beim anklicken des Bildes komme ein roter chosen Text und der Charakter Kämpfer wird angewählt
            selectedClass = 1;
            lbl_fighter.Text = "Chosen";
            lbl_archer.Text = "";
            lbl_mage.Text = "";
            btn_confirmLoadout.Text = "Confirm";
        }

        private void click_select_archer(object sender, EventArgs e)
        {
            //Beim anklicken des Bildes komme ein roter chosen Text und der Schütze Kämpfer wird angewählt
            selectedClass = 2;
            lbl_fighter.Text = "";
            lbl_archer.Text = "Chosen";
            lbl_mage.Text = "";
            btn_confirmLoadout.Text = "Confirm";
        }

        private void click_select_mage(object sender, EventArgs e)
        {
            //Beim anklicken des Bildes komme ein roter chosen Text und der Charakter Magier wird angewählt
            selectedClass = 3;
            lbl_fighter.Text = "";
            lbl_archer.Text = "";
            lbl_mage.Text = "Chosen";
            btn_confirmLoadout.Text = "Confirm";
        }

        private void click_btn_chooseChar(object sender, EventArgs e)
        {
            if (btn_confirmLoadout.Text == "Confirm")
            {
                //Name des Spielers
                charname = tbx_name.Text;

                //Ueberpruefung, ob Zahlen oder Lehrschläge im namen sind
                //Wenn ja, abbrechen des Befehls, ansonster Weiterführen
                numberName = charname.All(char.IsLetter);
                if (numberName == true)
                {
                    FcC_selectedClass[5] = charname;
                }
                else
                {
                    MessageBox.Show("Error: Number or space in name");
                    return;
                }

                
                //Variable zur übergabe in einem Array bereitstellen, welches alle daten des Charakters enthalten soll
                //Array Füllen mit den stats des gewählten Charakters
                if (selectedClass == 1)
                {
                    //Alle Daten des Klasse Kämpfer wird in eine Array verpackt, wenn er als held gewählt wurde
                    FcC_selectedClass[0] = Convert.ToString(selectedClass);
                    FcC_selectedClass[1] = Convert.ToString(lbl_fighterHealth.Text);
                    FcC_selectedClass[2] = Convert.ToString(lbl_fighterDamage.Text);
                    FcC_selectedClass[3] = Convert.ToString(lbl_fighterBlock.Text);
                    FcC_selectedClass[4] = Convert.ToString(lbl_fighterSpecial.Text);
                } else if (selectedClass == 2)
                {
                    //Alle Daten des Klasse Schütze wird in eine Array verpackt, wenn er als held gewählt wurde
                    FcC_selectedClass[0] = Convert.ToString(selectedClass);
                    FcC_selectedClass[1] = Convert.ToString(lbl_archerHealth.Text);
                    FcC_selectedClass[2] = Convert.ToString(lbl_archerDamage.Text);
                    FcC_selectedClass[3] = Convert.ToString(lbl_archerBlock.Text);
                    FcC_selectedClass[4] = Convert.ToString(lbl_archerSpecial.Text);
                } else if (selectedClass == 3)
                {
                    //Alle Daten des Klasse Magier wird in eine Array verpackt, wenn er als held gewählt wurde
                    FcC_selectedClass[0] = Convert.ToString(selectedClass);
                    FcC_selectedClass[1] = Convert.ToString(lbl_mageHealth.Text);
                    FcC_selectedClass[2] = Convert.ToString(lbl_mageDamage.Text);
                    FcC_selectedClass[3] = Convert.ToString(lbl_mageBlock.Text);
                    FcC_selectedClass[4] = Convert.ToString(lbl_mageSpecial.Text);
                }


                //Gamescreen Aufrufen und daten als array übergeben
                Form_gameScreen ingamescreen = new Form_gameScreen(FcC_selectedClass);
                ingamescreen.Show();

                //Dieses Fenster Schliessen
                this.Close();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
