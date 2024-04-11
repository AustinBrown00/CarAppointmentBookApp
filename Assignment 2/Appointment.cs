/*
 * Austin B & Favour A
 * 
 * Assignment 2
 * 
 * March 11 2024
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2
{
    public class Appointment
    {
        public string CustomerName { get; }
        public string Address { get; }
        public string City { get; }
        public string ProvinceCode { get; }
        public string PostalCode { get; }
        public string HomePhone { get; }
        public string CellPhone { get; }
        public string Email { get; }
        public string MakeAndModel { get; }
        public string Year { get; }
        public string Text { get; }
        public DateTime AppointmentDate { get; }

        public Appointment(
            string customerName,
            string address,
            string city,
            string provinceCode,
            string postalCode,
            string homePhone,
            string cellPhone,
            string email,
            string makeAndModel,
            string year,
            string text,
            DateTime appointmentDate)
        {
            Validationcs validator = new Validationcs();
            CustomerName = validator.Capitalize(customerName);
            Address = validator.Capitalize(address);
            City = validator.Capitalize(city);
            ProvinceCode = provinceCode.ToUpper(); 
            PostalCode = validator.FormatPostalCode(postalCode);
            HomePhone = validator.FormatPhoneNumber(homePhone); 
            CellPhone = validator.FormatPhoneNumber(cellPhone);
            Email = email.ToLower(); 
            MakeAndModel = validator.Capitalize(makeAndModel);
            Year = year;
            Text = text;
            AppointmentDate = appointmentDate;

        }
        public override string ToString()
        {
            return $"{AppointmentDate:MMMM dd, yyyy}|{CustomerName}|{Address}|{City}|{ProvinceCode}|{PostalCode}|{HomePhone}|{CellPhone}|{Email}|{MakeAndModel}|{Year}|{Text}";
        }
    }
}
