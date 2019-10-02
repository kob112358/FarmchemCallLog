namespace CallLog
{
    partial class CallLogUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CallLogUI));
            this.notesParagraph = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnEmailRep = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.repEmail = new System.Windows.Forms.ComboBox();
            this.btnNewCall = new System.Windows.Forms.Button();
            this.reasonForCall = new System.Windows.Forms.ComboBox();
            this.completedAnswer = new System.Windows.Forms.CheckBox();
            this.callDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contactPhone = new System.Windows.Forms.TextBox();
            this.contactEmail = new System.Windows.Forms.ComboBox();
            this.customerCode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.companyName = new System.Windows.Forms.ComboBox();
            this.contactName = new System.Windows.Forms.ComboBox();
            this.cityStateZip = new System.Windows.Forms.Label();
            this.comboCityStateZip = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.companyCity = new System.Windows.Forms.TextBox();
            this.companyZip = new System.Windows.Forms.TextBox();
            this.companyState = new System.Windows.Forms.TextBox();
            this.businessTabControl = new System.Windows.Forms.TabControl();
            this.businessNotes = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.callRecordNum = new System.Windows.Forms.Label();
            this.callRecordNumber = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.outsideRep = new System.Windows.Forms.Label();
            this.contactTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.callRecord = new System.Windows.Forms.Label();
            this.callLogGridView = new System.Windows.Forms.DataGridView();
            this.callIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contactNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.companyNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callNotesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callResolvedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dTCallBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.businessTabControl.SuspendLayout();
            this.contactTabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.callLogGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTCallBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // notesParagraph
            // 
            this.notesParagraph.CausesValidation = false;
            this.notesParagraph.Location = new System.Drawing.Point(150, 192);
            this.notesParagraph.Name = "notesParagraph";
            this.notesParagraph.Size = new System.Drawing.Size(448, 215);
            this.notesParagraph.TabIndex = 9;
            this.notesParagraph.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(156, 176);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Notes";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(156, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Reason for call";
            // 
            // btnEmailRep
            // 
            this.btnEmailRep.Location = new System.Drawing.Point(526, 151);
            this.btnEmailRep.Name = "btnEmailRep";
            this.btnEmailRep.Size = new System.Drawing.Size(68, 22);
            this.btnEmailRep.TabIndex = 15;
            this.btnEmailRep.TabStop = false;
            this.btnEmailRep.Text = "Email Rep";
            this.btnEmailRep.UseVisualStyleBackColor = true;
            this.btnEmailRep.Click += new System.EventHandler(this.EmailRep_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 214);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 38);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save Call (F4)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(455, 175);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(139, 17);
            this.checkBox1.TabIndex = 36;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "Email not yet sent to rep";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // repEmail
            // 
            this.repEmail.FormattingEnabled = true;
            this.repEmail.Items.AddRange(new object[] {
            "ashley.mccloughan@farmchem.com",
            "chad.simonson@farmchem.com",
            "erick@farmchem.com",
            "james.bluhm@farmchem.com",
            "jeff.thompson@farmchem.com",
            "jimself@farmchem.com",
            "justin.carr@farmchem.com",
            "kelly.eckenrod@farmchem.com",
            "lucast@farmchem.com",
            "richardm@farmchem.com",
            "steve.flanders@farmchem.com",
            "noah.macpherson@farmchem.com",
            "keith.huff@farmchem.com",
            "adam.thompson@farmchem.com",
            "terry.gibson@farmchem.com",
            "bw@farmchem.com",
            "dion@farmchem.com",
            "justin@farmchem.com"});
            this.repEmail.Location = new System.Drawing.Point(313, 151);
            this.repEmail.Name = "repEmail";
            this.repEmail.Size = new System.Drawing.Size(207, 21);
            this.repEmail.TabIndex = 8;
            // 
            // btnNewCall
            // 
            this.btnNewCall.Location = new System.Drawing.Point(12, 170);
            this.btnNewCall.Name = "btnNewCall";
            this.btnNewCall.Size = new System.Drawing.Size(100, 38);
            this.btnNewCall.TabIndex = 14;
            this.btnNewCall.Text = "New Call (F2)";
            this.btnNewCall.UseVisualStyleBackColor = true;
            this.btnNewCall.Click += new System.EventHandler(this.BtnNewCall_Click);
            // 
            // reasonForCall
            // 
            this.reasonForCall.CausesValidation = false;
            this.reasonForCall.FormattingEnabled = true;
            this.reasonForCall.Items.AddRange(new object[] {
            "Product Inquiry",
            "Issue with Product",
            "Bulk Project"});
            this.reasonForCall.Location = new System.Drawing.Point(150, 151);
            this.reasonForCall.Name = "reasonForCall";
            this.reasonForCall.Size = new System.Drawing.Size(155, 21);
            this.reasonForCall.TabIndex = 7;
            this.reasonForCall.Validating += new System.ComponentModel.CancelEventHandler(this.ReasonForCall_Validating);
            // 
            // completedAnswer
            // 
            this.completedAnswer.AutoSize = true;
            this.completedAnswer.Location = new System.Drawing.Point(507, 411);
            this.completedAnswer.Name = "completedAnswer";
            this.completedAnswer.Size = new System.Drawing.Size(91, 17);
            this.completedAnswer.TabIndex = 12;
            this.completedAnswer.Text = "Call Resolved";
            this.completedAnswer.UseVisualStyleBackColor = true;
            // 
            // callDate
            // 
            this.callDate.CausesValidation = false;
            this.callDate.Enabled = false;
            this.callDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.callDate.Location = new System.Drawing.Point(12, 7);
            this.callDate.Name = "callDate";
            this.callDate.Size = new System.Drawing.Size(100, 20);
            this.callDate.TabIndex = 0;
            this.callDate.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Company Name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(150, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Contact E-mail";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(155, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Contact Phone";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(472, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Customer Code";
            // 
            // contactPhone
            // 
            this.contactPhone.AllowDrop = true;
            this.contactPhone.CausesValidation = false;
            this.contactPhone.Location = new System.Drawing.Point(150, 33);
            this.contactPhone.Name = "contactPhone";
            this.contactPhone.Size = new System.Drawing.Size(155, 20);
            this.contactPhone.TabIndex = 0;
            this.contactPhone.LostFocus += new System.EventHandler(this.ContactPhone_LostFocus);
            this.contactPhone.Validating += new System.ComponentModel.CancelEventHandler(this.ContactPhone_Validating);
            // 
            // contactEmail
            // 
            this.contactEmail.CausesValidation = false;
            this.contactEmail.FormattingEnabled = true;
            this.contactEmail.Location = new System.Drawing.Point(150, 75);
            this.contactEmail.Name = "contactEmail";
            this.contactEmail.Size = new System.Drawing.Size(155, 21);
            this.contactEmail.TabIndex = 2;
            this.contactEmail.Validating += new System.ComponentModel.CancelEventHandler(this.ContactEmail_Validating);
            // 
            // customerCode
            // 
            this.customerCode.CausesValidation = false;
            this.customerCode.FormattingEnabled = true;
            this.customerCode.Location = new System.Drawing.Point(472, 32);
            this.customerCode.Name = "customerCode";
            this.customerCode.Size = new System.Drawing.Size(122, 21);
            this.customerCode.TabIndex = 3;
            this.customerCode.LostFocus += new System.EventHandler(this.CustomerCode_LostFocus);
            this.customerCode.Validating += new System.ComponentModel.CancelEventHandler(this.CustomerCode_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Contact Name";
            // 
            // companyName
            // 
            this.companyName.CausesValidation = false;
            this.companyName.FormattingEnabled = true;
            this.companyName.Location = new System.Drawing.Point(311, 75);
            this.companyName.Name = "companyName";
            this.companyName.Size = new System.Drawing.Size(155, 21);
            this.companyName.TabIndex = 4;
            // 
            // contactName
            // 
            this.contactName.CausesValidation = false;
            this.contactName.FormattingEnabled = true;
            this.contactName.Location = new System.Drawing.Point(311, 32);
            this.contactName.Name = "contactName";
            this.contactName.Size = new System.Drawing.Size(155, 21);
            this.contactName.TabIndex = 1;
            this.contactName.SelectedIndexChanged += new System.EventHandler(this.ContactName_SelectedIndexChanged);
            this.contactName.LostFocus += new System.EventHandler(this.ContactName_LostFocus);
            this.contactName.Validating += new System.ComponentModel.CancelEventHandler(this.ContactName_Validating);
            // 
            // cityStateZip
            // 
            this.cityStateZip.AutoSize = true;
            this.cityStateZip.Location = new System.Drawing.Point(156, 96);
            this.cityStateZip.Name = "cityStateZip";
            this.cityStateZip.Size = new System.Drawing.Size(120, 13);
            this.cityStateZip.TabIndex = 39;
            this.cityStateZip.Text = "Company City, State Zip";
            // 
            // comboCityStateZip
            // 
            this.comboCityStateZip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCityStateZip.FormattingEnabled = true;
            this.comboCityStateZip.Location = new System.Drawing.Point(150, 112);
            this.comboCityStateZip.Name = "comboCityStateZip";
            this.comboCityStateZip.Size = new System.Drawing.Size(316, 21);
            this.comboCityStateZip.TabIndex = 5;
            this.comboCityStateZip.SelectedIndexChanged += new System.EventHandler(this.ComboCityStateZip_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(472, 112);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 22);
            this.button2.TabIndex = 6;
            this.button2.Text = "Add New Address";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.AddAddress_Click);
            // 
            // companyCity
            // 
            this.companyCity.Location = new System.Drawing.Point(637, 408);
            this.companyCity.Name = "companyCity";
            this.companyCity.Size = new System.Drawing.Size(0, 20);
            this.companyCity.TabIndex = 42;
            this.companyCity.TabStop = false;
            this.companyCity.Validating += new System.ComponentModel.CancelEventHandler(this.CompanyCity_Validating);
            // 
            // companyZip
            // 
            this.companyZip.Location = new System.Drawing.Point(696, 408);
            this.companyZip.Name = "companyZip";
            this.companyZip.Size = new System.Drawing.Size(0, 20);
            this.companyZip.TabIndex = 43;
            this.companyZip.TabStop = false;
            this.companyZip.Validating += new System.ComponentModel.CancelEventHandler(this.CompanyZip_Validating);
            // 
            // companyState
            // 
            this.companyState.Location = new System.Drawing.Point(662, 408);
            this.companyState.Name = "companyState";
            this.companyState.Size = new System.Drawing.Size(0, 20);
            this.companyState.TabIndex = 44;
            this.companyState.TabStop = false;
            this.companyState.Validating += new System.ComponentModel.CancelEventHandler(this.CompanyState_Validating);
            // 
            // businessTabControl
            // 
            this.businessTabControl.Controls.Add(this.businessNotes);
            this.businessTabControl.ImeMode = System.Windows.Forms.ImeMode.On;
            this.businessTabControl.Location = new System.Drawing.Point(600, 16);
            this.businessTabControl.Name = "businessTabControl";
            this.businessTabControl.SelectedIndex = 0;
            this.businessTabControl.Size = new System.Drawing.Size(382, 196);
            this.businessTabControl.TabIndex = 10;
            // 
            // businessNotes
            // 
            this.businessNotes.Location = new System.Drawing.Point(4, 22);
            this.businessNotes.Name = "businessNotes";
            this.businessNotes.Padding = new System.Windows.Forms.Padding(3);
            this.businessNotes.Size = new System.Drawing.Size(374, 170);
            this.businessNotes.TabIndex = 0;
            this.businessNotes.Text = "Company Notes";
            this.businessNotes.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 317);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 40);
            this.button3.TabIndex = 16;
            this.button3.Text = "Update";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // callRecordNum
            // 
            this.callRecordNum.AutoSize = true;
            this.callRecordNum.Location = new System.Drawing.Point(12, 255);
            this.callRecordNum.Name = "callRecordNum";
            this.callRecordNum.Size = new System.Drawing.Size(75, 13);
            this.callRecordNum.TabIndex = 50;
            this.callRecordNum.Text = "Call Record #:";
            // 
            // callRecordNumber
            // 
            this.callRecordNumber.AutoSize = true;
            this.callRecordNumber.Location = new System.Drawing.Point(378, 163);
            this.callRecordNumber.Name = "callRecordNumber";
            this.callRecordNumber.Size = new System.Drawing.Size(0, 13);
            this.callRecordNumber.TabIndex = 51;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 363);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 40);
            this.button5.TabIndex = 18;
            this.button5.Text = "Load Call From Grid";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.LoadCallBasedOnGrid_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 271);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 40);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.Text = "Search (F5)";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // outsideRep
            // 
            this.outsideRep.AutoSize = true;
            this.outsideRep.Location = new System.Drawing.Point(321, 136);
            this.outsideRep.Name = "outsideRep";
            this.outsideRep.Size = new System.Drawing.Size(66, 13);
            this.outsideRep.TabIndex = 54;
            this.outsideRep.Text = "Outside Rep";
            // 
            // contactTabControl
            // 
            this.contactTabControl.Controls.Add(this.tabPage1);
            this.contactTabControl.Location = new System.Drawing.Point(600, 214);
            this.contactTabControl.Name = "contactTabControl";
            this.contactTabControl.SelectedIndex = 0;
            this.contactTabControl.Size = new System.Drawing.Size(382, 193);
            this.contactTabControl.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(374, 167);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Contact Notes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // callRecord
            // 
            this.callRecord.AutoSize = true;
            this.callRecord.ForeColor = System.Drawing.SystemColors.ControlText;
            this.callRecord.Location = new System.Drawing.Point(87, 255);
            this.callRecord.Name = "callRecord";
            this.callRecord.Size = new System.Drawing.Size(0, 13);
            this.callRecord.TabIndex = 56;
            // 
            // callLogGridView
            // 
            this.callLogGridView.AutoGenerateColumns = false;
            this.callLogGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.callLogGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.callIDDataGridViewTextBoxColumn,
            this.callDateDataGridViewTextBoxColumn,
            this.contactNameDataGridViewTextBoxColumn,
            this.companyNameDataGridViewTextBoxColumn,
            this.cityDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.callNotesDataGridViewTextBoxColumn,
            this.callResolvedDataGridViewCheckBoxColumn});
            this.callLogGridView.DataSource = this.dTCallBindingSource;
            this.callLogGridView.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.callLogGridView.Location = new System.Drawing.Point(12, 436);
            this.callLogGridView.Name = "callLogGridView";
            this.callLogGridView.Size = new System.Drawing.Size(986, 220);
            this.callLogGridView.TabIndex = 99;
            this.callLogGridView.TabStop = false;
            // 
            // callIDDataGridViewTextBoxColumn
            // 
            this.callIDDataGridViewTextBoxColumn.DataPropertyName = "CallID";
            this.callIDDataGridViewTextBoxColumn.HeaderText = "CallID";
            this.callIDDataGridViewTextBoxColumn.Name = "callIDDataGridViewTextBoxColumn";
            // 
            // callDateDataGridViewTextBoxColumn
            // 
            this.callDateDataGridViewTextBoxColumn.DataPropertyName = "CallDate";
            this.callDateDataGridViewTextBoxColumn.HeaderText = "CallDate";
            this.callDateDataGridViewTextBoxColumn.Name = "callDateDataGridViewTextBoxColumn";
            // 
            // contactNameDataGridViewTextBoxColumn
            // 
            this.contactNameDataGridViewTextBoxColumn.DataPropertyName = "ContactName";
            this.contactNameDataGridViewTextBoxColumn.HeaderText = "ContactName";
            this.contactNameDataGridViewTextBoxColumn.Name = "contactNameDataGridViewTextBoxColumn";
            // 
            // companyNameDataGridViewTextBoxColumn
            // 
            this.companyNameDataGridViewTextBoxColumn.DataPropertyName = "CompanyName";
            this.companyNameDataGridViewTextBoxColumn.HeaderText = "CompanyName";
            this.companyNameDataGridViewTextBoxColumn.Name = "companyNameDataGridViewTextBoxColumn";
            // 
            // cityDataGridViewTextBoxColumn
            // 
            this.cityDataGridViewTextBoxColumn.DataPropertyName = "City";
            this.cityDataGridViewTextBoxColumn.HeaderText = "City";
            this.cityDataGridViewTextBoxColumn.Name = "cityDataGridViewTextBoxColumn";
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "State";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            // 
            // callNotesDataGridViewTextBoxColumn
            // 
            this.callNotesDataGridViewTextBoxColumn.DataPropertyName = "CallNotes";
            this.callNotesDataGridViewTextBoxColumn.HeaderText = "CallNotes";
            this.callNotesDataGridViewTextBoxColumn.Name = "callNotesDataGridViewTextBoxColumn";
            // 
            // callResolvedDataGridViewCheckBoxColumn
            // 
            this.callResolvedDataGridViewCheckBoxColumn.DataPropertyName = "CallResolved";
            this.callResolvedDataGridViewCheckBoxColumn.HeaderText = "CallResolved";
            this.callResolvedDataGridViewCheckBoxColumn.Name = "callResolvedDataGridViewCheckBoxColumn";
            // 
            // dTCallBindingSource
            // 
            this.dTCallBindingSource.DataSource = typeof(ClassModels.CallClasses.DTCall);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 131);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 100;
            this.pictureBox1.TabStop = false;
            // 
            // CallLogUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 673);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.callRecord);
            this.Controls.Add(this.contactTabControl);
            this.Controls.Add(this.outsideRep);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.callRecordNumber);
            this.Controls.Add(this.callRecordNum);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.businessTabControl);
            this.Controls.Add(this.companyState);
            this.Controls.Add(this.companyZip);
            this.Controls.Add(this.companyCity);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboCityStateZip);
            this.Controls.Add(this.cityStateZip);
            this.Controls.Add(this.callLogGridView);
            this.Controls.Add(this.callDate);
            this.Controls.Add(this.completedAnswer);
            this.Controls.Add(this.reasonForCall);
            this.Controls.Add(this.btnNewCall);
            this.Controls.Add(this.contactName);
            this.Controls.Add(this.companyName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.customerCode);
            this.Controls.Add(this.contactEmail);
            this.Controls.Add(this.repEmail);
            this.Controls.Add(this.contactPhone);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnEmailRep);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.notesParagraph);
            this.KeyPreview = true;
            this.Name = "CallLogUI";
            this.Tag = "contactPhone";
            this.Text = "Call Log";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.businessTabControl.ResumeLayout(false);
            this.contactTabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.callLogGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dTCallBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnEmailRep;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox repEmail;
        private System.Windows.Forms.Button btnNewCall;
        private System.Windows.Forms.CheckBox completedAnswer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.RichTextBox notesParagraph;
        public System.Windows.Forms.ComboBox reasonForCall;
        public System.Windows.Forms.TextBox contactPhone;
        public System.Windows.Forms.ComboBox contactEmail;
        public System.Windows.Forms.ComboBox customerCode;
        public System.Windows.Forms.ComboBox companyName;
        public System.Windows.Forms.ComboBox contactName;
        public System.Windows.Forms.DateTimePicker callDate;
        private System.Windows.Forms.Label cityStateZip;
        private System.Windows.Forms.ComboBox comboCityStateZip;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox companyCity;
        private System.Windows.Forms.TextBox companyZip;
        private System.Windows.Forms.TextBox companyState;
        private System.Windows.Forms.TabControl businessTabControl;
        public System.Windows.Forms.TabPage businessNotes;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label callRecordNum;
        private System.Windows.Forms.Label callRecordNumber;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label outsideRep;
        private System.Windows.Forms.TabControl contactTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label callRecord;
        public System.Windows.Forms.DataGridView callLogGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn callIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contactNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn companyNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callNotesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn callResolvedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.BindingSource dTCallBindingSource;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

