using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemoveTests : AuthTestBase
    {
        [Test]
        public void ProjectRemoveTest()
        {


            ProjectData project = new ProjectData()
            {
                Name = "123"
            };
            app.Project.CreateIfNotExist(project);
            List<ProjectData> oldProjects = app.Project.GetProjectList();
            app.Project.Delete(1);


            List<ProjectData> newProjects = app.Project.GetProjectList();
            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(newProjects, oldProjects);
        }
    }
}
