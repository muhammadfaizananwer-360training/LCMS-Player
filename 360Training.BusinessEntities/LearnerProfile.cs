using System;
using System.Collections.Generic;
using System.Text;

namespace _360Training.BusinessEntities
{
    public class LearnerProfile
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string learnerGUID;

        public string LearningSessionID
        {
            get { return learnerGUID; }
            set { learnerGUID = value; }
        }
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        private string emailAddress;

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }
        private string mobilePhone;

        public string MobilePhone
        {
            get { return mobilePhone; }
            set { mobilePhone = value; }
        }
        private string officePhone;

        public string OfficePhone
        {
            get { return officePhone; }
            set { officePhone = value; }
        }
        private string address1;

        public string Address1
        {
            get { return address1; }
            set { address1 = value; }
        }
        private string address2;

        public string Address2
        {
            get { return address2; }
            set { address2 = value; }
        }
        private string address3;

        public string Address3
        {
            get { return address3; }
            set { address3 = value; }
        }
        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string zipCode;

        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private string country;

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string companyName;

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }
        private string website;

        public string Website
        {
            get { return website; }
            set { website = value; }
        }
        private string brandCode;

        public string BrandCode
        {
            get { return brandCode; }
            set { brandCode = value; }
        }
        private string variant;

        public string Variant
        {
            get { return variant; }
            set { variant = value; }
        }
        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        private string courseName;

        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; }
        }

        private string businessKey;

        public string BusinessKey
        {
            get { return businessKey; }
            set { businessKey = value; }
        }


        public LearnerProfile()
        {
            this.address1 = string.Empty;
            this.address2 = string.Empty;
            this.address3 = string.Empty;
            this.emailAddress = string.Empty;
            this.firstName = string.Empty;
            this.id = 0;
            this.lastName = string.Empty;
            this.learnerGUID = string.Empty;
            this.mobilePhone = string.Empty; 
            this.officePhone = string.Empty;
            this.city = string.Empty;
            this.state = string.Empty;
            this.zipCode = string.Empty;
            this.country = string.Empty;
            this.website = string.Empty;
            this.password = string.Empty;
            this.username = string.Empty;
            this.companyName = string.Empty;
            this.variant = string.Empty;
            this.brandCode = string.Empty;
            this.fullName = string.Empty;
            
        }
    }
}
