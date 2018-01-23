using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Forum.API.Connection;
using Forum.API.Models;

namespace Forum.API.DAL
{
    public class DAOTopico : SqlConnect
    {
        
        /// <summary>
            /// Fornece lista de todos os tópicos.
            /// </summary>
            /// <returns>List(Topico)</returns>
        public List<Topico> ListarTopicos(){
            var ls = new List<Topico>(); //empty list of posts

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                con.ConnectionString = DbPath(); //SQL connection using private string

                string query = "SELECT * FROM topicoforum"; //SQL Query

                cmd = new SqlCommand(query, con); //SQL command

                con.Open(); //open connection
                dr = cmd.ExecuteReader(); //start db reader

                while(dr.Read()){ //while read, add post to post list
                    ls.Add(new Topico(){
                        id = dr.GetInt32(0),
                        titulo = dr.GetString(1),
                        descricao = dr.GetString(2),
                        datacadastro = dr.GetDateTime(3)
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
        /// <summary>
            /// Cadastrar novo topico no forum.
            /// </summary>
            /// <param name="post">classe Topico recebido por JSON.</param><br />
            /// <returns>True or False</returns>
        public bool CadastrarTopico(Topico post){
                
            bool r = false;

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                con.ConnectionString = DbPath(); //SQL connection using private string

                string query = "INSERT INTO topicoforum (titulo, descricao) VALUES (@n, @d)"; //SQL Query

                cmd = new SqlCommand(query, con); //SQL command and parameters
                cmd.Parameters.AddWithValue("@n", post.titulo);
                cmd.Parameters.AddWithValue("@d", post.descricao);

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
        /// <summary>
            /// Edita informações de um tópico existente.
            /// </summary>
            /// <param name="post">classe Topico recebido por JSON.</param><br />
            /// <returns>True or False</returns>
        public bool EditarTopico(Topico post){
            bool r = false;

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                con.ConnectionString = DbPath(); //SQL connection using private string

                string query = "UPDATE topicoforum SET titulo=@n, descricao=@d WHERE id=@i"; //SQL Query

                cmd = new SqlCommand(query, con); //SQL command and parameters
                cmd.Parameters.AddWithValue("@n", post.titulo);
                cmd.Parameters.AddWithValue("@d", post.descricao);
                cmd.Parameters.AddWithValue("@i", post.id);

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
        /// <summary>
            /// Exclui tópico do banco de dados.  
            /// ***A ação NÃO pode ser desfeita.***
            /// </summary>
            /// <param name="post">classe Topico recebido por JSON.</param><br />
            /// <returns>True or False</returns>
        public bool ExcluirTopico(int id){
            bool r = false;

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                con.ConnectionString = DbPath(); //SQL connection using private string

                string query = "DELETE topicoforum WHERE id=@i"; //SQL Query

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