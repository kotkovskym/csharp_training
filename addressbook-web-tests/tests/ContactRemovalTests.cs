using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.IsEmptyCheck();
            app.Contact.RemoveContact(1, false);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void AllContactsRemovalTest()
        {
            app.Contact.IsEmptyCheck();
            app.Contact.RemoveContact(1, true);
            List<ContactData> newContacts = app.Contact.GetContactList();

            Assert.IsTrue(newContacts.Count == 0);
        }
    }
}
