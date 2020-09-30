namespace MinesweeperGUI
{
    partial class InfoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoView));
            this._minefieldGroupBox = new System.Windows.Forms.GroupBox();
            this._minesValueLabel = new System.Windows.Forms.Label();
            this._minesLabel = new System.Windows.Forms.Label();
            this._heightValueLabel = new System.Windows.Forms.Label();
            this._minefieldTypeNameLabel = new System.Windows.Forms.Label();
            this._heightLabel = new System.Windows.Forms.Label();
            this._lengthValueLabel = new System.Windows.Forms.Label();
            this._lengthLabel = new System.Windows.Forms.Label();
            this._minefieldTypeLabel = new System.Windows.Forms.Label();
            this._gameGroupBox = new System.Windows.Forms.GroupBox();
            this._modalityNameLabel = new System.Windows.Forms.Label();
            this._attemptsValueLabel = new System.Windows.Forms.Label();
            this._attemptsLabel = new System.Windows.Forms.Label();
            this._modalityLabel = new System.Windows.Forms.Label();
            this._minefieldGroupBox.SuspendLayout();
            this._gameGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _minefieldGroupBox
            // 
            this._minefieldGroupBox.Controls.Add(this._minesValueLabel);
            this._minefieldGroupBox.Controls.Add(this._minesLabel);
            this._minefieldGroupBox.Controls.Add(this._heightValueLabel);
            this._minefieldGroupBox.Controls.Add(this._minefieldTypeNameLabel);
            this._minefieldGroupBox.Controls.Add(this._heightLabel);
            this._minefieldGroupBox.Controls.Add(this._lengthValueLabel);
            this._minefieldGroupBox.Controls.Add(this._lengthLabel);
            this._minefieldGroupBox.Controls.Add(this._minefieldTypeLabel);
            this._minefieldGroupBox.Location = new System.Drawing.Point(12, 12);
            this._minefieldGroupBox.Name = "_minefieldGroupBox";
            this._minefieldGroupBox.Size = new System.Drawing.Size(176, 94);
            this._minefieldGroupBox.TabIndex = 0;
            this._minefieldGroupBox.TabStop = false;
            this._minefieldGroupBox.Text = "Campo Minato";
            // 
            // _minesValueLabel
            // 
            this._minesValueLabel.AutoSize = true;
            this._minesValueLabel.Location = new System.Drawing.Point(74, 72);
            this._minesValueLabel.Name = "_minesValueLabel";
            this._minesValueLabel.Size = new System.Drawing.Size(25, 13);
            this._minesValueLabel.TabIndex = 7;
            this._minesValueLabel.Text = "???";
            // 
            // _minesLabel
            // 
            this._minesLabel.AutoSize = true;
            this._minesLabel.Location = new System.Drawing.Point(35, 72);
            this._minesLabel.Name = "_minesLabel";
            this._minesLabel.Size = new System.Drawing.Size(33, 13);
            this._minesLabel.TabIndex = 4;
            this._minesLabel.Text = "Mine:";
            // 
            // _heightValueLabel
            // 
            this._heightValueLabel.AutoSize = true;
            this._heightValueLabel.Location = new System.Drawing.Point(74, 54);
            this._heightValueLabel.Name = "_heightValueLabel";
            this._heightValueLabel.Size = new System.Drawing.Size(25, 13);
            this._heightValueLabel.TabIndex = 6;
            this._heightValueLabel.Text = "???";
            // 
            // _minefieldTypeNameLabel
            // 
            this._minefieldTypeNameLabel.AutoSize = true;
            this._minefieldTypeNameLabel.Location = new System.Drawing.Point(74, 18);
            this._minefieldTypeNameLabel.Name = "_minefieldTypeNameLabel";
            this._minefieldTypeNameLabel.Size = new System.Drawing.Size(25, 13);
            this._minefieldTypeNameLabel.TabIndex = 8;
            this._minefieldTypeNameLabel.Text = "???";
            // 
            // _heightLabel
            // 
            this._heightLabel.AutoSize = true;
            this._heightLabel.Location = new System.Drawing.Point(24, 54);
            this._heightLabel.Name = "_heightLabel";
            this._heightLabel.Size = new System.Drawing.Size(44, 13);
            this._heightLabel.TabIndex = 3;
            this._heightLabel.Text = "Altezza:";
            // 
            // _lengthValueLabel
            // 
            this._lengthValueLabel.AutoSize = true;
            this._lengthValueLabel.Location = new System.Drawing.Point(74, 36);
            this._lengthValueLabel.Name = "_lengthValueLabel";
            this._lengthValueLabel.Size = new System.Drawing.Size(25, 13);
            this._lengthValueLabel.TabIndex = 5;
            this._lengthValueLabel.Text = "???";
            // 
            // _lengthLabel
            // 
            this._lengthLabel.AutoSize = true;
            this._lengthLabel.Location = new System.Drawing.Point(6, 36);
            this._lengthLabel.Name = "_lengthLabel";
            this._lengthLabel.Size = new System.Drawing.Size(62, 13);
            this._lengthLabel.TabIndex = 2;
            this._lengthLabel.Text = "Lunghezza:";
            // 
            // _minefieldTypeLabel
            // 
            this._minefieldTypeLabel.AutoSize = true;
            this._minefieldTypeLabel.Location = new System.Drawing.Point(15, 18);
            this._minefieldTypeLabel.Name = "_minefieldTypeLabel";
            this._minefieldTypeLabel.Size = new System.Drawing.Size(53, 13);
            this._minefieldTypeLabel.TabIndex = 1;
            this._minefieldTypeLabel.Text = "Tipologia:";
            // 
            // _gameGroupBox
            // 
            this._gameGroupBox.Controls.Add(this._modalityNameLabel);
            this._gameGroupBox.Controls.Add(this._attemptsValueLabel);
            this._gameGroupBox.Controls.Add(this._attemptsLabel);
            this._gameGroupBox.Controls.Add(this._modalityLabel);
            this._gameGroupBox.Location = new System.Drawing.Point(12, 112);
            this._gameGroupBox.Name = "_gameGroupBox";
            this._gameGroupBox.Size = new System.Drawing.Size(176, 60);
            this._gameGroupBox.TabIndex = 9;
            this._gameGroupBox.TabStop = false;
            this._gameGroupBox.Text = "Partita";
            // 
            // _modalityNameLabel
            // 
            this._modalityNameLabel.AutoSize = true;
            this._modalityNameLabel.Location = new System.Drawing.Point(74, 18);
            this._modalityNameLabel.Name = "_modalityNameLabel";
            this._modalityNameLabel.Size = new System.Drawing.Size(25, 13);
            this._modalityNameLabel.TabIndex = 12;
            this._modalityNameLabel.Text = "???";
            // 
            // _attemptsValueLabel
            // 
            this._attemptsValueLabel.AutoSize = true;
            this._attemptsValueLabel.Location = new System.Drawing.Point(74, 36);
            this._attemptsValueLabel.Name = "_attemptsValueLabel";
            this._attemptsValueLabel.Size = new System.Drawing.Size(25, 13);
            this._attemptsValueLabel.TabIndex = 11;
            this._attemptsValueLabel.Text = "???";
            // 
            // _attemptsLabel
            // 
            this._attemptsLabel.AutoSize = true;
            this._attemptsLabel.Location = new System.Drawing.Point(17, 36);
            this._attemptsLabel.Name = "_attemptsLabel";
            this._attemptsLabel.Size = new System.Drawing.Size(51, 13);
            this._attemptsLabel.TabIndex = 10;
            this._attemptsLabel.Text = "Tentativi:";
            // 
            // _modalityLabel
            // 
            this._modalityLabel.AutoSize = true;
            this._modalityLabel.Location = new System.Drawing.Point(18, 18);
            this._modalityLabel.Name = "_modalityLabel";
            this._modalityLabel.Size = new System.Drawing.Size(50, 13);
            this._modalityLabel.TabIndex = 9;
            this._modalityLabel.Text = "Modalità:";
            // 
            // InfoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 186);
            this.Controls.Add(this._gameGroupBox);
            this.Controls.Add(this._minefieldGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfoView";
            this.Text = "Informazioni sulla partita";
            this._minefieldGroupBox.ResumeLayout(false);
            this._minefieldGroupBox.PerformLayout();
            this._gameGroupBox.ResumeLayout(false);
            this._gameGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _minefieldGroupBox;
        private System.Windows.Forms.Label _minesLabel;
        private System.Windows.Forms.Label _heightLabel;
        private System.Windows.Forms.Label _lengthLabel;
        private System.Windows.Forms.Label _minefieldTypeLabel;
        private System.Windows.Forms.Label _minefieldTypeNameLabel;
        private System.Windows.Forms.Label _minesValueLabel;
        private System.Windows.Forms.Label _heightValueLabel;
        private System.Windows.Forms.Label _lengthValueLabel;
        private System.Windows.Forms.GroupBox _gameGroupBox;
        private System.Windows.Forms.Label _modalityLabel;
        private System.Windows.Forms.Label _attemptsLabel;
        private System.Windows.Forms.Label _attemptsValueLabel;
        private System.Windows.Forms.Label _modalityNameLabel;
    }
}