namespace AssignmentV2.ReadModels.Projects
{
	public class ProjectUserClaimReadModel
	{
		public Guid id { get; set; }
		public Guid project_id { get; set; }
		public string? name { get; set; }
		public Guid user_id { get; set; }
		public Guid claim_id { get; set; }
	}
}
