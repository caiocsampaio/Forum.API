using System;
using System.Collections.Generic;
using System.Linq;
using Forum.API.DAL;
using Forum.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        Usuario user = new Usuario();
        DAOUsuario dao = new DAOUsuario();
        
        [HttpGet]
        public IActionResult ListarUsuarios(){
            var r = new JsonResult(dao.ListarUsuarios());
            return Json(r);
        }

        [HttpGet("{id}", Name="Usuario")]
        public IActionResult Usuario(int id){
            var rs = new JsonResult(dao.ListarUsuarios().Where(u => u.id == id).FirstOrDefault());
            rs.ContentType = "application/json";

            if(rs.Value == null){ //Json value verification
                rs.StatusCode = 204;
                rs.Value = "Sem resultados";
            }
            else
                rs.StatusCode = 200;

            return Json(rs); //Json return
        }
        // public Usuario Usuario(int id){
        //     return dao.ListarUsuarios().Where(u => u.id == id).FirstOrDefault();
        // }

        [HttpPost]
        public Usuario CadastrarUsuario([FromBody] Usuario user){
            try{
                dao.CadastrarUsuario(user);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            return dao.ListarUsuarios().OrderByDescending(u => u.datacadastro).FirstOrDefault();//CreatedAtRoute("Usuario", new{id = user.id}, user);
        }

        [HttpPut("{id}")]
        public Usuario EditarUsuario([FromBody] Usuario user, int id){
            try{
                user.id = id;
                dao.EditarUsuario(user);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            return dao.ListarUsuarios().Where(u => u.id == id).FirstOrDefault();
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirUsuario(int id){
            dao.ExcluirUsuario(id);
            return Ok(id);
        }
    }
}