using LNF.Impl.Worker;
using LNF.Worker;
using Microsoft.AspNet.SignalR;
using System.Web.Http;

namespace Worker.Controllers
{

    [MultiAuthorize]
    public class DefaultController : ApiController
    {
        [HttpPost, Route("api/execute")]
        public string Execute([FromBody] WorkerRequest req)
        {
            var svc = new WorkerService();
            return svc.Execute(req);
        }

        [HttpPost, Route("api/broadcast")]
        public string Broadcast([FromBody] LogMessage msg)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<WorkerHub>();
            hub.Clients.All.broadcast(msg);
            return "ok";
        }
    }
}
