using BiblioGest.Data;
using BiblioGest.Helpers;
using BiblioGest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BiblioGest.ViewModels
{
    public class AdherentViewModel : ObservableObject
    {
        private readonly BiblioGestContext _context;
        private ObservableCollection<Adherent> _adherents;
        private string _recherche;
        private Adherent _selectedAdherent;

        public ObservableCollection<Adherent> Adherents
        {
            get => _adherents;
            set => SetProperty(ref _adherents, value);
        }

        public string Recherche
        {
            get => _recherche;
            set
            {
                SetProperty(ref _recherche, value);
                FiltrerAdherents();
            }
        }

        public Adherent SelectedAdherent
        {
            get => _selectedAdherent;
            set => SetProperty(ref _selectedAdherent, value);
        }

        public ICommand AjouterAdherentCommand { get; }
        public ICommand ModifierAdherentCommand { get; }
        public ICommand SupprimerAdherentCommand { get; }

        public AdherentViewModel(BiblioGestContext context)
        {
            _context = context;
            Adherents = new ObservableCollection<Adherent>(_context.Adherents.ToList());
            AjouterAdherentCommand = new RelayCommand(AjouterAdherent);
            ModifierAdherentCommand = new RelayCommand(ModifierAdherent, _ => SelectedAdherent != null);
            SupprimerAdherentCommand = new RelayCommand(SupprimerAdherent, _ => SelectedAdherent != null);
        }

        private void AjouterAdherent(object parameter)
        {
            var adherent = new Adherent
            {
                Nom = "Nouveau",
                Prenom = "Adhérent",
                Email = "email@example.com",
                DateInscription = DateTime.Now,
                Statut = "Actif"
            };
            _context.Adherents.Add(adherent);
            _context.SaveChanges();
            Adherents.Add(adherent);
        }

        private void ModifierAdherent(object parameter)
        {
            if (SelectedAdherent != null)
            {
                _context.Update(SelectedAdherent);
                _context.SaveChanges();
                MessageBox.Show("Adhérent modifié avec succès.");
            }
        }

        private void SupprimerAdherent(object parameter)
        {
            if (SelectedAdherent != null && MessageBox.Show($"Supprimer {SelectedAdherent.Nom} {SelectedAdherent.Prenom} ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _context.Adherents.Remove(SelectedAdherent);
                _context.SaveChanges();
                Adherents.Remove(SelectedAdherent);
            }
        }

        private void FiltrerAdherents()
        {
            if (string.IsNullOrEmpty(Recherche))
            {
                Adherents = new ObservableCollection<Adherent>(_context.Adherents.ToList());
            }
            else
            {
                Adherents = new ObservableCollection<Adherent>(_context.Adherents
                    .Where(a => a.Nom.Contains(Recherche, StringComparison.OrdinalIgnoreCase) ||
                                a.Prenom.Contains(Recherche, StringComparison.OrdinalIgnoreCase))
                    .ToList());
            }
        }
    }
}