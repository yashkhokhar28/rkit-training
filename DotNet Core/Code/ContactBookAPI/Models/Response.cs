using System.Data;

namespace ContactBookAPI.Models
{
    public class Response
    {
        public string Message { get; set; }

        public bool IsError { get; set; }

        public DataTable Data { get; set; }




    }
}
