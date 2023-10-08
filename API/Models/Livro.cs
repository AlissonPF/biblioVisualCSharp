
namespace API.Models

{
    public class Livro
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public DateTime DataPublicacao { get; set; }
        public List<Emprestimo> Emprestimos { get; set; }
    }
}