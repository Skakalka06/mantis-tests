using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_tests.MantisConnect;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }

        List<MantisConnect.ProjectData> projectCache;
        List<ProjectData> projectCache1;


        public void Create(ProjectData project)
        {
            manager.Menu.OpenManagePage();
            manager.Menu.OpenManageProjectPage();
            CreateNewProject();
            FillProjectForm(project);
            SubmitProjectCreation();

        }

        public void CreateWithMantisMetod(MantisConnect.ProjectData project)
        {
            manager.Menu.OpenManagePage();
            manager.Menu.OpenManageProjectPage();
            var mantis = new MantisConnect.MantisConnectPortTypeClient();
            mantis.mc_project_add("administrator", "root", project);
        }

        public void Delete(int index)
        {
            manager.Menu.OpenManagePage();
            manager.Menu.OpenManageProjectPage();
            SelectProject(index);
            SubmitDeleteProject();

        }

        private void SelectProject(int index)
        {
            driver.FindElements(By.CssSelector("table.width100"))[1]
                .FindElements(By.TagName("tr"))[index +1]
                .FindElements(By.TagName("td"))[0]
                .FindElement(By.TagName("a"))
                .Click();
        }

        private void SubmitDeleteProject()
        {
            driver.FindElement(By.XPath("//input[@value='Delete Project']")).Click();
            driver.FindElement(By.CssSelector("form > input.button")).Click();
        }


        public List<MantisConnect.ProjectData> GetProjectListWithMantis()
        {

            if (projectCache == null)
            {
                projectCache = new List<MantisConnect.ProjectData>();

                MantisConnect.MantisConnectPortTypeClient mantis = new MantisConnect.MantisConnectPortTypeClient();
                manager.Menu.OpenManagePage();
                manager.Menu.OpenManageProjectPage();
                projectCache = mantis.mc_projects_get_user_accessible("administrator", "root").ToList();
            }
            return projectCache;
        }

        public List<ProjectData> GetProjectList()
        {

            if (projectCache == null)
            {
                projectCache1 = new List<ProjectData>();


                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("table.width100"))[1]
                .FindElements(By.TagName("tr"));

                foreach (IWebElement element in elements.Skip(2))
                {
                    IList<IWebElement> items = element.FindElements(By.CssSelector("td"));
                    projectCache1.Add(new ProjectData()
                    { Name = items[0].Text });
                }
            }
            return projectCache1;
        }

        public void CreateIfNotExist()
        {
            manager.Menu.OpenManagePage();
            manager.Menu.OpenManageProjectPage();

            if (!ProjectIfNotExist())
            {
                var mantis = new MantisConnect.MantisConnectPortTypeClient();
                mantis.mc_project_add("administrator", "root", new MantisConnect.ProjectData() { name = "mantis" });
            }
        }

        private bool ProjectIfNotExist()
        {

            if (driver.FindElements(By.CssSelector("table.width100"))[1]
               .FindElements(By.TagName("tr")).Count < 3)
                return false;
            else return true;
        }

        private void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Add Project']")).Click();
        }

        private void CreateNewProject()
        {
            driver.FindElement(By.XPath("//input[@value='Create New Project']")).Click();
        }

        private void FillProjectForm(ProjectData project)
        {
            driver.FindElement(By.Name("name")).SendKeys(project.Name);
        }
    }
}
