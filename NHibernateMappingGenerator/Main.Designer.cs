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
			this.SuspendLayout();
			// 
			// btnGenerate
			// 
			this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGenerate.Enabled = false;
			this.btnGenerate.Location = new System.Drawing.Point(181, 363);
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
			this.lstDatabases.Location = new System.Drawing.Point(12, 39);
			this.lstDatabases.Name = "lstDatabases";
			this.lstDatabases.Size = new System.Drawing.Size(432, 304);
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
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(456, 398);
			this.Controls.Add(this.lstServers);
			this.Controls.Add(this.lstDatabases);
			this.Controls.Add(this.btnGenerate);
			this.Name = "frmMain";
			this.Text = "NHibernate Mapping Generator";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.CheckedListBox lstDatabases;
		private System.Windows.Forms.ComboBox lstServers;
	}
}

