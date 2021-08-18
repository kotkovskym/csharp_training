using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            List<ContactData> oldContacts = ContactData.GetAll();

            ContactData newData = new ContactData("Test1", "User1");
            newData.BirthDay = "-";
            newData.AnniversaryDay = "-";
            newData.BirthMonth = "-";
            newData.AnniversaryMonth = "-";

            ContactData toBeModified = oldContacts[0];

            app.Contact.IsEmptyCheck();
            app.Contact.Modify(toBeModified, newData);

            Assert.AreEqual(oldContacts.Count, app.Contact.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].FirstName = newData.FirstName;
            oldContacts[0].LastName = newData.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == toBeModified.Id)
                {
                    Assert.AreEqual(newData.FirstName, contact.FirstName);
                    Assert.AreEqual(newData.LastName, contact.LastName);
                }
            }
        }

        [Test]
        public void AddContactToGroupTest()
        {
            app.Contact.IsEmptyCheck();
            app.Contact.AddToGroup(1, 1, false);
        }

        [Test]
        public void AddAllContactsToGroupTest()
        {
            app.Contact.IsEmptyCheck();
            app.Contact.AddToGroup(1, 1, true);
        }
    }
}
