using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    public class ContactHelper : HelperBase
    {
        private string baseURL;
        public ContactHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }


        private string selectedGroupName;
        public string SelectedGroupName
        {
            get
            {
                return selectedGroupName;
            }

        }

        #region High level methods
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.OpenHomePage();

            InitContactCreation();
            FillContactForm(contact, false);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(int index, ContactData newData)
        {
            manager.Navigator.OpenHomePage();

            InitContactModification(index + 1);
            FillContactForm(newData, true);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper AddToGroup(int contact, int group, bool all)
        {
            manager.Navigator.OpenHomePage();

            SelectContact(contact, all);
            SelectGroup(group);
            SubmitContactToGroupAddition();
            manager.Navigator.GoToGroupPage();
            return this;
        }

        public ContactHelper RemoveContact(int contact, bool all)
        {
            manager.Navigator.OpenHomePage();

            SelectContact(contact, all);
            InitContactDeletion();
            SubmitContactDeletion();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper IsEmptyCheck()
        {
            manager.Navigator.OpenHomePage();

            if ((!IsElementPresent(By.Name("selected[]")) && driver.Url == baseURL + "/"))
            {
                //It seems that it is better for the secondary contact data to be here, since the contact data transmitted
                //from the tests may be deliberately invalid for negative checks.

                ContactData fortest = new ContactData("test", "user");
                Create(fortest);
            }
            return this;
        }
        #endregion

        #region Low level methods
        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact, bool IsContactModify)
        {
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.FirstName);
            driver.FindElement(By.Name("middlename")).Click();
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(contact.MiddleName);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
            driver.FindElement(By.Name("nickname")).Click();
            driver.FindElement(By.Name("nickname")).Clear();
            driver.FindElement(By.Name("nickname")).SendKeys(contact.Nickname);
            driver.FindElement(By.Name("title")).Click();
            driver.FindElement(By.Name("title")).Clear();
            driver.FindElement(By.Name("title")).SendKeys(contact.Title);
            driver.FindElement(By.Name("company")).Click();
            driver.FindElement(By.Name("company")).Clear();
            driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            driver.FindElement(By.Name("address")).Click();
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys(contact.Address);
            driver.FindElement(By.Name("home")).Click();
            driver.FindElement(By.Name("home")).Clear();
            driver.FindElement(By.Name("home")).SendKeys(contact.HomeTelephone);
            driver.FindElement(By.Name("mobile")).Click();
            driver.FindElement(By.Name("mobile")).Clear();
            driver.FindElement(By.Name("mobile")).SendKeys(contact.Mobile);
            driver.FindElement(By.Name("work")).Click();
            driver.FindElement(By.Name("work")).Clear();
            driver.FindElement(By.Name("work")).SendKeys(contact.WorkTelephone);
            driver.FindElement(By.Name("fax")).Click();
            driver.FindElement(By.Name("fax")).Clear();
            driver.FindElement(By.Name("fax")).SendKeys(contact.Fax);
            driver.FindElement(By.Name("email")).Click();
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(contact.Email);
            driver.FindElement(By.Name("email2")).Click();
            driver.FindElement(By.Name("email2")).Clear();
            driver.FindElement(By.Name("email2")).SendKeys(contact.Email2);
            driver.FindElement(By.Name("email3")).Click();
            driver.FindElement(By.Name("email3")).Clear();
            driver.FindElement(By.Name("email3")).SendKeys(contact.Email3);
            driver.FindElement(By.Name("homepage")).Click();
            driver.FindElement(By.Name("homepage")).Clear();
            driver.FindElement(By.Name("homepage")).SendKeys(contact.HomePage);
            driver.FindElement(By.Name("bday")).Click();
            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.BirthDay);
            driver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.BirthMonth);
            driver.FindElement(By.Name("byear")).Click();
            driver.FindElement(By.Name("byear")).Clear();
            driver.FindElement(By.Name("byear")).SendKeys(contact.BirthYear);
            driver.FindElement(By.Name("aday")).Click();
            new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText(contact.AnniversaryDay);
            driver.FindElement(By.Name("amonth")).Click();
            new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText(contact.AnniversaryMonth);
            driver.FindElement(By.Name("ayear")).Click();
            driver.FindElement(By.Name("ayear")).Clear();
            driver.FindElement(By.Name("ayear")).SendKeys(contact.AnniversaryYear);
            if (!IsContactModify)
            {
                driver.FindElement(By.Name("new_group")).Click();
                new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText(contact.Group);
                driver.FindElement(By.Name("new_group")).Click();
            }

            driver.FindElement(By.Name("address2")).Click();
            driver.FindElement(By.Name("address2")).Clear();
            driver.FindElement(By.Name("address2")).SendKeys(contact.Address2);
            driver.FindElement(By.Name("phone2")).Click();
            driver.FindElement(By.Name("phone2")).Clear();
            driver.FindElement(By.Name("phone2")).SendKeys(contact.HomeTelephone2);
            driver.FindElement(By.Name("notes")).Click();
            driver.FindElement(By.Name("notes")).Clear();
            driver.FindElement(By.Name("notes")).SendKeys(contact.Notes);

            //driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(contact.Photo);
            //driver.FindElement(By.Name("photo")).Click();
            //driver.FindElement(By.Name("photo")).Clear();
            //driver.FindElement(By.Name("photo")).SendKeys("");
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public ContactHelper InitContactModification(int ind)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr["+ ind +"]/td[8]/a/img")).Click();
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper SelectContact(int contact, bool all)
        {
            if (all)
            {
                driver.FindElement(By.Id("MassCB")).Click();
            }

            else
            {
                IList<IWebElement> allElements = driver.FindElements(By.Name("selected[]"));
                driver.FindElements(By.Name("selected[]")).ElementAt(contact - 1).Click();
            }

            return this;
        }
        public ContactHelper SelectGroup(int group)
        {
            driver.FindElement(By.Name("to_group")).Click();
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByIndex(group);
            driver.FindElement(By.XPath("//div[@id='content']/form[2]/div[4]/select/option[" + group + "]")).Click();
            selectedGroupName = driver.FindElement(By.XPath("//div[@id='content']/form[2]/div[4]/select/option["+group+"]")).Text;
            return this;
        }

        public ContactHelper SubmitContactToGroupAddition()
        {
            driver.FindElement(By.Name("add")).Click();
            return this;
        }

        public ContactHelper InitContactDeletion()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper SubmitContactDeletion()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }
        #endregion
    }
}
