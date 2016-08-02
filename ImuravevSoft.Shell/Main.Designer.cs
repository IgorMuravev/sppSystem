namespace ImuravevSoft.Shell
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTabs1 = new ImuravevSoft.Shell.Control.ToolTabs();
            this.dataViewer1 = new ImuravevSoft.Shell.Control.DataViewer();
            this.toolsPanel1 = new ImuravevSoft.Shell.Control.ToolsPanel();
            this.messageList1 = new ImuravevSoft.Shell.Control.MessageList();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.выходToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1132, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьToolStripMenuItem,
            this.сохранитьToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(49, 20);
            this.toolStripMenuItem1.Text = "Файл";
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("загрузитьToolStripMenuItem.Image")));
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.загрузитьToolStripMenuItem.Text = "Загрузить";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("сохранитьToolStripMenuItem.Image")));
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "IAM files|*.iam";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "iam";
            this.saveFileDialog1.Filter = "IAM files|*.iam";
            // 
            // toolTabs1
            // 
            this.toolTabs1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolTabs1.Location = new System.Drawing.Point(242, 113);
            this.toolTabs1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolTabs1.Name = "toolTabs1";
            this.toolTabs1.Size = new System.Drawing.Size(890, 335);
            this.toolTabs1.TabIndex = 5;
            // 
            // dataViewer1
            // 
            this.dataViewer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataViewer1.Location = new System.Drawing.Point(0, 113);
            this.dataViewer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataViewer1.Name = "dataViewer1";
            this.dataViewer1.Size = new System.Drawing.Size(242, 335);
            this.dataViewer1.TabIndex = 0;
            // 
            // toolsPanel1
            // 
            this.toolsPanel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.toolsPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolsPanel1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolsPanel1.Location = new System.Drawing.Point(0, 24);
            this.toolsPanel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.toolsPanel1.Name = "toolsPanel1";
            this.toolsPanel1.Size = new System.Drawing.Size(1132, 89);
            this.toolsPanel1.TabIndex = 4;
            // 
            // messageList1
            // 
            this.messageList1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.messageList1.Location = new System.Drawing.Point(0, 448);
            this.messageList1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.messageList1.Name = "messageList1";
            this.messageList1.Size = new System.Drawing.Size(1132, 214);
            this.messageList1.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 662);
            this.Controls.Add(this.toolTabs1);
            this.Controls.Add(this.dataViewer1);
            this.Controls.Add(this.toolsPanel1);
            this.Controls.Add(this.messageList1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.Text = "Система построение кратчайшего пути";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Control.DataViewer dataViewer1;
        private Control.MessageList messageList1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Control.ToolsPanel toolsPanel1;
        private Control.ToolTabs toolTabs1;
    }
}

