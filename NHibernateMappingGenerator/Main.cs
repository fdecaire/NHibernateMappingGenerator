using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using Helpers;

namespace NHibernateMappingGenerator
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			// lookup all the data severs on this network and allow the user to select one
			DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
			foreach (DataRow row in servers.Rows)
			{
				if ((row["InstanceName"] as string) != null)
				{
					lstServers.Items.Add(row["ServerName"] + "\\" + row["InstanceName"]);
				}
				else
				{
					lstServers.Items.Add(row["ServerName"]);
				}
			}

			//TODO: set the selected item to the last saved item


		}

		private void lstServers_SelectedIndexChanged(object sender, EventArgs e)
		{
			txtServerName.Text = lstServers.Items[lstServers.SelectedIndex].ToString();

			PopulateDatabaseList();
		}

		private string GetConnectionString()
		{
			string result = "";

			if (cbUseSQLAuthentication.Checked)
			{
				result = String.Format("server={0};Trusted_Connection=yes;database=master;User ID={1};Password={2}", txtServerName.Text, txtUserId.Text, txtPassword.Text);
			}
			else
			{
				result = String.Format("server={0};Trusted_Connection=yes;database=master;Integrated Security=true;", txtServerName.Text);
			}

			return result;
		}

		// fill the check list box of databases from the server selected
		private void PopulateDatabaseList()
		{
			// clear the list first
			lstDatabases.Items.Clear();

			string query = "SELECT name, database_id, create_date FROM sys.databases WHERE name NOT IN ('master','tempdb','model','msdb')";

			using (var db = new ADODatabaseContext(GetConnectionString()))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					lstDatabases.Items.Add(reader["name"].ToString());
				}
			}

			// disable the generate button
			btnGenerate.Enabled = false;
			lblResult.Visible = false;
		}

		private void lstDatabases_SelectedValueChanged(object sender, EventArgs e)
		{
			// check to see if at least one item is checked, enable the generate button.  Otherwise disable button.
			btnGenerate.Enabled = (lstDatabases.CheckedIndices.Count > 0);
			lblResult.Visible = false;
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			string serverName = "";

			if (lstServers.SelectedIndex == -1)
			{
				serverName = txtServerName.Text;
			}
			else
			{
				serverName = lstServers.Items[lstServers.SelectedIndex].ToString();
			}

			foreach (int index in lstDatabases.CheckedIndices)
			{
				GenerateMappings nhibernateMappings = new GenerateMappings
					{
						DatabaseName = lstDatabases.Items[index].ToString(),
						ConnectionString = GetConnectionString(),
						GenerateIntegrityConstraintMappings = cbStoreProcMappings.Checked,
						GenerateNHibernateMappings = cbNHibernateMappings.Checked,
						GenerateStoredProcedureMappings = cbStoreProcMappings.Checked,
						GenerateViewMappings = cbViewMappings.Checked
					};
				nhibernateMappings.CreateMappings();
			}

			// indicate that the operation has completed
			btnGenerate.Enabled = false;
			lblResult.Visible = true;
		}

		private void btnConnect_Click(object sender, EventArgs e)
		{
			PopulateDatabaseList();
		}

		private void cbUseSQLAuthentication_CheckedChanged(object sender, EventArgs e)
		{
			if (cbUseSQLAuthentication.Checked)
			{
				txtUserId.Enabled = true;
				txtPassword.Enabled = true;
			}
			else
			{
				txtUserId.Enabled = false;
				txtPassword.Enabled = false;
			}
		}
	}
}
