using Forest_fire_control.Data.Entity;
using Forest_fire_control.Data.Model;
using Forest_fire_control.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forest_fire_control.BI.ServiceInterfaces
{
    public interface IObservationService
    {
        Task<Region> GetOrCreateRegion(string region);

        Task<List<Region>> GetRegions();

        Task<List<Incedent>> GetIncedents();

        Task<List<Incedent>> GetIncedentObservation(Guid id);

        Task<List<VideoArchive>> GetVideoArchiveObservation(Guid id);

        Task<List<ObservationSiteModel>> GetObservations();

        Task<AuthenticationResult> CreateObservation(ObservationSiteModel observation);

        Task<AuthenticationResult> DeleteObservation(ObservationSiteModel observationModel);

        Task<AuthenticationResult> UpdateObservation(ObservationSiteModel observationModel);

        Task<ObservationSite> GetObservation(float longitude, float latitude);
    }
}
