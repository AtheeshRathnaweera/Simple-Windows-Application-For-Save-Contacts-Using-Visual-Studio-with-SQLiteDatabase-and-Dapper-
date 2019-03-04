using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testingC.ProjectClasses
{
    public class contactClass
    {
        //Getters and Setters
        //Work as a data carrier
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        public string FullName
        {
            get {
                return $"{ ContactID } { FirstName } { LastName }";
            }
        }

        public override string ToString()
        {
            return string.Format("My name is {0} {1} .This is my id: {2}.", FirstName, LastName, ContactID);
        }

    }
 
    }
