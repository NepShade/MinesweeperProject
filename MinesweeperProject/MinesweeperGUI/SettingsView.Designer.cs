namespace MinesweeperGUI
{
    partial class SettingsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsView));
            this._modalityGroupBox = new System.Windows.Forms.GroupBox();
            this._modalityComboBox = new System.Windows.Forms.ComboBox();
            this._modalityInfoLabel = new System.Windows.Forms.Label();
            this._parametersGroupBox = new System.Windows.Forms.GroupBox();
            this._difficultyComboBox = new System.Windows.Forms.ComboBox();
            this._minMaxMinesLabel = new System.Windows.Forms.Label();
            this._minesLabel = new System.Windows.Forms.Label();
            this._minMaxHeightLabel = new System.Windows.Forms.Label();
            this._minesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._minMaxLengthLabel = new System.Windows.Forms.Label();
            this._heightLabel = new System.Windows.Forms.Label();
            this._lengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._lengthLabel = new System.Windows.Forms.Label();
            this._heightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this._cancelButton = new System.Windows.Forms.Button();
            this._okButton = new System.Windows.Forms.Button();
            this._minefieldGroupBox = new System.Windows.Forms.GroupBox();
            this._minefieldComboBox = new System.Windows.Forms.ComboBox();
            this._minefieldInfoLabel = new System.Windows.Forms.Label();
            this._modalityGroupBox.SuspendLayout();
            this._parametersGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._minesNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._lengthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._heightNumericUpDown)).BeginInit();
            this._minefieldGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _modalityGroupBox
            // 
            this._modalityGroupBox.Controls.Add(this._modalityComboBox);
            this._modalityGroupBox.Controls.Add(this._modalityInfoLabel);
            this._modalityGroupBox.Location = new System.Drawing.Point(12, 83);
            this._modalityGroupBox.Name = "_modalityGroupBox";
            this._modalityGroupBox.Size = new System.Drawing.Size(235, 65);
            this._modalityGroupBox.TabIndex = 1;
            this._modalityGroupBox.TabStop = false;
            this._modalityGroupBox.Text = "Modalità di Gioco";
            // 
            // _modalityComboBox
            // 
            this._modalityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._modalityComboBox.FormattingEnabled = true;
            this._modalityComboBox.Location = new System.Drawing.Point(9, 17);
            this._modalityComboBox.Name = "_modalityComboBox";
            this._modalityComboBox.Size = new System.Drawing.Size(217, 21);
            this._modalityComboBox.TabIndex = 7;
            // 
            // _modalityInfoLabel
            // 
            this._modalityInfoLabel.AutoSize = true;
            this._modalityInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._modalityInfoLabel.Location = new System.Drawing.Point(6, 41);
            this._modalityInfoLabel.Name = "_modalityInfoLabel";
            this._modalityInfoLabel.Size = new System.Drawing.Size(200, 13);
            this._modalityInfoLabel.TabIndex = 8;
            this._modalityInfoLabel.Text = "Informazioni sulla modalità di gioco scelta";
            // 
            // _parametersGroupBox
            // 
            this._parametersGroupBox.Controls.Add(this._difficultyComboBox);
            this._parametersGroupBox.Controls.Add(this._minMaxMinesLabel);
            this._parametersGroupBox.Controls.Add(this._minesLabel);
            this._parametersGroupBox.Controls.Add(this._minMaxHeightLabel);
            this._parametersGroupBox.Controls.Add(this._minesNumericUpDown);
            this._parametersGroupBox.Controls.Add(this._minMaxLengthLabel);
            this._parametersGroupBox.Controls.Add(this._heightLabel);
            this._parametersGroupBox.Controls.Add(this._lengthNumericUpDown);
            this._parametersGroupBox.Controls.Add(this._lengthLabel);
            this._parametersGroupBox.Controls.Add(this._heightNumericUpDown);
            this._parametersGroupBox.Location = new System.Drawing.Point(12, 154);
            this._parametersGroupBox.Name = "_parametersGroupBox";
            this._parametersGroupBox.Size = new System.Drawing.Size(235, 128);
            this._parametersGroupBox.TabIndex = 2;
            this._parametersGroupBox.TabStop = false;
            this._parametersGroupBox.Text = "Difficoltà di Gioco";
            // 
            // _difficultyComboBox
            // 
            this._difficultyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._difficultyComboBox.FormattingEnabled = true;
            this._difficultyComboBox.Location = new System.Drawing.Point(9, 19);
            this._difficultyComboBox.Name = "_difficultyComboBox";
            this._difficultyComboBox.Size = new System.Drawing.Size(217, 21);
            this._difficultyComboBox.TabIndex = 9;
            // 
            // _minMaxMinesLabel
            // 
            this._minMaxMinesLabel.AutoSize = true;
            this._minMaxMinesLabel.Location = new System.Drawing.Point(120, 100);
            this._minMaxMinesLabel.Name = "_minMaxMinesLabel";
            this._minMaxMinesLabel.Size = new System.Drawing.Size(107, 13);
            this._minMaxMinesLabel.TabIndex = 18;
            this._minMaxMinesLabel.Text = "[ Min: ?? - Max: ??? ]";
            // 
            // _minesLabel
            // 
            this._minesLabel.AutoSize = true;
            this._minesLabel.Location = new System.Drawing.Point(55, 100);
            this._minesLabel.Name = "_minesLabel";
            this._minesLabel.Size = new System.Drawing.Size(30, 13);
            this._minesLabel.TabIndex = 15;
            this._minesLabel.Text = "Mine";
            // 
            // _minMaxHeightLabel
            // 
            this._minMaxHeightLabel.AutoSize = true;
            this._minMaxHeightLabel.Location = new System.Drawing.Point(120, 74);
            this._minMaxHeightLabel.Name = "_minMaxHeightLabel";
            this._minMaxHeightLabel.Size = new System.Drawing.Size(107, 13);
            this._minMaxHeightLabel.TabIndex = 17;
            this._minMaxHeightLabel.Text = "[ Min: ?? - Max: ??? ]";
            // 
            // _minesNumericUpDown
            // 
            this._minesNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._minesNumericUpDown.Location = new System.Drawing.Point(9, 98);
            this._minesNumericUpDown.Name = "_minesNumericUpDown";
            this._minesNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this._minesNumericUpDown.TabIndex = 12;
            // 
            // _minMaxLengthLabel
            // 
            this._minMaxLengthLabel.AutoSize = true;
            this._minMaxLengthLabel.Location = new System.Drawing.Point(120, 48);
            this._minMaxLengthLabel.Name = "_minMaxLengthLabel";
            this._minMaxLengthLabel.Size = new System.Drawing.Size(107, 13);
            this._minMaxLengthLabel.TabIndex = 16;
            this._minMaxLengthLabel.Text = "[ Min: ?? - Max: ??? ]";
            // 
            // _heightLabel
            // 
            this._heightLabel.AutoSize = true;
            this._heightLabel.Location = new System.Drawing.Point(55, 74);
            this._heightLabel.Name = "_heightLabel";
            this._heightLabel.Size = new System.Drawing.Size(41, 13);
            this._heightLabel.TabIndex = 14;
            this._heightLabel.Text = "Altezza";
            // 
            // _lengthNumericUpDown
            // 
            this._lengthNumericUpDown.Location = new System.Drawing.Point(9, 46);
            this._lengthNumericUpDown.Name = "_lengthNumericUpDown";
            this._lengthNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this._lengthNumericUpDown.TabIndex = 10;
            // 
            // _lengthLabel
            // 
            this._lengthLabel.AutoSize = true;
            this._lengthLabel.Location = new System.Drawing.Point(55, 48);
            this._lengthLabel.Name = "_lengthLabel";
            this._lengthLabel.Size = new System.Drawing.Size(59, 13);
            this._lengthLabel.TabIndex = 13;
            this._lengthLabel.Text = "Lunghezza";
            // 
            // _heightNumericUpDown
            // 
            this._heightNumericUpDown.Location = new System.Drawing.Point(9, 72);
            this._heightNumericUpDown.Name = "_heightNumericUpDown";
            this._heightNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this._heightNumericUpDown.TabIndex = 11;
            // 
            // _cancelButton
            // 
            this._cancelButton.Location = new System.Drawing.Point(27, 291);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 3;
            this._cancelButton.Text = "Annulla";
            this._cancelButton.UseVisualStyleBackColor = true;
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(152, 291);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 4;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            // 
            // _minefieldGroupBox
            // 
            this._minefieldGroupBox.Controls.Add(this._minefieldComboBox);
            this._minefieldGroupBox.Controls.Add(this._minefieldInfoLabel);
            this._minefieldGroupBox.Location = new System.Drawing.Point(12, 12);
            this._minefieldGroupBox.Name = "_minefieldGroupBox";
            this._minefieldGroupBox.Size = new System.Drawing.Size(235, 65);
            this._minefieldGroupBox.TabIndex = 0;
            this._minefieldGroupBox.TabStop = false;
            this._minefieldGroupBox.Text = "Tipologia di Campo Minato";
            // 
            // _minefieldComboBox
            // 
            this._minefieldComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._minefieldComboBox.FormattingEnabled = true;
            this._minefieldComboBox.Location = new System.Drawing.Point(9, 17);
            this._minefieldComboBox.Name = "_minefieldComboBox";
            this._minefieldComboBox.Size = new System.Drawing.Size(217, 21);
            this._minefieldComboBox.TabIndex = 5;
            // 
            // _minefieldInfoLabel
            // 
            this._minefieldInfoLabel.AutoSize = true;
            this._minefieldInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._minefieldInfoLabel.Location = new System.Drawing.Point(6, 41);
            this._minefieldInfoLabel.Name = "_minefieldInfoLabel";
            this._minefieldInfoLabel.Size = new System.Drawing.Size(206, 13);
            this._minefieldInfoLabel.TabIndex = 6;
            this._minefieldInfoLabel.Text = "Informazioni sulla tipologia di campo scelta";
            // 
            // SettingsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 326);
            this.Controls.Add(this._minefieldGroupBox);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._parametersGroupBox);
            this.Controls.Add(this._modalityGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsView";
            this.Text = "Impostazioni Partita";
            this._modalityGroupBox.ResumeLayout(false);
            this._modalityGroupBox.PerformLayout();
            this._parametersGroupBox.ResumeLayout(false);
            this._parametersGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._minesNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._lengthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._heightNumericUpDown)).EndInit();
            this._minefieldGroupBox.ResumeLayout(false);
            this._minefieldGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _modalityGroupBox;
        private System.Windows.Forms.GroupBox _parametersGroupBox;
        private System.Windows.Forms.Label _minesLabel;
        private System.Windows.Forms.NumericUpDown _minesNumericUpDown;
        private System.Windows.Forms.Label _heightLabel;
        private System.Windows.Forms.NumericUpDown _lengthNumericUpDown;
        private System.Windows.Forms.Label _lengthLabel;
        private System.Windows.Forms.NumericUpDown _heightNumericUpDown;
        private System.Windows.Forms.Label _minMaxLengthLabel;
        private System.Windows.Forms.Label _minMaxMinesLabel;
        private System.Windows.Forms.Label _minMaxHeightLabel;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Label _modalityInfoLabel;
        private System.Windows.Forms.GroupBox _minefieldGroupBox;
        private System.Windows.Forms.Label _minefieldInfoLabel;
        private System.Windows.Forms.ComboBox _modalityComboBox;
        private System.Windows.Forms.ComboBox _difficultyComboBox;
        private System.Windows.Forms.ComboBox _minefieldComboBox;
    }
}