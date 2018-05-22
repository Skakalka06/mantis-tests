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
            ProjectDataComparator comparator = new ProjectDataComparator();
            List<MantisConnect.ProjectData> oldProjects = app.Project.GetProjectListWithMantis();

            MantisConnect.ProjectData project = new MantisConnect.ProjectData() { name = "334"};

            app.Project.CreateWithMantisMetod(project);

            List<MantisConnect.ProjectData> newProjects = app.Project.GetProjectListWithMantis();
            oldProjects.Add(project);
            oldProjects.Sort(comparator);
            newProjects.Sort(comparator);

            Assert.AreEqual(newProjects, oldProjects);
            
        }
    }
}
