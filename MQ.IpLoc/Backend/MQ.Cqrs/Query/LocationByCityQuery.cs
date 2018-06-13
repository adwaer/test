﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQ.Dal;
using MQ.Domain;

namespace MQ.Cqrs.Query
{
    public class LocationByCityQuery : IQuery<string, Task<IEnumerable<Location>>>
    {
        private static readonly Dictionary<string, IEnumerable<Location>> CityLocationCacheDictionary;

        static LocationByCityQuery()
        {
            CityLocationCacheDictionary = new Dictionary<string, IEnumerable<Location>>();
        }

        private readonly EntityDataSet _entityDataSet;
        public LocationByCityQuery(EntityDataSet entityDataSet)
        {
            _entityDataSet = entityDataSet;
        }


        public async Task<IEnumerable<Location>> Execute(string city)
        {
            IEnumerable<Location> locations;
            if (!CityLocationCacheDictionary.TryGetValue(city, out locations))
            {
                locations = await FindLocationByCity(city);

                var array = locations as Location[] ?? locations.ToArray();
                if (array.Any())
                {
                    CityLocationCacheDictionary[city] = array;
                }
            }

            return locations;
        }

        private async Task<IEnumerable<Location>> FindLocationByCity(string city)
        {
            return await Task
                .Run(() =>
                {
                    return _entityDataSet
                        .Locations
                        .Where(l => l.City.Trim().Equals(city, StringComparison.Ordinal));
                });
        }
    }
}
