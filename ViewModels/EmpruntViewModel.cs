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
    public class EmpruntViewModel : ObservableObject
    {
        private readonly BiblioGestContext _context;
        private ObservableCollection<Emprunt> _emprunts;
        private Emprunt _selectedEmprunt;

        public ObservableCollection<Emprunt> Emprunts
        {
            get => _emprunts;
            set => SetProperty(ref _emprunts, value);
        }

        public Emprunt SelectedEmprunt
        {
            get => _selectedEmprunt;
            set => SetProperty(ref _selectedEmprunt, value);
        }

        public ICommand NouvelEmpruntCommand { get; }
        public ICommand RetourEmpruntCommand { get; }
        public ICommand VerifierRetardsCommand { get; }

        public EmpruntViewModel(BiblioGestContext context)
        {
            _context = context;
            Emprunts = new ObservableCollection<Emprunt>(_context.Emprunts
                .Include(e => e.Livre)
                .Include(e => e.Adherent)
                .ToList());
            NouvelEmpruntCommand = new RelayCommand(NouvelEmprunt);
            RetourEmpruntCommand = new RelayCommand(RetourEmprunt, _ => SelectedEmprunt != null);
            VerifierRetardsCommand = new RelayCommand(VerifierRetards);
        }

        private void NouvelEmprunt(object parameter)
        {
            var livre = _context.Livres.FirstOrDefault(l => l.NombreExemplaires > 0);
            var adherent = _context.Adherents.FirstOrDefault();
            if (livre != null && adherent != null)
            {
                var emprunt = new Emprunt
                {
                    ISBN = livre.ISBN,
                    AdherentId = adherent.Id,
                    DateEmprunt = DateTime.Now,
                    DateRetourPrevue = DateTime.Now.AddDays(14)
                };
                livre.NombreExemplaires--;
                _context.Emprunts.Add(emprunt);
                _context.SaveChanges();
                Emprunts.Add(emprunt);
            }
            else
            {
                MessageBox.Show("Aucun livre ou adhérent disponible.");
            }
        }

        private void RetourEmprunt(object parameter)
        {
            if (SelectedEmprunt != null)
            {
                SelectedEmprunt.DateRetourEffective = DateTime.Now;
                var livre = _context.Livres.FirstOrDefault(l => l.ISBN == SelectedEmprunt.ISBN);
                if (livre != null) livre.NombreExemplaires++;
                _context.SaveChanges();
                MessageBox.Show("Retour enregistré.");
            }
        }

        private void VerifierRetards(object parameter)
        {
            var retards = _context.Emprunts
                .Include(e => e.Adherent)
                .Where(e => e.DateRetourPrevue < DateTime.Now && e.DateRetourEffective == null)
                .ToList();
            if (retards.Any())
            {
                MessageBox.Show($"Il y a {retards.Count} emprunts en retard.", "Alertes");
            }
            else
            {
                MessageBox.Show("Aucun emprunt en retard.");
            }
        }
    }
}