using BiblioGest.Data;
using BiblioGest.Helpers;
using BiblioGest.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BiblioGest.ViewModels
{
    public class LivreViewModel : ObservableObject
    {
        private readonly BiblioGestContext _context;
        private ObservableCollection<Livre> _livres;
        private string _recherche;
        private Livre _selectedLivre;
        public ICommand RechercheAvanceeCommand { get; }
        private string _auteurFilter;
        public string AuteurFilter
        {
            get => _auteurFilter;
            set => SetProperty(ref _auteurFilter, value);
        }
        public ObservableCollection<Livre> Livres
        {
            get => _livres;
            set => SetProperty(ref _livres, value);
        }

        public string Recherche
        {
            get => _recherche;
            set
            {
                SetProperty(ref _recherche, value);
                FiltrerLivres();
            }
        }

        public Livre SelectedLivre
        {
            get => _selectedLivre;
            set => SetProperty(ref _selectedLivre, value);
        }

        public ICommand AjouterLivreCommand { get; }
        public ICommand ModifierLivreCommand { get; }
        public ICommand SupprimerLivreCommand { get; }
        public ICommand ExporterCsvCommand { get; }

        public LivreViewModel(BiblioGestContext context)
        {
            _context = context;
            Livres = new ObservableCollection<Livre>(_context.Livres.Include(l => l.Categorie).ToList());
            AjouterLivreCommand = new RelayCommand(AjouterLivre);
            ModifierLivreCommand = new RelayCommand(ModifierLivre, _ => SelectedLivre != null);
            SupprimerLivreCommand = new RelayCommand(SupprimerLivre, _ => SelectedLivre != null);
            ExporterCsvCommand = new RelayCommand(ExporterCsv);
            RechercheAvanceeCommand = new RelayCommand(RechercheAvancee);
        }
        private void RechercheAvancee(object parameter)
        {
            var query = _context.Livres.Include(l => l.Categorie).AsQueryable();
            if (!string.IsNullOrEmpty(Recherche))
                query = query.Where(l => l.Titre.Contains(Recherche, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(AuteurFilter))
                query = query.Where(l => l.Auteur.Contains(AuteurFilter, StringComparison.OrdinalIgnoreCase));
            Livres = new ObservableCollection<Livre>(query.ToList());
        }
        private void AjouterLivre(object parameter)
        {
            var livre = new Livre
            {
                ISBN = $"ISBN{System.DateTime.Now.Ticks}",
                Titre = "Nouveau Livre",
                Auteur = "Auteur",
                Editeur = "Éditeur",
                AnneePublication = 2023,
                NombreExemplaires = 1,
                CategorieId = 1
            };
            _context.Livres.Add(livre);
            _context.SaveChanges();
            Livres.Add(livre);
        }

        private void ModifierLivre(object parameter)
        {
            if (SelectedLivre != null)
            {
                _context.Update(SelectedLivre);
                _context.SaveChanges();
                MessageBox.Show("Livre modifié avec succès.");
            }
        }

        private void SupprimerLivre(object parameter)
        {
            if (SelectedLivre != null && MessageBox.Show($"Supprimer {SelectedLivre.Titre} ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _context.Livres.Remove(SelectedLivre);
                _context.SaveChanges();
                Livres.Remove(SelectedLivre);
            }
        }

        private void FiltrerLivres()
        {
            if (string.IsNullOrEmpty(Recherche))
            {
                Livres = new ObservableCollection<Livre>(_context.Livres.Include(l => l.Categorie).ToList());
            }
            else
            {
                Livres = new ObservableCollection<Livre>(_context.Livres
                    .Include(l => l.Categorie)
                    .Where(l => l.Titre.Contains(Recherche, System.StringComparison.OrdinalIgnoreCase) ||
                                l.Auteur.Contains(Recherche, System.StringComparison.OrdinalIgnoreCase))
                    .ToList());
            }
        }

        private void ExporterCsv(object parameter)
        {
            var csv = "ISBN,Titre,Auteur,Éditeur,Année\n";
            foreach (var livre in Livres)
            {
                csv += $"{livre.ISBN},{livre.Titre},{livre.Auteur},{livre.Editeur},{livre.AnneePublication}\n";
            }
            System.IO.File.WriteAllText("livres.csv", csv);
            MessageBox.Show("Exportation terminée !");
        }
    }
}