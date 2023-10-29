using System;
using System.Collections.Generic;

namespace MinimalApi
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }
}
