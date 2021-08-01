using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactInformationTest : AuthTestBase
    {
        [Test]         
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contact.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(0);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }


        [Test]
        public void TestContactInformationProperties()
        {
            ContactData fromDetails = app.Contact.GetContactInformationFromContactDetails(0);
            ContactData fromForm = app.Contact.GetContactInformationFromEditForm(0);

            //verification
            Assert.AreEqual(fromDetails.Initials, fromForm.Initials);
            Assert.AreEqual(fromDetails.AllData, fromForm.AllData);
        }
    }
}
