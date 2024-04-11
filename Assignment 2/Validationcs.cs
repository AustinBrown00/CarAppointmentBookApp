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
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2
{
    public class Validationcs
    {
        public ValidationResult Validate(Appointment appointment, Mainform mainForm)
        {
            List<string> errorMessages = new List<string>();
            Control firstInvalidControl = null;

            // validate customer name
            if (string.IsNullOrWhiteSpace(appointment.CustomerName))
            {
                errorMessages.Add("Customer name is required.");
                if (firstInvalidControl == null)
                {
                    firstInvalidControl = mainForm.Controls["textBoxCustomerName"];
                }
            }

            // validate postal information if email is not provided
            if (string.IsNullOrWhiteSpace(appointment.Email))
            {
                if (string.IsNullOrWhiteSpace(appointment.Address))
                {
                    errorMessages.Add("Address is required when email is not provided.");
                    if (firstInvalidControl == null)
                    {
                        firstInvalidControl = mainForm.Controls["textBoxAddress"];
                    }
                }

                if (string.IsNullOrWhiteSpace(appointment.City))
                {
                    errorMessages.Add("City is required when email is not provided.");
                    if (firstInvalidControl == null)
                    {
                        firstInvalidControl = mainForm.Controls["textBoxCity"];
                    }
                }

                if (string.IsNullOrWhiteSpace(appointment.ProvinceCode))
                {
                    errorMessages.Add("Province code is required when email is not provided.");
                    if (firstInvalidControl == null)
                    {
                        firstInvalidControl = mainForm.Controls["textBoxProvince"];
                    }
                }

                if (string.IsNullOrWhiteSpace(appointment.PostalCode))
                {
                    errorMessages.Add("Postal code is required when email is not provided.");
                    if (firstInvalidControl == null)
                    {
                        firstInvalidControl = mainForm.Controls["textBoxPostalCode"];
                    }
                }
            }

            // validate province code 
            if (!string.IsNullOrWhiteSpace(appointment.ProvinceCode))
            {
                if (appointment.ProvinceCode.Length != 2)
                {
                    errorMessages.Add("Province code must be exactly two letters.");
                    if (firstInvalidControl == null)
                    {
                        firstInvalidControl = mainForm.Controls["textBoxProvince"];
                    }
                }
                else if (!IsValidProvinceCode(appointment.ProvinceCode))
                {
                    errorMessages.Add("Invalid province code.");
                    if (firstInvalidControl == null)
                    {
                        firstInvalidControl = mainForm.Controls["textBoxProvince"];
                    }
                }
            }

            // validate postal code
            if (!string.IsNullOrWhiteSpace(appointment.PostalCode) && !IsValidPostalCode(appointment.PostalCode))
            {
                errorMessages.Add("Invalid postal code format.");
                if (firstInvalidControl == null)
                {
                    firstInvalidControl = mainForm.Controls["textBoxPostalCode"];
                }
            }

            // validate to make sure one phone number is provided
            if (string.IsNullOrWhiteSpace(appointment.HomePhone) && string.IsNullOrWhiteSpace(appointment.CellPhone))
            {
                errorMessages.Add("At least one phone number (home or cell) must be provided.");
                if (firstInvalidControl == null)
                {
                    firstInvalidControl = mainForm.Controls["textBoxHomePhone"];
                }
            }

            // validate email format
            if (!string.IsNullOrWhiteSpace(appointment.Email) && !IsValidEmail(appointment.Email))
            {
                errorMessages.Add("Invalid email format.");
                if (firstInvalidControl == null)
                {
                    firstInvalidControl = mainForm.Controls["textBoxEmail"];
                }
            }

            // validate make & model
            if (string.IsNullOrWhiteSpace(appointment.MakeAndModel))
            {
                errorMessages.Add("Make && model is required.");
                if (firstInvalidControl == null)
                {
                    firstInvalidControl = mainForm.Controls["textBoxMakeModel"];
                }
            }

            // validate year format and range
            if (!string.IsNullOrWhiteSpace(appointment.Year))
            {
                if (!int.TryParse(appointment.Year, out int year) || year < 1900 || year > DateTime.Now.Year + 1)
                {
                    errorMessages.Add("Invalid year format or out of range.");
                    if (firstInvalidControl == null)
                    {
                        firstInvalidControl = mainForm.Controls["textBoxYear"];
                    }
                }
            }

            // validate appointment date
            if (appointment.AppointmentDate < DateTime.Today)
            {
                errorMessages.Add("Appointment date cannot be in the past.");
            }

            return new ValidationResult(errorMessages, firstInvalidControl);
        }

        // method to validate province code
        public bool IsValidProvinceCode(string provinceCode)
        {
            
            string[] validCodes = { "AB", "BC", "MB", "NB", "NL", "NS", "NT", "NU", "ON", "PE", "QC", "SK", "YT" };
            return validCodes.Contains(provinceCode.ToUpper());
        }

        // method to validate postal code format
        public bool IsValidPostalCode(string postalCode)
        {
            // validates canadian postal codes (A2B 3C4)
            return Regex.IsMatch(postalCode, @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$");
        }

        // method to validate email format
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // method to capitalize first letter of string
        public string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input; 
            else
            {
                string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length > 1)
                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                    else
                        words[i] = words[i].ToUpper();
                }
                return string.Join(" ", words);
            }
        }

        // method to format postal code
        public string FormatPostalCode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input; 
            else
            {
                input = input.ToUpper().Trim(); 
                if (input.Length == 6)
                    return input.Insert(3, " ");
                else
                    return input;
            }
        }

        // method to format phone numbers
        public string FormatPhoneNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            else
            {
                input = input.Trim().Replace("-", ""); 
                if (input.Length == 10)
                    return input.Insert(3, "-").Insert(7, "-");
                else
                    return input;
            }
        }
    }
}

