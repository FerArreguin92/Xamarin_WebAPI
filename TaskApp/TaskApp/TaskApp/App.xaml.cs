using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Models;
using TaskApp.Services;
using Xamarin.Forms;

namespace TaskApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            LstTareas = new List<Tareas>();
            Task.Run(async () => LstTareas = await XamarinAPI.Methods.GetAllTask());

            MainPage = new TaskApp.Home();
        }
        public static List<Tareas> LstTareas { get; set; }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
