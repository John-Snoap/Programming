namespace ConnectFourGUI
{
   partial class CnnctFourFrm
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
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.blkLbl = new System.Windows.Forms.Label();
            this.redLbl = new System.Windows.Forms.Label();
            this.StartOvrBtn = new System.Windows.Forms.Button();
            this.grpGmeBrd = new System.Windows.Forms.GroupBox();
            this.gmeBrdPnl = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.grpGmeBrd.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Location = new System.Drawing.Point(0, 24);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(544, 24);
            this.menuStrip2.TabIndex = 8;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // blkLbl
            // 
            this.blkLbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.blkLbl.AutoSize = true;
            this.blkLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blkLbl.Location = new System.Drawing.Point(232, 26);
            this.blkLbl.Name = "blkLbl";
            this.blkLbl.Size = new System.Drawing.Size(46, 18);
            this.blkLbl.TabIndex = 4;
            this.blkLbl.Text = "blkLbl";
            // 
            // redLbl
            // 
            this.redLbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.redLbl.AutoSize = true;
            this.redLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.redLbl.ForeColor = System.Drawing.Color.Red;
            this.redLbl.Location = new System.Drawing.Point(232, 26);
            this.redLbl.Name = "redLbl";
            this.redLbl.Size = new System.Drawing.Size(48, 18);
            this.redLbl.TabIndex = 3;
            this.redLbl.Text = "redLbl";
            // 
            // StartOvrBtn
            // 
            this.StartOvrBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StartOvrBtn.Location = new System.Drawing.Point(431, 21);
            this.StartOvrBtn.Name = "StartOvrBtn";
            this.StartOvrBtn.Size = new System.Drawing.Size(75, 32);
            this.StartOvrBtn.TabIndex = 2;
            this.StartOvrBtn.Text = "Start Over";
            this.StartOvrBtn.UseVisualStyleBackColor = true;
            this.StartOvrBtn.Click += new System.EventHandler(this.StartOvrBtn_Click);
            // 
            // grpGmeBrd
            // 
            this.grpGmeBrd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGmeBrd.BackColor = System.Drawing.Color.Transparent;
            this.grpGmeBrd.Controls.Add(this.blkLbl);
            this.grpGmeBrd.Controls.Add(this.gmeBrdPnl);
            this.grpGmeBrd.Controls.Add(this.StartOvrBtn);
            this.grpGmeBrd.Controls.Add(this.redLbl);
            this.grpGmeBrd.Location = new System.Drawing.Point(0, 27);
            this.grpGmeBrd.Name = "grpGmeBrd";
            this.grpGmeBrd.Size = new System.Drawing.Size(544, 472);
            this.grpGmeBrd.TabIndex = 7;
            this.grpGmeBrd.TabStop = false;
            // 
            // gmeBrdPnl
            // 
            this.gmeBrdPnl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gmeBrdPnl.BackColor = System.Drawing.Color.Transparent;
            this.gmeBrdPnl.BackgroundImage = global::ConnectFourGUI.Properties.Resources.connectFourBackground;
            this.gmeBrdPnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gmeBrdPnl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gmeBrdPnl.Location = new System.Drawing.Point(38, 59);
            this.gmeBrdPnl.Name = "gmeBrdPnl";
            this.gmeBrdPnl.Size = new System.Drawing.Size(476, 396);
            this.gmeBrdPnl.TabIndex = 1;
            this.gmeBrdPnl.Paint += new System.Windows.Forms.PaintEventHandler(this.gmeBrdPnl_Paint);
            this.gmeBrdPnl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GmeBrdPnl_MouseDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(544, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenu,
            this.saveAsMenu});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openMenu
            // 
            this.openMenu.Name = "openMenu";
            this.openMenu.Size = new System.Drawing.Size(152, 22);
            this.openMenu.Text = "Open...";
            this.openMenu.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsMenu
            // 
            this.saveAsMenu.Name = "saveAsMenu";
            this.saveAsMenu.Size = new System.Drawing.Size(152, 22);
            this.saveAsMenu.Text = "Save As...";
            this.saveAsMenu.Click += new System.EventHandler(this.saveasToolStripMenuItem_Click);
            // 
            // CnnctFourFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 499);
            this.Controls.Add(this.grpGmeBrd);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.menuStrip1);
            this.Name = "CnnctFourFrm";
            this.Text = "Connect Four";
            this.grpGmeBrd.ResumeLayout(false);
            this.grpGmeBrd.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip menuStrip2;
      private System.Windows.Forms.Label blkLbl;
      private System.Windows.Forms.Label redLbl;
      private System.Windows.Forms.Button StartOvrBtn;
      private System.Windows.Forms.Panel gmeBrdPnl;
      private System.Windows.Forms.GroupBox grpGmeBrd;
      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem openMenu;
      private System.Windows.Forms.ToolStripMenuItem saveAsMenu;

   }
}

