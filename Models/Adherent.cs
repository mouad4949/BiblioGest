using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BiblioGest.Models
{
    public class Adherent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Telephone { get; set; }
        public DateTime DateInscription { get; set; }
        public string Statut { get; set; } = "Actif";
        public List<Emprunt> Emprunts { get; set; } = new List<Emprunt>();
    }
}