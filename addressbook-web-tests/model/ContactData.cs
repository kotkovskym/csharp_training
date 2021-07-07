using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    public class ContactData
    {
        private string firstname;
        private string lastname;
        private string middlename = "";
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
        private string photo = "";

        public ContactData(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public string MiddleName
        {
            get { return middlename; }
            set { middlename = value; }
        }

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string HomeTelephone
        {
            get { return hometelephone; }
            set { hometelephone = value; }
        }

        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        public string WorkTelephone
        {
            get { return worktelephone; }
            set { worktelephone = value; }
        }

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }


        public string Email2
        {
            get { return email2; }
            set { email2 = value; }
        }

        public string Email3
        {
            get { return email3; }
            set { email3 = value; }
        }

        public string HomePage
        {
            get { return homepage; }
            set { homepage = value; }
        }

        public string BirthDay
        {
            get { return bday; }
            set { bday = value; }
        }

        public string BirthMonth
        {
            get { return bmonth; }
            set { bmonth = value; }
        }

        public string BirthYear
        {
            get { return byear; }
            set { byear = value; }
        }

        public string AnniversaryDay
        {
            get { return aday; }
            set { aday = value; }
        }

        public string AnniversaryMonth
        {
            get { return amonth; }
            set { amonth = value; }
        }

        public string AnniversaryYear
        {
            get { return ayear; }
            set { ayear = value; }
        }

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        public string Address2
        {
            get { return address2; }
            set { address2 = value; }
        }

        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }

        public string HomeTelephone2
        {
            get { return phone2; }
            set { phone2 = value; }
        }

        public string Photo
        {
            get { return photo; }
            set { photo = value; }
        }
    }
}
