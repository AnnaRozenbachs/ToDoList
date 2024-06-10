using ToDoList.Model;
using ToDoList.Services;



TaskListHelper helper  = new TaskListHelper();
var taskList = helper.GetTaskList() == null ? new List<ToDoTask>() : helper.GetTaskList();

while (true)
{
    StartApplication();
}


void StartApplication()
{
    Console.WriteLine("Welcome! Pick a option");
    Console.WriteLine("(1) Show Task List");
    Console.WriteLine("(2) Add New Task");
    Console.WriteLine("(3) Edit Task");
    Console.WriteLine("(4) Save");

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
                var editedTask = taskList.Where(t=>t.Id==id).FirstOrDefault();
                taskList= helper.EditTask(editedTask, taskList);
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("Task has been updated. Press (4) for saving.");
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case "4":
                helper.Save(taskList);
                break;
        }
    }
    catch (Exception e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(e.Message);
        Console.ForegroundColor = ConsoleColor.White;
    }


}