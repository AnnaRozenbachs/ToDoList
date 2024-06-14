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
    public class TaskListHelper :ITaskListHelper
    {
        private string jsonFile = System.IO.Path.GetFullPath(@"../../../TaskList.json");

        //Load and read tasks from file
        public List<ToDoTask> GetTaskList()
        {           
            var taskList = new List<ToDoTask>();

            try
            {
                if (File.Exists(jsonFile))
                {
                    using (StreamReader sr = new StreamReader(jsonFile))
                    {
                        var jsonData = sr.ReadToEnd();
                        taskList = JsonConvert.DeserializeObject<List<ToDoTask>>(jsonData);
                    };

                }

            }
            catch(Exception ex) 
            {
                ConsoleWrite($"Something went wrong when reading from file. {ex.Message}", ConsoleColor.Red);
            }
            return taskList;
        }

        //Add new task to list

        public ToDoTask AddNewTask()
        {
            Random r = new Random();

            var task = new ToDoTask()
            {
                Id = r.Next(1,1000),
                Title = ConsoleWrite("Add title: ", ConsoleColor.White, true),
                Project = ConsoleWrite("Add project: ", ConsoleColor.White, true),
                Status = ConsoleWrite("Add status: ", ConsoleColor.White, true),
                DueDate =Convert.ToDateTime(ConsoleWrite("Add duedate: ", ConsoleColor.White, true)),

            };
            return task;           

        }

        //Edit a specific task in list

        public List<ToDoTask> EditTask(ToDoTask task, List<ToDoTask> taskList)
        {

            ConsoleWrite("Edit title (1) project (2) status (3) duedate (4) done (5). Enter (6) for remove", ConsoleColor.White);
            
            var inputValue = Console.ReadLine();
            switch (inputValue)
            {
                case "1":
                    taskList[taskList.IndexOf(task)].Title = ConsoleWrite("Edit title: ", ConsoleColor.White, true); 
                    break;
                case "2":
                    taskList[taskList.IndexOf(task)].Project = ConsoleWrite("Edit project: ", ConsoleColor.White, true);
                    break;
                case "3":
                    taskList[taskList.IndexOf(task)].Status = ConsoleWrite("Edit status: ", ConsoleColor.White, true);
                    break;
                case "4":
                    taskList[taskList.IndexOf(task)].DueDate = Convert.ToDateTime(ConsoleWrite("Edit duedate: ", ConsoleColor.White, true));
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
            var sortedValue = ConsoleWrite("Choose how you want to show the list. (1) Sorted by date (2) Sorted by project", ConsoleColor.White, true);

            switch (sortedValue)
            {
                case "1":
                    taskList = taskList.OrderByDescending(t => t.DueDate).ToList();
                    break;
                case "2":
                    taskList = taskList.OrderBy(t => t.Project).ToList();
                    break;
            }

            ConsoleWrite(
                "Id".PadRight(20) + 
                "Title".PadRight(20) + 
                "DueDate".PadRight(20) + 
                "Project".PadRight(20) + 
                "Status".PadRight(20) + 
                "Done", ConsoleColor.Yellow);

            foreach (var task in taskList)
            {
                var done = task.Done == true ? "Yes" : "No";

                ConsoleWrite(
                    task.Id.ToString().PadRight(20) +  
                    task.Title.PadRight(20) + 
                    task.DueDate.ToString("yyyy-MM-dd").PadRight(20) + 
                    task.Project.PadRight(20) + 
                    task.Status.PadRight(20) + done, ConsoleColor.White);
            }
        }

        public string ConsoleWrite(string message, ConsoleColor color, bool needInput = false, ConsoleColor defaultColour = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            if (!string.IsNullOrEmpty(message))
            {
               Console.WriteLine(message);
            }
            Console.ForegroundColor = defaultColour;

            return needInput? Console.ReadLine() : string.Empty;
        }

        //Save tasks to file
        public void Save(List<ToDoTask> taskList)
        {
            try
            {
                if (!File.Exists(jsonFile))
                {
                    File.Create(jsonFile);
                }
                using (var streamWriter = new StreamWriter(jsonFile))
                {
                    streamWriter.Write(JsonConvert.SerializeObject(taskList, Formatting.Indented));
                    streamWriter.Close();
                }

            }
            catch (Exception e)
            {
                ConsoleWrite($"Something went wrong when saving. {e.Message}", ConsoleColor.Red);
            }


            
        }
    }
}
