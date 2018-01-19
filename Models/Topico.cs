using System;

namespace Forum.API.Models
{
    public class Topico
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public DateTime datacadastro { get; set; }
    }
}