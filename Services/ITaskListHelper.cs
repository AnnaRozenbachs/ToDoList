using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Services
{
    public interface ITaskListHelper
    {
        public List<ToDoTask> GetTaskList();
        public ToDoTask AddNewTask();
        public List<ToDoTask> EditTask(ToDoTask task, List<ToDoTask> taskList);

        public void ShowTaskList(List<ToDoTask> taskList);

        public void Save(List<ToDoTask> taskList);
    }
}
