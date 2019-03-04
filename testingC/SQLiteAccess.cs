using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testingC.ProjectClasses
{
    public class SQLiteAccess
    {

        public static void Display()
        {
            Console.WriteLine("Method in other class");
        }

        private static string LoadConnectionString(string id = "Default")
        {
            Console.WriteLine("LoadConnectionString method executed." + ConfigurationManager.ConnectionStrings[id].ConnectionString);
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;

        }

        public static bool SaveAContact(contactClass c)
        {
            bool success = false;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                string obj = c.FirstName;
                Console.WriteLine("object string received for SQLiteAccess class: " + obj);

                int i = cnn.Execute("INSERT INTO CONTACT_DETAILS (FIRST_NAME, LAST_NAME, CONTACT_NUMBER, ADDRESS, GENDER) VALUES (@FirstName," +
                    "@LastName,@ContactNumber,@Address,@Gender)", c);

                if (i > 0)
                {
                    Console.WriteLine("Saved successfully");
                   
                    return success = true;
               
                }
                else
                {
                    Console.WriteLine("Nothing Saved.");
                    return success = false;
                }
            }
        }

        public static List<contactClass> LoadContact()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<contactClass>("SELECT * FROM CONTACT_DETAILS", new DynamicParameters());
                return output.ToList();
            }

        }

        public static bool UpdateAContact(contactClass c)
        {
            bool success = false;
            Console.WriteLine("Update contacts method started successfully");
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                int i = cnn.Execute("UPDATE CONTACT_DETAILS " +
                    "SET FIRST_NAME = @FirstName, LAST_NAME = @LastName, CONTACT_NUMBER = @ContactNumber, ADDRESS = @Address," +
                   "GENDER = @Gender WHERE CONTACT_ID = @ContactID", c);

                if (i > 0)
                {
                    Console.WriteLine("Updated successfully");
                    return success = true;
                }
                else
                {
                    Console.WriteLine("Nothing updated.");
                    return success = false;
                }

            }


        }

        public static bool Delete(contactClass c) {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                bool success = false;
                int i = cnn.Execute("DELETE FROM CONTACT_DETAILS WHERE CONTACT_ID = @ContactID", c);

                cnn.Close();
                if (i > 0)
                {
                    Console.WriteLine("Updated successfully");
                    return success = true;
                }
                else
                {
                    Console.WriteLine("Nothing updated.");
                    return success = false;
                }

                

            }

        }
    }
}
