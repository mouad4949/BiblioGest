using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiblioGest.Models
{
    public class Categorie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public string Description { get; set; }
        public List<Livre> Livres { get; set; } = new List<Livre>();
    }
}