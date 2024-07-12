using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace LoginScreen
{
    public partial class Form3 : Form
    {
        private Dictionary<string, string> users;       //Dictionary to store username & password pairs

        //Constructor to initialize Form3 with users dictionary
        public Form3(Dictionary<string, string> users)
        {
            InitializeComponent();
            this.users = users;         //Assign the passed users dictionary to the local field
        }


        //Event handler for Register click event
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;     //Retrieve username from TextBox
            string password = txtpassword.Text;     //Retrieve password from TextBox

            //Check if username or password is empty 
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;     //Exit method if either username or password is empty
            }

            //Check if username already exists in the users dictionary
            if (users.ContainsKey(username))
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Clear();
                txtpassword.Clear();
                txtUserName.Focus();
                return;         //Exit mehod if username already exists 
            }

            //Validate password complexity 
            if (!ValidatePassword(password))
            {
                return;     //Exit method if password doesnt meet complexity requirements
            }

            //Hash the password before string 
            string hashedPassword = HashPassword(password);
            users.Add(username, hashedPassword);        //Add username and hashed password to users dictionary

            //show register success message
            MessageBox.Show("Registration successful! You can now login with your new account.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Show Login form and hide Register Form
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        
        //Event handler for Clear button click event 
        private void label4_Click(object sender, EventArgs e)
        {
            //Clear username and password TextBoxes
            txtUserName.Clear();
            txtpassword.Clear();

            //Set focus to username TextBox
            txtUserName.Focus();
        }

        //Hash password using SHA256 algorithm
        private string HashPassword(string password)
        {
            using (SHA256  sha256 = SHA256.Create())
            {
                //Compute hash of password bytes
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Compute hash of password bytes
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));    //Convert bytes to hexadecimal string
                }
                return builder.ToString();      //Return hashed password
            }
        }

        //Validate password complexity 
        private bool ValidatePassword(string password)
        {
            //Check if password length is at least 8 character
            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;       //Return false if password length is less than 8
            }

            //Check complexity requirements: Must have characters from at least 2 of the following categories
            int complexityCount = 0;
            if (password.Any(char.IsDigit)) complexityCount++;      //Contains numeric characters 
            if (password.Any(char.IsLower)) complexityCount++;      //Contains lowercase letters
            if (password.Any(char.IsUpper)) complexityCount++;      //Contains uppercase letters 

            //If complesxity count is less than 2, show error message and return false 
            if (complexityCount < 2)
            {
                MessageBox.Show("Password must contain characters from at least 2 of the the following categories: numeric, lowercase, uppercase", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;        //Return true if password meets complexity requirements
        }
    }
}
