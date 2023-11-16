using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_t6
{
    internal class TaskManager
    {
        #region Fields area
        private Task currentTask = new Task(); 
        private List<Task> tasks;

        #endregion

        #region Manual functions
        public TaskManager()
        { 
            tasks = new List<Task>();
        }
        public bool AddTask(Task tasks) //done
        {
            bool validAdd = false;

            if (tasks != null)
            {
                this.tasks.Add(tasks);
                validAdd = true;
            }

            return validAdd;
        }
        public bool ChangeTaskAt(Task tasks, int index) //done
        {
            bool validChange = false;

            if ((CheckIndex(index)) && (tasks != null))
            {
                this.tasks[index] = tasks;
                validChange = true;
            }
            return validChange;
        }
        private bool CheckIndex(int index) //done
        {
           return index >= 0 && index < tasks.Count;
        }
        public bool DeleteTaskAt(int index) //done
        {
            bool validDelete = false;

            if (CheckIndex(index))
            {
                tasks.RemoveAt(index);
                validDelete = true;
            }
            return validDelete;
        }
        public Task GetTaskAt(int index)//done
        {
            if (CheckIndex(index))
            { 
                Task currentTask = tasks[index];
                Task copyTask = new Task(currentTask);
                return copyTask;
            }
            return null;
        }
        public string[] GetTaskInfo()
        {
            string[] taskOut = new string[tasks.Count];

            for (int i = 0; i < taskOut.Length; i++)
            {
                taskOut[i] = tasks[i].ToString();
            }
            return taskOut;
        }
        public bool WriteDataToFile(string fileName)//done
        {
            FileManager fileManager = new FileManager();
            return fileManager.SaveTaskListToFile(tasks, fileName);
        }
        public bool ReadDataFromFile(string fileName)//done
        {
            FileManager fileManager = new FileManager();
            return fileManager.ReadTaskListFile(tasks, fileName);
        }
        public string[] GetPriorityStrings()//done
        {
            string[] priorityElement = Enum.GetNames(typeof(PriorityType));

            for (int i = 0; i < priorityElement.Length; i++)
            {
                priorityElement[i] = priorityElement[i].Replace("_", " ");
            }
            return priorityElement;
        }
        #endregion

        #region Property Area
        public Task GainTask
        {
            get { return currentTask; }
        }
        #endregion
    }
}
