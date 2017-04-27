using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TaskApp
{
    public class Home : ContentPage
    {
        public Home()
        {
            Button btnVerListado = new Button();
            btnVerListado.Clicked += BtnVerListado_Clicked;
            btnVerListado.Text = "Ir al Listado";
            Content = btnVerListado;
        }

        private void BtnVerListado_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new List_old());
        }
    }
}
