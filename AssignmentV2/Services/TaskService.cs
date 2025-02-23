using System.Data.SqlClient;
using System.Threading.Tasks;
using AssignmentV2.ReadModels;
using AssignmentV2.Services.DataBase;
using AssignmentV2.Utilities;
using static AssignmentV2.Constants.Claims;

namespace AssignmentV2.Services
{
	public class TaskService
	{
		#region Properties
		private readonly TasksDbService _taskService;
		private readonly TasksUsersClaimDbService _tasksUsersClaimDbService;
		#endregion

		#region .ctor
		public TaskService()
		{
			_taskService = new TasksDbService();
			_tasksUsersClaimDbService = new TasksUsersClaimDbService();
		}
		#endregion

		#region Public
		public async Task CreateTask(Guid userId, Guid projectId, string name, string description)
		{
			var taskId = Guid.NewGuid();
			var taskUserClaimId = Guid.NewGuid();

			await _taskService.CreateTask(new TaskDbReadModel
			{
				id = taskId,
				project_id = projectId,
				name = name,
				description = description,
			});

			await _tasksUsersClaimDbService.CreateTaskUserClaim(new TasksUsersClaimDbReadModel
			{
				id = taskUserClaimId,
				task_id = taskId,
				user_id = userId,
				claim_id = Guid.Parse(TASK_OWNER)
			});
		}

		public async Task<IEnumerable<TaskDbReadModel>?> GetTasksByUserId(Guid userId)
		{
			List<TaskDbReadModel> tasks = new List<TaskDbReadModel>();
			var currentTasks = await _tasksUsersClaimDbService.GetTasksByUserId(userId);
			if (currentTasks is null) return null;
			foreach (var currentTask in currentTasks)
			{
				var task = await _taskService.GetTaskById(currentTask.task_id);
				if (task is null) continue;
				tasks.Add(task);
			}
			return tasks;
		}
		#endregion
	}
}
