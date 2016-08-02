namespace ImuravevSoft.Shell.Control
{
    partial class MessageList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageList));
            this.listView1 = new System.Windows.Forms.ListView();
            this.msgTypeCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.msgDatetimeCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.msgTextCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.msgTypeCol,
            this.msgDatetimeCol,
            this.msgTextCol});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(863, 150);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Resize += new System.EventHandler(this.listView1_Resize);
            // 
            // msgTypeCol
            // 
            this.msgTypeCol.Text = "Тип";
            this.msgTypeCol.Width = 50;
            // 
            // msgDatetimeCol
            // 
            this.msgDatetimeCol.Text = "Время";
            this.msgDatetimeCol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.msgDatetimeCol.Width = 150;
            // 
            // msgTextCol
            // 
            this.msgTextCol.Text = "Текст";
            this.msgTextCol.Width = 66;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "info.png");
            this.imageList1.Images.SetKeyName(1, "warning.png");
            this.imageList1.Images.SetKeyName(2, "error.png");
            // 
            // MessageList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.Name = "MessageList";
            this.Size = new System.Drawing.Size(863, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader msgTypeCol;
        private System.Windows.Forms.ColumnHeader msgDatetimeCol;
        private System.Windows.Forms.ColumnHeader msgTextCol;
        private System.Windows.Forms.ImageList imageList1;
    }
}
