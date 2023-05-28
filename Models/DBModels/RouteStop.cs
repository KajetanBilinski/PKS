namespace PKS.Models.DBModels
{
    public class RouteStop
    {
        public int idRouteStop { get; set; }
        public DateTime ArriveTime { get; set; }
        public DateTime DepartueTime { get; set; }
        public int idStop { get; set; }
        public int idRoute { get; set; }

        public virtual Stop NavigationStop { get; set; }
        public virtual Route NavigationRoute { get; set; }

    }
}
