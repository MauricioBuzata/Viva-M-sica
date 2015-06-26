using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Viva_Música.Entity;

namespace Viva_Música
{
    public partial class Internet : PhoneApplicationPage
    {
        public Noticias pagina { get; set; }

        public Internet()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            TxtPagina.Text = pagina.Link.ToString();
            string var = TxtPagina.Text;
            internet.Navigate(new Uri(var));
        }
    }
}