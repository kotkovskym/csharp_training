using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace mantis_tests
{ 
    [TestFixture]
    public class ProjectsRemovalTests : AuthTestBase
    {
        [Test]
        public void TestMethod1()
        {
            List<ProjectData> oldProjects = ProjectData.GetAll();
            ProjectData toBeRemoved = oldProjects[0];

            app.Project.IsEmptyCheck();
            app.Project.Remove(toBeRemoved);

            Assert.AreEqual(oldProjects.Count - 1, app.Project.GetProjectCount());

            List<ProjectData> newProjects = ProjectData.GetAll();

            oldProjects.RemoveAt(0);

            Assert.AreEqual(oldProjects, newProjects);

            foreach (ProjectData project in newProjects)
            {
                Assert.AreNotEqual(project.Id, toBeRemoved.Id);
            }
        }
    }
}
