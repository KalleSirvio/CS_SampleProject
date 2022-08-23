using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using WorldDatabase.Models;

namespace WorldDatabase.ViewModels
{
    class MainWindowViewModel : BindableBase, INotifyPropertyChanged
    {
        private List<Country>? _countries;

        public List<Country> Countries
        {
            get { return _countries ?? new List<Country>(); }
            set { SetProperty(ref _countries, value); }
        }

        private List<NewsArticle>? _newsArticles;

        public List<NewsArticle> NewsArticles
        {
            get { return _newsArticles ?? new List<NewsArticle>(); }
            set
            {
                if (!value.Equals(_newsArticles))
                {
                    _newsArticles = value;
                    OnPropertyChanged();
                }
            }
        }

        private Country _selectedCountry;

        public Country SelectedCountry
        {
            get
            {
                return _selectedCountry;
            }

            set
            {
                if (!value.Equals(_selectedCountry))
                {
                    _selectedCountry = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedCountryName = "";

        public string SelectedCountryName
        {
            get { return _selectedCountryName; }
            set { SetProperty(ref _selectedCountryName, value); }
        }

        public DelegateCommand SearchButtonClicked{ get; set; }

        public new event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainWindowViewModel()
        {
            GetCountryDetails();
            SearchButtonClicked = new DelegateCommand(ShowCountryInformation);
        }

        /*
         Shows single country information and gets the new related to that country based on user input.
         */
        private void ShowCountryInformation()
        {
            foreach (Country country in Countries)
            {
                if (country.Name == SelectedCountryName)
                {
                    SelectedCountry = country;
                    GetCountryNews();
                }
            }
        }

        /*
         Gets all the worlds countries basic information as a JSON and turn it to string. Then that string representation is converted to Country object list.
         */
        private async void GetCountryDetails()
        {
            HttpResponseMessage countryDetails = await WebAPI.GetCall("https://raw.githubusercontent.com/OpenBookPrices/country-data/master/data/countries.json");
            if (countryDetails.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string result = await countryDetails.Content.ReadAsStringAsync();
                if(result is not null)
                {
                    Countries = JsonConvert.DeserializeObject<List<Country>>(result) ?? new List<Country>();
                }
            }
        }

        /*
         Get all news articles related to the country that the user searched
        Input (country name) comes from the MainWindow.xaml TextBoxCountry

        NOTE: New York Times API key must be provided either in Models/APIKeys.cs or Models/APIKeysLocal.cs
         */
        private async void GetCountryNews()
        {
            
            HttpResponseMessage countryNews = await WebAPI.GetCall($"https://api.nytimes.com/svc/search/v2/articlesearch.json?q={SelectedCountry.Name}&api-key={APIKeys.NYTimesAPIKey}");
            if(countryNews.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string result = await countryNews.Content.ReadAsStringAsync();
                if (result is not null)
                {
                    string trimmedRes = result[(result.IndexOf("abstract") - 3)..];
                    string finalRes = trimmedRes.Remove(trimmedRes.IndexOf(",\"meta\":"));
                    NewsArticles = JsonConvert.DeserializeObject<List<NewsArticle>>(finalRes) ?? new List<NewsArticle>();
                }
            }
        }


    }
}
