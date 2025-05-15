namespace AssignmentV2.ReadModels
{
	public class TaskDbReadModel
	{
		public Guid id { get; set; }
		public Guid project_id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public DateTime created_date { get; set; }
		public DateTime end_date { get; set; }
		public bool isDeleted { get; set; }
	}
}
