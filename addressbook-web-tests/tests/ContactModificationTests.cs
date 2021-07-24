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
            List<ContactData> oldContacts = app.Contact.GetContactList();

            ContactData newData = new ContactData("Test1", "User1");
            newData.BirthDay = "-";
            newData.AnniversaryDay = "-";

            app.Contact.IsEmptyCheck();
            app.Contact.Modify(1, newData);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts[0].FirstName = newData.FirstName;
            oldContacts[0].LastName = newData.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
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
