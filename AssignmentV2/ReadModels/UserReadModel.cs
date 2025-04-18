﻿using AssignmentV2.ReadModels.Projects;

namespace AssignmentV2.ReadModels
{
	public class UserReadModel
	{
		/// <summary>
		/// Идентификатор.
		/// </summary>
		public Guid id { get; set; }

		/// <summary>
		/// Логин.
		/// </summary>
		public string? login { get; set; }

		/// <summary>
		/// Системный админ.
		/// </summary>
		public bool isSa {  get; set; }

		/// <summary>
		/// Право на создание проектов.
		/// </summary>
		public bool isCanCreateProject { get; set; }

		/// <summary>
		/// Право на создание задачи.
		/// </summary>
		public bool isCanCreateTask { get; set; }

		/// <summary>
		/// Созданные проекты.
		/// </summary>
		public IEnumerable<ProjectUserClaimReadModel> projects { get; set; }
	}
}
