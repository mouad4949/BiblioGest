using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiblioGest.Models
{
    public class Livre
    {
        [Key]
        public string ISBN { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Auteur { get; set; }
        public string Editeur { get; set; }
        public int AnneePublication { get; set; }
        public int NombreExemplaires { get; set; }
        public int CategorieId { get; set; }
        public Categorie Categorie { get; set; }
        public List<Emprunt> Emprunts { get; set; } = new List<Emprunt>();
    }
}