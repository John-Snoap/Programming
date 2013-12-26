namespace Star_Wars_Space_Invaders
{
   partial class FormSettings
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCANCEL = new System.Windows.Forms.Button();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.grpBxModes = new System.Windows.Forms.GroupBox();
            this.chckBxAutoFire = new System.Windows.Forms.CheckBox();
            this.chckBxInvincibleShielding = new System.Windows.Forms.CheckBox();
            this.radBtnCheating = new System.Windows.Forms.RadioButton();
            this.radBtnNoCheat = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpBxModes.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.btnOK.Location = new System.Drawing.Point(38, 277);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCANCEL
            // 
            this.btnCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCANCEL.FlatAppearance.BorderSize = 0;
            this.btnCANCEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCANCEL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCANCEL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(206)))));
            this.btnCANCEL.Location = new System.Drawing.Point(163, 277);
            this.btnCANCEL.Name = "btnCANCEL";
            this.btnCANCEL.Size = new System.Drawing.Size(75, 23);
            this.btnCANCEL.TabIndex = 1;
            this.btnCANCEL.Text = "Cancel";
            this.btnCANCEL.UseVisualStyleBackColor = true;
            // 
            // cmbLevel
            // 
            this.cmbLevel.BackColor = System.Drawing.Color.Black;
            this.cmbLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLevel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(206)))));
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Items.AddRange(new object[] {
            "Youngling",
            "Padawan",
            "Jedi Knight",
            "Jedi Master"});
            this.cmbLevel.Location = new System.Drawing.Point(18, 26);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(121, 24);
            this.cmbLevel.TabIndex = 2;
            // 
            // grpBxModes
            // 
            this.grpBxModes.BackColor = System.Drawing.Color.Black;
            this.grpBxModes.Controls.Add(this.chckBxAutoFire);
            this.grpBxModes.Controls.Add(this.chckBxInvincibleShielding);
            this.grpBxModes.Controls.Add(this.radBtnCheating);
            this.grpBxModes.Controls.Add(this.radBtnNoCheat);
            this.grpBxModes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpBxModes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBxModes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.grpBxModes.Location = new System.Drawing.Point(38, 111);
            this.grpBxModes.Name = "grpBxModes";
            this.grpBxModes.Size = new System.Drawing.Size(200, 143);
            this.grpBxModes.TabIndex = 3;
            this.grpBxModes.TabStop = false;
            this.grpBxModes.Text = "Modes";
            // 
            // chckBxAutoFire
            // 
            this.chckBxAutoFire.AutoSize = true;
            this.chckBxAutoFire.BackColor = System.Drawing.Color.Black;
            this.chckBxAutoFire.FlatAppearance.BorderSize = 0;
            this.chckBxAutoFire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chckBxAutoFire.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(156)))));
            this.chckBxAutoFire.Location = new System.Drawing.Point(17, 87);
            this.chckBxAutoFire.Name = "chckBxAutoFire";
            this.chckBxAutoFire.Size = new System.Drawing.Size(81, 21);
            this.chckBxAutoFire.TabIndex = 3;
            this.chckBxAutoFire.Text = "Auto Fire";
            this.chckBxAutoFire.UseVisualStyleBackColor = false;
            // 
            // chckBxInvincibleShielding
            // 
            this.chckBxInvincibleShielding.AutoSize = true;
            this.chckBxInvincibleShielding.BackColor = System.Drawing.Color.Black;
            this.chckBxInvincibleShielding.FlatAppearance.BorderSize = 0;
            this.chckBxInvincibleShielding.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chckBxInvincibleShielding.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(156)))));
            this.chckBxInvincibleShielding.Location = new System.Drawing.Point(17, 109);
            this.chckBxInvincibleShielding.Name = "chckBxInvincibleShielding";
            this.chckBxInvincibleShielding.Size = new System.Drawing.Size(144, 21);
            this.chckBxInvincibleShielding.TabIndex = 2;
            this.chckBxInvincibleShielding.Text = "Invincible Shielding";
            this.chckBxInvincibleShielding.UseVisualStyleBackColor = false;
            // 
            // radBtnCheating
            // 
            this.radBtnCheating.AutoSize = true;
            this.radBtnCheating.BackColor = System.Drawing.Color.Black;
            this.radBtnCheating.FlatAppearance.BorderSize = 0;
            this.radBtnCheating.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radBtnCheating.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(156)))));
            this.radBtnCheating.Location = new System.Drawing.Point(17, 52);
            this.radBtnCheating.Name = "radBtnCheating";
            this.radBtnCheating.Size = new System.Drawing.Size(62, 21);
            this.radBtnCheating.TabIndex = 1;
            this.radBtnCheating.TabStop = true;
            this.radBtnCheating.Text = "Cheat";
            this.radBtnCheating.UseVisualStyleBackColor = false;
            this.radBtnCheating.Click += new System.EventHandler(this.radBtnCheating_Click);
            // 
            // radBtnNoCheat
            // 
            this.radBtnNoCheat.AutoSize = true;
            this.radBtnNoCheat.BackColor = System.Drawing.Color.Black;
            this.radBtnNoCheat.FlatAppearance.BorderSize = 0;
            this.radBtnNoCheat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radBtnNoCheat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(156)))));
            this.radBtnNoCheat.Location = new System.Drawing.Point(17, 29);
            this.radBtnNoCheat.Name = "radBtnNoCheat";
            this.radBtnNoCheat.Size = new System.Drawing.Size(84, 21);
            this.radBtnNoCheat.TabIndex = 0;
            this.radBtnNoCheat.TabStop = true;
            this.radBtnNoCheat.Text = "No Cheat";
            this.radBtnNoCheat.UseVisualStyleBackColor = false;
            this.radBtnNoCheat.Click += new System.EventHandler(this.radBtnNoCheat_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbLevel);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(206)))));
            this.groupBox1.Location = new System.Drawing.Point(38, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 62);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Difficulty";
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(284, 312);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpBxModes);
            this.Controls.Add(this.btnCANCEL);
            this.Controls.Add(this.btnOK);
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.grpBxModes.ResumeLayout(false);
            this.grpBxModes.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.Button btnCANCEL;
      public System.Windows.Forms.ComboBox cmbLevel;
      public System.Windows.Forms.GroupBox grpBxModes;
      public System.Windows.Forms.RadioButton radBtnCheating;
      public System.Windows.Forms.RadioButton radBtnNoCheat;
      public System.Windows.Forms.CheckBox chckBxAutoFire;
      public System.Windows.Forms.CheckBox chckBxInvincibleShielding;
      public System.Windows.Forms.GroupBox groupBox1;
   }
}