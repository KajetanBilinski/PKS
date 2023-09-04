using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKS.Models.DBModels;
using PKS.Models.DTO.Discount;
using PKS.Models.DTO.Passenger;
using PKS.Models.DTO.Route;
using PKS.Models.DTO.Stop;
using PKS.Services;
using System.ComponentModel.DataAnnotations;
using Route = PKS.Models.DBModels.Route;
namespace PKS.Controllers
{
    [ApiController]
    [Route("api/route")]
    public class RouteController : ControllerBase
    {
        private readonly PKSContext pks;
        private readonly IPKSModelValidator validator;
        public RouteController(PKSContext pks, IPKSModelValidator pKSModelValidator)
        {
            this.pks = pks;
            this.validator = pKSModelValidator;
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

        [HttpPost]
        public async Task<IActionResult> AddRoute(RouteAddDTO routeAdd)
        {
            var error = validator.ValidateRouteAddDTO(routeAdd);
            if (error != null)
            {
                return BadRequest(error);
            }
            else
            {
                int idRoute = await pks.Route.CountAsync() > 0 ? await pks.Route.MaxAsync(r => r.idRoute) + 1 : 1;
                var route = new Route()
                {
                    idRoute = idRoute,
                    RouteName = routeAdd.RouteName,
                    Distance = routeAdd.Distance,
                    Cost = routeAdd.Cost
                };
                await pks.Route.AddAsync(route);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Route added");
            }
        }

        [HttpDelete("{idRoute}")]
        public async Task<IActionResult> DeleteRoute(int idRoute)
        {
            if (!await pks.Route.AnyAsync(r => r.idRoute == idRoute))
            {
                return BadRequest($"Route with id: {idRoute} doesn't exist");
            }
            else if (await pks.Ticket.AnyAsync(b => b.idRoute == idRoute))
            {
                return BadRequest($"Cannot remove Route due to connection with one or more tickets");
            }
            else if (await pks.RouteStop.AnyAsync(b => b.idRoute == idRoute))
            {
                return BadRequest($"Cannot remove Route due to connection with one or more route stops");
            }
            else
            {
                var route = await pks.Route.FirstOrDefaultAsync(r => r.idRoute == idRoute);
                pks.Route.Remove(route);
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok($"Route with id: {idRoute} was deleted");
            }
        }

        [HttpPut("{idRoute}")]
        public async Task<IActionResult> UpdateRoute(int idRoute, RouteAddDTO routeUpdate)
        {
            var error = validator.ValidateRouteAddDTO(routeUpdate);
            if (error != null)
            {
                return BadRequest(error);
            }
            else if (!await pks.Route.AnyAsync(r => r.idRoute == idRoute))
            {
                return BadRequest($"Route with id: {idRoute} doesn't exist");
            }
            else
            {
                var route = await pks.Route.FirstOrDefaultAsync(r => r.idRoute == idRoute);
                route.RouteName = routeUpdate.RouteName;
                route.Distance = routeUpdate.Distance;
                route.Cost = routeUpdate.Cost;
                if (await pks.SaveChangesAsync() <= 0)
                    return StatusCode(505);
                else
                    return Ok("Route updated");
            }
        }
    }
}
