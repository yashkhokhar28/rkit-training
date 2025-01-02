using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CSharpAdvanceApp
{
    public class DynamicTypeDemo
    {
        public void RunDynamicTypeDemo()
        {
            dynamic value = 5;
            Console.WriteLine(value + 10); // 15

            value = "Hello";
            Console.WriteLine(value.ToUpper()); // HELLO

            string jsonString = "{\"Name\":\"Alice\",\"Age\":25}";
            dynamic person = JsonConvert.DeserializeObject(jsonString);
            Console.WriteLine(person.Name); // Alice
            Console.WriteLine(person.Age);  // 25

            dynamic excel = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
            excel.Visible = true;
            excel.Workbooks.Add();
            dynamic workbook = excel.Workbooks[1];
            dynamic sheet = workbook.Sheets[1];
            sheet.Cells[1, 1].Value = "Hiii";

            // Reflection
            // Method
        }
    }


}
