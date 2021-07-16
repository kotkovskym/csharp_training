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
            ContactData newData = new ContactData("Test1", "User1");
            newData.BirthDay = "-";
            newData.AnniversaryDay = "-";

            app.Contact.Modify(1, newData);
        }

        [Test]
        public void AddContactToGroupTest()
        {
            app.Contact.AddToGroup(1, 1, false);
        }

        [Test]
        public void AddAllContactsToGroupTest()
        {
            app.Contact.AddToGroup(1, 1, true);
        }
    }
}
