using TaskManager.helpers;
using TaskManager.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;


namespace TaskManager.services
{
    public class ItemService
    {
        private List<Item> items;
        private TaskNavigator<Item> taskNav;
        private string query = string.Empty;
        static private ItemService instance;

        public bool ShowComplete {  get; set; }
        public string Query
        {
            get => query.ToUpper();
            set => query = value;
        }

        public List<Item> Items
        {
            get
            {
                return items;
            }
        }

        public IEnumerable<Item> FilteredItems
        {
            get
            {
                var incompleteItems = Items.Where(i =>
               (!ShowComplete && !((i as ToDo)?.IsCompleted ?? true) || ShowComplete
               || DateTime.Compare((i as Appointment)?.End ?? DateTime.MinValue, DateTime.Now) > 0));

                var searchResults = incompleteItems.Where(i => string.IsNullOrWhiteSpace(Query)
                //there is no query
                || (i?.Name?.ToUpper()?.Contains(Query) ?? false)
                //i is any item and the name contains the query
                || (i?.Description?.ToUpper()?.Contains(Query) ?? false)
                //i is any item and the description contains the query
                || ((i as Appointment)?.Attendees?.Select(t => t.ToUpper())?.Contains(Query) ?? false));
               
                return searchResults;
            }
        }

        public static ItemService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemService();
                }
                return instance;
            }
        }

        private ItemService()
        {
            items = new List<Item>();
            taskNav = new TaskNavigator<Item>(FilteredItems, 5);
        }

        public void Add(Item i)
        {
            items.Add(i);
        }

        public void Remove(Item i)
        {
            items.Remove(i);
        }

        public Dictionary<object, Item> GetPage()
        {
            var page = taskNav.GetCurrentPage();

            if (taskNav.HasNextPage)
            {
                page.Add("N", new Item { Name = "Next" });
            }
            if (taskNav.HasPreviousPage)
            {
                page.Add("P", new Item { Name = "Previous" });
            }
            return page;
        }

        public Dictionary<object, Item> NextPage()
        {
            return taskNav.GoForward();
        }

        public Dictionary<object, Item> PreviousPage()
        {
            return taskNav.GoBackward();
        }

        public Dictionary<object, Item> FirstPage()
        {
            return taskNav.GoToFirstPage();
        }

        public void Save()
        {
            var persistencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var folder = Path.Combine(persistencePath, "TaskManager");
            Directory.CreateDirectory(folder);
            var path = Path.Combine(persistencePath, "TaskManager\\SaveData.json");
            var json = JsonConvert.SerializeObject(items, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            File.WriteAllText(path, json);
        }

        public bool Load()
        {
            try
            {
                var persistencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var path = Path.Combine(persistencePath, "TaskManager\\SaveData.json");
                string dataText = File.ReadAllText(path);
                items.Clear();
                items.AddRange(JsonConvert.DeserializeObject<List<Item>>(dataText, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }));
                //items = JsonConvert.DeserializeObject<List<Item>>(dataText, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
