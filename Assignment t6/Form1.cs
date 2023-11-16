using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_t6
{
    public partial class MainForm : Form
    {
        #region Fields Area
        private TaskManager taskManager;
        private string fileName = Application.StartupPath + "\\Tasks.txt";
        #endregion

        #region Program Main functions
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
        }
        private void InitializeGUI()
        {
            this.Text = "To Do Reminder by Ploypailin Junker";
            
            taskManager = new TaskManager();

            dateTimePicker.Value = DateTime.Now; //Set to current date as a default

            toDoListBox.Items.Clear();
            timerTextbox.Text = string.Empty;
            timer1.Start();

            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "yyyy-MM-dd   HH:mm";

            toolTip1.ShowAlways = true;
            toolTip2.ShowAlways = true;

            toDoTextBox.Text = string.Empty;

            string tips = "TO CHANGE: Select an item first!" + Environment.NewLine;
            tips += "Make your changes in the input controls," + Environment.NewLine;
            tips += "Click the change button." + Environment.NewLine;

            toolTip1.SetToolTip(toDoListBox, tips);
            toolTip1.SetToolTip(changeButton, tips);

            string delTips = "Select an item first and then click this button!";
            toolTip1.SetToolTip(deleteButton, delTips);

            string desTips = "Write your sins here!";
            toolTip1.SetToolTip(toDoTextBox, desTips);

            ListBoxEnable();

            InitializeComboBox();
        }
        private void Form1_Load(object sender, EventArgs e)
        { CenterToScreen(); }
        private void InitializeComboBox()//Done
        {
            string[] priorityElement = taskManager.GetPriorityStrings();

            foreach (string item in priorityElement)
            {
                priorityComboBox.Items.Add(item).ToString();
                priorityComboBox.SelectedIndex = 0;
            }
        }
        #endregion

        #region Button Functions
        private void toDoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = toDoListBox.SelectedIndex;

            if (selectedIndex >= 0)
            {
                Task task = taskManager.GetTaskAt(selectedIndex);
                toDoTextBox.Text = task.Description;
                priorityComboBox.SelectedIndex = (int)task.Priority;
            }
        }
        private void newToolSubMenu_Click(object sender, EventArgs e)
        {}
        private void timer1_Tick_1(object sender, EventArgs e)//Done
        {
            timerTextbox.Text = DateTime.Now.ToLongTimeString();
        }
        private void newCtrlNToolStripMenuItem_Click(object sender, EventArgs e)//done
        {
            InitializeGUI();
        }
        private void exitAltF4ToolStripMenuItem_Click(object sender, EventArgs e) //done
        {
            DialogResult dlgResult = MessageBox.Show("Sure to exit program?", "Warning", MessageBoxButtons.YesNo);

            if (dlgResult == DialogResult.Yes)
            { 
                Application.Exit();
            }
        }
        private void addButton_Click(object sender, EventArgs e)//done
        {
            //this object reference from ReadInput 
            Task currentTask = ReadInput();

            if (taskManager.AddTask(currentTask))
            {
                UpdateGUI();

                changeButton.Enabled = true;
                deleteButton.Enabled = true;
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)//done
        {
            int selectedIndex = toDoListBox.SelectedIndex;

            if (selectedIndex != -1)
            {
                DialogResult dlResult = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButtons.YesNo);

                if (dlResult == DialogResult.Yes)
                {
                    taskManager.DeleteTaskAt(selectedIndex);
                    UpdateGUI();
                    toDoTextBox.Text = string.Empty;
                    ListBoxEnable();
                }
            }
        }
        private void changeButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = toDoListBox.SelectedIndex;

            if (selectedIndex >= 0)
            {
                Task currentTask = ReadInput();
                bool validChange = taskManager.ChangeTaskAt(currentTask, selectedIndex);

                if (validChange == true)
                {
                    UpdateGUI();
                }
                else
                {
                    MessageBox.Show("Select one item from the list before delete an item", "Oops!", MessageBoxButtons.OK);
                }
            }
        }
        private void saveDataFileToolStripMenuItem_Click(object sender, EventArgs e)//done
        {
            string errMessage = "Something went wrong while saving data to file";

            bool validSave = taskManager.WriteDataToFile(fileName);

            if (!validSave)
            { MessageBox.Show(errMessage); }
            else
            {
                MessageBox.Show("Data saved to file: " + Environment.NewLine + fileName);
            }

        }
        private void openDataFileToolStripMenuItem_Click(object sender, EventArgs e)//done
        {
            string errMessage = "Something went wrong when opening the file";

            bool validOpen = taskManager.ReadDataFromFile(fileName);

            if (!validOpen)
            { MessageBox.Show(errMessage); }
            else
            { UpdateGUI();}
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
  
            box.ShowDialog();
        }   
        
        #endregion

        #region Manual functions
        private void UpdateGUI()//done
        { 
            //this function is for update GUI everytime user add or clear the programe

            toDoListBox.Items.Clear();

            string[] updateTask = taskManager.GetTaskInfo();

            if (updateTask != null)
            {
                toDoListBox.Items.AddRange(updateTask);  
            }
        }
        private void ListBoxEnable()
        {
            if (toDoListBox.Items.Count > 0)
            {
                changeButton.Enabled = true;
                deleteButton.Enabled = true;
            }
            else
            {
                changeButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }
        #endregion

        #region Read/ validate data Area
        private Task ReadInput()
        {
            Task currentTask = new Task();

            string todoTask = toDoTextBox.Text;

            if (!string.IsNullOrEmpty(todoTask))
            {
                currentTask.Description = todoTask;
                currentTask.TaskDate = dateTimePicker.Value;
                currentTask.Priority = (PriorityType)priorityComboBox.SelectedIndex;
            }
            else
            { 
                MessageBox.Show("Please enter a correct description", "Opps!", MessageBoxButtons.OK);
            }
            return currentTask;
        }
        #endregion

    }
}
