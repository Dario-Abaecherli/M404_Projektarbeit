using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAbächerli_Pruefungsarbeit_Game
{
    public partial class Form_gameScreen : Form
    {
        //Variablen erstellen
        private string[] selectedClass;


        //Variablen für Charakterstatistiken erstellen
        int maxHealth;
        int health;
        int damage;
        int block;
        int healed;


        //zusatzvariablen
        int level;
        int exp;
        int itemNR;
        int armorcount;
        int swordcount;
        int bowcount;
        int wandcount;
        String playerclass;
        int playerdamage;
        int mobdamage;
        int dble = 0;
        int monsterlevel = 1;
        int totaldamage;
        int totaldamagedealt;
        int gainExp;


        //Zählvariablen für Aktionen
        int restcount = 0;
        int maxrest = 10;
        int searchcount = 0;
        int maxsearch = 8;
        int fightcount = 0;
        int maxfight;
        int bosscount = 0;
        int maxboss;
        



        //struktur für Items erstellen
        struct struItem
        {
            public String itemtype;
            public int addHealth;
            public int addDamage;
            public int addBlock;
        }


        Random rng = new Random();

        //Enumerator für Itemkalssen 
        public enum Itemclass:byte {space, Armor, Shield, Sword, Bow, Wand}

        //Variable Abrufen aus charCreation
        public Form_gameScreen(string[] FcC_selectedClass)
        {
            InitializeComponent();

            //Stats je nach charauswaahl aus array laden
            selectedClass = FcC_selectedClass;
            damage = Convert.ToInt32(selectedClass[2]);
            block = Convert.ToInt32(selectedClass[3]);

            // Leben zuweisen
            maxHealth = Convert.ToInt32(selectedClass[1]);
            health = maxHealth;
            lbl_Special.Text = selectedClass[4].ToString();

            //Bild für Helden laden (vermutlich nicht funktoinstauglich)
            if (Convert.ToInt32(selectedClass[0]) == 1)
            {
                //Klassenbild laden und richtige Klasse zuweisen
                playerclass = "Fighter";
                lbl_charPicture.Image = Image.FromFile(@"D:\VisualStudio\Bilder\Fighter.jpg");
            }
            else if (Convert.ToInt32(selectedClass[0]) == 2)
            {
                //Klassenbild laden und richtige Klasse zuweisen
                playerclass = "Archer";
                lbl_charPicture.Image = Image.FromFile(@"D:\VisualStudio\Bilder\Archer_02.jpg");
            }
            else if (Convert.ToInt32(selectedClass[0]) == 3)
            {
                //Klassenbild laden und richtige Klasse zuweisen
                playerclass = "Mage";
                lbl_charPicture.Image = Image.FromFile(@"D:\VisualStudio\Bilder\Mage.jpg");
            }

            //Status Updaten
            updateStats();

        }

        private void btn_click_return_titleScreen(object sender, EventArgs e)
        {
            this.Close();
        }

        private void click_btn_exitGame(object sender, EventArgs e)
        {
            //Erstellen und aufrufen des Exitreminders
            Form_exitReminder inexitreminder = new Form_exitReminder();
            inexitreminder.Show();
        }

        private void click_btn_rest(object sender, EventArgs e)
        {
            //Ereigniss Rest erstellen
            if (restcount < maxrest)
            {
                //Heilung und Exp verteilung an Spieler
                healed = maxHealth - health;
                health = maxHealth;
                gainExp = rng.Next(1, 2)*monsterlevel/2;
                exp += gainExp;
                restcount += 1;

                //Farbwechsel des Buttontextes falls alle ladungen aufgebraucht
                if (restcount == maxrest) { 
                btn_rest.ForeColor = Color.Red;
                }

                //Updaten
                levelupdate();
                updateStats();
            }

            MessageBox.Show("Rested\n"+ healed +" Healed\n"+ gainExp +" Exp gained");
        }

        private void click_btn_search(object sender, EventArgs e)
        {
            //Search Ereigniss erstellen
            if (searchcount < maxsearch)
            {

                searchcount += 1;

                itemNR = searchcount;
                string[] genItem = generateItem();

                //Leben und weitere Stats der Waffen hinzufügen
                maxHealth += Convert.ToInt32(genItem[1]);
                health += Convert.ToInt32(genItem[1]);
                damage += Convert.ToInt32(genItem[2]);
                block += Convert.ToInt32(genItem[3]);

                //Inventananzahl der jeweiligen Ausrüstung anzeigen
                if (Convert.ToInt32(genItem[0]) == Convert.ToInt32(Itemclass.Armor) || Convert.ToInt32(genItem[0]) == Convert.ToInt32(Itemclass.Shield))
                {
                    //Umwandlung des Strings aus dem Label in ein integer, damit dieser verrechnet werden kann
                    armorcount += 1;
                    if (playerclass == "Fighter")
                    {
                        maxHealth += 1;
                    }
                }
                else if (Convert.ToInt32(genItem[0]) == Convert.ToInt32(Itemclass.Sword))
                {
                    //Käpferbonus für Schwerter
                    swordcount += 1;
                    if (playerclass == "Fighter")
                    {
                        damage += 1;
                    }
                }
                else if (Convert.ToInt32(genItem[0]) == Convert.ToInt32(Itemclass.Bow))
                {
                    bowcount += 1;
                    //Schützenbonus Für bögen
                    if (playerclass == "Archer")
                    {
                        damage += 2;
                    }
                }
                else if (Convert.ToInt32(genItem[0]) == Convert.ToInt32(Itemclass.Wand))
                {
                    wandcount += 1;
                    //Magierbonus Für Stäbe
                    if (playerclass == "Mage")
                    {
                        damage += 2;
                    }
                }

                //Aufrufen des Statusupdaters
                updateStats();


                //Farbwechsel des Buttontextes falls alle ladungen aufgebraucht
                if (searchcount == maxsearch)
                {
                    btn_search.ForeColor = Color.Red;
                }

                MessageBox.Show("Weapon found\nStats\n" + genItem[1] + " Health\n" + genItem[2] + " Damage\n" + genItem[3] + " Block");
            }

        }

        //itemstruktur, generieren eines items
       
        
        //Fight befehle
        private void click_btn_fight(object sender, EventArgs e)
        {

            //Totaldamages auf 0 setzen
            totaldamage = 0;
            totaldamagedealt = 0;

            //Generatemonster Variable aufrufen
            int[] genMonster = generateMonster();

            dble = 0;

            //Angrifszähler erhöhen
            fightcount += 1;

            //Monsterkampf
            for ( int i = (genMonster[0]); i > 0; )
            {
                //Spielerschaden am Monster
                playerdamage = (damage - (genMonster[2]));

                //Monsterschaden am Spieler
                mobdamage = ((genMonster[1]) - block);

                //Schaut ob spieler oder Monster überhaupt schaden macht bzw. gemacht hat
                if (playerdamage > 0)
                {
                    i -= playerdamage;
                    totaldamagedealt += playerdamage;
                }
                if (mobdamage > 0)
                {
                    health -= mobdamage;
                    totaldamage += mobdamage;
                }    


                if (i <= 0 && dble == 0)
                {
                    dble = 1;
                    exp += (genMonster[3]);

                    //Kampfstatistik
                    //Erteilter und Verteilter schaden ausstellen
                    MessageBox.Show("You Won\n" + totaldamagedealt + " Damage dealt\n" + totaldamage + " Damage recieved\n" + genMonster[3] + " Exp gained");

                    //Level und Statusupdate
                    levelupdate();
                    updateStats();
                    

                }


            }



        }

        //Monstergenerator erstellen
        int[] generateMonster()
        {
            
            //Variablen für Monster
            int monsterHp;
            int monsterDamage;
            int monsterShield;
            int monsterExp;

            //Monsterstatistiken erstellen und mit Erfahrunf des Spielers skalen
            monsterHp = rng.Next(3, 5) * monsterlevel/2;
            monsterDamage = rng.Next(1,3) * monsterlevel/2;
            monsterShield = rng.Next(1, 2)* monsterlevel/2;
            monsterExp = rng.Next(1, 3)* monsterlevel;

            //Monsterdaten in ein Array verpacken
            int[] monster = new int[4] { monsterHp, monsterDamage, monsterShield, monsterExp };

            //und zurücksenden
            return monster;
        }


        string[] generateItem()
        {
            //Auf Struktur von oben zugreifen
            struItem item1;
            item1.itemtype = rng.Next(1, 6).ToString();
            item1.addHealth = rng.Next(1, 2);
            item1.addDamage = rng.Next(1, 2);
            item1.addBlock = rng.Next(0, 1);

            //Zusatz für Leben
            if (Convert.ToInt32(item1.itemtype) == 1)
            {
                item1.addHealth += 4;
            }
            //Zusatz Stats für Schild
            else if (Convert.ToInt32(item1.itemtype) == 2)
            {
                item1.addBlock += 3;
            }
            //Zusatz für Schwert
            else if (Convert.ToInt32(item1.itemtype) == 3)
            {
                item1.addDamage += 3;
            }
            //Zusatz für Bogen
            else if (Convert.ToInt32(item1.itemtype) == 4)
            {
                item1.addDamage += 3;
            }
            //Zusatz für Stab
            else if (Convert.ToInt32(item1.itemtype) == 5)
            {
                item1.addDamage += 3;
            }

            //Verpacken aller Daten in ein String
            string[] item = new string[4] { item1.itemtype, item1.addHealth.ToString(), item1.addDamage.ToString(), item1.addBlock.ToString() };

            //Rückgabe des Strings
            return item;
        }

        //Erstellen von Levenupdate, auf das später zugegriffen werden kann
        void levelupdate()
        {
            //Updated das Level
            if (level <= 10)
            {
                level = Convert.ToInt32(Math.Round(Math.Sqrt(Convert.ToDouble(exp))));
                monsterlevel = level + 1;
            }
            

        }

        //Erstellen von Updatestatus, auf das später zugegriffen werden kann
        void updateStats()
        {
            //Alle daten, die Aktualisiert werden, wird Update Status aufgerufen
            lbl_Health.Text = (health + 3 * level) + "/" + (maxHealth + 3 * level);
            lbl_Damage.Text = (damage + 2 * level).ToString();
            lbl_Block.Text = block.ToString();
            lbl_armorcount.Text = armorcount.ToString();
            lbl_swordcount.Text = swordcount.ToString();
            lbl_bowcount.Text = bowcount.ToString();
            lbl_wandcount.Text = wandcount.ToString();
            lbl_level.Text = level.ToString();
            lbl_exp.Text = exp.ToString();
            lbl_search.Text = searchcount + "/" + maxsearch;
            lbl_rest.Text = restcount + "/" + maxrest;
            lbl_fight.Text = fightcount.ToString();
            lbl_state.Text = selectedClass[5] + "/" + playerclass;


            //Todestracker (schaut, ob der spieler tot ist)
            if (health <=0)
            {
                this.Close();
                Form_deathScreen indeathscreen = new Form_deathScreen(level, playerclass, selectedClass[5]);
                indeathscreen.Show();
            }
                        
        }

    }
}
