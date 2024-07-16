namespace TimeClock
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.idTxt = new System.Windows.Forms.TextBox();
            this.passwordTxt = new System.Windows.Forms.TextBox();
            this.enterbtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.addUserBtn = new System.Windows.Forms.Button();
            this.changePassBtn = new System.Windows.Forms.Button();
            this.TimeEntriesBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "password";
            // 
            // idTxt
            // 
            this.idTxt.Location = new System.Drawing.Point(338, 115);
            this.idTxt.Name = "idTxt";
            this.idTxt.Size = new System.Drawing.Size(100, 26);
            this.idTxt.TabIndex = 2;
            this.idTxt.TextChanged += new System.EventHandler(this.idTxt_TextChanged);
            // 
            // passwordTxt
            // 
            this.passwordTxt.Location = new System.Drawing.Point(338, 178);
            this.passwordTxt.Name = "passwordTxt";
            this.passwordTxt.Size = new System.Drawing.Size(100, 26);
            this.passwordTxt.TabIndex = 3;
            this.passwordTxt.TextChanged += new System.EventHandler(this.passwordTxt_TextChanged);
            // 
            // enterbtn
            // 
            this.enterbtn.Location = new System.Drawing.Point(70, 339);
            this.enterbtn.Name = "enterbtn";
            this.enterbtn.Size = new System.Drawing.Size(153, 50);
            this.enterbtn.TabIndex = 4;
            this.enterbtn.Text = "enter";
            this.enterbtn.UseVisualStyleBackColor = true;
            this.enterbtn.Click += new System.EventHandler(this.enterbtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(279, 339);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(159, 49);
            this.exitBtn.TabIndex = 5;
            this.exitBtn.Text = "exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // addUserBtn
            // 
            this.addUserBtn.Location = new System.Drawing.Point(660, 12);
            this.addUserBtn.Name = "addUserBtn";
            this.addUserBtn.Size = new System.Drawing.Size(128, 46);
            this.addUserBtn.TabIndex = 6;
            this.addUserBtn.Text = "addUser";
            this.addUserBtn.UseVisualStyleBackColor = true;
            // 
            // changePassBtn
            // 
            this.changePassBtn.Location = new System.Drawing.Point(624, 349);
            this.changePassBtn.Name = "changePassBtn";
            this.changePassBtn.Size = new System.Drawing.Size(164, 75);
            this.changePassBtn.TabIndex = 7;
            this.changePassBtn.Text = "change Password";
            this.changePassBtn.UseVisualStyleBackColor = true;
            this.changePassBtn.Click += new System.EventHandler(this.changePassBtn_Click);
            // 
            // TimeEntriesBtn
            // 
            this.TimeEntriesBtn.Location = new System.Drawing.Point(12, 12);
            this.TimeEntriesBtn.Name = "TimeEntriesBtn";
            this.TimeEntriesBtn.Size = new System.Drawing.Size(116, 46);
            this.TimeEntriesBtn.TabIndex = 8;
            this.TimeEntriesBtn.Text = "Time Entries";
            this.TimeEntriesBtn.UseVisualStyleBackColor = true;
            this.TimeEntriesBtn.Click += new System.EventHandler(this.TimeEntriesBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TimeEntriesBtn);
            this.Controls.Add(this.changePassBtn);
            this.Controls.Add(this.addUserBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.enterbtn);
            this.Controls.Add(this.passwordTxt);
            this.Controls.Add(this.idTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox idTxt;
        private System.Windows.Forms.TextBox passwordTxt;
        private System.Windows.Forms.Button enterbtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button addUserBtn;
        private System.Windows.Forms.Button changePassBtn;
        private System.Windows.Forms.Button TimeEntriesBtn;
    }
}

