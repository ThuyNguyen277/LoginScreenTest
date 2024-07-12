using System;
using System.Windows.Forms;

namespace LoginScreen
{
    public partial class Form2 : Form
    {
        private string userName;         //Field to store the username

        //Constructor to initialize Form2 with a username parameter
        public Form2(string username)
        {
            InitializeComponent();
            this.userName = username;      //Store the username passed form Form1
        }
        
        //Event handler for Form2 load event
        private void Form2_Load(object sender, EventArgs e)
        {
            //Set the text of label1 to greet the user with their username
            label1.Text = "Hello 【　" + userName + "　】!";
        }

        //Evnet handler for Logout button click
        private void button1_Click_1(object sender, EventArgs e)
        {
            //Prompt the user with a confirmation dialog
            DialogResult result = MessageBox.Show("Are you sure yo want to logout?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            //If the user clicks Yes in the confirmation dialog
            if (result == DialogResult.Yes)
            {
                //Create a new instance of Login Form(Form1)
                Form1 form1 = new Form1();
                form1.Show();       //Show login form
                this.Close();       //Close the Logout Form(Form2)
            }
        }
    }
}
