using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto.DAO;
using Projeto.Model;
using Projeto.Service;
using Projeto.VO;

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : BaseRoutes<Grupo, GrupoVO, DataBaseContext, AutoMapperProfile, GrupoDAO, GrupoService>
    {
        

    }
}
