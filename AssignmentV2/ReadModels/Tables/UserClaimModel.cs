namespace AssignmentV2.ReadModels.Tables
{
	public class UserClaimModel
	{
		/// <summary>
		/// Идентификатор.
		/// </summary>
		public Guid id { get; set; }

		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public Guid user_id { get; set; }

		/// <summary>
		/// Идентификатор утверждения.
		/// </summary>
		public Guid claim_id { get; set; }
	}
}
