using System;
using System.Collections.Generic;
using System.Linq;
using Forum.API.DAL;
using Forum.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    public class PostagemController : Controller
    {
        Postagem message = new Postagem();
        DAOPostagem dao = new DAOPostagem();

        [HttpGet]
        public IEnumerable<Postagem> ListarPostagens(){
            return dao.ListarPostagens();
        }

        [HttpGet("{id}", Name="Postagem")]
        public Postagem Postagem(int id){
            return dao.ListarPostagens().Where(u => u.id == id).FirstOrDefault();
        }

        [HttpPost]
        public Postagem CadastrarPostagem([FromBody] Postagem user){
            try{
                dao.CadastrarPostagem(user);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            return dao.ListarPostagens().OrderByDescending(u => u.datapublicacao).FirstOrDefault();
        }

        [HttpPut("{id}")]
        public Postagem EditarPostagem([FromBody] Postagem user, int id){
            try{
                user.id = id;
                dao.EditarPostagem(user);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            return dao.ListarPostagens().Where(u => u.id == id).FirstOrDefault();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirPostagem(int id){
            dao.ExcluirPostagem(id);
            return Ok(id);
        }
    }
}