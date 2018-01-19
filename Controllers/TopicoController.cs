using System;
using System.Collections.Generic;
using System.Linq;
using Forum.API.DAL;
using Forum.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    public class TopicoController : Controller
    {
        Topico post = new Topico();
        DAOTopico dao = new DAOTopico();

        [HttpGet]
        public IEnumerable<Topico> ListarTopicos(){
            return dao.ListarTopicos();
        }

        [HttpGet("{id}", Name="Topico")]
        public Topico Topico(int id){
            return dao.ListarTopicos().Where(u => u.id == id).FirstOrDefault();
        }

        [HttpPost]
        public Topico CadastrarTopico([FromBody] Topico user){
            try{
                dao.CadastrarTopico(user);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            return dao.ListarTopicos().OrderByDescending(u => u.datacadastro).FirstOrDefault();//CreatedAtRoute("Topico", new{id = user.id}, user);
        }

        [HttpPut("{id}")]
        public Topico EditarTopico([FromBody] Topico user, int id){
            try{
                user.id = id;
                dao.EditarTopico(user);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            return dao.ListarTopicos().Where(u => u.id == id).FirstOrDefault();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirTopico(int id){
            dao.ExcluirTopico(id);
            return Ok(id);
        }
    }
}