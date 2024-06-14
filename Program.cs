using ToDoList.Model;
using ToDoList.Services;



TaskListHelper helper  = new TaskListHelper();
var taskList = helper.GetTaskList() == null ? new List<ToDoTask>() : helper.GetTaskList();



StartApplication();


void StartApplication()
{
    var taskDone = taskList.Where(t => t.Done).Count();
    var taskNotDone = taskList.Where(t => !t.Done).Count();

    helper.ConsoleWrite($"Welcome! You have {taskDone} tasks done and {taskNotDone} to do. Pick a option", ConsoleColor.White);

    while (true)
    {

        //Input from user
        var input = helper.ConsoleWrite(
                        $"(1) Show Task List\n" +
                        $"(2) Add New Task\n" +
                        $"(3) Edit Task\n" +
                        $"(4) Save", ConsoleColor.White, true);

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
                    helper.ConsoleWrite("Task is created. Press (4) for saving.", ConsoleColor.Green);
                    break;
                case "3":
                    var id = helper.ConsoleWrite("Enter id of the item in list you want to edit", ConsoleColor.White, true);
                    var taskToEdit = taskList.Where(t => t.Id == int.Parse(id)).FirstOrDefault();
                    taskList = helper.EditTask(taskToEdit, taskList);
                    helper.ConsoleWrite("Task has been updated. Press (4) for saving.", ConsoleColor.Green);
                    break;
                case "4":
                    helper.Save(taskList);
                    break;
            }
        }
        catch (FormatException)
        {
            helper.ConsoleWrite($"You have enter some wrong value format: {input}", ConsoleColor.Red);
        }
        catch (ArgumentOutOfRangeException)
        {
            helper.ConsoleWrite($"A error occured. Check if you have enter correct id:  {input}", ConsoleColor.Red);
        }
    }




}