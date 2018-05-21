using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            
            List<ProjectData> oldProjects = app.Project.GetProjectList();

            ProjectData project = new ProjectData()
            {
                Name = "test"
            };
            app.Project.Create(project);


            List<ProjectData> newProjects = app.Project.GetProjectList();
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(newProjects, oldProjects);
            
        }
    }
}
