/*
 * Austin B & Favour A
 * 
 * Assignment 2
 * 
 * March 11 2024
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
        }

         private void BtnBookappointment_Click(object sender, EventArgs e)
        {
            Appointment appointment = new Appointment(
                textBoxCustomerName.Text,
                textBoxAddress.Text,
                textBoxCity.Text,
                textBoxProvince.Text,
                textBoxPostalCode.Text,
                textBoxHomePhone.Text,
                textBoxCellPhone.Text,
                textBoxEmail.Text,
                textBoxMakeModel.Text,
                textBoxYear.Text,
                richTextBox1.Text,
                dateTimePicker1.Value
            );

            Validationcs validator = new Validationcs();
            ValidationResult validationResult = validator.Validate(appointment, this);

            if (!validationResult.IsValid)
            {
                string errorMessage = string.Join(Environment.NewLine, validationResult.ErrorMessage);
                lblErrorMessages.Text = errorMessage;
                FocusFirstInvalidControl(validationResult.FirstInvalidControl);
                return;
            }
            SaveAppointmentToFile(appointment);
            ResetForm();
        }

        // Pre-fill with data
        private void BtnPrefill_Click(object sender, EventArgs e)
        {
            textBoxCustomerName.Text = "Joe Mama";
            textBoxAddress.Text = "100 King St";
            textBoxCity.Text = "Waterloo";
            textBoxProvince.Text = "ON";
            textBoxPostalCode.Text = "N2A2B3";
            textBoxHomePhone.Text = "123-456-7890";
            textBoxCellPhone.Text = "911";
            textBoxEmail.Text = "joe.mama@gmail.com";
            textBoxMakeModel.Text = "Honda Ridgeline";
            textBoxYear.Text = "2008";
            richTextBox1.Text = "My car exploded!!!";
        }

        // close form
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        // reset form
        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Clear();
                    richTextBox1.Clear();
                    lblErrorMessages.Text = "";
                }
            }
            dateTimePicker1.Value = DateTime.Now;
        }

        // save appointment to text file
        private void SaveAppointmentToFile(Appointment appointment)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appointments.txt");
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(appointment.ToString());
                }
                MessageBox.Show("Appointment saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblErrorMessages.Text = "";
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error saving appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FocusFirstInvalidControl(Control firstInvalidControl)
        {
            firstInvalidControl.Focus();
        }       
    }
}

