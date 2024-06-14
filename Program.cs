using ToDoList.Model;
using ToDoList.Services;



TaskListHelper helper  = new TaskListHelper();
var taskList = helper.GetTaskList() == null ? new List<ToDoTask>() : helper.GetTaskList();


StartApplication();


void StartApplication()
{
    var taskDone = taskList.Where(t => t.Done).Count();
    var taskNotDone = taskList.Where(t => !t.Done).Count();

    Console.WriteLine($"Welcome! You have {taskDone} tasks done and {taskNotDone} to do. Pick a option");
    Console.WriteLine("(1) Show Task List");
    Console.WriteLine("(2) Add New Task");
    Console.WriteLine("(3) Edit Task");
    Console.WriteLine("(4) Save");

    
    while (true)
    {
        //Input from user
        var input = Console.ReadLine();

        try
        {
            switch (input)
            {
                case "1":
                    helper.ShowTaskList(taskList);
                    break;
                case "2":
                    var task = helper.AddNewTask();
                    taskList.Add(task);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Task is created. Press (4) for saving.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "3":
                    Console.WriteLine("Enter id of the item in list you want to edit");
                    var id = int.Parse(Console.ReadLine());
                    var taskToEdit = taskList.Where(t => t.Id == id).FirstOrDefault();
                    taskList = helper.EditTask(taskToEdit, taskList);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Task has been updated. Press (4) for saving.");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "4":
                    helper.Save(taskList);
                    break;
            }
        }
        catch (FormatException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"You have enter some wrong value format: {input}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"A error occured. Check if you have enter correct id:  {input}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }




}