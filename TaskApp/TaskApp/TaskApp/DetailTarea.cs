using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using TaskApp.Models;
using Xamarin.Forms;

namespace TaskApp
{
    public class DetailTarea : ContentPage
    {
        public DetailTarea(Tareas itemSelected)
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Detalle de Tarea" },
                    new Label { Text = itemSelected.Id.ToString() },
                    new Label { Text = itemSelected.Name.ToString() },
                    new Label { Text = itemSelected.Description.ToString() },
                    new Label { Text = itemSelected.EndDate.ToString("dd/MM/yyyy HH:mm") },
                }
            };
        }
    }
}
