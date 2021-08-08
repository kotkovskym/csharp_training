using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace addressbook_web_tests
{
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
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Id { get; set; }
        public string MiddleName { get; set; }

        public string Nickname { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string HomeTelephone { get; set; }

        public string Mobile { get; set; }

        public string WorkTelephone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }


        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string HomePage { get; set; }

        public string BirthDay { get; set; }

        public string BirthMonth { get; set; }

        public string BirthYear { get; set; }

        public string AnniversaryDay { get; set; }

        public string AnniversaryMonth { get; set; }

        public string AnniversaryYear { get; set; }

        public string Group { get; set; }

        public string Address2 { get; set; }

        public string Notes { get; set; }

        public string HomeTelephone2 { get; set; }

        public string Photo { get; set; }

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
    }
}
