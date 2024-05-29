// nao apague
namespace Glimpse.Models;
public class BreadCrumb
{    
    public string ActionName { get; set; }
    public string ControllerName { get; set; }
    public bool HasRouteId { get; set; }
    public int? RouteId { get; set; }
    public BreadCrumb(string an, string cn, bool hrid, int rid) {
        this.ActionName = an;
        this.ControllerName = cn;
        this.HasRouteId = hrid;
        this.RouteId = rid;
    }
    public BreadCrumb(string an, string cn, bool hrid) {
        this.ActionName = an;
        this.ControllerName = cn;
        this.HasRouteId = hrid;
    }
}