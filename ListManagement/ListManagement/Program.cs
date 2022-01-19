
using ListManagement.models;
using System2 = System;
using System.Linq;

namespace ListManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            var todos = new List<ToDo>();
            Console.WriteLine("Welcome to the List Management App");

            var nextTodo = new ToDo();
            PrintMenu();

            int input = -1;
            if(int.TryParse(Console.ReadLine(),out input)) {
                while (input != 0)
                {
                    nextTodo = new ToDo();

                    if (input == 1) //C - Create task
                    {
                        
                        //ask for property values
                        Console.WriteLine("Name of Task: ");
                        nextTodo.Name = Console.ReadLine();

                        Console.WriteLine("Task Description: ");
                        nextTodo.Description = Console.ReadLine();

                        Console.WriteLine("Task Deadline: ");
                        nextTodo.Deadline = DateTime.Parse(Console.ReadLine());

                        todos.Add(nextTodo);

                    }
                    else if (input == 2) //D - Delete/Remove tasks
                    {
                        PrintTasks(todos);
                        if (todos.Any())
                        {
                            Console.WriteLine("Which Task Should Be Deleted?");
                            var strIndex = int.Parse(Console.ReadLine());
                            todos.RemoveAt(strIndex - 1);
                        }

                    } 
                    else if (input == 3) //U - Update/Edit
                    {
                        PrintTasks(todos);

                        if (todos.Any())
                        {
                            Console.WriteLine("Which Task Would You Like To Edit?");
                            var strIndex = int.Parse(Console.ReadLine());

                            nextTodo = new ToDo();
                            nextTodo = todos[strIndex - 1];
                            int eInput = -1;

                            Console.WriteLine("What would you like to edit?");
                            PrintEditMenu();

                            if (int.TryParse(Console.ReadLine(), out eInput))
                            {
                                while (eInput != 0)
                                {
                                    if (eInput == 1)
                                    {
                                        Console.WriteLine("Enter New Name: ");
                                        todos[strIndex - 1].Name = Console.ReadLine();
                                    }
                                    else if (eInput == 2)
                                    {
                                        Console.WriteLine("Enter New Description: ");
                                        todos[strIndex - 1].Description = Console.ReadLine();
                                    }
                                    else if (eInput == 3)
                                    {
                                        Console.WriteLine("Enter New Deadline: ");
                                        todos[strIndex - 1].Deadline = DateTime.Parse(Console.ReadLine());
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input, please enter a valid entry.");
                                    }

                                    PrintEditMenu();
                                    eInput = int.Parse(Console.ReadLine());
                                }

                            }
                            else
                            {
                                Console.WriteLine("User did not specify a valid int!");
                            }
                            //Console.ReadLine();

                            Console.WriteLine($"Succesfully Updated Task: {todos[strIndex - 1].Name}");
                        }

                    }
                    else if (input == 4) //Complete Task
                    {
                        PrintTasks(todos);
                        if (todos.Any())
                        {
                            Console.WriteLine("Which Task Should Be Marked as Completed?");
                            var strIndex = int.Parse(Console.ReadLine());

                            todos[strIndex - 1].IsCompleted = true;

                            Console.WriteLine($"Succesfully Completed Task: {todos[strIndex - 1].Name}");
                        }

                    }
                    else if(input == 5) //R - Read / List uncompleted tasks
                    {
                        bool isCompleted = true;
                        int index = 1;
                        Console.WriteLine("--------------------------------------------");
                        if (todos.Any())
                        {
                            foreach (var todo in todos)
                            {
                                if (!todo.IsCompleted)
                                {
                                    Console.WriteLine($"{index}. {todo.ToString()}");
                                    isCompleted = false;
                                }
                                index++;
                            }

                            if (isCompleted)
                            {
                                Console.WriteLine("You Completed all your tasks.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("There are no tasks here...");
                        }
                        Console.WriteLine("--------------------------------------------");

                    } 
                    else if (input == 6) //R - Read / List all tasks
                    {

                        PrintTasks(todos);
                        
                    } 
                    else
                    {
                        Console.WriteLine("Invalid input, please enter a valid entry.");
                    }

                    PrintMenu();
                    input = int.Parse(Console.ReadLine());
                }
            }
           else
            {
                Console.WriteLine("User did not specify a valid int!");
            }
                
        }

        public static void PrintMenu()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Delete Task");
            Console.WriteLine("3. Edit a Task");
            Console.WriteLine("4. Mark a Task as Completed");
            Console.WriteLine("5. List All Outstanding Tasks");
            Console.WriteLine("6. List All Tasks");
            Console.WriteLine("0. Exit");
            Console.WriteLine("--------------------------------");
        }

        public static void PrintEditMenu()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Description");
            Console.WriteLine("3. Deadline");
            Console.WriteLine("0. Stop Editing");
            Console.WriteLine("------------------");
        }

        public static void PrintTasks(List<ToDo> todos)
        {
            bool isEmpty = !todos.Any();
            if (isEmpty)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("There are no tasks here...");
                Console.WriteLine("---------------------------");
                return;
            }
            int index = 1;
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("{0,-10} {1,-20} {2,5} {3,5}\n", "Name", "Description", "Deadline", "Status");
            foreach (var todo in todos)
            {
                Console.WriteLine($"{index}. {todo.ToString()}");
                index++;
            }
            Console.WriteLine("--------------------------------------------");
        }
    }
}