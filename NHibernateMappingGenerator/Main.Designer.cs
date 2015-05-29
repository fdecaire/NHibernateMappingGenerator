namespace NHibernateMappingGenerator
{
	partial class frmMain
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
			this.btnGenerate = new System.Windows.Forms.Button();
			this.lstDatabases = new System.Windows.Forms.CheckedListBox();
			this.lstServers = new System.Windows.Forms.ComboBox();
			this.txtServerName = new System.Windows.Forms.TextBox();
			this.btnConnect = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.cbUseSQLAuthentication = new System.Windows.Forms.CheckBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUserId = new System.Windows.Forms.TextBox();
			this.lblResult = new System.Windows.Forms.Label();
			this.cbNHibernateMappings = new System.Windows.Forms.CheckBox();
			this.cbStoreProcMappings = new System.Windows.Forms.CheckBox();
			this.cbViewMappings = new System.Windows.Forms.CheckBox();
			this.cbRelationalIntegrityMappings = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnGenerate
			// 
			this.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnGenerate.Enabled = false;
			this.btnGenerate.Location = new System.Drawing.Point(181, 550);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(104, 23);
			this.btnGenerate.TabIndex = 0;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.UseVisualStyleBackColor = true;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// lstDatabases
			// 
			this.lstDatabases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstDatabases.CheckOnClick = true;
			this.lstDatabases.FormattingEnabled = true;
			this.lstDatabases.Location = new System.Drawing.Point(12, 249);
			this.lstDatabases.Name = "lstDatabases";
			this.lstDatabases.Size = new System.Drawing.Size(432, 184);
			this.lstDatabases.TabIndex = 1;
			this.lstDatabases.SelectedValueChanged += new System.EventHandler(this.lstDatabases_SelectedValueChanged);
			// 
			// lstServers
			// 
			this.lstServers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lstServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstServers.FormattingEnabled = true;
			this.lstServers.Location = new System.Drawing.Point(12, 12);
			this.lstServers.Name = "lstServers";
			this.lstServers.Size = new System.Drawing.Size(432, 21);
			this.lstServers.TabIndex = 2;
			this.lstServers.SelectedIndexChanged += new System.EventHandler(this.lstServers_SelectedIndexChanged);
			// 
			// txtServerName
			// 
			this.txtServerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtServerName.Location = new System.Drawing.Point(13, 40);
			this.txtServerName.Name = "txtServerName";
			this.txtServerName.Size = new System.Drawing.Size(431, 20);
			this.txtServerName.TabIndex = 3;
			// 
			// btnConnect
			// 
			this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConnect.Location = new System.Drawing.Point(12, 209);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(75, 26);
			this.btnConnect.TabIndex = 4;
			this.btnConnect.Text = "Connect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.cbUseSQLAuthentication);
			this.groupBox1.Controls.Add(this.txtPassword);
			this.groupBox1.Controls.Add(this.txtUserId);
			this.groupBox1.Location = new System.Drawing.Point(13, 66);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(431, 137);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 82);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 14;
			this.label2.Text = "Password";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(18, 13);
			this.label1.TabIndex = 13;
			this.label1.Text = "ID";
			// 
			// cbUseSQLAuthentication
			// 
			this.cbUseSQLAuthentication.AutoSize = true;
			this.cbUseSQLAuthentication.Location = new System.Drawing.Point(11, 20);
			this.cbUseSQLAuthentication.Name = "cbUseSQLAuthentication";
			this.cbUseSQLAuthentication.Size = new System.Drawing.Size(152, 17);
			this.cbUseSQLAuthentication.TabIndex = 12;
			this.cbUseSQLAuthentication.Text = "SQL Server Authentication";
			this.cbUseSQLAuthentication.UseVisualStyleBackColor = true;
			this.cbUseSQLAuthentication.CheckedChanged += new System.EventHandler(this.cbUseSQLAuthentication_CheckedChanged);
			// 
			// txtPassword
			// 
			this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPassword.Enabled = false;
			this.txtPassword.Location = new System.Drawing.Point(11, 100);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(414, 20);
			this.txtPassword.TabIndex = 11;
			// 
			// txtUserId
			// 
			this.txtUserId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUserId.Enabled = false;
			this.txtUserId.Location = new System.Drawing.Point(11, 59);
			this.txtUserId.Name = "txtUserId";
			this.txtUserId.Size = new System.Drawing.Size(414, 20);
			this.txtUserId.TabIndex = 10;
			// 
			// lblResult
			// 
			this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblResult.AutoSize = true;
			this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblResult.ForeColor = System.Drawing.Color.Green;
			this.lblResult.Location = new System.Drawing.Point(290, 551);
			this.lblResult.Name = "lblResult";
			this.lblResult.Size = new System.Drawing.Size(154, 20);
			this.lblResult.TabIndex = 11;
			this.lblResult.Text = "Process Complete";
			this.lblResult.Visible = false;
			// 
			// cbNHibernateMappings
			// 
			this.cbNHibernateMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbNHibernateMappings.AutoSize = true;
			this.cbNHibernateMappings.Checked = true;
			this.cbNHibernateMappings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbNHibernateMappings.Location = new System.Drawing.Point(13, 439);
			this.cbNHibernateMappings.Name = "cbNHibernateMappings";
			this.cbNHibernateMappings.Size = new System.Drawing.Size(204, 17);
			this.cbNHibernateMappings.TabIndex = 12;
			this.cbNHibernateMappings.Text = "Generate NHibernate ORM Mappings";
			this.cbNHibernateMappings.UseVisualStyleBackColor = true;
			// 
			// cbStoreProcMappings
			// 
			this.cbStoreProcMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbStoreProcMappings.AutoSize = true;
			this.cbStoreProcMappings.Checked = true;
			this.cbStoreProcMappings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbStoreProcMappings.Location = new System.Drawing.Point(12, 462);
			this.cbStoreProcMappings.Name = "cbStoreProcMappings";
			this.cbStoreProcMappings.Size = new System.Drawing.Size(199, 17);
			this.cbStoreProcMappings.TabIndex = 13;
			this.cbStoreProcMappings.Text = "Generate Store Procedure Mappings";
			this.cbStoreProcMappings.UseVisualStyleBackColor = true;
			// 
			// cbViewMappings
			// 
			this.cbViewMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbViewMappings.AutoSize = true;
			this.cbViewMappings.Checked = true;
			this.cbViewMappings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbViewMappings.Location = new System.Drawing.Point(12, 485);
			this.cbViewMappings.Name = "cbViewMappings";
			this.cbViewMappings.Size = new System.Drawing.Size(145, 17);
			this.cbViewMappings.TabIndex = 14;
			this.cbViewMappings.Text = "Generate View Mappings";
			this.cbViewMappings.UseVisualStyleBackColor = true;
			// 
			// cbRelationalIntegrityMappings
			// 
			this.cbRelationalIntegrityMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbRelationalIntegrityMappings.AutoSize = true;
			this.cbRelationalIntegrityMappings.Checked = true;
			this.cbRelationalIntegrityMappings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbRelationalIntegrityMappings.Location = new System.Drawing.Point(12, 508);
			this.cbRelationalIntegrityMappings.Name = "cbRelationalIntegrityMappings";
			this.cbRelationalIntegrityMappings.Size = new System.Drawing.Size(209, 17);
			this.cbRelationalIntegrityMappings.TabIndex = 15;
			this.cbRelationalIntegrityMappings.Text = "Generate Relational Integrity Mappings";
			this.cbRelationalIntegrityMappings.UseVisualStyleBackColor = true;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(456, 585);
			this.Controls.Add(this.cbRelationalIntegrityMappings);
			this.Controls.Add(this.cbViewMappings);
			this.Controls.Add(this.cbStoreProcMappings);
			this.Controls.Add(this.cbNHibernateMappings);
			this.Controls.Add(this.lblResult);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.txtServerName);
			this.Controls.Add(this.lstServers);
			this.Controls.Add(this.lstDatabases);
			this.Controls.Add(this.btnGenerate);
			this.Name = "frmMain";
			this.Text = "NHibernate Mapping Generator";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.CheckedListBox lstDatabases;
		private System.Windows.Forms.ComboBox lstServers;
		private System.Windows.Forms.TextBox txtServerName;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox cbUseSQLAuthentication;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUserId;
		private System.Windows.Forms.Label lblResult;
		private System.Windows.Forms.CheckBox cbNHibernateMappings;
		private System.Windows.Forms.CheckBox cbStoreProcMappings;
		private System.Windows.Forms.CheckBox cbViewMappings;
		private System.Windows.Forms.CheckBox cbRelationalIntegrityMappings;
	}
}

