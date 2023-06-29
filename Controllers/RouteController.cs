using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Discount;
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


        [HttpGet("{routeId}")]
        public async Task<IActionResult> GetRouteById(int routeId)
        {
            if (routeId < 0)
            {
                return BadRequest("Route id cannot be less than 0");
            }
            var route = await pks.Route.FirstOrDefaultAsync(b => b.idRoute == routeId);
            if (route is null)
            {
                return NotFound($"Route with id: {routeId} does not exists");
            }
            var stops = new List<StopSelectDTO>();
            foreach(var routeStop in route.NavigationRouteStops.ToList())
            {
                stops.Add(new StopSelectDTO()
                {
                    StopName = routeStop.NavigationStop.StopName,
                });
            }
            var routeReturn = new RouteSelectDTO()
            {
               RouteName= route.RouteName,
               Distance= route.Distance,
               stops= stops
            };
            return Ok(routeReturn);
        }
    }
}
