using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contact.RemoveContact(1, false);
        }

        [Test]
        public void AllContactsRemovalTest()
        {
            app.Contact.RemoveContact(1, true);
        }
    }
}
