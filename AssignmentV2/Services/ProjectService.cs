using AssignmentV2.ReadModels;
using AssignmentV2.Services.DataBase;
using AssignmentV2.View.UserControls.ProjectPanel;
using System.Windows;

namespace AssignmentV2.Services
{
	public class ProjectService
	{
		#region Properties
		private ProjectPanelUserControl? _projectPanelUserControl => Application.Current.Windows.OfType<MainWindow>().FirstOrDefault()?.MainProjectPanelUserControl?.ProjectPanelUserControl;
		private ProjectDbService projectDbService => new ProjectDbService();
		#endregion

		public void RemoveProject(ProjectInProjectPanelReadModel project)
		{
			Repository.Projects.Remove(project);
		}

		public async Task RemoveProjectById(Guid id)
		{
			var projectForDelete = GetProjectById(id);
			if (projectForDelete is not null)
			{
				Repository.Projects.Remove(projectForDelete);
				_projectPanelUserControl.ProjectsStackPanel.Children.Remove(projectForDelete.Button);
				await projectDbService.UpdateProject(new ReadModels.Projects.ProjectReadModel
				{
					id = projectForDelete.Project.id,
					name = projectForDelete.Project.name,
					isDeleted = true
				});
			}
		}

		public void AddProjectInRepository(ProjectInProjectPanelReadModel project)
		{
			Repository.Projects.Add(project);
		}

		public ProjectInProjectPanelReadModel? GetProjectById(Guid id)
		{
			return Repository.Projects.Where(x => x.Project.id == id).FirstOrDefault();
		}

		public async Task RenameProjectById(Guid id, string newName)
		{
			var projectForRename = GetProjectById(id);
			if (projectForRename is not null)
			{
				projectForRename.Button.Content = newName;
				await projectDbService.UpdateProject(new ReadModels.Projects.ProjectReadModel
				{
					id = projectForRename.Project.id,
					name = newName,
					isDeleted = false
				});
			}
		}
	}
}
