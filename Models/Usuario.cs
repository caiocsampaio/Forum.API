using System;

namespace Forum.API.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public DateTime datacadastro { get; set; }
    }
}