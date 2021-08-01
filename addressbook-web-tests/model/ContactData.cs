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
        private string allData;
        private string initials;

        public ContactData(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
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
                    return (CleanUp(HomeTelephone) + CleanUp(Mobile) + CleanUp(WorkTelephone)).Trim();
                }
            }
            set { allPhones = value; }
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
                    return (ForSpaces(FirstName) + ForSpaces(MiddleName) + CleanUp(LastName) + CleanUp(Nickname) + CleanUp(Title) + CleanUp(Company) + CleanUp(Address) +
                        CleanUp(HomeTelephone) + CleanUp(Mobile) + CleanUp(WorkTelephone) +  CleanUp(Fax) + CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3) + CleanUp(HomePage) + (BirthDay) + ". " + ForSpaces(BirthMonth) + ForSpaces(BirthYear) + "(" + (2021 - Convert.ToInt32(BirthYear)) + ")" + "\r\n" +(AnniversaryDay) + ". " + ForSpaces(AnniversaryMonth) + ForSpaces(AnniversaryYear) + "(" + (2021 - Convert.ToInt32(AnniversaryYear)) + ")" + "\r\n" + CleanUp(Address2) + CleanUp(HomeTelephone2) + CleanUp(Notes)).Trim();
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

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
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
