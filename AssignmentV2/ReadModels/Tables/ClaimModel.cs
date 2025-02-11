namespace AssignmentV2.ReadModels.Tables
{
	public class ClaimModel
	{
		/// <summary>
		/// Идентификатор утверждения.
		/// </summary>
		public Guid id { get; set; }

		/// <summary>
		/// Наименование.
		/// </summary>
		public string name { get; set; } = string.Empty;

		/// <summary>
		/// Описание.
		/// </summary>
		public string description { get; set; } = string.Empty;
	}
}
