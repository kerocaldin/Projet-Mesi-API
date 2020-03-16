using System;
using System.Web.Http;
using Michelin.ePC.Library.Logging;
using Michelin.ePC.Library.Unity;
using Microsoft.Web.Http;
using System.Web.Http.Description;
using ugame.api.Models.v1;
using System.Net.Http;
using System.Net;

namespace ugame.api.Controllers.v1
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/v{version:apiVersion}/example")]
    public class ExampleController : ApiController
    {
        /// <summary>
        /// The lazy parameter ILogger
        /// </summary>
        private readonly ILogger _logger;

        public ExampleController(ILogger logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [ResponseType(typeof(HelloApiModel))]
        public IHttpActionResult Hello()
        {
            try
            {
                return Ok(
                    new HelloApiModel
                    {
                        Hello = "Hello"
                    }
                );
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
        }

        [HttpPost]
        [Route("")]
        [ResponseType(typeof(HelloApiModel))]
        public IHttpActionResult SayHello(SayHelloApiModel sayHelloApiModel)
        {
            try
            {
                string response = $"Hello {sayHelloApiModel.Name}.";

                return Created(Request.RequestUri.ToString(), response);
            }
            catch(Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message));
            }
        }

        [HttpDelete]
        [Route("{ID}")]
        public IHttpActionResult Delete(string ID)
        {
            try
            {
                //Delete the object

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
            }
            catch(Exception e)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
            }
        }
    }
}
