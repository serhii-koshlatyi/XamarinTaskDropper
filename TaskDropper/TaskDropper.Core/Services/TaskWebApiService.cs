﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskDropper.Core.Interface;
using TaskDropper.Core.Models;

namespace TaskDropper.Core.Services
{
    public class TaskWebApiService : ITaskWebApiService
    {
        HttpClient client;
        public List<ItemTask> Items { get; private set; }
        string RestUrl;

        public TaskWebApiService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }


        public async  Task<List<ItemTask>> RefreshDataAsync(string email)
        {
            string RestUrl = "http://10.10.3.183:50176/api/task/";
            Items = new List<ItemTask>();
            string temp = RestUrl+"?id=" + email;
            var uri = new Uri(temp);

            
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<ItemTask>>(content);           
            }

            Console.WriteLine(Items);
            return Items;
        }

        public async Task SaveItemTaskAsync(ItemTask item, bool isNewItem=false)
        {
            RestUrl = "http://10.10.3.183:50176/api/task/";
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            if (isNewItem)
            {
                response = await client.PostAsync(uri, content);
            }
            else
            {
                response = await client.PutAsync(uri, content);
            }

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"                Task successfully saved.");

            }
        }

        public async Task DeleteItemTaskAsync(string id)
        {
            RestUrl = "http://10.10.3.183:50176/api/task/"+id;
            var uri = new Uri(string.Format(RestUrl));
            var response = await client.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"          Task deleted.");
            }
        }

        public List<ItemTask> GetItems()
        {
            return Items;
        }
    }
}
