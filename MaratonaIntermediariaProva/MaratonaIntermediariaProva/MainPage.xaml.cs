using MaratonaIntermediariaProva.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MaratonaIntermediariaProva
{
    public class TimesFutebol
    {
        public string Nome { get; set; }
        public string Estado { get; set; }
    }

    public partial class MainPage : ContentPage
    {

        public List<TimesFutebol> Times;

        public MainPage()
        {

            #region dados
            Times = new List<TimesFutebol>();
            Times.Add(new TimesFutebol { Nome = "São Paulo" , Estado = "São Paulo" });
            Times.Add(new TimesFutebol { Nome = "Flamengo", Estado = "Rio de Janeiro" });
            Times.Add(new TimesFutebol { Nome = "Palmeiras", Estado = "São Paulo" });
            Times.Add(new TimesFutebol { Nome = "Santos", Estado = "São Paulo" });
            Times.Add(new TimesFutebol { Nome = "Vasco da Gama", Estado = "Rio de Janeiro" });
            Times.Add(new TimesFutebol { Nome = "Fluminense", Estado = "Rio de Janeiro" });
            Times.Add(new TimesFutebol { Nome = "Bota Fogo", Estado = "Rio de Janeiro" });
            Times.Add(new TimesFutebol { Nome = "Corinthias", Estado = "São Paulo" });
            #endregion

            ListView ListaTimes = new ListView
            {
                ItemsSource = Times
            };
            ListaTimes.ItemTemplate = new DataTemplate(typeof(TextCell));
            ListaTimes.ItemTemplate.SetBinding(TextCell.TextProperty, "Nome");

            ListaTimes.ItemTapped += (e, s) =>
            {
                DisplayAlert("Estado", (s.Item as TimesFutebol).Estado, "Fechar");
            };

            Content = new StackLayout
            {
                Children =
                {
                    ListaTimes
                }
            };

            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
