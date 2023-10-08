namespace API.Models
{
    public class Emprestimo
    {
        public int EmprestimoId { get; set; }
        public int LivroId { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }

        // Propriedades de navegação
        public Livro Livro { get; set; }
        public Cliente Cliente { get; set; }
    }
}