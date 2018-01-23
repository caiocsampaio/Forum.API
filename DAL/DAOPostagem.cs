using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Forum.API.Connection;
using Forum.API.Models;

namespace Forum.API.DAL
{
    public class DAOPostagem : SqlConnect
    {

        public List<Postagem> ListarPostagens(){
            //var ls = new List<Postagem>(); //empty list of messages

            var ls = new List<Postagem>(); 

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                con.ConnectionString = DbPath();
                
                string query = "SELECT p.id, p.idtopico, t.titulo, t.descricao, p.mensagem, p.idusuario, u.nome, p.datapublicacao " +
                                "FROM postagens AS p " +
                                    "INNER JOIN usuarios AS u ON p.idusuario = u.id " +
                                    "INNER JOIN topicoforum AS t ON p.idtopico = t.id"; //SQL Query

                cmd = new SqlCommand(query, con); //SQL command

                con.Open(); //open connection
                dr = cmd.ExecuteReader(); //start db reader

                while(dr.Read()){
                    ls.Add(new Postagem{
                        id = dr.GetInt32(0),
                        idtopico = dr.GetInt32(1),
                        titulotopico = dr.GetString(2),
                        descricaotopico = dr.GetString(3),
                        mensagem = dr.GetString(4),
                        idusuario = dr.GetInt32(5),
                        nomeusuario = dr.GetString(6),
                        datapublicacao = dr.GetDateTime(7)
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
                con.Close();
            }

            return ls;
        }
        /// <summary>
            /// Cadastrar uma nova postagem/mensagem no forum.
            /// </summary>
            /// <param name="message">classe Postagem recebido por JSON.</param><br />
            /// <returns>True or False</returns>
        public bool CadastrarPostagem(Postagem message){
                
            bool r = false;

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                con.ConnectionString = DbPath(); //SQL connection using private string

                string query = "INSERT INTO postagens (idtopico, idusuario, mensagem) VALUES (@idt, @idu, @msg)"; //SQL Query

                cmd = new SqlCommand(query, con); //SQL command and parameters
                cmd.Parameters.AddWithValue("@idt", message.idtopico);
                cmd.Parameters.AddWithValue("@idu", message.idusuario);
                cmd.Parameters.AddWithValue("@msg", message.mensagem);

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
            /// <param name="message">classe Postagem recebido por JSON.</param><br />
            /// <returns>True or False</returns>
        public bool EditarPostagem(Postagem message){
            bool r = false;

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                con.ConnectionString = DbPath(); //SQL connection using private string

                string query = "UPDATE postagens SET mensagem=@msg WHERE id=@i"; //SQL Query

                cmd = new SqlCommand(query, con); //SQL command and parameters
                cmd.Parameters.AddWithValue("@msg", message.mensagem);

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
            /// <param name="message">classe Postagem recebido por JSON.</param><br />
            /// <returns>True or False</returns>
        public bool ExcluirPostagem(int id){
            bool r = false;

            try{
                con = new SqlConnection();
                cmd = new SqlCommand();
                con.ConnectionString = DbPath(); //SQL connection using private string

                string query = "DELETE postagens WHERE id=@i"; //SQL Query

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