
using TaskManager.helpers;
using TaskManager.models;
using TaskManager.services;
using System;

namespace TaskManager
{
    public class Program
    {
        static void Main(string[] args)
        {
            var todos = new List<ToDo>();
            var CalendarAppointments = new List<Appointment>();
            var itemService = ItemService.Current;
            itemService.Load();

            Console.WriteLine("Welcome to the Task Management App");

            var nextTodo = new ToDo();
            var nextCA = new Appointment();
            PrintMenu();

            int input = -1;
            if (int.TryParse(Console.ReadLine(), out input))
            {
                while (input != 0)
                {
                    nextTodo = new ToDo();
                    nextCA = new Appointment();

                    if (input == 1) //C - Create task
                    {
                        FillTaskProperties(nextTodo);
                        itemService.Add(nextTodo);
                    }
                    else if (input == 2)
                    {
                        FillAppointmentProperties(nextCA);
                        itemService.Add(nextCA);
                    }
                    else if (input == 3) //D - Delete/Remove tasks
                    {

                        Console.WriteLine("What Task/Appointment Would You Like To Delete?");

                        if (int.TryParse(Console.ReadLine(), out int selection))
                        {
                            if (itemService.Items.Count < selection)
                            {
                                Console.WriteLine("That Index does not exist.");
                            }
                            else
                            {
                                var selectedItem = itemService.Items[selection - 1];
                                itemService.Remove(selectedItem);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, That Task does not exist");
                        }

                    }
                    else if (input == 4) //U - Update/Edit
                    {

                        Console.WriteLine("What Entry Would You Like to Edit?");

                        if (int.TryParse(Console.ReadLine(), out int selection))
                        {

                            if (itemService.Items[selection - 1] is ToDo)
                            {
                                var selectedItem = itemService.Items[selection - 1] as ToDo;

                                if (selectedItem != null)
                                {
                                    FillTaskProperties(selectedItem);
                                }

                                Console.WriteLine("Succesfully Updated Task.");
                            }
                            else
                            {
                                var selectedItem = itemService.Items[selection - 1] as Appointment;

                                if (selectedItem != null)
                                {
                                    FillAppointmentProperties(selectedItem);
                                }

                                Console.WriteLine("Succesfully Updated Appointment.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Sorry, Cant find that Task.");
                        }

                    }
                    else if (input == 5) //Complete Task
                    {

                        Console.WriteLine("Which Item Should be Marked as Completed?");

                        if (int.TryParse(Console.ReadLine(), out int selection))
                        {
                            var selectedItem = itemService.Items[selection-1] as ToDo;

                            if (selectedItem != null)
                            {
                                selectedItem.IsCompleted = true;
                            }

                        }
                        else
                        {
                            Console.WriteLine("Sorry, I was Unable to Find that Task.");
                        }

                    }
                    else if (input == 6) // List uncompleted tasks
                    {
                        itemService.ShowComplete = false;
                        itemService.Query = string.Empty;

                        if (itemService.FilteredItems.Count() <= 0)
                        {
                            Console.WriteLine("No Entries to Display. Consider Adding some. ");
                        }
                        else
                        {

                            Console.WriteLine("[E, Exit]");
                            itemService.FirstPage();
                            var userSelection = string.Empty;

                            while (userSelection != "E")
                            {
                                foreach (var item in itemService.GetPage())
                                {
                                    Console.WriteLine(item);
                                }

                                if (itemService.FilteredItems.Count() <= 5)
                                {
                                    userSelection = "E";
                                }
                                else
                                {
                                    userSelection = Console.ReadLine().ToUpper();
                                }

                                if (userSelection == "N")
                                {
                                    itemService.NextPage();
                                }
                                else if (userSelection == "P")
                                {
                                    itemService.PreviousPage();
                                }
                                else if (userSelection != "P" && userSelection != "N" && userSelection != "E")
                                {
                                    Console.WriteLine("Invalid Entry, Try Again.");
                                }
                            }
                        }
                        itemService.ShowComplete = true;

                    }
                    else if (input == 7) // List all tasks
                    {

                        if (itemService.Items.Count <= 0)
                        {
                            Console.WriteLine("No Entries to Display. Consider adding some. ");
                        }
                        else
                        {
                            itemService.ShowComplete = true;
                            itemService.Query = string.Empty;

                            Console.WriteLine("[E, Exit]");
                            itemService.FirstPage();
                            var userSelection = string.Empty;

                            while (userSelection != "E")
                            {
                                foreach (var item in itemService.GetPage())
                                {
                                    Console.WriteLine(item);
                                }

                                if (itemService.Items.Count <= 5)
                                {
                                    userSelection = "E";
                                }
                                else
                                {
                                    userSelection = Console.ReadLine().ToUpper();
                                }

                                if (userSelection == "N")
                                {
                                    itemService.NextPage();
                                }
                                else if (userSelection == "P")
                                {
                                    itemService.PreviousPage();
                                }
                                else if (userSelection != "P" && userSelection != "N" && userSelection != "E")
                                {
                                    Console.WriteLine("Invalid Entry, Try Again.");
                                }
                            }
                            itemService.FirstPage();
                        }
                    }
                    else if (input == 8) // search
                    {

                        Console.WriteLine("Type String You Would Like To Search: ");
                        string query = Console.ReadLine()??"";

                        itemService.ShowComplete = false;
                        itemService.Query = query;

                        if (itemService.FilteredItems.Count() <= 0)
                        {
                            Console.WriteLine("No Entries Found.");
                        }
                        else
                        {

                            Console.WriteLine("[E, Exit]");
                            itemService.FirstPage();
                            var userSelection = string.Empty;

                            while (userSelection != "E")
                            {
                                foreach (var item in itemService.GetPage())
                                {
                                    Console.WriteLine(item);
                                }

                                if (itemService.FilteredItems.Count() <= 5)
                                {
                                    userSelection = "E";
                                }
                                else
                                {
                                    userSelection = Console.ReadLine().ToUpper();
                                }

                                if (userSelection == "N")
                                {
                                    itemService.NextPage();
                                }
                                else if (userSelection == "P")
                                {
                                    itemService.PreviousPage();
                                }
                                else if (userSelection != "P" && userSelection != "N" && userSelection != "E")
                                {
                                    Console.WriteLine("Invalid Entry, Try Again.");
                                }
                            }
                        }
                        itemService.ShowComplete = true;
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
            itemService.Save();
        }

        public static void PrintMenu()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("1. Add a Task");
            Console.WriteLine("2. Add a Calendar Appointment");
            Console.WriteLine("3. Delete an Entry");
            Console.WriteLine("4. Edit an Entry");
            Console.WriteLine("5. Mark a Task as Completed");
            Console.WriteLine("6. List All Outstanding Tasks");
            Console.WriteLine("7. List All Entries");
            Console.WriteLine("8. Search");
            Console.WriteLine("0. Exit");
            Console.WriteLine("--------------------------------");
        }

        public static void FillTaskProperties(ToDo todo)
        {
            
            Console.WriteLine("Name of Task: ");
            todo.Name = Console.ReadLine();

            Console.WriteLine("Task Description: ");
            todo.Description = Console.ReadLine()?.Trim();

            Console.WriteLine("Task Deadline: ");
            todo.Deadline = DateTime.Parse(Console.ReadLine());

        }

        public static void FillAppointmentProperties(Appointment ap)
        {
            Console.WriteLine("Name of Appointment: ");
            ap.Name = Console.ReadLine();

            Console.WriteLine("Appointment Description: ");
            ap.Description = Console.ReadLine()?.Trim();

            Console.WriteLine("Appointment Starts: ");
            ap.Start = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Appointment Ends: ");
            ap.End = DateTime.Parse(Console.ReadLine());

            var userSelection = string.Empty;
            List<string> temp = new List<string>();

            Console.WriteLine("Who will be attending? (E to Exit)");

            while(userSelection != "E")
            {
                userSelection = Console.ReadLine();

                if(userSelection != "E")
                {
                    temp.Add(userSelection);
                }
            }

            ap.Attendees = temp;

        }
    }
}