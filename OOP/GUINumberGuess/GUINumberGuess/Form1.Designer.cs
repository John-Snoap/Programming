namespace GUINumberGuess
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnEnterGuess = new System.Windows.Forms.Button();
            this.btnPlayAgain = new System.Windows.Forms.Button();
            this.lblFirstHint = new System.Windows.Forms.Label();
            this.lblSecondHint = new System.Windows.Forms.Label();
            this.mskTxtUserGuess = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Your Guess:";
            // 
            // btnEnterGuess
            // 
            this.btnEnterGuess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnterGuess.Location = new System.Drawing.Point(145, 225);
            this.btnEnterGuess.Name = "btnEnterGuess";
            this.btnEnterGuess.Size = new System.Drawing.Size(118, 34);
            this.btnEnterGuess.TabIndex = 4;
            this.btnEnterGuess.Text = "Enter Guess";
            this.btnEnterGuess.UseVisualStyleBackColor = true;
            this.btnEnterGuess.Click += new System.EventHandler(this.btnEnterGuess_Click);
            // 
            // btnPlayAgain
            // 
            this.btnPlayAgain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayAgain.Location = new System.Drawing.Point(30, 225);
            this.btnPlayAgain.Name = "btnPlayAgain";
            this.btnPlayAgain.Size = new System.Drawing.Size(95, 34);
            this.btnPlayAgain.TabIndex = 5;
            this.btnPlayAgain.Text = "Play Again";
            this.btnPlayAgain.UseVisualStyleBackColor = true;
            this.btnPlayAgain.Click += new System.EventHandler(this.btnPlayAgain_Click);
            // 
            // lblFirstHint
            // 
            this.lblFirstHint.AutoSize = true;
            this.lblFirstHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstHint.Location = new System.Drawing.Point(26, 19);
            this.lblFirstHint.Name = "lblFirstHint";
            this.lblFirstHint.Size = new System.Drawing.Size(51, 20);
            this.lblFirstHint.TabIndex = 6;
            this.lblFirstHint.Text = "label1";
            // 
            // lblSecondHint
            // 
            this.lblSecondHint.AutoSize = true;
            this.lblSecondHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecondHint.Location = new System.Drawing.Point(26, 78);
            this.lblSecondHint.Name = "lblSecondHint";
            this.lblSecondHint.Size = new System.Drawing.Size(51, 20);
            this.lblSecondHint.TabIndex = 7;
            this.lblSecondHint.Text = "label2";
            // 
            // mskTxtUserGuess
            // 
            this.mskTxtUserGuess.AllowPromptAsInput = false;
            this.mskTxtUserGuess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskTxtUserGuess.Location = new System.Drawing.Point(163, 147);
            this.mskTxtUserGuess.Mask = "00000";
            this.mskTxtUserGuess.Name = "mskTxtUserGuess";
            this.mskTxtUserGuess.PromptChar = ' ';
            this.mskTxtUserGuess.Size = new System.Drawing.Size(100, 26);
            this.mskTxtUserGuess.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 313);
            this.Controls.Add(this.mskTxtUserGuess);
            this.Controls.Add(this.lblSecondHint);
            this.Controls.Add(this.lblFirstHint);
            this.Controls.Add(this.btnPlayAgain);
            this.Controls.Add(this.btnEnterGuess);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "The Number Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEnterGuess;
        private System.Windows.Forms.Button btnPlayAgain;
        private System.Windows.Forms.Label lblFirstHint;
        private System.Windows.Forms.Label lblSecondHint;
        private System.Windows.Forms.MaskedTextBox mskTxtUserGuess;
    }
}

