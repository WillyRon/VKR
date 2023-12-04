using Forest_fire_control.BI.ServiceInterfaces;
using Forest_fire_control.Data.Config;
using Forest_fire_control.Data.Entity;
using Forest_fire_control.Data.Model;
using Forest_fire_control.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest_fire_control.BI.Services
{
    public class ObservationService : IObservationService
    {

        private readonly ApplicationDbContext _dbContext;

        public ObservationService( ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Region>> GetRegions()
        {
            return await _dbContext.Region.ToListAsync();
        }

        public async Task<List<ObservationSiteModel>> GetObservations()
        {
            var observations = await _dbContext.ObservationSite.Include(o => o.Region).ToListAsync();
            var observationModels = new List<ObservationSiteModel>();

            foreach (var observation in observations)
            {
                var observationModel = new ObservationSiteModel
                {
                    Name = observation.Name,
                    Longitude = observation.Longitude,
                    Latitude = observation.Latitude,
                    Address = observation.Address,
                    Region = observation.Region.Name,
                    Url = observation.Url,
                    IsActiveIncident = observation.IsActiveIncident,
                };

                observationModels.Add(observationModel);
            }

            return observationModels;
        }

        public async Task<Region> GetOrCreateRegion(string regionName)
        {

            Region region = await _dbContext.Region.FirstOrDefaultAsync(o => o.Name == regionName);

            if (region == null)
            {
                region = new Region { 
                    Name = regionName,
                    SysName = Transliterate(regionName),
                };

                _dbContext.Region.Add(region);
                await _dbContext.SaveChangesAsync();
            }

            return region;
        }

        public async Task<AuthenticationResult> CreateObservation(ObservationSiteModel observationModel)
        {
            var result = new AuthenticationResult();
            var observationDb = await GetObservation(observationModel.Longitude, observationModel.Latitude);
            if (observationDb != null)
            {
                result.ErrorMessage = "Точка с такими координатами уже существует";
                return result;
            }
            var region = await GetOrCreateRegion(observationModel.Region);
            var observation = new ObservationSite
            {
                Name = observationModel.Name,
                Longitude = observationModel.Longitude,
                Latitude = observationModel.Latitude,
                Address =  observationModel.Address,
                RegionId = region.Id,
                Url = observationModel.Url,
            };
            _dbContext.ObservationSite.Add(observation);
            var saveChangesResult = await _dbContext.SaveChangesAsync();

            if (saveChangesResult > 0)
            {
                result.Success = true;
                return result;
            }
            else
            {
                result.ErrorMessage = "Точка видеонаблюдения не добавлена";
                return result;
            }

        }

        public async Task<ObservationSite> GetObservation(float longitude, float latitude)
        {
            return await _dbContext.ObservationSite
                .FirstOrDefaultAsync(o => o.Longitude == longitude && o.Latitude == latitude);
        }

        public async Task<List<Incedent>> GetIncedentObservation(Guid observationid)
        {
            return await _dbContext.Incedent.Where(i => i.ObservationSiteId == observationid).OrderByDescending(i => i.Data).ToListAsync();
        }

        public async Task<List<VideoArchive>> GetVideoArchiveObservation(Guid observationid)
        {
            return await _dbContext.VideoArchive.Where(i => i.ObservationSiteId == observationid).OrderByDescending(i => i.Data).ToListAsync();
        }

        public async Task<List<Incedent>> GetIncedents()
        {
            return await _dbContext.Incedent.ToListAsync();
        }

        public async Task<AuthenticationResult> UpdateObservation(ObservationSiteModel observationModel)
        {
            var result = new AuthenticationResult();
            var observationDb = await GetObservation(observationModel.Longitude, observationModel.Latitude);

            if (observationDb == null)
            {
                result.ErrorMessage = "Точка видеонаблюдения не найдена";
                return result;
            }

            var region = await GetOrCreateRegion(observationModel.Region);

            observationDb.Name = observationModel.Name;
            observationDb.Longitude = observationModel.Longitude;
            observationDb.Latitude = observationModel.Latitude;
            observationDb.Address = observationModel.Address;
            observationDb.RegionId = region.Id;
            observationDb.Url = observationModel.Url;

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.ObservationSite.Update(observationDb);
                    var saveChangesResult = await _dbContext.SaveChangesAsync();

                    if (saveChangesResult > 0)
                    {
                        result.Success = true;
                        transaction.Commit();
                    }
                    else
                    {
                        result.ErrorMessage = "Точка видеонаблюдения не обновлена";
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    result.ErrorMessage = $"Ошибка при обновлении данных: {ex.Message}";
                    transaction.Rollback();
                }
            }

            return result;
        }

        public async Task<AuthenticationResult> DeleteObservation(ObservationSiteModel observationModel)
        {
            var result = new AuthenticationResult();
            var observationDb = await GetObservation(observationModel.Longitude, observationModel.Latitude);

            if (observationDb == null)
            {
                result.ErrorMessage = "Точка видеонаблюдения не найдена";
                return result;
            }

            _dbContext.ObservationSite.Remove(observationDb);
            var saveChangesResult = await _dbContext.SaveChangesAsync();

            if (saveChangesResult > 0)
            {
                result.Success = true;
                return result;
            }
            else
            {
                result.ErrorMessage = "Точка видеонаблюдения не удалена";
                return result;
            }

        }






        public static string Transliterate(string cyrillic)
        {
            Dictionary<char, string> translitDictionary = new Dictionary<char, string>
            {
                {'а', "a"}, {'б', "b"}, {'в', "v"}, {'г', "g"}, {'д', "d"},
                {'е', "e"}, {'ё', "e"}, {'ж', "zh"}, {'з', "z"}, {'и', "i"},
                {'й', "y"}, {'к', "k"}, {'л', "l"}, {'м', "m"}, {'н', "n"},
                {'о', "o"}, {'п', "p"}, {'р', "r"}, {'с', "s"}, {'т', "t"},
                {'у', "u"}, {'ф', "f"}, {'х', "kh"}, {'ц', "ts"}, {'ч', "ch"},
                {'ш', "sh"}, {'щ', "sch"}, {'ъ', ""}, {'ы', "y"}, {'ь', "'"},
                {'э', "e"}, {'ю', "yu"}, {'я', "ya"},
                {'А', "A"}, {'Б', "B"}, {'В', "V"}, {'Г', "G"}, {'Д', "D"},
                {'Е', "E"}, {'Ё', "E"}, {'Ж', "Zh"}, {'З', "Z"}, {'И', "I"},
                {'Й', "Y"}, {'К', "K"}, {'Л', "L"}, {'М', "M"}, {'Н', "N"},
                {'О', "O"}, {'П', "P"}, {'Р', "R"}, {'С', "S"}, {'Т', "T"},
                {'У', "U"}, {'Ф', "F"}, {'Х', "Kh"}, {'Ц', "Ts"}, {'Ч', "Ch"},
                {'Ш', "Sh"}, {'Щ', "Sch"}, {'Ъ', ""}, {'Ы', "Y"}, {'Ь', "'"},
                {'Э', "E"}, {'Ю', "Yu"}, {'Я', "Ya"}
            };

            StringBuilder result = new StringBuilder();

            foreach (char symbol in cyrillic)
            {
                if (translitDictionary.TryGetValue(symbol, out string translit))
                {
                    result.Append(translit);
                }
                else
                {
                    result.Append(symbol);
                }
            }

            return result.ToString();
        }
    }
}
