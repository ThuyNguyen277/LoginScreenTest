using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace LoginScreen
{
    public partial class Form1 : Form
    {
        //Dictionary to store username-password pairs
        private Dictionary<string, string> users = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();

            //Add a default user for testing 
            users.Add("Ttn", HashPassword("12348765"));
        }

        //Event handler for Login button click
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            //Retrieve username and password input from TextBoxes
            string username = txtUserName.Text;
            string password = txtpassword.Text;

            //Validate user credentials
            if (IsValidUser(username, password))
            {
                OpenForm2(username);        //Open Form2 with the username on successful login
            }
            else
            {
                //Prompt user to register if login fails
                DialogResult result = MessageBox.Show("The User name or password you entered is incorrect. Do you want to register an account?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                //Clear input fields and focus on username TextBox
                txtUserName.Clear();
                txtpassword.Clear();
                txtUserName.Focus();
            }
        }

        //Open Form2 with the username and display it
        private void OpenForm2(string username)
        {
            Form2 form2 = new Form2(username);
            form2.Show();

            //Hide the current Form1 login form
            this.Hide();
        }

        //Validate user credentials
        private bool IsValidUser(string username, string password)
        {
            //Check if the username exists in the users dictionary
            if (users.ContainsKey(username))
            {
                string storedHash = users[username];            //Gets the hashed password from the dictionary
                string enteredHash = HashPassword(password);    //Hash the entered password
                
                //Compare the stored hashed password with the entered hashed password
                return storedHash == enteredHash;
            }
            return false;       //Return false if username doesnt exist in the dictionary
        }

        //Event handler for the click event of Clear button
        private void label4_Click(object sender, EventArgs e)
        {

            //Clear the username and password TextBox
            txtUserName.Clear();
            txtpassword.Clear();

            //Set focus back to the username TextBox
            txtUserName.Focus();
        }

        //Event handler for the click event of Exit button
        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();     //Exit the application
        }

        //Event handler for Register button click
        private void buttonRegister_Click(object sender, EventArgs e)
        {
            //Initialize Form3 with he users dictionary and display it 
            Form3 form3 = new Form3(users);
            form3.Show();       //Show Register Form
        }

        //Hash password using SHA256 algorithm
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                //Compute hash of password bytes
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                //Convert hash bytes to a hexadicimal string representation
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));        // Convert bytes to hexadecimal string
                }
                return builder.ToString();      //Return the hashed password as a string 
            }
        }

       
    }
}
