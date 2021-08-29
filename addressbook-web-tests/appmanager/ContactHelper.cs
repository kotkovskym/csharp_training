using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };

        }

        public ContactData GetContactInformationFromContactDetails(int index)
        {
            manager.Navigator.OpenHomePage();
            manager.Navigator.GoToContactDetailsPage(0);
            string allData = driver.FindElement(By.Id("content")).GetAttribute("innerText");
           

            string firstName = null;
            string lastName = null;
            string creds = driver.FindElement(By.Id("content")).FindElement(By.TagName("b")).Text;
            string[] fullName = creds.Split(' ');
            if (fullName.Length == 3)
            {
                firstName = fullName[0];
                lastName = fullName[2];
            }
            if (fullName.Length == 2)
            {
                firstName = fullName[0];
                lastName = fullName[1];
            }
            else
            {
                firstName = fullName[0];
            }

            return new ContactData(firstName, lastName)
            {
                AllData = allData,
                Initials = firstName + lastName
            };
        }

        

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(0);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string nick = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string bday = driver.FindElement(By.Name("bday")).GetAttribute("value");
            string bmonth = driver.FindElement(By.Name("bmonth")).GetAttribute("value");
            string byear = driver.FindElement(By.Name("byear")).GetAttribute("value");
            string aday = driver.FindElement(By.Name("aday")).GetAttribute("value");
            string amonth = driver.FindElement(By.Name("amonth")).GetAttribute("value");
            string ayear = driver.FindElement(By.Name("ayear")).GetAttribute("value");
            string address2 = driver.FindElement(By.Name("address2")).GetAttribute("value");
            string homePhone2 = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");
            string homePage = driver.FindElement(By.Name("homepage")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                MiddleName = middleName,
                Address = address,
                HomeTelephone = homePhone,
                Mobile = mobilePhone,
                WorkTelephone = workPhone,
                Nickname = nick,
                Company = company,
                Title = title,
                Fax = fax,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                BirthDay = bday,
                BirthMonth = bmonth,
                BirthYear = byear,
                AnniversaryDay = aday,
                AnniversaryMonth = amonth,
                AnniversaryYear = ayear,
                Address2 = address2,
                HomeTelephone2 = homePhone2,
                Notes = notes,
                HomePage = homePage
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        private List<ContactData> contactCache = null;
        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();

                manager.Navigator.OpenHomePage();

                List<string> first = new List<string>();
                List<string> last = new List<string>();
                int j = driver.FindElements(By.Name("entry")).Count();
                List<IWebElement> elements = driver.FindElements(By.Name("entry")).ToList();
                foreach(IWebElement element in elements)
                {
                    List<IWebElement>  names = element.FindElements(By.TagName("td")).ToList();
                    first.Add((names[2].Text));
                    last.Add((names[1].Text));
                }

                for (int i = 0; i < first.Count(); i++)
                {
                    contactCache.Add(new ContactData(first[i], last[i]) {
                        Id = driver.FindElement(By.Name("selected[]")).GetAttribute("value")
                    });
                }

            }
            
            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.Name("selected[]")).Count;
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

        public ContactHelper Modify(ContactData toBeModified, ContactData newData)
        {
            manager.Navigator.OpenHomePage();

            InitContactModification(toBeModified.Id);
            FillContactForm(newData, true);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper AddToGroup(int contact, int group, bool all)
        {
            manager.Navigator.OpenHomePage();

            SelectContact(contact, all);
            SelectGroupToAdd(group);
            SubmitContactToGroupAddition();
            manager.Navigator.GoToGroupPage();
            return this;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContactById(contact.Id);
            SelectGroupToAddByName(group.Name);
            SubmitContactToGroupAddition();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count() > 0);

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

        public ContactHelper RemoveContact(ContactData contact, bool all)
        {
            manager.Navigator.OpenHomePage();

            SelectContactById(contact.Id);
            InitContactDeletion();
            SubmitContactDeletion();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();

            SelectGroupFilter(group.Name);
            SelectContactById(contact.Id);
            SubmitContactRemoval();
            
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

        public ContactHelper IsEmptyCheck(ContactData contact)
        {
            if (contact == null)
            {
                ContactData fortest = new ContactData("test", "user");
                Create(fortest);
            }
            return this;
        }

        public ContactHelper IsRemovable(ContactData contact, GroupData group)
        {
            if (contact == null)
            {
                ContactData fortest = new ContactData("test", "user");
                Create(fortest);
                AddContactToGroup(fortest, group);
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
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomeTelephone);
            Type(By.Name("mobile"), contact.Mobile);
            Type(By.Name("work"), contact.WorkTelephone);
            Type(By.Name("fax"), contact.Fax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.HomePage);
            Type(By.Name("byear"), contact.BirthYear);
            Type(By.Name("ayear"), contact.AnniversaryYear);
            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.HomeTelephone2);
            Type(By.Name("notes"), contact.Notes);

            driver.FindElement(By.Name("bday")).Click();
            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.BirthDay);
            driver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.BirthMonth);

            driver.FindElement(By.Name("aday")).Click();
            new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText(contact.AnniversaryDay);
            driver.FindElement(By.Name("amonth")).Click();
            new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText(contact.AnniversaryMonth);

            if (!IsContactModify)
            {
                driver.FindElement(By.Name("new_group")).Click();
                new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText(contact.Group);
                driver.FindElement(By.Name("new_group")).Click();
            }

            //driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(contact.Photo);
            //driver.FindElement(By.Name("photo")).Click();
            //driver.FindElement(By.Name("photo")).Clear();
            //driver.FindElement(By.Name("photo")).SendKeys("");
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int ind)
        {
            driver.FindElements(By.Name("entry"))[ind]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper InitContactModification(string id)
        {
            driver.FindElement(By.XPath("//a[@href='edit.php?id="+id+"']")).Click();

            /*driver.FindElement(By.XPath("(//input[@name='selected[]' and @value ='" + id + "'])"))
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();*/
            return this;
        }
        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }
        public void SelectContactById(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
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
        public ContactHelper SelectGroupToAdd(int group)
        {
            driver.FindElement(By.Name("to_group")).Click();
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByIndex(group);
            driver.FindElement(By.XPath("//div[@id='content']/form[2]/div[4]/select/option[" + group + "]")).Click();
            selectedGroupName = driver.FindElement(By.XPath("//div[@id='content']/form[2]/div[4]/select/option["+group+"]")).Text;
            return this;
        }

        public void SelectGroupToAddByName(string groupName)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(groupName);
        }

        public void SelectGroupFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }


        public ContactHelper SubmitContactToGroupAddition()
        {
            driver.FindElement(By.Name("add")).Click();
            return this;
        }

        public void SubmitContactRemoval()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public ContactHelper InitContactDeletion()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper SubmitContactDeletion()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }
        #endregion
    }
}
