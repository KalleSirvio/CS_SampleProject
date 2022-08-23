namespace WorldDatabase.Models
{
    public struct Country
    {
        public Country(string alpha2, string alpha3, string[] countryCallingCodes, string[] currencies, string emoji, string ioc, string[] languages, string name, string status)
        {
            Alpha2 = alpha2;
            Alpha3 = alpha3;
            CountryCallingCodes = countryCallingCodes;
            Currencies = currencies;
            Emoji = emoji;
            Ioc = ioc;
            Languages = languages;
            Name = name;
            Status = status;
        }

        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public string[] CountryCallingCodes { get; set; }
        public string[] Currencies { get; set; }
        public string Emoji { get; set; }
        public string Ioc { get; set; }
        public string[] Languages { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public override string ToString() => $"Name {Name}, Alpha2 {Alpha2})";
    }
}
