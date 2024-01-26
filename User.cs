using Ado_all;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Xml.Serialization;


internal class User : input, Login, Instructions
{

    string query;
    SqlConnection conn = new SqlConnection("Server=LAPTOP-M98CS32R\\SQLEXPRESS; initial catalog=Abhi1;integrated security=true");
    SqlCommand cmd;


    protected static int id { get; set; }
    protected static string st_name { get; set; }
    protected static string Email { get; set; }
    protected static string Password { get; set; }
    protected static Double Mobile { get; set; }

    //-------------------------------------------------------//

    //protected static int Q_id { get; set; }
    //protected static string Questions { get; set; }
    //protected static int option1 { get; set; }
    //protected static int option2 { get; set; }
    //protected static int option3 { get; set; }
    //protected static int option4 { get; set; }

    public void Userinput()
    {
        try
        {


            Console.WriteLine("Enter Your Student Id:");
            id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Your Name:");
            st_name = Console.ReadLine();

            Console.WriteLine("Enter your Email");
            Email = Console.ReadLine();

            Console.WriteLine("Enter Your Password:");
            Password = Console.ReadLine();

            Console.WriteLine("Enter Your Mobile Number");
            Mobile = Convert.ToDouble(Console.ReadLine());


            //------------------------------------------------------//


            //  query = "insert into Student(id,st_name,Email,Password,Mobile) values(@id,@st_name,@Email,@Password,@Mobile)";

            // Stored Procedure
            query = "CreateAccount";
            cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@st_name", st_name);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Mobile", Mobile);

            // Connect
            if (conn.State == ConnectionState.Closed)
            {
               
                conn.Open();
                // check_Duplicate();
                //   Console.WriteLine("You id is Already Exists");

                cmd.ExecuteNonQuery();
                Console.WriteLine("Your Record is saved\n\n");
                // choice();
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                //   UserLogin();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    //Login Data
    public void UserLogin()
    {
        try
        {


            Console.WriteLine("Enter Your Name");
            st_name = Console.ReadLine();
            Console.WriteLine("Enter Your Email");
            Email = Console.ReadLine();
            Console.WriteLine("Enter Your Password");
            Password = Console.ReadLine();

            //disconnect Login

            string Query = "Select * from Student where st_name=@st_name and Email=@Email and Password=@Password";

            cmd = new SqlCommand(Query, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("st_name", st_name);
            cmd.Parameters.AddWithValue("Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine("\n\n\n  \t Hello, {0} Welcome to the Online Quiz System Login Page\n\n", st_name);
                LoginChoice();
            }
            else
            {
               
                Console.WriteLine("\t\t Try Again With the Correct Data\n");
                choice();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    //check duplicate
    public void check_Duplicate()  // Data Already Exists
    {
        try
        {



            query = "Select Email from Student where Email=@Email";
            cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Email", Email);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Console.WriteLine("Already Registered with this Email Id choose new one\n\n");

            }
            
           
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void SelectAllrcd()
    {


        string Query1 = "Select * from Student";
        SqlCommand cmd = new SqlCommand(Query1, conn);
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        using (SqlDataReader sdr = cmd.ExecuteReader())
        {
            while (sdr.Read())
            {
                Console.WriteLine("Id: " + sdr["id"] +  "\tStudent Name: " + sdr["st_name"] +    "\tEmail:    " + sdr["Email"] +    "   \tPassword: " + sdr["Password"] +   "\t  Mobile: " + sdr["Mobile"] + "\n");
            }

        }
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
    }

    public void choice()
    {
        try
        {



            Console.WriteLine("\n\n\n\t\t\tMain Menu");
            Console.WriteLine("\n\n\t 1. Create Account");
            Console.WriteLine("\n\n\t 2. Log in");
            Console.WriteLine("\n\n\t 3. Instructions");
            Console.WriteLine("\n\n\t 4. Admin Panel");
            Console.WriteLine("\n\n\t 5. Exit\n");
            Console.WriteLine("Please Select your option (1-5)\n");

            int ch = Convert.ToInt32(Console.ReadLine());
            switch (ch)
            {
                case 1:
                     Userinput();
                   
                    Console.WriteLine("Hello, {0} Hurrah your Account is created Successfully \n\n", st_name);
                    Console.WriteLine("Now you can select more options to login if Account created");
                    choice();


                    break;

                case 2:
                    UserLogin();
                    Console.WriteLine("\n\n\n  \t Hello, {0} Welcome to the Online Quiz System Login Page\n\n", st_name);

                    break;
                case 3:
                    Console.WriteLine("Instructions");
                    Exam_Instructions();
                    Console.WriteLine("\n If u Read all the instructions then Login Here ");
                    Console.WriteLine("  Login Page is Redirected for U\n");
                    UserLogin();

                    break;
                case 4:
                    Console.WriteLine("\n");
                    AdminChoice();
                    admininterface();

                    break;

                case 5:
                    Console.WriteLine("Exit screen");
                    Environment.Exit(0);
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void LoginChoice()
    {
        try
        {

            Console.WriteLine("\n\t\tLogin Page\n");
            Console.WriteLine("\n\t\t 1. Exam Instructions");
            Console.WriteLine("\n\t\t 2. Start the Quiz");
            //     Console.WriteLine("\n\t\t 3. View All Registered Students ");// Select all rcds
            Console.WriteLine("\n\t\t 3. Forget the Email And Password");
            Console.WriteLine("\n\t\t 4. Main menu \n");
            Console.WriteLine("Please Select the option from (1-4) \n");
            int ch2 = Convert.ToInt32(Console.ReadLine());

            switch (ch2)
            {
                case 1:
                    Exam_Instructions();
                    Console.WriteLine("----------------------------------------------------------------");
                    Console.WriteLine(" You've read all the instructions so plz Start the quiz as u want \n");
                    Console.WriteLine("Otherwise you can select More options from (1-4)");
                    LoginChoice();


                    break;
                case 2:
                    Console.WriteLine("All the best Students \n");
                    Exam_Questions();
                    //   Console.WriteLine();
                    break;
                case 3:
                    Update();
                    choice();
                    break;
                //case 4:
                //    Console.WriteLine("Check Result: It is  Inactive Now");
                //    break;

                case 4:
                    Console.WriteLine("Main Menu Page");
                    choice();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }
    public void Exam_Instructions()
    {
        Console.WriteLine("Exam Instructions\n");
        Console.WriteLine("\t Welcome to the Online Quiz game\n");
        Console.WriteLine("\t Please folllow the given Instructions below\n");
        Console.WriteLine("\n\t Step 1: Quiz Contains total 10 Questions \n");
        Console.WriteLine("\t Step 2:  you will given 1 marks for each right answer \n");
        Console.WriteLine("\t Step 3: There will be no negative (-ve) marking \n");
        Console.WriteLine("\tStep 4: Answer only in one words\n");
        Console.WriteLine("Warning::: Hit the ENTER key After Answer the Question\n");
        Console.WriteLine("\t  Begin the Quiz\n");

        Console.WriteLine("\t All the Best Students\n");
        Console.WriteLine("\n\t\t\t*****START THE QUIZ*****\t\n");
        //Console.ReadLine();
    }
    //  char option;

    public void Exam_Questions()
    {
        try
        {


            int score = 0;



            Console.WriteLine(" Q1. How many Bytes are stored by ‘Long’ Data type in C# .net?");
            Console.WriteLine("A. 8 bytes\t B. 4 bytes \t C. 2 bytes\t D. 1 byte\n");
            char option;
            option = Convert.ToChar(Console.ReadLine());
            if (option == 'a' || option == 'A')
            {
                Console.WriteLine(" Absolutely Right Answer {0} \n", st_name);
                score = score + 10;
            }
            else
            {
                Console.WriteLine("Sorry! {0} Wrong Answer \n", st_name);
                Console.WriteLine("Correct Answer is:  8 bytes \n");
                score = score + 0;
                Console.ReadLine();

            }

            Console.WriteLine("Q2. Who is the founder of C# programming language?");
            Console.WriteLine("A. Brendan Eich \t B. Douglas Crockford\t C. Anders Hejlsberg\t D. Rasmus Lerdorf\n");
            char op;
            op = Convert.ToChar(Console.ReadLine());
            if (op == 'c' || op == 'C')
            {
                Console.WriteLine("Absolutely Right Answer {0} \n", st_name);
                Console.WriteLine("Explanation: C# programming language is developed by Anders Hejlsberg. \n");
                score = score + 10;
            }
            else
            {
                Console.WriteLine("Sorry ! {0} Wrong Answer\n", st_name);
                Console.WriteLine("Correct Answer: Anders Hejlsberg\n");
                score = score + 0;
            }

            Console.WriteLine("Q3. What will be the output of following C# code ? \n               using System;\r\n\r\nnamespace MyApplication {\r\n  class Program {\r\n    static void Main(string[] args) {\r\n      double x = 10.25;\r\n      Console.Write(Convert.ToInt32(x));\r\n    }\r\n  }\r\n}\r\n");
            Console.WriteLine("A. 10.30\t B. 10.25\t C. 10\t D. Error \n");

            char op1 = Convert.ToChar(Console.ReadLine());
            if (op1 == 'c' || op1 == 'C')
            {
                Console.WriteLine("Absolutely Right Answer {0}\n", st_name);
                Console.WriteLine("Explanation:  In the above C# code, we are using Convert.ToInt32() method which converts double to int. Thus, the output will be \"10\".\n");
                score = score + 10;
            }
            else
            {
                Console.WriteLine("Sorry ! {0} Wrong Answer\n", st_name);
                Console.WriteLine("Right Answer is: 10\n");
                score = score + 0;
            }

            Console.WriteLine("Q4. What will be the output of the following C# code, if the input is 123? \n");
            Console.WriteLine("\t using System;\r\n\r\nnamespace MyApplication {\r\n  class Program {\r\n    static void Main(string[] args) {\r\n      Console.WriteLine(\"Enter a number:\");\r\n      int num = Console.ReadLine();\r\n      Console.WriteLine(\"Given number is: \" + num);\r\n    }\r\n  }\r\n}  \n");
            Console.WriteLine(" A. Given number is:123\t B. Given number is: 123\tC. Given number is: \"123\"\tD.  Error \n");

            char op2 = Convert.ToChar(Console.ReadLine());
            if (op2 == 'd' || op2 == 'D')
            {
                Console.WriteLine("Absolutely Right Answer {0} \n", st_name);
                Console.WriteLine("Explanation: In C#, Console.ReadLine() is used to read the string and here we are trying to input an integer value. Thus, the output will be an error.\n ");
                Console.WriteLine(" Cannot implicitly convert type `string' to `int'\r\n");

                score = score + 10;
            }
            else
            {
                Console.WriteLine("Sorry !{0} Wrong Answer \n", st_name);
                Console.WriteLine("Correct option is: D\n");
                score = score + 0;
            }
            Console.WriteLine("Q5. What will be the output of the following C# Code:\n");
            Console.WriteLine("using System;\r\n\r\nnamespace MyApplication {\r\n  class Program {\r\n    static void Main(string[] args) {\r\n      int a = 10, b = 20;\r\n      Console.WriteLine(\"{0},{0}\", a, b);\r\n    }\r\n  }\r\n}    \n");
            Console.WriteLine("A. 10,10\t B. 10,20 \t C. 20,20 \t D. Error \n");

            char op3 = Convert.ToChar(Console.ReadLine());
            if (op3 == 'A' || op3 == 'a')
            {
                Console.WriteLine("Absolutely Right Answer {0} \n", st_name);
                Console.WriteLine("Explanation:In the above code, there are two variables a and b, but while printing the values of a and b, we are using the same placeholder {0}, that will print the value of the first variable. Thus, the output is 10,10. \n");
                score = score + 10;
            }
            else
            {
                Console.WriteLine("Sorry !{0} Wrong Answer\n", st_name);
                Console.WriteLine("Correction option is: A \n");
                score = score + 0;
            }
            Console.WriteLine("Q6. Can we use == operator to compare two strings?\r\n\r\n");
            Console.WriteLine("A. Yes\t B. No\n");

            char op4 = Convert.ToChar(Console.ReadLine());
            if (op4 == 'A' || op4 == 'a')
            {
                Console.WriteLine("Absolutely Right Answer {0}\n", st_name);
                score = score + 10;
            }
            else
            {
                Console.WriteLine(" Sorry ! {0} Wrong Answer\n", st_name);
                Console.WriteLine("Correct option is A\n ");
                score = score + 0;
            }
            Console.WriteLine("Q7. Which of the access specifier is used in interface in C#?\r\n\r\n");
            Console.WriteLine("A. private\t B. public\t C. protected\t D. All of the Above\n");

            char op5 = Convert.ToChar(Console.ReadLine());
            if (op5 == 'B' || op5 == 'b')
            {
                Console.WriteLine("Absolutely Right Answer {0}\n", st_name);
                score = score + 10;
            }
            else
            {
                Console.WriteLine("Sorry ! {0} Wrong Answer\n", st_name);
                Console.WriteLine("Correct option is B\n");
                score = score + 0;
            }
            // score = option + op + op1 + op2 + op3 + op4 + op5;
            // Console.WriteLine("Scorecard: "+score);
            Console.WriteLine("Q8. A C# pointer is used to store the ___ of another type \n");
            Console.WriteLine("A. Value\t B. Memory Addess\t C. Size of the type\t D. Reference of the variable\n");

            char op6 = Convert.ToChar(Console.ReadLine());
            if (op6 == 'B' || op6 == 'b')
            {
                Console.WriteLine("Absolutely Right Answer {0} \n", st_name);
            }
            else
            {
                Console.WriteLine("Sorry ! {0} Wrong Answer \n", st_name);
                Console.WriteLine("Correct option is: B \n");
            }

            Console.WriteLine("Q9. Which is the first line of a C# program?\r\n\r\n");
            Console.WriteLine("A. using System;\t B. using system;\t C. using Namespace\t D. None of these\n");

            char op7 = Convert.ToChar(Console.ReadLine());
            if (op7 == 'A' || op7 == 'a')
            {
                Console.WriteLine("Absolutely Right Answer {0} \n", st_name);
            }
            else
            {
                Console.WriteLine("Sorry ! {0} Wrong Answer \n", st_name);
                Console.WriteLine("The Right option is: A \n");
            }

            Console.WriteLine("Q10. Which is the correct for() statement to run an infinite loop?\r\n\r\n");
            Console.WriteLine("A. for(;;)\t B. for(;;);\t C. for(1;1;1)\t D. None of these \n");

            char op8 = Convert.ToChar(Console.ReadLine());
            if (op8 == 'A' || op8 == 'a')
            {
                Console.WriteLine("Absolutely Right Answer {0} \n", st_name);
            }
            else
            {
                Console.WriteLine("Sorry ! {0} Wrong Answer \n");
                Console.WriteLine("The correct option is A \n");
            }
            Console.WriteLine("THANK U ! {0} to choose this Quiz, Hope You Really Enjoyed It \n", st_name);
            choice();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    //-------------------------------------Admin Login-------------------------------------------------------------//
    public void AdminLogin()
    {
        try
        {
            Console.WriteLine("\nEnter Username");
            string Adm_username = Console.ReadLine();
            Console.WriteLine("\nEnter Your Password");
            string Adm_password = Console.ReadLine();

            if (Adm_username == "Admin" && Adm_password == "Abhi")
            {
                Console.WriteLine("\n Welcome to Admin Panel {0}", Adm_username);
            }
            else
            {
                Console.WriteLine("\nYou're not a Admin go back to the Main Menu");
                choice();

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void AdminChoice()
    {
        try
        {


            Console.WriteLine("\n\t\t Admin Login Interface \n ");
            Console.WriteLine("\t\t\t 1. Adminstrator Login \n");
            Console.WriteLine("\t\t\t 2. Go to Home ");
            int ch3 = Convert.ToInt32(Console.ReadLine());
            switch (ch3)
            {
                case 1:
                    AdminLogin();
                    break;
                case 2:
                    choice();
                    break;

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public void admininterface()
    {
        try
        {
            Console.WriteLine("\n\t\t\t 1. Select all user Data");
            Console.WriteLine("\n\t\t\t 2. Delete Any User");
            Console.WriteLine("\n\t\t\t 3. Exit ");
            int ch4 = Convert.ToInt32(Console.ReadLine());
            switch (ch4)
            {
                case 1:
                    //  AdminChoice();
                    Console.WriteLine("\n-----------------------------------------------------");
                    Console.WriteLine("All Student Records are::\n");
                    SelectAllrcd();
                    admininterface();
                    break;
                case 2:
                    Delete_record();
                    Console.WriteLine("\n Select All Record to Verify that the Record is Deleted or not\n");
                    admininterface();
                    break;

                case 3:
                    Console.WriteLine("\n Return to the Home Menu \n");
                    choice();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // Update Record
    public void Update()
    {
        try
        {
            Console.WriteLine("Enter Your Email Id\n");
            Email = Console.ReadLine();
            Console.WriteLine("Enter Your Password\n");
            Password = Console.ReadLine();
            Console.WriteLine("Enter Your Student Id:");
            id = Convert.ToInt32(Console.ReadLine());

            string query2;
            query2 = "update student set Email=@Email,Password=@Password where id=@id;\r\n";
            cmd = new SqlCommand(query2, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@id", id);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Your Record has been Successfully updated\n");
            }
            if (conn.State == ConnectionState.Open)
            {
                Console.WriteLine("Enter the correct details");
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Delete_record()
    {
        try
        {
            Console.WriteLine("Enter a student id to delete that User\n");
            id = Convert.ToInt32(Console.ReadLine());
            string query3;
            query3 = "Delete from Student where id=@id";
            cmd = new SqlCommand(query3, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("\n Record is Deleted Successfully\n");
            }
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    //protected static string Name { get; set; }
    //protected static string Email_id { get; set; }
    //protected static Double Mobile_no { get; set; }
    //protected static string feedback_message { get; set; }

}