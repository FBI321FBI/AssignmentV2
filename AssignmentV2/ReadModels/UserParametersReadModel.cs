namespace AssignmentV2.ReadModels
{
	public class UserParametersReadModel
	{
		public Guid id { get; set; }
		public Guid user_id { get; set; }
		public Guid parameter_id { get; set; }
		public string value { get; set; }
		public Guid data_type { get; set; }
	}
}
