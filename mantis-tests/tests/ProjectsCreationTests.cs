using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectsCreationTests : AuthTestBase
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
    }
}
