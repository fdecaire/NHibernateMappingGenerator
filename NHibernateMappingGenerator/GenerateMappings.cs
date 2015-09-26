using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HelperLibrary;
using System.Xml;

namespace NHibernateMappingGenerator
{
	public class GenerateMappings
	{
		public string DatabaseName { get; set; }
		public string ConnectionString { get; set; }
		public string RootDirectory = "..\\..\\..\\NHibernateDataLayer\\"; //TODO: need to implement a user interface to be able to change this
		public bool GenerateNHibernateMappings { get; set; }
		public bool GenerateViewMappings { get; set; }
		public bool GenerateIntegrityConstraintMappings { get; set; }
		public bool GenerateStoredProcedureMappings { get; set; }

		private List<string> DeletedFiles = new List<string>();
		private List<string> AddedFiles = new List<string>();
		private List<string> folderList = new List<string>();

		public void CreateMappings()
		{
			Setup();

			if (GenerateNHibernateMappings)
			{
				CreateTableMappings(); // nhibernate tables
				CreateWrapperMappings();
			}

			CreateTableGeneratorMappings();

			if (GenerateStoredProcedureMappings)
			{
				CreateStoredProcedureMappings();
			}

			if (GenerateViewMappings)
			{
				CreateViewMappings();
			}

			if (GenerateIntegrityConstraintMappings)
			{
				CreateConstraintMappings();
			}

			ModifyVSProjectFile();
			DeleteUnusedFiles();
		}

		private void Setup()
		{
			// scan for all the files that currently exist and insert them into DeleteFiles
			DirSearch(RootDirectory + DatabaseName);
			folderList.Add(DatabaseName + "\\Constraints");
			folderList.Add(DatabaseName + "\\StoredProcedures");
			folderList.Add(DatabaseName + "\\Tables");
			folderList.Add(DatabaseName + "\\Views");
		}

		private void DeleteUnusedFiles()
		{
			foreach (var item in DeletedFiles)
			{
				File.Delete(item);
			}

			DeletedFiles.Clear();
		}

		private void DirSearch(string sDir)
		{
			if (Directory.Exists(sDir))
			{
				foreach (string d in Directory.GetDirectories(sDir))
				{
					foreach (string f in Directory.GetFiles(d))
					{
						if (f.EndsWith(".cs"))
						{
							DeletedFiles.Add(f);
						}
					}
					DirSearch(d);
				}
			}
		}

		public void CreateTableMappings()
		{
			Directory.CreateDirectory(RootDirectory + DatabaseName + "\\Tables");

			bool noTablesCreated = true;
			string query = "SELECT * FROM " + DatabaseName + ".INFORMATION_SCHEMA.tables";
			using (var db = new ADODatabaseContext(ConnectionString))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					noTablesCreated = false;

					// generate any new table mappings
					CreateTable(reader["TABLE_NAME"].ToString());
				}
			}

			if (!noTablesCreated)
			{
				UpdateProjectFileList("Tables", "");
			}
		}

		private void CreateWrapperMappings()
		{
			Directory.CreateDirectory(RootDirectory + DatabaseName + "\\Wrappers");
			WrapperMappings wrapperMappings = new WrapperMappings();


			string query = "SELECT * FROM " + DatabaseName + ".INFORMATION_SCHEMA.tables";
			using (StreamWriter sessionWrapperFile = new StreamWriter(RootDirectory + DatabaseName + "\\Wrappers\\SessionWrapper.cs"))
			{
				using (StreamWriter statelessSessionWrapperFile = new StreamWriter(RootDirectory + DatabaseName + "\\Wrappers\\StatelessSessionWrapper.cs"))
				{
					sessionWrapperFile.Write(wrapperMappings.EmitHeader("SessionWrapper"));
					statelessSessionWrapperFile.Write(wrapperMappings.EmitHeader("StatelessSessionWrapper"));

					using (var db = new ADODatabaseContext(ConnectionString))
					{
						var reader = db.ReadQuery(query);
						while (reader.Read())
						{
							string tableName = reader["TABLE_NAME"].ToString();
							sessionWrapperFile.Write(wrapperMappings.EmitCode(DatabaseName, tableName));
							statelessSessionWrapperFile.Write(wrapperMappings.EmitCode(DatabaseName, tableName));
						}
					}

					sessionWrapperFile.Write(wrapperMappings.EmitFooter());
					statelessSessionWrapperFile.Write(wrapperMappings.EmitFooter());
				}
			}

			UpdateProjectFileList("Wrappers", "SessionWrapper");
			UpdateProjectFileList("Wrappers", "StatelessSessionWrapper");
		}

		private void CreateTableGeneratorMappings()
		{
			//TODO: these mappings will be used by unit tests to generate the tables in the database so NHibernate is not needed
			Directory.CreateDirectory(RootDirectory + DatabaseName + "\\TableGeneratorCode");

			TableGeneratorMappings tableGeneratorMappings = new TableGeneratorMappings(ConnectionString, DatabaseName);
			string result = tableGeneratorMappings.EmitCode();

			using (StreamWriter file = new StreamWriter(RootDirectory + DatabaseName + "\\TableGeneratorCode\\" + DatabaseName + "TableGeneratorCode.cs"))
			{
				file.Write(result);
			}

			UpdateProjectFileList("TableGeneratorCode", DatabaseName + "TableGeneratorCode");
		}

		private void UpdateProjectFileList(string tableSPView, string Name)
		{
			// delete any existing table mappings first (in case a table was deleted)
			//TODO: refactor this
			int foundIndex = DeletedFiles.IndexOf(RootDirectory + DatabaseName + "\\" + tableSPView + "\\" + Name + ".cs");
			if (foundIndex > -1)
			{
				DeletedFiles.RemoveAt(foundIndex);
			}
			
			if (Name != "")
			{
				// added file
				AddedFiles.Add(DatabaseName + "\\" + tableSPView + "\\" + Name + ".cs");
			}
		}

		private void CreateTable(string tableName)
		{
			// create cs file named same as table
			using (StreamWriter file = new StreamWriter(RootDirectory + DatabaseName + "\\Tables\\" + tableName + ".cs"))
			{
				TableMappings tableMappings = new TableMappings(ConnectionString, DatabaseName, tableName);
				file.Write(tableMappings.EmitCode());
			}

			UpdateProjectFileList("Tables", tableName);
		}

		public void CreateStoredProcedureMappings()
		{
			Directory.CreateDirectory(RootDirectory + DatabaseName + "\\StoredProcedures");

			bool noStoredProceduresCreated = true;
			string query = "SELECT ROUTINE_NAME FROM " + DatabaseName + ".information_schema.routines WHERE routine_type = 'PROCEDURE'";
			using (var db = new ADODatabaseContext(ConnectionString))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					// generate any new stored procedure mappings
					CreateStoredProcedure(reader["ROUTINE_NAME"].ToString());
				}
			}

			if (noStoredProceduresCreated)
			{
				UpdateProjectFileList("StoredProcedures", "");
			}
		}

		public void CreateStoredProcedure(string storedProcedureName)
		{
			using (StreamWriter file = new StreamWriter(RootDirectory + DatabaseName + "\\StoredProcedures\\" + storedProcedureName + ".cs"))
			{
				StoredProcedureMappings storedProcedureMappings = new StoredProcedureMappings(ConnectionString, DatabaseName, storedProcedureName);

				file.Write(storedProcedureMappings.EmitCode());
			}

			UpdateProjectFileList("StoredProcedures", storedProcedureName);
		}

		public void CreateViewMappings()
		{
			Directory.CreateDirectory(RootDirectory + DatabaseName + "\\Views");

			bool noViewsCreated = true;
			string query = "SELECT TABLE_NAME FROM " + DatabaseName + ".information_schema.views";
			using (var db = new ADODatabaseContext(ConnectionString))
			{
				var reader = db.ReadQuery(query);
				while (reader.Read())
				{
					// generate any new view mappings
					CreateView(reader["TABLE_NAME"].ToString());
					noViewsCreated = false;
				}
			}

			if (noViewsCreated)
			{
				UpdateProjectFileList("Views", "");
			}
		}

		public void CreateView(string viewName)
		{
			using (StreamWriter file = new StreamWriter(RootDirectory + DatabaseName + "\\Views\\" + viewName + ".cs"))
			{
				ViewMappings viewMappings = new ViewMappings(ConnectionString, DatabaseName, viewName);

				file.Write(viewMappings.EmitCode());
			}

			UpdateProjectFileList("Views", viewName);
		}

		public void CreateConstraintMappings()
		{
			Directory.CreateDirectory(RootDirectory + DatabaseName + "\\Constraints");

			ConstraintMappings constraintMappings = new ConstraintMappings(ConnectionString, DatabaseName);
			string result = constraintMappings.EmitCode();

			using (StreamWriter file = new StreamWriter(RootDirectory + DatabaseName + "\\Constraints\\" + DatabaseName + "Constraints.cs"))
			{
				file.Write(result);
			}

			UpdateProjectFileList("Constraints", DatabaseName + "Constraints");
		}

		private void ModifyVSProjectFile()
		{
			// create xml code inside .csproj file
			string projectFileName = "";

			// search for a project file in the rootdirectory and then modify the proj file.
			// otherwise, just create the files there but ignore this section
			foreach (string f in Directory.GetFiles(RootDirectory))
			{
				if (f.EndsWith(".csproj"))
				{
					projectFileName = f;
					break;
				}
			}

			if (projectFileName == "")
			{
				return;
			}

			XmlDocument doc = new XmlDocument();
			doc.Load(projectFileName);

			var nsmgr = new XmlNamespaceManager(doc.NameTable);
			nsmgr.AddNamespace("a", "http://schemas.microsoft.com/developer/msbuild/2003");
			XmlNodeList itemGroupNodes = doc.SelectNodes("//a:Project/a:ItemGroup", nsmgr);

			DeleteNodeContainingChildName(itemGroupNodes, "Folder");
			DeleteNodeContainingChildName(itemGroupNodes, "Compile");
			DeleteEmptyNodes(itemGroupNodes);
			AddNewNodes(doc, nsmgr, itemGroupNodes);

			doc.Save(projectFileName);
		}

		private void AddNewNodes(XmlDocument doc, XmlNamespaceManager nsmgr, XmlNodeList itemGroupNodes)
		{
			bool containsFolderItemGroup = false;
			bool containsCompileItemGroup = false;

			foreach (XmlNode itemGroupNode in itemGroupNodes)
			{
				XmlNodeList childNodes = itemGroupNode.ChildNodes;

				foreach (XmlNode childNode in childNodes)
				{
					if (childNode.Name == "Folder")
					{
						containsFolderItemGroup = true;

						XmlAttribute includeAttribute = childNode.Attributes["Include"];

						if (includeAttribute != null)
						{
							foreach (var item in folderList)
							{
								itemGroupNode.AppendChild(CreateChildNode(doc, "Folder", item));
							}
							break;
						}
					}
					else if (childNode.Name == "Compile")
					{
						containsCompileItemGroup = true;

						XmlAttribute includeAttribute = childNode.Attributes["Include"];

						foreach (var item in AddedFiles)
						{
							itemGroupNode.AppendChild(CreateChildNode(doc, "Compile", item));
						}
						break;
					}
					else
					{
						break;
					}
				}
			}

			// need to handle situation where the Compile or folder itemgroups do not exist
			if (!containsFolderItemGroup)
			{
				XmlNodeList projectNode = doc.SelectNodes("//a:Project", nsmgr);
				XmlNode itemGroupNode = doc.CreateNode(XmlNodeType.Element, "ItemGroup", "http://schemas.microsoft.com/developer/msbuild/2003");
				projectNode[0].AppendChild(itemGroupNode);

				foreach (var item in folderList)
				{
					itemGroupNode.AppendChild(CreateChildNode(doc, "Folder", item));
				}
			}

			if (!containsCompileItemGroup)
			{
				XmlNodeList projectNode = doc.SelectNodes("//a:Project", nsmgr);
				XmlNode itemGroupNode = doc.CreateNode(XmlNodeType.Element, "ItemGroup", "http://schemas.microsoft.com/developer/msbuild/2003");
				projectNode[0].AppendChild(itemGroupNode);

				foreach (var item in AddedFiles)
				{
					itemGroupNode.AppendChild(CreateChildNode(doc, "Compile", item));
				}
			}
		}

		private void DeleteEmptyNodes(XmlNodeList itemGroupNodes)
		{
			// remove any empty ItemGroup nodes
			foreach (XmlNode itemGroupNode in itemGroupNodes)
			{
				if (itemGroupNode.ChildNodes.Count == 0)
				{
					itemGroupNode.ParentNode.RemoveChild(itemGroupNode);
				}
			}
		}

		private void DeleteNodeContainingChildName(XmlNodeList itemGroupNodes, string childGroupName)
		{
			List<XmlNode> toBeRemoved = new List<XmlNode>();

			foreach (XmlNode itemGroupNode in itemGroupNodes)
			{
				XmlNodeList childNodes = itemGroupNode.ChildNodes;

				foreach (XmlNode childNode in childNodes)
				{
					if (childNode.Name == childGroupName)
					{
						XmlAttribute includeAttribute = childNode.Attributes["Include"];

						if (includeAttribute != null && includeAttribute.Value != null && includeAttribute.Value.Contains(DatabaseName + "\\"))
						{
							toBeRemoved.Add(childNode);
						}
					}
				}

				foreach (var item in toBeRemoved)
				{
					itemGroupNode.RemoveChild(item);
				}

				toBeRemoved.Clear();
			}
		}

		private XmlNode CreateChildNode(XmlDocument doc, string elementName, string attributeValue)
		{
			XmlNode folderNode = doc.CreateNode(XmlNodeType.Element, elementName, "http://schemas.microsoft.com/developer/msbuild/2003");
			XmlAttribute xKey = doc.CreateAttribute("Include");
			xKey.Value = attributeValue;
			folderNode.Attributes.Append(xKey);

			return folderNode;
		}

	}
}
