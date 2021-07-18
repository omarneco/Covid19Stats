
namespace Covid19Stat.Models

{
    public class Region
    {
        public string iso { get; set; }
        public string name { get; set; }
    }

    public class Data
    { 
        public dynamic date { get; set; }
        public dynamic confirmed { get; set; }
        public dynamic deaths { get; set; }
        public dynamic recovered { get; set; }
        public dynamic confirmed_diff { get; set; }
        public dynamic deaths_diff { get; set; }
        public dynamic recovered_diff { get; set; }
        public dynamic last_update { get; set; }
        public dynamic active { get; set; }
        public dynamic active_diff { get; set; }
        public dynamic fatality_rate { get; set; }
        public dynamic region_code { get; set; }
        public dynamic region_name { get; set; }
        public dynamic province_name { get; set; }
    }

    public class Statistic
    {
        public string region_name { get; set; }
        public int cases { get; set; }
        public int deaths { get; set; }
    }
}