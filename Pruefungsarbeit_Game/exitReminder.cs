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
    public partial class Form_exitReminder : Form
    {       

        public Form_exitReminder()
        {
            InitializeComponent();
        }

        private void click_btn_exit_yes(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.ExitThread();
        }

        private void click_btn_exit_no(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
