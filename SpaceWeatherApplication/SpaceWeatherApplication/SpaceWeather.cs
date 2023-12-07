namespace SpaceWeatherApplication
{
    public class SpaceWeather
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<PlanetData> PlanetDataList { get; set; }
    }

    public class PlanetData
    {
        public string PlanetName { get; set; }
        public List<SatelliteData> SatelliteDataList { get; set; }
    }
    public class SatelliteData
    {
        public string SatelliteName { get; set; }
        public TemperatureData TemperatureData { get; set; }
    }
    public class TemperatureData
    {
        public double DailyTemperature { get; set; }
        public double WeeklyTemperature { get; set; }
        public double MonthlyTemperature { get; set; }
    }
}