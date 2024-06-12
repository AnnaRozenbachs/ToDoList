using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using ToDoList.Model;

namespace ToDoList.Services
{
    public class TaskListHelper
    {
        private string jsonFile = System.IO.Path.GetFullPath(@"..\..\..\TaskList.json");

        //Load and read tasks from file
        public List<ToDoTask> GetTaskList()
        {           
            var taskList = new List<ToDoTask>();
            using (StreamReader sr = new StreamReader(jsonFile))
            {
                var jsonData = sr.ReadToEnd();
                taskList = JsonConvert.DeserializeObject<List<ToDoTask>>(jsonData);
            };
            return taskList;
        }

        //Add new task to list

        public ToDoTask AddNewTask()
        {
            Random r = new Random();
            Console.WriteLine("Add title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Add project: ");
            string project = Console.ReadLine();
            Console.WriteLine("Add status: ");
            string status = Console.ReadLine();
            Console.WriteLine("Add duedate: ");
            string duedate = Console.ReadLine();

            var task = new ToDoTask()
            {
                Id = r.Next(1,1000),
                Title = title,
                Project = project,
                Status = status,
                DueDate = Convert.ToDateTime(duedate)

            };
            return task;           

        }

        //Edit a specific task in list

        public List<ToDoTask> EditTask(ToDoTask task, List<ToDoTask> taskList)
        {

            Console.WriteLine("Edit title (1) project (2) status (3) duedate (4) done (5). Enter (6) for remove");
            
            var inputValue = Console.ReadLine();
            switch (inputValue)
            {
                case "1":
                    Console.WriteLine("Edit title: ");
                    var title = Console.ReadLine();
                    taskList[taskList.IndexOf(task)].Title = title;
                    break;
                case "2":
                    Console.WriteLine("Edit project: ");
                    var project = Console.ReadLine(); ;
                    taskList[taskList.IndexOf(task)].Project = project;
                    break;
                case "3":
                    Console.WriteLine("Edit status: ");
                    var status = Console.ReadLine();
                    taskList[taskList.IndexOf(task)].Status = status;
                    break;
                case "4":
                    Console.WriteLine("Edit duedate: ");
                    var duedate = Console.ReadLine();
                    taskList[taskList.IndexOf(task)].DueDate = Convert.ToDateTime(duedate);
                    break;
                case "5":
                    taskList[taskList.IndexOf(task)].Done = true;
                    break;
                case "6":
                    taskList.Remove(task);
                    break;
            }
            return taskList;
        }

        //Display the list of tasks 

        public void ShowTaskList(List<ToDoTask> taskList)
        {
            Console.WriteLine("Choose how you want to show the list. (1) Sorted by date (2) Sorted by project");
            var sortedValue = Console.ReadLine();
            switch (sortedValue)
            {
                case "1":
                    taskList = taskList.OrderByDescending(t => t.DueDate).ToList();
                    break;
                case "2":
                    taskList = taskList.OrderBy(t => t.Project).ToList();
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(
                "Id".PadRight(20) + 
                "Title".PadRight(20) + 
                "DueDate".PadRight(20) + 
                "Project".PadRight(20) + 
                "Status".PadRight(20) + 
                "Done");

            Console.ForegroundColor = ConsoleColor.White;
            foreach (var task in taskList)
            {
                var done = task.Done == true ? "Yes" : "No";

                Console.WriteLine(
                    task.Id.ToString().PadRight(20) +  
                    task.Title.PadRight(20) + 
                    task.DueDate.ToString("yy-MM-dd").PadRight(20) + 
                    task.Project.PadRight(20) + 
                    task.Status.PadRight(20) + done);
            }
        }

        //Save tasks to file
        public void Save(List<ToDoTask> taskList)
        {
            using (var stramWriter = new StreamWriter(jsonFile))
            {
                stramWriter.Write(JsonConvert.SerializeObject(taskList, Formatting.Indented));
                stramWriter.Close();
            }
        }
    }
}
