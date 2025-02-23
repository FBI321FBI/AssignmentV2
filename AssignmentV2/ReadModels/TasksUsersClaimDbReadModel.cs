namespace AssignmentV2.ReadModels
{
	public class TasksUsersClaimDbReadModel
	{
		public Guid id { get; set; }
		public Guid task_id { get; set; }
		public Guid user_id { get; set; }
		public Guid claim_id { get; set; }
	}
}
