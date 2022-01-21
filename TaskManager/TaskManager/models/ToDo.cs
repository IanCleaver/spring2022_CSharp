using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.models
{
    public class ToDo
    {
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                }
            }
        }

        private DateTime deadline;
        public DateTime Deadline
        {
            get
            {
                return deadline;
            }
            set
            {
                if (deadline != value)
                {
                    deadline = value;
                }
            }
        }

        private bool isCompleted;
        public bool IsCompleted
        {
            get
            {
                return isCompleted;
            }
            set
            {
                if (isCompleted != value)
                {
                    isCompleted = value;
                }
            }
        }


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

            return $"{Name}\t {Description}\t\t\t {Deadline}\t {temp}";
        }
    }
}
