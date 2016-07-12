namespace TestTask
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnRemoveEvent = new System.Windows.Forms.Button();
            this.bntAddEvent = new System.Windows.Forms.Button();
            this.cbLocalization = new System.Windows.Forms.ComboBox();
            this.btnStopRun = new System.Windows.Forms.Button();
            this.btnRectangle = new System.Windows.Forms.Button();
            this.btnCircle = new System.Windows.Forms.Button();
            this.btnTriangle = new System.Windows.Forms.Button();
            this.listViewFigures = new System.Windows.Forms.ListView();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkInterfacesColizionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkMyListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            resources.ApplyResources(this.pnlButtons, "pnlButtons");
            this.pnlButtons.Controls.Add(this.btnRemoveEvent);
            this.pnlButtons.Controls.Add(this.bntAddEvent);
            this.pnlButtons.Controls.Add(this.cbLocalization);
            this.pnlButtons.Controls.Add(this.btnStopRun);
            this.pnlButtons.Controls.Add(this.btnRectangle);
            this.pnlButtons.Controls.Add(this.btnCircle);
            this.pnlButtons.Controls.Add(this.btnTriangle);
            this.pnlButtons.Name = "pnlButtons";
            // 
            // btnRemoveEvent
            // 
            resources.ApplyResources(this.btnRemoveEvent, "btnRemoveEvent");
            this.btnRemoveEvent.Name = "btnRemoveEvent";
            this.btnRemoveEvent.UseVisualStyleBackColor = true;
            this.btnRemoveEvent.Click += new System.EventHandler(this.btnRemoveEvent_Click);
            // 
            // bntAddEvent
            // 
            resources.ApplyResources(this.bntAddEvent, "bntAddEvent");
            this.bntAddEvent.Name = "bntAddEvent";
            this.bntAddEvent.UseVisualStyleBackColor = true;
            this.bntAddEvent.Click += new System.EventHandler(this.bntAddEvent_Click);
            // 
            // cbLocalization
            // 
            resources.ApplyResources(this.cbLocalization, "cbLocalization");
            this.cbLocalization.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLocalization.FormattingEnabled = true;
            this.cbLocalization.Items.AddRange(new object[] {
            resources.GetString("cbLocalization.Items"),
            resources.GetString("cbLocalization.Items1")});
            this.cbLocalization.Name = "cbLocalization";
            this.cbLocalization.SelectedIndexChanged += new System.EventHandler(this.cbLocalization_SelectedIndexChanged);
            // 
            // btnStopRun
            // 
            resources.ApplyResources(this.btnStopRun, "btnStopRun");
            this.btnStopRun.Name = "btnStopRun";
            this.btnStopRun.UseVisualStyleBackColor = true;
            this.btnStopRun.Click += new System.EventHandler(this.btnStopRun_Click);
            // 
            // btnRectangle
            // 
            resources.ApplyResources(this.btnRectangle, "btnRectangle");
            this.btnRectangle.Name = "btnRectangle";
            this.btnRectangle.UseVisualStyleBackColor = true;
            this.btnRectangle.Click += new System.EventHandler(this.btnRectangle_Click);
            // 
            // btnCircle
            // 
            resources.ApplyResources(this.btnCircle, "btnCircle");
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // btnTriangle
            // 
            resources.ApplyResources(this.btnTriangle, "btnTriangle");
            this.btnTriangle.Name = "btnTriangle";
            this.btnTriangle.UseVisualStyleBackColor = true;
            this.btnTriangle.Click += new System.EventHandler(this.btnTriangle_Click);
            // 
            // listViewFigures
            // 
            resources.ApplyResources(this.listViewFigures, "listViewFigures");
            this.listViewFigures.Name = "listViewFigures";
            this.listViewFigures.UseCompatibleStateImageBehavior = false;
            this.listViewFigures.View = System.Windows.Forms.View.SmallIcon;
            this.listViewFigures.Click += new System.EventHandler(this.listViewFigures_Click);
            // 
            // pbMain
            // 
            resources.ApplyResources(this.pbMain, "pbMain");
            this.pbMain.BackColor = System.Drawing.SystemColors.Window;
            this.pbMain.Name = "pbMain";
            this.pbMain.TabStop = false;
            this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 14;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // menuStrip
            // 
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.checkInterfacesColizionToolStripMenuItem,
            this.checkMyListToolStripMenuItem});
            this.menuStrip.Name = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.tsSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // openToolStripMenuItem
            // 
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // tsSeparator
            // 
            resources.ApplyResources(this.tsSeparator, "tsSeparator");
            this.tsSeparator.Name = "tsSeparator";
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // checkInterfacesColizionToolStripMenuItem
            // 
            resources.ApplyResources(this.checkInterfacesColizionToolStripMenuItem, "checkInterfacesColizionToolStripMenuItem");
            this.checkInterfacesColizionToolStripMenuItem.Name = "checkInterfacesColizionToolStripMenuItem";
            this.checkInterfacesColizionToolStripMenuItem.Click += new System.EventHandler(this.checkInterfaceColizionToolStripMenuItem_Click);
            // 
            // checkMyListToolStripMenuItem
            // 
            resources.ApplyResources(this.checkMyListToolStripMenuItem, "checkMyListToolStripMenuItem");
            this.checkMyListToolStripMenuItem.Name = "checkMyListToolStripMenuItem";
            this.checkMyListToolStripMenuItem.Click += new System.EventHandler(this.checkMyListToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.listViewFigures);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Button btnStopRun;
        private System.Windows.Forms.Button btnRectangle;
        private System.Windows.Forms.Button btnCircle;
        private System.Windows.Forms.Button btnTriangle;
        public System.Windows.Forms.ListView listViewFigures;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator tsSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbLocalization;
        private System.Windows.Forms.ToolStripMenuItem checkInterfacesColizionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkMyListToolStripMenuItem;
        private System.Windows.Forms.Button btnRemoveEvent;
        private System.Windows.Forms.Button bntAddEvent;
    }
}

