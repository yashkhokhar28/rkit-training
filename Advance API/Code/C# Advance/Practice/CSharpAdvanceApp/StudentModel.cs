using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanceApp
{
    public class StudentModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int Grade { get; set; }

        public List<string> Subjects { get; set; } = new List<string>();
    }
}
