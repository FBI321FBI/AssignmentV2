using AssignmentV2.ReadModels;
using AssignmentV2.Services.DataBase;
using static AssignmentV2.Constants.Claims;

namespace AssignmentV2.Services
{
	public class TaskService
	{
		#region Properties
		private readonly TasksDbService _taskDbService;
		private readonly TasksUsersClaimDbService _tasksUsersClaimDbService;

		private List<UserReadModel> _usersInTask;
		#endregion

		#region .ctor
		public TaskService()
		{
			_taskDbService = new TasksDbService();
			_tasksUsersClaimDbService = new TasksUsersClaimDbService();
			_usersInTask = new List<UserReadModel>();
		}
		#endregion

		#region Public
		public async Task CreateTask(Guid owner, IEnumerable<UserReadModel> participants, Guid taskId, Guid projectId, string name, string description)
		{
			await _taskDbService.CreateTask(new TaskDbReadModel
			{
				id = taskId,
				project_id = projectId,
				name = name,
				description = description,
			});

			await _tasksUsersClaimDbService.CreateTaskUserClaim(new TasksUsersClaimDbReadModel
			{
				id = Guid.NewGuid(),
				task_id = taskId,
				user_id = owner,
				claim_id = Guid.Parse(TASK_OWNER)
			});

			foreach (var participant in participants)
			{
				await _tasksUsersClaimDbService.CreateTaskUserClaim(new TasksUsersClaimDbReadModel
				{
					id = Guid.NewGuid(),
					task_id = taskId,
					user_id = participant.id,
					claim_id = Guid.Parse(TASK_PARTICIPANT)
				});
			}
		}

		public async Task<IEnumerable<TaskDbReadModel>?> GetTasksByUserId(Guid userId)
		{
			List<TaskDbReadModel> tasks = new List<TaskDbReadModel>();
			var currentTasks = await _tasksUsersClaimDbService.GetTasksByUserId(userId);
			if (currentTasks is null) return null;
			foreach (var currentTask in currentTasks)
			{
				var task = await _taskDbService.GetTaskById(currentTask.task_id);
				if (task is null) continue;
				tasks.Add(task);
			}
			return tasks;
		}
		#endregion
	}
}
