using System;
using System.ComponentModel.DataAnnotations;

namespace AzureSecureSqlHelloWorld.Models
{
    public class Student
    {
        [Key]
        public Int32 Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
    }
}
