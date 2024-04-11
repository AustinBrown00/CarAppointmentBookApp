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
using System.Windows.Forms;

namespace Assignment_2
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string ErrorMessage { get; }
        public Control FirstInvalidControl { get; }

        public ValidationResult(List<string> errorMessages, Control firstInvalidControl)
        {
            IsValid = errorMessages.Count == 0;
            ErrorMessage = string.Join(Environment.NewLine, errorMessages);
            FirstInvalidControl = firstInvalidControl;
        }
    }
}
