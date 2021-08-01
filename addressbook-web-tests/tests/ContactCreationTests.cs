using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomGroupDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenrateRandomString(30), GenrateRandomString(30))
                {
                    MiddleName = (GenrateRandomString(30)),
                    Address = (GenrateRandomString(50)),
                    Company = (GenrateRandomString(70)),
                    Notes = (GenrateRandomString(300))

                });
            }
            return contacts;
        }

        [Test]
        public void ContactCreationTest()
        {
            List<ContactData> oldContacts = app.Contact.GetContactList();

            ContactData contact = new ContactData("Test", "User");
            app.Contact.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}
