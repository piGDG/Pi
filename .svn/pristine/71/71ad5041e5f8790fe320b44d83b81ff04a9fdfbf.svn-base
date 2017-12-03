using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication5.Controllers
{
    [Produces("application/json")]
    public class ConnectController : Controller
    {
        [HttpGet]
        [Route("api/ConnectMobile")]
        [Authorize(Roles = "Admin,User,Mod")]
        public async Task<bool> GetAll()
        {
                return true;
        }
    }
}