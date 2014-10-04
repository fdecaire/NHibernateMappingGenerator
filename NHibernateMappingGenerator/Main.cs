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
					lstServers.Items.Add(row["ServerName"] + "\\" + row["InstanceName"]);
				else
					lstServers.Items.Add(row["ServerName"]);
			}

			//TODO: set the selected item to the last saved item


		}

		private void lstServers_SelectedIndexChanged(object sender, EventArgs e)
		{
			// clear the list first
			lstDatabases.Items.Clear();

			// fill the check list box of databases from the server selected
			string serverName = lstServers.Items[lstServers.SelectedIndex].ToString();
			string query = "SELECT name, database_id, create_date FROM sys.databases WHERE name NOT IN ('master','tempdb','model','msdb')";

			using (var db = new ADODatabaseContext("server=" + serverName + ";Trusted_Connection=yes;database=master;Integrated Security=true;"))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					lstDatabases.Items.Add(reader["name"].ToString());
				}
			}

			// disable the generate button
			btnGenerate.Enabled = false;
		}

		private void lstDatabases_SelectedValueChanged(object sender, EventArgs e)
		{
			// check to see if at least one item is checked, enable the generate button.  Otherwise disable button.
			btnGenerate.Enabled = (lstDatabases.CheckedIndices.Count > 0);
		}

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			string serverName = lstServers.Items[lstServers.SelectedIndex].ToString();

			foreach (int index in lstDatabases.CheckedIndices)
			{
				NHibernateMappings nhibernateMappings = new NHibernateMappings
					{
						DatabaseName = lstDatabases.Items[index].ToString(),
						ConnectionString = "server=" + serverName + ";Trusted_Connection=yes;database=master;Integrated Security=true;"
					};
				nhibernateMappings.CreateMappings();
			}

			//TODO: need to indicate that the operation has completed
			btnGenerate.Enabled = false; //TODO: this is temporary
		}
	}
}
