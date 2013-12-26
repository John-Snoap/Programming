using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Start_Settings
{
   public partial class FormSettings : Form
   {
      // Constructor for FormSettings
      public FormSettings(int level, bool cheat, bool autoFire, bool invincibleShielding)
      {
         InitializeComponent();

         // Initialize cmbLevel to easiest setting
         cmbLevel.SelectedIndex = level;

         // Initialize noCheat to true and
         // Invincible to false
         radBtnNoCheat.Checked = !cheat;
         radBtnCheating.Checked = cheat;

         chckBxAutoFire.Checked = autoFire;
         chckBxAutoFire.Enabled = cheat;

         chckBxInvincibleShielding.Checked = invincibleShielding;
         chckBxInvincibleShielding.Enabled = cheat;
      }

   
      // Method for returning Modes status
      private void Modes() // before returned bool
      {
         //bool noCheat;

         if (radBtnNoCheat.Checked == true)
         {
            //noCheat = true;
            chckBxInvincibleShielding.Enabled = false;
            chckBxInvincibleShielding.Checked = false;
            chckBxAutoFire.Enabled = false;
            chckBxAutoFire.Checked = false;
         }
         else
         {
            //noCheat = false;
            chckBxInvincibleShielding.Enabled = true;
            chckBxAutoFire.Enabled = true;
         }

         //return noCheat;
      }

      // Either radio button is clicked then change noCheats value

      private void radBtnNoCheat_Click(object sender, EventArgs e)
      {
         Modes();
      }

      private void radBtnCheating_Click(object sender, EventArgs e)
      {
         Modes();
      }



   }
}
