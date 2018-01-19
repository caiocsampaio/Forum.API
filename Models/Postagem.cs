using System;

namespace Forum.API.Models
{
    public class Postagem
    {
        public int id { get; set; }
        public int idtopico { get; set; }
        public int idusuario { get; set; }
        public string mensagem { get; set; }
        public DateTime datapublicacao { get; set; }
    }
}