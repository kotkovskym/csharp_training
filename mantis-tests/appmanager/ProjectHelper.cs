using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{ 

    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager)
        {

        }

        public void Create(ProjectData project)
        {
            manager.Navigator.GoToProjectManagmentPage();
            InitProjectCreation();
            FillProjectCreationForm(project);
            SubmitProjectCreation();
        }

        public void Remove(ProjectData toBeRemoved)
        {
            manager.Navigator.GoToProjectManagmentPage();
            InitProjectRemoval(toBeRemoved.Name);
            DeleteProject();
            SubmitProjectRemoval();
        }


        public ProjectHelper IsEmptyCheck()
        {
            manager.Navigator.GoToProjectManagmentPage();

            int count = GetProjectCount();

            if (count == 0)
            {
                ProjectData fortest = new ProjectData()
                {
                    Name = "test"
                };

                Create(fortest);
            }
            return this;
        }

        public int GetProjectCount()
        {
            return driver.FindElements(By.CssSelector("tbody"))[0].FindElements(By.CssSelector("tr")).Count;
        }

        #region Low Level Methods

        public void SubmitProjectCreation()
        {
            driver.FindElement(By.CssSelector("input[value='Добавить проект']")).Click();
        }

        public void FillProjectCreationForm(ProjectData project)
        {
            Type(By.XPath("//input[@id='project-name']"), project.Name);
        }

        public void InitProjectCreation()
        {
            driver.FindElement(By.XPath("//button[@type='submit', @text='Создать новый проект']")).Click();            
        }

        public void SubmitProjectRemoval()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        public void DeleteProject()
        {
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        public void InitProjectRemoval(string name)
        {
            driver.FindElement(By.XPath("//a[contains(text(), '"+ name + "')]")).Click();
        }
        #endregion Low Level Methods

    }
}
