using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml.Serialization;
using System.IO;

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

        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactData(parts[0], parts[1])
                {
                    Mobile = parts[2],
                    HomeTelephone = parts[3],
                    Company = parts[4],
                    Address = parts[5]
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));

        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contacts.json"));

        }

        public static IEnumerable<ContactData> ContactDataFromExcelFile()
        {
            List<ContactData> contacts = new List<ContactData>();

            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"contact.xlsx"));
            Excel.Worksheet sheet = wb.ActiveSheet;
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactData()
                {
                    FirstName = range.Cells[i, 1].Value,
                    LastName = range.Cells[i, 2].Value,
                    Mobile = range.Cells[i, 3].Value,
                    HomeTelephone = range.Cells[i, 4].Value,
                    Company = range.Cells[i, 5].Value,
                    Address = range.Cells[i, 6].Value
                });
            }

            wb.Close();
            app.Visible = false;
            app.Quit();
            return contacts;

        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContacts = app.Contact.GetContactList();

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
