using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager) { }

        List<ProjectData> projectCache;


        public void Create(ProjectData project)
        {
            manager.Menu.OpenManagePage();
            manager.Menu.OpenManageProjectPage();
            CreateNewProject();
            FillProjectForm(project);
            SubmitProjectCreation();

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


        public List<ProjectData> GetProjectList()
        {

            if (projectCache == null)
            {
                projectCache = new List<ProjectData>();
                manager.Menu.OpenManagePage();
                manager.Menu.OpenManageProjectPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("table.width100"))[1]
                .FindElements(By.TagName("tr")); 

                foreach (IWebElement element in elements.Skip(2))
                {
                    IList<IWebElement> items = element.FindElements(By.CssSelector("td"));
                    projectCache.Add(new ProjectData()
                        { Name = items[0].Text });
                }
            }
            return projectCache;
        }

        public void CreateIfNotExist(ProjectData project)
        {
            manager.Menu.OpenManagePage();
            manager.Menu.OpenManageProjectPage();

            if (!ProjectIfNotExist())
            {
                Create(project);
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
