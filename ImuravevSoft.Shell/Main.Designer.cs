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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTabs1 = new ImuravevSoft.Shell.Control.ToolTabs();
            this.dataViewer1 = new ImuravevSoft.Shell.Control.DataViewer();
            this.messageList1 = new ImuravevSoft.Shell.Control.MessageList();
            this.ribbon1 = new ImuravevSoft.Shell.Control.Ribbon();
            this.SuspendLayout();
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
            this.toolTabs1.IndexActiveTool = -1;
            this.toolTabs1.Location = new System.Drawing.Point(242, 138);
            this.toolTabs1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolTabs1.Name = "toolTabs1";
            this.toolTabs1.Size = new System.Drawing.Size(890, 310);
            this.toolTabs1.TabIndex = 5;
            // 
            // dataViewer1
            // 
            this.dataViewer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dataViewer1.Location = new System.Drawing.Point(0, 138);
            this.dataViewer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataViewer1.Name = "dataViewer1";
            this.dataViewer1.Size = new System.Drawing.Size(242, 310);
            this.dataViewer1.TabIndex = 0;
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
            // ribbon1
            // 
            this.ribbon1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbon1.Location = new System.Drawing.Point(0, 0);
            this.ribbon1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ribbon1.Name = "ribbon1";
            this.ribbon1.Size = new System.Drawing.Size(1132, 138);
            this.ribbon1.TabIndex = 6;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 662);
            this.Controls.Add(this.toolTabs1);
            this.Controls.Add(this.dataViewer1);
            this.Controls.Add(this.messageList1);
            this.Controls.Add(this.ribbon1);
            this.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Main";
            this.Text = "Система построение кратчайшего пути";
            this.ResumeLayout(false);

        }

        #endregion

        private Control.DataViewer dataViewer1;
        private Control.MessageList messageList1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Control.ToolTabs toolTabs1;
        private Control.Ribbon ribbon1;
    }
}

