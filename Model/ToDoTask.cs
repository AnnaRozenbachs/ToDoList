using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }

        public string Project { get; set; }

        public string Status { get; set; }

        public bool Done { get; set; }
    }
}
