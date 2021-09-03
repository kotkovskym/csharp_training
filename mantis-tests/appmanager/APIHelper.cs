using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager)
        { }

        public void CreateNewProject(AccountData account, ProjectData project)
        {
            mantis.MantisConnectPortTypeClient client = new mantis.MantisConnectPortTypeClient();
            mantis.ProjectData projectData = new mantis.ProjectData();
            projectData.name = project.Name;
            client.mc_project_add(account.Name, account.Password, projectData);
        }

        public List<ProjectData> GetProjects(AccountData account)
        {
            mantis.MantisConnectPortTypeClient client = new mantis.MantisConnectPortTypeClient();
            IList<mantis.ProjectData> namelist = new List<mantis.ProjectData> { };
            namelist = client.mc_projects_get_user_accessible(account.Name, account.Password);
            List<ProjectData> projectsList = new List<ProjectData>();
            foreach (mantis.ProjectData item in namelist)
            {
                ProjectData i = new ProjectData();
                item.name = i.Name;
                projectsList.Add(i);
            }
            return projectsList;
        }

        public void IsEmptyCheck(List<ProjectData> oldProjects, AccountData account, ProjectData project)
        {
            if (oldProjects.Count() == 0)
            {
                CreateNewProject(account, project);
            }
        }

        public void RemovaProject(AccountData account, ProjectData toBeRemoved)
        {
            mantis.MantisConnectPortTypeClient client = new mantis.MantisConnectPortTypeClient();
            mantis.ProjectData project = new mantis.ProjectData();
            project.id = toBeRemoved.Id;

            client.mc_project_delete(account.Name, account.Password, project.id);
        }
    }
}
