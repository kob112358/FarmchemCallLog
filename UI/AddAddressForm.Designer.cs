namespace CallLog
{
    partial class AddAddressForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.addCompanyCity = new System.Windows.Forms.TextBox();
            this.addCompanyState = new System.Windows.Forms.TextBox();
            this.addCompanyZip = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Company City";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Company State";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(294, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Company Zip";
            // 
            // addCompanyCity
            // 
            this.addCompanyCity.Location = new System.Drawing.Point(24, 39);
            this.addCompanyCity.Name = "addCompanyCity";
            this.addCompanyCity.Size = new System.Drawing.Size(129, 20);
            this.addCompanyCity.TabIndex = 16;
            // 
            // addCompanyState
            // 
            this.addCompanyState.Location = new System.Drawing.Point(159, 39);
            this.addCompanyState.Name = "addCompanyState";
            this.addCompanyState.Size = new System.Drawing.Size(123, 20);
            this.addCompanyState.TabIndex = 17;
            // 
            // addCompanyZip
            // 
            this.addCompanyZip.Location = new System.Drawing.Point(288, 39);
            this.addCompanyZip.Name = "addCompanyZip";
            this.addCompanyZip.Size = new System.Drawing.Size(95, 20);
            this.addCompanyZip.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(180, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AddAddressButton);
            // 
            // AddAddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 104);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.addCompanyZip);
            this.Controls.Add(this.addCompanyState);
            this.Controls.Add(this.addCompanyCity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.KeyPreview = true;
            this.Name = "AddAddressForm";
            this.Text = "AddAddressForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddAddressForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox addCompanyCity;
        public System.Windows.Forms.TextBox addCompanyState;
        public System.Windows.Forms.TextBox addCompanyZip;
    }
}