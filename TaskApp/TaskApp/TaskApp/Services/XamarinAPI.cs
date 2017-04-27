using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Models;

namespace TaskApp.Services
{
    public class XamarinAPI
    {
        public static XamarinAPI Methods = new XamarinAPI();
        public const string urlapi = "http://webapixamarin.azurewebsites.net/api/task";
        public async Task<List<Tareas>> GetAllTask()
        {
            List<Tareas> LstTask = new List<Tareas>();
            try
            {
                HttpClient client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                var uri = new Uri(urlapi);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    LstTask = JsonConvert.DeserializeObject<List<Tareas>>(content);
                }
            }
            catch (Exception ex)
            {

            }
            return LstTask;
        }
        public async Task<Tareas> GetTaskById(int Id)
        {
            Tareas ItemTask = new Tareas();
            try
            {
                HttpClient client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                var uri = new Uri(urlapi + "/" + Id);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ItemTask = JsonConvert.DeserializeObject<Tareas>(content);
                }
            }
            catch (Exception ex)
            {

            }
            return ItemTask;
        }
        public async Task<bool> SaveItemTarea(Tareas model, bool esNuevo = false)
        {
            bool ejecucionCorrecta = false;
            try
            {
                HttpClient client = new HttpClient();
                client.MaxResponseContentBufferSize = 256000;
                var uri = new Uri(urlapi + (esNuevo == true ? string.Empty : "/" + model.Id));
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (esNuevo)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }
                if (response.IsSuccessStatusCode)
                {
                    ejecucionCorrecta = true;
                }
            }
            catch (Exception ex)
            {

            }
            return ejecucionCorrecta;
        }
    }
}
