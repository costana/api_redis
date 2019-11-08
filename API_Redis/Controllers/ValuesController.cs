using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;

namespace API_Redis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values

        [HttpGet]
        public ContentResult Get(
            [FromServices]IConfiguration config,
            [FromServices]IDistributedCache cache)
        {
            string retorno = null;
            try
            {
                retorno = cache.GetString("time");
                if (retorno == null)
                {
                    retorno = "Que horas são? " + DateTime.Now.ToShortTimeString();

                    DistributedCacheEntryOptions opcoesCache = new DistributedCacheEntryOptions();
                    opcoesCache.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    cache.SetString("time", retorno, opcoesCache);
                }
            }
            catch (Exception ex)
            {
                retorno = "Ocorreu um erro: " + ex.Message + "<br>" + ex.StackTrace + "<br>" + ex.InnerException;
            }
            return Content(retorno);
        }
    }
}
