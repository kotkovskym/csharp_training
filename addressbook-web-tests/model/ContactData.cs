using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinqToDB.Mapping;


namespace addressbook_web_tests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        /*private string middlename = "";
        private string nickname = "";
        private string title = "";
        private string company = "";
        private string address = "";
        private string hometelephone = "";
        private string mobile = "";
        private string worktelephone = "";
        private string fax = "";
        private string email = "";
        private string email2 = "";
        private string email3 = "";
        private string homepage = "";
        private string bday = "";
        private string bmonth = "-";
        private string byear = "";
        private string aday = "";
        private string amonth = "-";
        private string ayear = "";
        private string group = "[none]";
        private string address2 = "";
        private string phone2 = "";
        private string notes = "";
        private string photo = "";*/
        private string allPhones;
        private string allEmails;
        private string allData;
        private string initials;

        public ContactData(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public ContactData()
        {
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return FirstName == other.FirstName
            && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return ("First Name and Last Name=" + FirstName + " "  + LastName);
        }
        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            
            if (LastName.CompareTo(other.LastName) == 0)
            {
                return FirstName.CompareTo(other.FirstName);
            }

            return LastName.CompareTo(other.LastName);
        }

        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }

        [Column(Name = "middlename")]
        public string MiddleName { get; set; }

        [Column(Name = "nickname")]
        public string Nickname { get; set; }

        [Column(Name = "title")]
        public string Title { get; set; }

        [Column(Name = "company")]
        public string Company { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomeTelephone { get; set; }

        [Column(Name = "mobile")]
        public string Mobile { get; set; }

        [Column(Name = "work")]
        public string WorkTelephone { get; set; }

        [Column(Name = "fax")]
        public string Fax { get; set; }

        [Column(Name = "email")]
        public string Email { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "homepage")]
        public string HomePage { get; set; }

        [Column(Name = "bday")]
        public string BirthDay { get; set; }

        [Column(Name = "bmonth")]
        public string BirthMonth { get; set; }

        [Column(Name = "byear")]
        public string BirthYear { get; set; }

        [Column(Name = "aday")]
        public string AnniversaryDay { get; set; }

        [Column(Name = "amonth")]
        public string AnniversaryMonth { get; set; }

        [Column(Name = "ayear")]
        public string AnniversaryYear { get; set; }

        public string Group { get; set; }

        [Column(Name = "address2")]
        public string Address2 { get; set; }

        [Column(Name = "notes")]
        public string Notes { get; set; }

        [Column(Name = "phone2")]
        public string HomeTelephone2 { get; set; }

        [Column(Name = "photo")]
        public string Photo { get; set; }

        [Column (Name = "deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomeTelephone) + CleanUp(Mobile) + CleanUp(WorkTelephone) + CleanUp(HomeTelephone2)).Trim();
                }
            }
            set { allPhones = value; }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set { allEmails = value; }
        }

        public string AllData
        {
            get
            {
                if (allData != null)
                {
                    return allData;
                }
                else
                {
                    return ForSpaces(FirstName) + ForSpaces(MiddleName) + CleanUp(LastName) + CleanUp(Nickname) + Block(CleanUp(Title) + CleanUp(Company) + CleanUp(Address)) + Block(PreparePhone(CleanUpPhone(HomeTelephone)) + PreparePhone(CleanUpPhone(Mobile)) + PreparePhone(CleanUpPhone(WorkTelephone)) + PreparePhone(CleanUpPhone(Fax))) + Block(CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)) + CleanUp(HomePage) + Block(PrepareDay(BirthDay) + ForSpaces(BirthMonth) + PrepareYear(BirthYear) + PrepareDay(AnniversaryDay) + ForSpaces(AnniversaryMonth) + PrepareYear(AnniversaryYear)) + Block(CleanUp(Address2)) + Block(PreparePhone(CleanUpPhone(HomeTelephone2))) + CleanUp(Notes);
                }
            }
            set { allData = value; }
        }

        public string Initials
        {
            get
            {
                if (initials != null)
                {
                    return initials;
                }
                else
                {
                    return ((FirstName) + (LastName)).Trim();
                }
            }
            set { initials = value; }
        }

        private string CleanUpPhone(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        private string PreparePhone(string phone)
        {
            if (phone == CleanUpPhone(Mobile))
            {
                return "M: " + phone;
            }
            else if(phone == CleanUpPhone(HomeTelephone))
            {
                return "H: " + phone;
            }
            else if(phone == CleanUpPhone(Fax))
            {
                return "F: " + phone;
            }
             else if (phone == CleanUpPhone(WorkTelephone))
            {
                return "W: " + phone;
            }
            else if (phone == CleanUpPhone(HomeTelephone2))
            {
                return "P: " + phone;
            }
            return "";
            
        }

        private string CleanUp(string data)
        {
            if (data == null || data == "")
            {
                return "";
            }
            if (data == HomePage)
            {
                return "Homepage:" + "\r\n" + data + "\r\n\r\n";
            }
            if (data == Notes)
            {
                return (data + "\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
            }
            return data + "\r\n";
        }

        private string Block(string data)
        {
            if (data == null || data == "")
            {
                return "";
            }
            return data + "\r\n";
        }

        private string PrepareDay(string day)
        {
            if (day == null || day == "")
            {
                return "";
            }
            if (day == BirthDay)
            {
                return "Birthday " + day + ". ";
            }
            if (day == AnniversaryDay)
            {
                return "Anniversary " + day + ". ";
            }
            return "";
        }

        private string PrepareYear(string year)
        {
            if (year == null || year == "")
            {
                return "";
            }
            return year + " (" + (2021 - Convert.ToInt32(year)) + ")" + "\r\n";
        }


        private string ForSpaces(string initials)
        {
            if (initials == null || initials == "")
            {
                return "";
            }
            return initials + " ";
        }

        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
    }
}
