using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_t6
{
    internal class FileManager
    {
        #region Fields area 
        private const string fileToken = "ToDoListApp";
        private const double fileNumber = 1.0; 
        #endregion

        #region Manual methods
        public bool ReadTaskListFile(List<Task> list, string fileName)//done
        {
            bool validRead = true;

            //using StreamReader to read text from a file
            StreamReader reader = null;

            try
            {
                //clear or renew the list before reading the file
                if (list != null)
                {
                    list.Clear();
                }
                else
                {
                    list = new List<Task>();
                }

                reader = new StreamReader(fileName);

                //Ensure that it is the correct file
                string versionTest = reader.ReadLine();
                //Ensure that it has the correct version nr.
                double version = double.Parse(reader.ReadLine());

                if ((versionTest == fileToken) && (version == fileNumber)) // if correct file
                {
                    //Read num of items (tasks) to read
                    int count = int.Parse(reader.ReadLine());

                    for (int i = 0; i < count; i++)
                    {
                        Task task = new Task();
                        task.Description = reader.ReadLine();
                        task.Priority = (PriorityType)Enum.Parse(typeof(PriorityType), reader.ReadLine());

                        //DatteTime.Year,Month, etc are readonly!
                        int year = 0, month = 0, day = 0;
                        int hour = 0, minute = 0, second = 0;

                        year = int.Parse(reader.ReadLine());
                        month = int.Parse(reader.ReadLine());
                        day = int.Parse(reader.ReadLine());
                        hour = int.Parse(reader.ReadLine());
                        minute = int.Parse(reader.ReadLine());
                        second = int.Parse(reader.ReadLine());

                        task.TaskDate = new DateTime(year, month,day, hour, minute, second);

                        list.Add(task);
                    }
                }
                else
                { validRead = false; }
            }
            catch
            {validRead = false;}

            finally
            {
                if (reader != null)
                { reader.Close(); }
            }
        
            return validRead;
        }
        public bool SaveTaskListToFile(List<Task> list, string fileName)//done
        {
            bool validSave = true;

            //Using StreamWriter class to write text to a file. Initialized to null first
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(fileName); 
                writer.WriteLine(fileToken);
                writer.WriteLine(fileNumber);
                writer.WriteLine(list.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    //save each iteration in writer

                    writer.WriteLine(list[i].Description);
                    writer.WriteLine(list[i].Priority.ToString());
                    writer.WriteLine(list[i].TaskDate.Year);
                    writer.WriteLine(list[i].TaskDate.Month);
                    writer.WriteLine(list[i].TaskDate.Day);
                    writer.WriteLine(list[i].TaskDate.Hour);
                    writer.WriteLine(list[i].TaskDate.Minute);
                    writer.WriteLine(list[i].TaskDate.Second);
                }
            }
            catch
            {
                validSave = false; //will return false if try is error
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }

            return validSave;
        }
        #endregion

    }
}
