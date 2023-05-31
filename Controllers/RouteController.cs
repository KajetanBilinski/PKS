using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Route;
using PKS.Models.DTO.Stop;
using Route = PKS.Models.DBModels.Route;
namespace PKS.Controllers
{
    [ApiController]
    [Route("api/route")]
    public class RouteController : ControllerBase
    {
        private readonly PKSContext pks;
        public RouteController(PKSContext pks)
        {
            this.pks = pks;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoutes()
        {
            List<RouteSelectDTO> routes = new List<RouteSelectDTO>();
            foreach(Route route in await pks.Route.ToListAsync())
            {
                List<StopSelectDTO> stops = new List<StopSelectDTO>();
                foreach (RouteStop routeStop in route.NavigationRouteStops)
                {
                    stops.Add(new StopSelectDTO()
                    {
                        StopName = routeStop.NavigationStop.StopName
                    });
                }
                routes.Add(new RouteSelectDTO()
                {
                    RouteName = route.RouteName,
                    Distance = route.Distance,
                    stops = stops
                });
            }
            return Ok(routes);
        }
    }
}
