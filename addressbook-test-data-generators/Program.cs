using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using addressbook_web_tests;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataType = args[0];
            int count = Convert.ToInt32(args[1]);
            string filename = args[2];
            string format = args[3];

            if (dataType == "group")
            {
                List<GroupData> groups = new List<GroupData>();
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestBase.GenrateRandomString(10))
                    {
                        Header = TestBase.GenrateRandomString(100),
                        Footer = TestBase.GenrateRandomString(100)
                    });
                }

                if (format == "excel")
                {
                    WriteGroupsToExcelFile(groups, filename);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(args[1]);
                    if (format == "csv")
                    {
                        WriteGroupsToCsvFile(groups, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteGroupsToXmlFile(groups, writer);
                    }
                    else if (format == "json")
                    {
                        WriteGroupsToJsonFile(groups, writer);
                    }
                    else
                    {
                        System.Console.Out.Write("Unrecognized format" + format);
                    }
                    writer.Close();
                }
            }
            else if (dataType == "contact")
            {
                List<ContactData> contacts = new List<ContactData>();
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new ContactData(TestBase.GenrateRandomString(10), TestBase.GenrateRandomString(10))
                    {
                        Mobile = TestBase.GenrateRandomString(12),
                        HomeTelephone = TestBase.GenrateRandomString(12),
                        Company = TestBase.GenrateRandomString(30),
                        Address = TestBase.GenrateRandomString(30)
                    });
                }

                if (format == "excel")
                {
                    WriteContactsToExcelFile(contacts, filename);
                }
                else
                {
                    StreamWriter writer = new StreamWriter(args[1]);
                    if (format == "csv")
                    {
                        WriteContactsToCsvFile(contacts, writer);
                    }
                    else if (format == "xml")
                    {
                        WriteContactsToXmlFile(contacts, writer);
                    }
                    else if (format == "json")
                    {
                        WriteContactsToJsonFile(contacts, writer);
                    }
                    else
                    {
                        System.Console.Out.Write("Unrecognized format" + format);
                    }
                    writer.Close();
                }
            }
            else 
            {
                System.Console.Out.Write("Unrecognized data type" + dataType);
            }
        }

        #region WriteGroups methods
        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
            {
                foreach (GroupData group in groups)
                {
                    writer.WriteLine(String.Format("${0},${1},${2}",
                        group.Name, group.Header, group.Footer));
                }

            }

            static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
            {
                new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
            }

            static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
            {
                writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
            }

            static void WriteGroupsToExcelFile(List<GroupData> groups, string filename)
            {
                Excel.Application app = new Excel.Application();
                app.Visible = true;
                Excel.Workbook wb = app.Workbooks.Add();
                Excel.Worksheet sheet = wb.ActiveSheet;

                int row = 1;
                foreach (GroupData group in groups)
                {
                    sheet.Cells[row, 1] = group.Name;
                    sheet.Cells[row, 2] = group.Header;
                    sheet.Cells[row, 3] = group.Footer;
                    row++;
                }
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
                File.Delete(fullPath);
                wb.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), filename));

                wb.Close();
                app.Visible = false;
                app.Quit();
            }
        #endregion

        #region WriteContacts methods
        static void WriteContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("${0},${1},${2},${3},${4},${5}",
                    contact.FirstName, contact.LastName, contact.Mobile, contact.HomeTelephone, contact.Company, contact.Address));
            }

        }

        static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteContactsToExcelFile(List<ContactData> contacts, string filename)
        {
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Add();
            Excel.Worksheet sheet = wb.ActiveSheet;

            int row = 1;
            foreach (ContactData contact in contacts)
            {
                sheet.Cells[row, 1] = contact.FirstName;
                sheet.Cells[row, 2] = contact.LastName;
                sheet.Cells[row, 3] = contact.Mobile;
                sheet.Cells[row, 4] = contact.HomeTelephone;
                sheet.Cells[row, 5] = contact.Company;
                sheet.Cells[row, 6] = contact.Address;
                row++;
            }
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            File.Delete(fullPath);
            wb.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), filename));

            wb.Close();
            app.Visible = false;
            app.Quit();
        }
        #endregion


    }
}
