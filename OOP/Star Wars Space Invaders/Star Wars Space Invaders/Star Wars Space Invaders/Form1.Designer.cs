namespace Star_Wars_Space_Invaders
{
   partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.lblShipsDestroyed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblLivesLeft = new System.Windows.Forms.Label();
            this.lblShpsDestryd = new System.Windows.Forms.Label();
            this.lblLivesCntr = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.pnlGameOptions = new System.Windows.Forms.Panel();
            this.pnlGameOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.btnStart.Location = new System.Drawing.Point(12, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(156)))));
            this.btnSettings.Location = new System.Drawing.Point(12, 41);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 35);
            this.btnSettings.TabIndex = 1;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // lblShipsDestroyed
            // 
            this.lblShipsDestroyed.AutoSize = true;
            this.lblShipsDestroyed.BackColor = System.Drawing.Color.Transparent;
            this.lblShipsDestroyed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShipsDestroyed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(206)))));
            this.lblShipsDestroyed.Location = new System.Drawing.Point(19, 329);
            this.lblShipsDestroyed.Name = "lblShipsDestroyed";
            this.lblShipsDestroyed.Size = new System.Drawing.Size(64, 20);
            this.lblShipsDestroyed.TabIndex = 4;
            this.lblShipsDestroyed.Text = "lblShips";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(206)))));
            this.label2.Location = new System.Drawing.Point(3, 299);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destroyed :";
            // 
            // lblLivesLeft
            // 
            this.lblLivesLeft.AutoSize = true;
            this.lblLivesLeft.BackColor = System.Drawing.Color.Transparent;
            this.lblLivesLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLivesLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.lblLivesLeft.Location = new System.Drawing.Point(19, 197);
            this.lblLivesLeft.Name = "lblLivesLeft";
            this.lblLivesLeft.Size = new System.Drawing.Size(60, 20);
            this.lblLivesLeft.TabIndex = 2;
            this.lblLivesLeft.Text = "lblLives";
            // 
            // lblShpsDestryd
            // 
            this.lblShpsDestryd.AutoSize = true;
            this.lblShpsDestryd.BackColor = System.Drawing.Color.Transparent;
            this.lblShpsDestryd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShpsDestryd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(206)))));
            this.lblShpsDestryd.Location = new System.Drawing.Point(3, 275);
            this.lblShpsDestryd.Name = "lblShpsDestryd";
            this.lblShpsDestryd.Size = new System.Drawing.Size(49, 20);
            this.lblShpsDestryd.TabIndex = 1;
            this.lblShpsDestryd.Text = "Ships";
            // 
            // lblLivesCntr
            // 
            this.lblLivesCntr.AutoSize = true;
            this.lblLivesCntr.BackColor = System.Drawing.Color.Transparent;
            this.lblLivesCntr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLivesCntr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.lblLivesCntr.Location = new System.Drawing.Point(3, 167);
            this.lblLivesCntr.Name = "lblLivesCntr";
            this.lblLivesCntr.Size = new System.Drawing.Size(85, 20);
            this.lblLivesCntr.TabIndex = 0;
            this.lblLivesCntr.Text = "Lives Left :";
            // 
            // btnPause
            // 
            this.btnPause.FlatAppearance.BorderSize = 0;
            this.btnPause.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(156)))));
            this.btnPause.Location = new System.Drawing.Point(13, 12);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 23);
            this.btnPause.TabIndex = 5;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.BackColor = System.Drawing.Color.Transparent;
            this.btnQuit.FlatAppearance.BorderSize = 0;
            this.btnQuit.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnQuit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnQuit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(206)))));
            this.btnQuit.Location = new System.Drawing.Point(12, 47);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 6;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // pnlGameOptions
            // 
            this.pnlGameOptions.BackColor = System.Drawing.Color.Transparent;
            this.pnlGameOptions.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlGameOptions.BackgroundImage")));
            this.pnlGameOptions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlGameOptions.Controls.Add(this.btnQuit);
            this.pnlGameOptions.Controls.Add(this.btnStart);
            this.pnlGameOptions.Controls.Add(this.btnPause);
            this.pnlGameOptions.Controls.Add(this.lblLivesCntr);
            this.pnlGameOptions.Controls.Add(this.lblShipsDestroyed);
            this.pnlGameOptions.Controls.Add(this.btnSettings);
            this.pnlGameOptions.Controls.Add(this.label2);
            this.pnlGameOptions.Controls.Add(this.lblShpsDestryd);
            this.pnlGameOptions.Controls.Add(this.lblLivesLeft);
            this.pnlGameOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlGameOptions.ForeColor = System.Drawing.Color.Transparent;
            this.pnlGameOptions.Location = new System.Drawing.Point(0, 0);
            this.pnlGameOptions.Name = "pnlGameOptions";
            this.pnlGameOptions.Size = new System.Drawing.Size(100, 562);
            this.pnlGameOptions.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(584, 562);
            this.Controls.Add(this.pnlGameOptions);
            this.Name = "Form1";
            this.Text = "Form1";
            this.pnlGameOptions.ResumeLayout(false);
            this.pnlGameOptions.PerformLayout();
            this.ResumeLayout(false);

      }

      #endregion

      public System.Windows.Forms.Panel pnlGameOptions;
      public System.Windows.Forms.Button btnStart;
      public System.Windows.Forms.Button btnSettings;
      public System.Windows.Forms.Label lblShpsDestryd;
      public System.Windows.Forms.Label lblLivesCntr;
      public System.Windows.Forms.Label lblShipsDestroyed;
      public System.Windows.Forms.Label label2;
      public System.Windows.Forms.Label lblLivesLeft;
      public System.Windows.Forms.Button btnQuit;
      public System.Windows.Forms.Button btnPause;
   }
}

