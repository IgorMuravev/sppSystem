namespace ImuravevSoft.FejerTool
{
    partial class MinPathByFejer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.paramPanel = new System.Windows.Forms.Panel();
            this.cbUseScript = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSelDir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbHasEdge = new System.Windows.Forms.TextBox();
            this.tbRelax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbMarkers = new System.Windows.Forms.TextBox();
            this.tbEps = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAutoRelax = new System.Windows.Forms.CheckBox();
            this.cbModMethod = new System.Windows.Forms.CheckBox();
            this.tbLogs = new System.Windows.Forms.TextBox();
            this.cbLogs = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.reportPanel = new System.Windows.Forms.Panel();
            this.tbDialog = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbScript = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.paramPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.reportPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // paramPanel
            // 
            this.paramPanel.Controls.Add(this.cbUseScript);
            this.paramPanel.Controls.Add(this.panel1);
            this.paramPanel.Controls.Add(this.btnClear);
            this.paramPanel.Controls.Add(this.btnStart);
            this.paramPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.paramPanel.Location = new System.Drawing.Point(0, 0);
            this.paramPanel.Name = "paramPanel";
            this.paramPanel.Size = new System.Drawing.Size(271, 637);
            this.paramPanel.TabIndex = 0;
            // 
            // cbUseScript
            // 
            this.cbUseScript.AutoSize = true;
            this.cbUseScript.Location = new System.Drawing.Point(9, 4);
            this.cbUseScript.Name = "cbUseScript";
            this.cbUseScript.Size = new System.Drawing.Size(137, 17);
            this.cbUseScript.TabIndex = 16;
            this.cbUseScript.Text = "Использовать скрипт";
            this.cbUseScript.UseVisualStyleBackColor = true;
            this.cbUseScript.CheckedChanged += new System.EventHandler(this.cbUseScript_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSelDir);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbHasEdge);
            this.panel1.Controls.Add(this.tbRelax);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbMarkers);
            this.panel1.Controls.Add(this.tbEps);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cbAutoRelax);
            this.panel1.Controls.Add(this.cbModMethod);
            this.panel1.Controls.Add(this.tbLogs);
            this.panel1.Controls.Add(this.cbLogs);
            this.panel1.Location = new System.Drawing.Point(3, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 213);
            this.panel1.TabIndex = 15;
            // 
            // btnSelDir
            // 
            this.btnSelDir.Location = new System.Drawing.Point(209, 171);
            this.btnSelDir.Name = "btnSelDir";
            this.btnSelDir.Size = new System.Drawing.Size(56, 28);
            this.btnSelDir.TabIndex = 9;
            this.btnSelDir.Text = "Выбор";
            this.btnSelDir.UseVisualStyleBackColor = true;
            this.btnSelDir.Click += new System.EventHandler(this.btnSelDir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Коэффициент релаксации";
            // 
            // tbHasEdge
            // 
            this.tbHasEdge.Location = new System.Drawing.Point(149, 67);
            this.tbHasEdge.Name = "tbHasEdge";
            this.tbHasEdge.Size = new System.Drawing.Size(113, 20);
            this.tbHasEdge.TabIndex = 13;
            this.tbHasEdge.Text = "1E-3";
            // 
            // tbRelax
            // 
            this.tbRelax.Location = new System.Drawing.Point(149, 16);
            this.tbRelax.Name = "tbRelax";
            this.tbRelax.Size = new System.Drawing.Size(113, 20);
            this.tbRelax.TabIndex = 1;
            this.tbRelax.Text = "1,2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Порог ребра";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Точность";
            // 
            // tbMarkers
            // 
            this.tbMarkers.Location = new System.Drawing.Point(149, 93);
            this.tbMarkers.Name = "tbMarkers";
            this.tbMarkers.Size = new System.Drawing.Size(113, 20);
            this.tbMarkers.TabIndex = 11;
            this.tbMarkers.Text = "start;end";
            // 
            // tbEps
            // 
            this.tbEps.Location = new System.Drawing.Point(149, 41);
            this.tbEps.Name = "tbEps";
            this.tbEps.Size = new System.Drawing.Size(113, 20);
            this.tbEps.TabIndex = 4;
            this.tbEps.Text = "1E-4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Маркеры S и T";
            // 
            // cbAutoRelax
            // 
            this.cbAutoRelax.AutoSize = true;
            this.cbAutoRelax.Checked = true;
            this.cbAutoRelax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoRelax.Location = new System.Drawing.Point(6, 131);
            this.cbAutoRelax.Name = "cbAutoRelax";
            this.cbAutoRelax.Size = new System.Drawing.Size(174, 17);
            this.cbAutoRelax.TabIndex = 5;
            this.cbAutoRelax.Text = "Автовычисление релаксации";
            this.cbAutoRelax.UseVisualStyleBackColor = true;
            // 
            // cbModMethod
            // 
            this.cbModMethod.AutoSize = true;
            this.cbModMethod.Checked = true;
            this.cbModMethod.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbModMethod.Location = new System.Drawing.Point(6, 154);
            this.cbModMethod.Name = "cbModMethod";
            this.cbModMethod.Size = new System.Drawing.Size(163, 17);
            this.cbModMethod.TabIndex = 6;
            this.cbModMethod.Text = "Модифицированный метод";
            this.cbModMethod.UseVisualStyleBackColor = true;
            // 
            // tbLogs
            // 
            this.tbLogs.Location = new System.Drawing.Point(94, 175);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.Size = new System.Drawing.Size(113, 20);
            this.tbLogs.TabIndex = 8;
            // 
            // cbLogs
            // 
            this.cbLogs.AutoSize = true;
            this.cbLogs.Location = new System.Drawing.Point(6, 177);
            this.cbLogs.Name = "cbLogs";
            this.cbLogs.Size = new System.Drawing.Size(82, 17);
            this.cbLogs.TabIndex = 7;
            this.cbLogs.Text = "Вести логи";
            this.cbLogs.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(97, 592);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 39);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "Снять метки с ребер";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(190, 592);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 39);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Пуск";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // reportPanel
            // 
            this.reportPanel.Controls.Add(this.tbDialog);
            this.reportPanel.Controls.Add(this.label6);
            this.reportPanel.Controls.Add(this.label5);
            this.reportPanel.Controls.Add(this.tbScript);
            this.reportPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportPanel.Location = new System.Drawing.Point(271, 0);
            this.reportPanel.Name = "reportPanel";
            this.reportPanel.Size = new System.Drawing.Size(709, 637);
            this.reportPanel.TabIndex = 1;
            // 
            // tbDialog
            // 
            this.tbDialog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDialog.Location = new System.Drawing.Point(6, 391);
            this.tbDialog.Multiline = true;
            this.tbDialog.Name = "tbDialog";
            this.tbDialog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDialog.Size = new System.Drawing.Size(700, 240);
            this.tbDialog.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 375);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Вывод";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Скрипт моделирования";
            // 
            // tbScript
            // 
            this.tbScript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbScript.Location = new System.Drawing.Point(6, 26);
            this.tbScript.Multiline = true;
            this.tbScript.Name = "tbScript";
            this.tbScript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbScript.Size = new System.Drawing.Size(700, 346);
            this.tbScript.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MinPathByFejer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportPanel);
            this.Controls.Add(this.paramPanel);
            this.Name = "MinPathByFejer";
            this.Size = new System.Drawing.Size(980, 637);
            this.paramPanel.ResumeLayout(false);
            this.paramPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.reportPanel.ResumeLayout(false);
            this.reportPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel paramPanel;
        private System.Windows.Forms.CheckBox cbModMethod;
        private System.Windows.Forms.CheckBox cbAutoRelax;
        private System.Windows.Forms.TextBox tbEps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbRelax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel reportPanel;
        private System.Windows.Forms.Button btnSelDir;
        private System.Windows.Forms.TextBox tbLogs;
        private System.Windows.Forms.CheckBox cbLogs;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox tbMarkers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox tbHasEdge;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbScript;
        private System.Windows.Forms.CheckBox cbUseScript;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
    }
}
