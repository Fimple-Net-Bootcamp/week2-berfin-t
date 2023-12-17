namespace SpaceWeatherApplication.Entities
{
    public class SpaceWeather
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<PlanetData> PlanetDataList { get; set; }
    }
}
