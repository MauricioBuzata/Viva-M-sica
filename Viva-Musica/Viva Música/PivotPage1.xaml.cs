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
using System.Xml.Linq;
using Viva_Música.Repository;
using System.Text;

namespace Viva_Música
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        Noticias noticia;
        // Constructor
        public PivotPage1()
        {
            InitializeComponent();
        }

        void RssClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                var RssData = from rss in XElement.Parse(e.Result).Descendants("item")
                              select new Noticias
                              {
                                  Title = rss.Element("title").Value,
                                  PubDate = rss.Element("pubDate").Value,
                                  Link = rss.Element("link").Value,
                                  Description = rss.Element("description").Value
                              };
                LstNoticias.ItemsSource = RssData;
                LstAgenda.ItemsSource = RssData;
            }
            catch (Exception)
            {
                if (MessageBox.Show("Você está sem conexão com a internet!\nConecte-se e tente novamente.")
                   == MessageBoxResult.OK)
                { }
                InitializeComponent();
            }

            progress.Visibility = System.Windows.Visibility.Collapsed;
            progress2.Visibility = System.Windows.Visibility.Collapsed;

        }

        private void Pivot_LoadedPivotItem(object sender, PivotItemEventArgs e)
        {
            progress.Visibility = System.Windows.Visibility.Visible;
            progress2.Visibility = System.Windows.Visibility.Visible;

            WebClient RssClient1 = new WebClient();

            WebClient RssClient2 = new WebClient();
            RssClient2.Encoding = Encoding.GetEncoding("iso-8859-1");

            LstNoticias.ItemsSource = null;
            LstAgenda.ItemsSource = null;

            RssClient1.DownloadStringCompleted += RssClient_DownloadStringCompleted;
            RssClient2.DownloadStringCompleted += RssClient_DownloadStringCompleted;
            if (e.Item == Noticias)
                RssClient1.DownloadStringAsync(new Uri(@"http://g1.globo.com/dynamo/musica/rss2.xml"));
            else if (e.Item == Agenda)
                RssClient2.DownloadStringAsync(new Uri(@"http://www.territoriodamusica.com/rss/agenda.xml"));
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

        }

        private void Navigate(string pPage)
        {
            NavigationService.Navigate(new Uri(pPage, UriKind.Relative));
        }

        private void abrirBrowser_Click(object sender, EventArgs e)
        {
            if(noticia!=null)
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

        private void addFav_Click(object sender, EventArgs e)
        {
            if (noticia != null)
            {
                Noticias noticias = new Noticias
                {
                    Title = noticia.Title,
                    PubDate = noticia.PubDate,
                    Link = noticia.Link
                };
                NoticiasRepository.Create(noticia);
                if (MessageBox.Show("Adicionado com sucesso!")
                    == MessageBoxResult.OK)
                { }
            }
            else
            {
                if (MessageBox.Show("Você deve selecionar uma noticia primeiro!")
                    == MessageBoxResult.OK)
                { }
            }
        }

        private void verFav_Click(object sender, EventArgs e)
        {
            Navigate("/Favoritos.xaml");
        }

        private void sobre_Click(object sender, EventArgs e)
        {
            Navigate("/Sobre.xaml");
        }
    }
}