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
            ProjectDataComparator comparator = new ProjectDataComparator();
            app.Project.CreateIfNotExist();
            List<MantisConnect.ProjectData> oldProjects = app.Project.GetProjectListWithMantis();
            app.Project.Delete(1);


            List<MantisConnect.ProjectData> newProjects = app.Project.GetProjectListWithMantis();
            oldProjects.RemoveAt(0);
            oldProjects.Sort(comparator);
            newProjects.Sort(comparator);

            Assert.AreEqual(newProjects, oldProjects);
        }
    }
}
