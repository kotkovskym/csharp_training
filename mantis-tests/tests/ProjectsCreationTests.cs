using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectsCreationTests : TestBase
    {
        [Test]
        public void CreateNewProjectTest()
        {
            ProjectData project = new ProjectData();

            List<ProjectData> oldProjects = ProjectData.GetAll();

            app.Project.Create(project);

            Assert.AreEqual(oldProjects.Count + 1, app.Project.GetProjectCount());

            List<ProjectData> newProjects = ProjectData.GetAll();
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }

        [Test]
        public void CreateNewProjectAPI()
        {
            AccountData account = new AccountData("administrator", "qwerty");
            ProjectData project = new ProjectData() { Name = "test" };

            List<ProjectData> oldProjects = app.API.GetProjects(account);

            app.API.CreateNewProject(account, project);

            Assert.AreEqual(oldProjects.Count + 1, app.Project.GetProjectCount());

            List<ProjectData> newProjects = app.API.GetProjects(account);
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
