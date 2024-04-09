namespace society_management_system
{
    partial class SelectSocietyDialog
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
            this.societyDropdown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.confirm_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // societyDropdown
            // 
            this.societyDropdown.FormattingEnabled = true;
            this.societyDropdown.Location = new System.Drawing.Point(399, 135);
            this.societyDropdown.Name = "societyDropdown";
            this.societyDropdown.Size = new System.Drawing.Size(121, 21);
            this.societyDropdown.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.75F);
            this.label1.Location = new System.Drawing.Point(158, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please Select Your Society:";
            // 
            // confirm_button
            // 
            this.confirm_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.confirm_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.confirm_button.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.confirm_button.ForeColor = System.Drawing.SystemColors.Control;
            this.confirm_button.Location = new System.Drawing.Point(310, 217);
            this.confirm_button.Name = "confirm_button";
            this.confirm_button.Size = new System.Drawing.Size(92, 31);
            this.confirm_button.TabIndex = 40;
            this.confirm_button.Text = "Confirm";
            this.confirm_button.UseVisualStyleBackColor = false;
            this.confirm_button.Click += new System.EventHandler(this.confirm_button_Click);
            // 
            // SelectSocietyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.confirm_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.societyDropdown);
            this.Name = "SelectSocietyDialog";
            this.Text = "SelectSocietyDialog";
            this.Load += new System.EventHandler(this.SelectSocietyDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox societyDropdown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button confirm_button;
    }
}