namespace TimeClock
{
    partial class TimeEntriesForm
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
            this.listBoxEntries = new System.Windows.Forms.ListBox();
            this.listViewEntries = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listBoxEntries
            // 
            this.listBoxEntries.FormattingEnabled = true;
            this.listBoxEntries.ItemHeight = 20;
            this.listBoxEntries.Location = new System.Drawing.Point(12, 77);
            this.listBoxEntries.Name = "listBoxEntries";
            this.listBoxEntries.Size = new System.Drawing.Size(399, 364);
            this.listBoxEntries.TabIndex = 0;
            this.listBoxEntries.SelectedIndexChanged += new System.EventHandler(this.listBoxEntries_SelectedIndexChanged);
            // 
            // listViewEntries
            // 
            this.listViewEntries.HideSelection = false;
            this.listViewEntries.Location = new System.Drawing.Point(443, 77);
            this.listViewEntries.Name = "listViewEntries";
            this.listViewEntries.Size = new System.Drawing.Size(345, 361);
            this.listViewEntries.TabIndex = 1;
            this.listViewEntries.UseCompatibleStateImageBehavior = false;
            // 
            // TimeEntriesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listViewEntries);
            this.Controls.Add(this.listBoxEntries);
            this.Name = "TimeEntriesForm";
            this.Text = "TimeEntriesForm";
            this.Load += new System.EventHandler(this.TimeEntriesForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxEntries;
        private System.Windows.Forms.ListView listViewEntries;
    }
}