using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Projeto.DAO;
using Projeto.Model;
using Projeto.Service;
using Projeto.VO;

namespace Projeto.Controllers
{
    
    [ApiController]
    public class BaseRoutes<Entity, VO, TContext, AutoMapProfile, DAO, Service> : ControllerBase
        where VO : BaseVO, new()
        where Entity : BaseEntity, new()
        where TContext : DbContext, new()
        where AutoMapProfile: Profile, new()
        where DAO : BaseDAO<Entity, VO, TContext, AutoMapProfile>, new()
        where Service : BaseService<Entity, VO, TContext, AutoMapProfile, DAO>, new()
    {
        Service service = new Service();

        [HttpGet("{pageNumber}/{pageSize}/")]
        public List<VO> Get(int pageNumber = 1, int pageSize = 100, [FromQuery] VO filter = null)
        {
            return service.GetAll(pageNumber, pageSize, filter);
        }

        [HttpGet("{id}")]
        // GET api/<controller>/5
        public VO Get(int id)
        {
            return service.GetOne(id);
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody] VO vo)
        {
            try
            {
                service.save(vo);                
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(400, e.Message);
            }
            
        }

        [HttpPut]
        public ActionResult Put([FromBody]VO vo)
        {
            if (vo.id > 0)
            {
                try
                {
                    service.Update(vo);
                    return Ok();
                }
                catch
                {
                    return StatusCode(400, "Coloque o erro correto e uma msg");
                }

            }
            else
            {
                return StatusCode(400, "Coloque o erro correto e uma msg");
            }

        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (service.Delete(id))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(400, e.Message);
            }
        }


    }
}