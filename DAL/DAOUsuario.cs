using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Forum.API.Connection;
using Forum.API.Models;

namespace Forum.API.DAL
{
    public class DAOUsuario
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
                
        /// <summary>
            /// Fornece lista de todos os usuários cadastrados.
            /// </summary>
            /// <returns>List(Usuario)</returns>
        public List<Usuario> ListarUsuarios(){
            var ls = new List<Usuario>(); //empty list of users

            try{
                con = new SqlConn().Connection(); //SQL connection using private string

                string query = "SELECT * FROM Usuarios"; //SQL Query

                cmd = new SqlCommand(query, con); //SQL command

                con.Open(); //open connection
                dr = cmd.ExecuteReader(); //start db reader

                while(dr.Read()){ //while read, add user to user list
                    ls.Add(new Usuario(){
                        id = dr.GetInt32(0),
                        nome = dr.GetString(1),
                        login = dr.GetString(2),
                        senha = dr.GetString(3),
                        datacadastro = dr.GetDateTime(4)
                    });
                }
            }
            catch(SqlException ex){
                throw new Exception(ex.Message);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            finally{
                con.Close(); //close connection
            }

            return ls;
        }
        public bool CadastrarUsuario(Usuario user){
            bool r = false;

            try{
                con = new SqlConn().Connection(); //SQL connection

                string query = "INSERT INTO usuarios (nome, login, senha) VALUES (@n, @l, @s)"; //SQL Query

                cmd = new SqlCommand(query, con); //SQL command and parameters
                cmd.Parameters.AddWithValue("@n", user.nome);
                cmd.Parameters.AddWithValue("@l", user.login);
                cmd.Parameters.AddWithValue("@s", user.senha);

                con.Open(); //open connection

                int i = cmd.ExecuteNonQuery();
                
                if(i > 0)
                    r = true;
                
                cmd.Parameters.Clear();
            }
            catch(SqlException ex){
                throw new Exception(ex.Message);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            finally{
                con.Close();
            }
            
            return r;
        }

        public bool EditarUsuario(Usuario user){
            bool r = false;

            try{
                con = new SqlConn().Connection(); //SQL connection

                string query = "UPDATE usuarios SET nome=@n, login=@l, senha=@s WHERE id=@i"; //SQL Query

                cmd = new SqlCommand(query, con); //SQL command and parameters
                cmd.Parameters.AddWithValue("@n", user.nome);
                cmd.Parameters.AddWithValue("@l", user.login);
                cmd.Parameters.AddWithValue("@s", user.senha);
                cmd.Parameters.AddWithValue("@i", user.id);

                con.Open(); //open connection

                int i = cmd.ExecuteNonQuery();
                
                if(i > 0)
                    r = true;
                
                cmd.Parameters.Clear();
            }
            catch(SqlException ex){
                throw new Exception(ex.Message);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            finally{
                con.Close();
            }
            
            return r;
        }

        public bool ExcluirUsuario(int id){
            bool r = false;

            try{
                con = new SqlConn().Connection(); //SQL connection

                string query = "DELETE usuarios WHERE id=@i"; //SQL Query

                cmd = new SqlCommand(query, con); //SQL command and parameter
                cmd.Parameters.AddWithValue("@i", id);

                con.Open(); //open connection

                int i = cmd.ExecuteNonQuery();
                
                if(i > 0)
                    r = true;
                
                cmd.Parameters.Clear();
            }
            catch(SqlException ex){
                throw new Exception(ex.Message);
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
            finally{
                con.Close();
            }
            
            return r;
        }
    }
}