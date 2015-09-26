
namespace HelperLibrary
{
	public class ConstraintDefinition
	{
		public string DatabaseName { get; set; }
		public string PkTable { get; set; }
		public string PkField { get; set; }
		public string FkTable { get; set; }
		public string FkField { get; set; }
		public string SchemaName { get; set; }
	}
}
