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
using Viva_Música.Repository;

namespace Viva_Música
{
    public partial class Favoritos : PhoneApplicationPage
    {
        Noticias noticia;

        public Favoritos()
        {
            InitializeComponent();
            Refresh();
        }

        private void onSelecionChange(object sender, SelectionChangedEventArgs e)
        {
            noticia = null;
            noticia = (sender as ListBox).SelectedItem as Noticias;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Internet page = e.Content as Internet;
            if (page != null)
                page.pagina = noticia;
            Refresh();

        }

        private void Navigate(string pPage)
        {
            NavigationService.Navigate(new Uri(pPage, UriKind.Relative));
        }

        private void abrirBrowser_Click(object sender, EventArgs e)
        {
            if (noticia != null)
            {
                Navigate("/Internet.xaml");
            }
            else
            {
                if (MessageBox.Show("Você deve selecionar uma noticia primeiro!")
                    == MessageBoxResult.OK)
                { }
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (noticia != null)
            {
                if (MessageBox.Show("Você deseja realmente excluir esta noticia?")
                    == MessageBoxResult.OK)
                {
                    NoticiasRepository.Delete(noticia);
                    if (MessageBox.Show("Excluido com sucesso!")
                    == MessageBoxResult.OK)
                    { }
                    Refresh();
                }
            }
            else
            {
                if (MessageBox.Show("Você deve selecionar uma noticia primeiro!")
                    == MessageBoxResult.OK)
                { }
            }
            Refresh();
        }

        private void Refresh()
        {
            List<Noticias> noticias = NoticiasRepository.Get();
            LstFavoritos.ItemsSource = noticias;
        }     
    }
}