using System;
using System.ComponentModel.DataAnnotations;

namespace EmpTemp.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required]
        public string EmployeeNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public float Temperature { get; set; }
        [Required]
        public DateTime RecordDate { get; set; }
    }
}
