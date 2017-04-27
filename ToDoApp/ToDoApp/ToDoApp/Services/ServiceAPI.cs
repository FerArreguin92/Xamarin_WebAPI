using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ToDoApp.Services
{
    public class ServiceAPI
    {
        public static ServiceAPI Methods => new ServiceAPI();
        public const string urlapi = "http://webapixamarin.azurewebsites.net/api/task";
        public async Task<List<Task>> GetAllTask()
        {
            List<Task> LstTask = new List<Task>();
            try
            {
                //HttpClient client;
                //var Response = await client.GetAsync(BaseUrl + "state/getAll", HttpCompletionOption.ResponseHeadersRead);
                //ProcessResponse(Response);
                //await Response.Content.ReadAsStringAsync().ContinueWith((readTask) =>
                //{
                //    result = JsonConvert.DeserializeObject<List<StateViewModel>>(readTask.Result);
                //});
            }
            catch(Exception ex)
            {
                    
            }
            return LstTask;
        }
    }
}
