using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_t6
{
    internal class Task
    {
        #region Fields area
        private PriorityType priority;
        private DateTime date;
        private string description;

        #endregion

        #region Manual functions
        public Task() //default constructor, done
        { 
            priority = PriorityType.Very_important; //always set on very important as a default
        }
        public Task(DateTime taskDate) : this(taskDate, string.Empty, PriorityType.Very_important)
        {
            //Chain calling on the other constructor
        }
        public Task (Task theOther) //copy constructor
        { 
            this.priority = theOther.priority;
            this.date = theOther.date;  
            this.description = theOther.description;
        }
        public Task(DateTime date, string description, PriorityType priority)
        {
            this.priority = priority;
            this.date = date;
            this.description = description;
        }
        public override string ToString()//done
        {
            //modify our own string to be displayed as an output
            string stringOut = $"{date.ToLongDateString(), -20} {GetTimeString(), 50} {GetPriorityString(), 29} {description, 42}";

            return stringOut;
        }
        private string GetTimeString()//done
        {
            return date.ToString("hh:mm");
        }
        public string GetPriorityString()//done
        {
            string strPriority = priority.ToString();

            strPriority = strPriority.Replace("_", " ");
            return strPriority;
        }
        #endregion 

        #region Property Area 
        public PriorityType Priority 
       { 
            get { return this.priority; } 
            set { this.priority = value; }
       }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public string Description
        {
            get { return description; }
            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    description = value; 
                }    
            }
        }
        public DateTime TaskDate
        {
            get { return date; }
            set { date = value; }
        }
        #endregion

    }
}
