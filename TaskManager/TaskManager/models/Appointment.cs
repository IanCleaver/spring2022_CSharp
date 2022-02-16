using TaskManager.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.models
{
    public class Appointment: Item
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<String> Attendees { get; set; }

        public override string ToString()
        {
            return $"Appointment:\t{Name}\t {Description}\t\t\t {Start}\t {End}";
        }
    }
}
