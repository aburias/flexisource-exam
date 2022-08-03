using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpTemp.Data.Entities
{
    public class Employee : BaseEntity
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Temperature { get; set; } // assume that it's in C
        public DateTime RecordDate { get; set; }
    }
}
