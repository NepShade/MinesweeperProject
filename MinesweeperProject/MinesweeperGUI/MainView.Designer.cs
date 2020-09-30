namespace MinesweeperGUI
{
    partial class MainView
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this._gameButton = new System.Windows.Forms.Button();
            this._minesCounterLabel = new System.Windows.Forms.Label();
            this._minesweeperMenuStrip = new System.Windows.Forms.MenuStrip();
            this._gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._rulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._gameDurationLabel = new System.Windows.Forms.Label();
            this._gameTimer = new System.Windows.Forms.Timer(this.components);
            this._minesweeperMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gameButton
            // 
            this._gameButton.Location = new System.Drawing.Point(67, 30);
            this._gameButton.Name = "_gameButton";
            this._gameButton.Size = new System.Drawing.Size(35, 35);
            this._gameButton.TabIndex = 0;
            this._gameButton.UseVisualStyleBackColor = true;
            // 
            // _minesCounterLabel
            // 
            this._minesCounterLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._minesCounterLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._minesCounterLabel.Location = new System.Drawing.Point(10, 35);
            this._minesCounterLabel.Name = "_minesCounterLabel";
            this._minesCounterLabel.Size = new System.Drawing.Size(50, 25);
            this._minesCounterLabel.TabIndex = 1;
            this._minesCounterLabel.Text = "????";
            this._minesCounterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _minesweeperMenuStrip
            // 
            this._minesweeperMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._gameToolStripMenuItem,
            this._infoToolStripMenuItem});
            this._minesweeperMenuStrip.Location = new System.Drawing.Point(0, 0);
            this._minesweeperMenuStrip.Name = "_minesweeperMenuStrip";
            this._minesweeperMenuStrip.Size = new System.Drawing.Size(180, 24);
            this._minesweeperMenuStrip.TabIndex = 2;
            // 
            // _gameToolStripMenuItem
            // 
            this._gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newToolStripMenuItem,
            this._settingsToolStripMenuItem,
            this._rulesToolStripMenuItem,
            this._exitToolStripMenuItem});
            this._gameToolStripMenuItem.Name = "_gameToolStripMenuItem";
            this._gameToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this._gameToolStripMenuItem.Text = "Partita";
            // 
            // _newToolStripMenuItem
            // 
            this._newToolStripMenuItem.Name = "_newToolStripMenuItem";
            this._newToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this._newToolStripMenuItem.Text = "Nuova";
            // 
            // _settingsToolStripMenuItem
            // 
            this._settingsToolStripMenuItem.Name = "_settingsToolStripMenuItem";
            this._settingsToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this._settingsToolStripMenuItem.Text = "Configura";
            // 
            // _rulesToolStripMenuItem
            // 
            this._rulesToolStripMenuItem.Name = "_rulesToolStripMenuItem";
            this._rulesToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this._rulesToolStripMenuItem.Text = "Regole";
            // 
            // _exitToolStripMenuItem
            // 
            this._exitToolStripMenuItem.Name = "_exitToolStripMenuItem";
            this._exitToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this._exitToolStripMenuItem.Text = "Esci";
            // 
            // _infoToolStripMenuItem
            // 
            this._infoToolStripMenuItem.Name = "_infoToolStripMenuItem";
            this._infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this._infoToolStripMenuItem.Text = "Info";
            // 
            // _gameDurationLabel
            // 
            this._gameDurationLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gameDurationLabel.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._gameDurationLabel.Location = new System.Drawing.Point(110, 35);
            this._gameDurationLabel.Name = "_gameDurationLabel";
            this._gameDurationLabel.Size = new System.Drawing.Size(60, 25);
            this._gameDurationLabel.TabIndex = 3;
            this._gameDurationLabel.Text = "??:??";
            this._gameDurationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _gameTimer
            // 
            this._gameTimer.Interval = 1000;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 240);
            this.Controls.Add(this._gameDurationLabel);
            this.Controls.Add(this._minesCounterLabel);
            this.Controls.Add(this._gameButton);
            this.Controls.Add(this._minesweeperMenuStrip);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._minesweeperMenuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainView";
            this.Text = "Campo Minato";
            this._minesweeperMenuStrip.ResumeLayout(false);
            this._minesweeperMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _gameButton;
        private System.Windows.Forms.Label _minesCounterLabel;
        private System.Windows.Forms.MenuStrip _minesweeperMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _rulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _exitToolStripMenuItem;
        private System.Windows.Forms.Label _gameDurationLabel;
        private System.Windows.Forms.Timer _gameTimer;
        private System.Windows.Forms.ToolStripMenuItem _infoToolStripMenuItem;
    }
}

