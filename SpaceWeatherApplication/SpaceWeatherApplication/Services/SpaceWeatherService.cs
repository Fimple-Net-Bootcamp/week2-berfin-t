using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using SpaceWeatherApplication.Entities;

namespace SpaceWeatherApplication.Services
{
    public class SpaceWeatherService
    {
        private static List<SpaceWeather> spaceWeatherList = new List<SpaceWeather>();
        private static int idCounter = 1;

        public SpaceWeather Create(SpaceWeather spaceWeather)
        {
            spaceWeather.Id = idCounter++;
            spaceWeatherList.Add(spaceWeather);
            return spaceWeather;
        }
        public List<SpaceWeather> GetAll()
        {
            return spaceWeatherList.ToList();
        }
                public SpaceWeather GetById(int id)
        {
            return spaceWeatherList.FirstOrDefault(sw => sw.Id == id);
        }
        public List<PlanetData> GetPlanetDataList(int id, string sortOrder = null)
        {
            var spaceWeather = spaceWeatherList.FirstOrDefault(sw => sw.Id == id);
            if (spaceWeather == null)
            {
                return null;
            }

            switch (sortOrder)
            {
                case "desc":
                    return spaceWeather.PlanetDataList.OrderByDescending(i => i.PlanetName).ToList();
                case "asc":
                    return spaceWeather.PlanetDataList.OrderBy(i => i.PlanetName).ToList();
                default:
                    return spaceWeather.PlanetDataList.ToList();
            }
        }
        public bool Update(int id, SpaceWeather updatedSpaceWeather)
        {
            var spaceWeather = spaceWeatherList.FirstOrDefault(sw => sw.Id == id);
            if (spaceWeather == null)
            {
                return false;
            }
            spaceWeather.Date = updatedSpaceWeather.Date;
            spaceWeather.PlanetDataList = updatedSpaceWeather.PlanetDataList;

            return true;
        }
        public bool PartialUpdateSpaceWeather(int id, JsonPatchDocument<SpaceWeather> patch)
        {
            var spaceWeather = spaceWeatherList.FirstOrDefault(sw => sw.Id == id);
            if (spaceWeather == null)
            {
                return false;
            }

            try
            {
                patch.ApplyTo(spaceWeather);
                return true;
            }
            catch (JsonPatchException)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            var spaceWeather = spaceWeatherList.FirstOrDefault(sw => sw.Id == id);
            if (spaceWeather == null)
            {
                return false;
            }

            spaceWeatherList.Remove(spaceWeather);
            return true;
        }
    }
}
