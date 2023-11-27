using Forest_fire_control.Data.Entity;
using Forest_fire_control.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forest_fire_control.BI.ServiceInterfaces
{
    public interface IObservationService
    {
        Task<Region> GetOrCreateRegion(string region);

        Task<List<Region>> GetRegions();

        Task<List<ObservationSiteModel>> GetObservations();

        Task<AuthenticationResult> CreateObservation(ObservationSiteModel observation);
    }
}
