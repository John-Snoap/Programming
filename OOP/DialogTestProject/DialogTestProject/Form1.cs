using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DialogTestProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // display my dialog
            FormSettings dlg;

            dlg = new FormSettings(); // makes instance, not shown on screen yet

            // Modeless vs. Modal
            // Modal - Don't go on in the program until they click OK/Cancel
            // Modal - Normal, ShowDialog()
            // Modeless - Show()
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int level = dlg.Level.SelectedIndex;
                
                MessageBox.Show("got ok, level is:  " + level);


            } // end if
        }
    }
}
