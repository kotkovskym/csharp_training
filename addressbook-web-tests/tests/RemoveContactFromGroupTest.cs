using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class RemoveContactFromGroupTest : AuthTestBase
    {

        [Test]
        public void TestRemovingContactFromGroup()
        {
            //checks
            app.Groups.IsEmptyCheck();
            app.Contact.IsEmptyCheck();

            //data preparation
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = oldList.First();
            
            
            app.Contact.IsRemovable(contact, group);
            contact = oldList.First();

            //actions
            app.Contact.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
