using TaskManager.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.models
{
    public class ToDo: Item
    {
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            string temp;
            if(IsCompleted)
            {
                temp = "Completed";
            }
            else
            {
                temp = "Incomplete";
            }

            return $"Task:\t\t{Name}\t {Description}\t\t\t\t {Deadline}\t {temp}";
        }
    }
}
