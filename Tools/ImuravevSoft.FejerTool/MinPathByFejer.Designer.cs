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
            this.btnClear = new System.Windows.Forms.Button();
            this.tbHasEdge = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMarkers = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelDir = new System.Windows.Forms.Button();
            this.tbLogs = new System.Windows.Forms.TextBox();
            this.cbLogs = new System.Windows.Forms.CheckBox();
            this.cbModMethod = new System.Windows.Forms.CheckBox();
            this.cbAutoRelax = new System.Windows.Forms.CheckBox();
            this.tbEps = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbRelax = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reportPanel = new System.Windows.Forms.Panel();
            this.tbDialog = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.paramPanel.SuspendLayout();
            this.reportPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // paramPanel
            // 
            this.paramPanel.Controls.Add(this.btnClear);
            this.paramPanel.Controls.Add(this.tbHasEdge);
            this.paramPanel.Controls.Add(this.label4);
            this.paramPanel.Controls.Add(this.tbMarkers);
            this.paramPanel.Controls.Add(this.label3);
            this.paramPanel.Controls.Add(this.btnSelDir);
            this.paramPanel.Controls.Add(this.tbLogs);
            this.paramPanel.Controls.Add(this.cbLogs);
            this.paramPanel.Controls.Add(this.cbModMethod);
            this.paramPanel.Controls.Add(this.cbAutoRelax);
            this.paramPanel.Controls.Add(this.tbEps);
            this.paramPanel.Controls.Add(this.label2);
            this.paramPanel.Controls.Add(this.btnStart);
            this.paramPanel.Controls.Add(this.tbRelax);
            this.paramPanel.Controls.Add(this.label1);
            this.paramPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.paramPanel.Location = new System.Drawing.Point(0, 0);
            this.paramPanel.Name = "paramPanel";
            this.paramPanel.Size = new System.Drawing.Size(271, 591);
            this.paramPanel.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(97, 546);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(87, 39);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "Снять метки с ребер";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbHasEdge
            // 
            this.tbHasEdge.Location = new System.Drawing.Point(152, 72);
            this.tbHasEdge.Name = "tbHasEdge";
            this.tbHasEdge.Size = new System.Drawing.Size(113, 20);
            this.tbHasEdge.TabIndex = 13;
            this.tbHasEdge.Text = "1E-3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(63, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Порог ребра";
            // 
            // tbMarkers
            // 
            this.tbMarkers.Location = new System.Drawing.Point(152, 98);
            this.tbMarkers.Name = "tbMarkers";
            this.tbMarkers.Size = new System.Drawing.Size(113, 20);
            this.tbMarkers.TabIndex = 11;
            this.tbMarkers.Text = "start;end";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Маркеры S и T";
            // 
            // btnSelDir
            // 
            this.btnSelDir.Location = new System.Drawing.Point(212, 176);
            this.btnSelDir.Name = "btnSelDir";
            this.btnSelDir.Size = new System.Drawing.Size(56, 28);
            this.btnSelDir.TabIndex = 9;
            this.btnSelDir.Text = "Выбор";
            this.btnSelDir.UseVisualStyleBackColor = true;
            this.btnSelDir.Click += new System.EventHandler(this.btnSelDir_Click);
            // 
            // tbLogs
            // 
            this.tbLogs.Location = new System.Drawing.Point(97, 180);
            this.tbLogs.Name = "tbLogs";
            this.tbLogs.Size = new System.Drawing.Size(113, 20);
            this.tbLogs.TabIndex = 8;
            // 
            // cbLogs
            // 
            this.cbLogs.AutoSize = true;
            this.cbLogs.Location = new System.Drawing.Point(9, 182);
            this.cbLogs.Name = "cbLogs";
            this.cbLogs.Size = new System.Drawing.Size(82, 17);
            this.cbLogs.TabIndex = 7;
            this.cbLogs.Text = "Вести логи";
            this.cbLogs.UseVisualStyleBackColor = true;
            // 
            // cbModMethod
            // 
            this.cbModMethod.AutoSize = true;
            this.cbModMethod.Checked = true;
            this.cbModMethod.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbModMethod.Location = new System.Drawing.Point(9, 159);
            this.cbModMethod.Name = "cbModMethod";
            this.cbModMethod.Size = new System.Drawing.Size(163, 17);
            this.cbModMethod.TabIndex = 6;
            this.cbModMethod.Text = "Модифицированный метод";
            this.cbModMethod.UseVisualStyleBackColor = true;
            // 
            // cbAutoRelax
            // 
            this.cbAutoRelax.AutoSize = true;
            this.cbAutoRelax.Checked = true;
            this.cbAutoRelax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAutoRelax.Location = new System.Drawing.Point(9, 136);
            this.cbAutoRelax.Name = "cbAutoRelax";
            this.cbAutoRelax.Size = new System.Drawing.Size(174, 17);
            this.cbAutoRelax.TabIndex = 5;
            this.cbAutoRelax.Text = "Автовычисление релаксации";
            this.cbAutoRelax.UseVisualStyleBackColor = true;
            // 
            // tbEps
            // 
            this.tbEps.Location = new System.Drawing.Point(152, 46);
            this.tbEps.Name = "tbEps";
            this.tbEps.Size = new System.Drawing.Size(113, 20);
            this.tbEps.TabIndex = 4;
            this.tbEps.Text = "1E-4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Точность";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(190, 546);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 39);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Пуск";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbRelax
            // 
            this.tbRelax.Location = new System.Drawing.Point(152, 21);
            this.tbRelax.Name = "tbRelax";
            this.tbRelax.Size = new System.Drawing.Size(113, 20);
            this.tbRelax.TabIndex = 1;
            this.tbRelax.Text = "1,2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Коэффициент релаксации";
            // 
            // reportPanel
            // 
            this.reportPanel.Controls.Add(this.tbDialog);
            this.reportPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportPanel.Location = new System.Drawing.Point(271, 0);
            this.reportPanel.Name = "reportPanel";
            this.reportPanel.Size = new System.Drawing.Size(701, 591);
            this.reportPanel.TabIndex = 1;
            // 
            // tbDialog
            // 
            this.tbDialog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDialog.Location = new System.Drawing.Point(6, 3);
            this.tbDialog.Multiline = true;
            this.tbDialog.Name = "tbDialog";
            this.tbDialog.Size = new System.Drawing.Size(692, 268);
            this.tbDialog.TabIndex = 0;
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
            this.Size = new System.Drawing.Size(972, 591);
            this.paramPanel.ResumeLayout(false);
            this.paramPanel.PerformLayout();
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
    }
}
