using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase
    {
        public ManagementMenuHelper(ApplicationManager manager) : base(manager) { }

        public void OpenManagePage()
        {
            driver.FindElement(By.LinkText("Manage")).Click();
        }

        public void OpenManageProjectPage()
        {
            driver.FindElement(By.LinkText("Manage Projects")).Click();
        }
    }
}
