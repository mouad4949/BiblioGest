using BiblioGest.Data;
using BiblioGest.Helpers;
using System.Windows.Input;

namespace BiblioGest.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private object _currentView;
        private readonly BiblioGestContext _context;
        public int LivresDisponibles => _context.Livres.Sum(l => l.NombreExemplaires);
        public int EmpruntsEnCours => _context.Emprunts.Count(e => e.DateRetourEffective == null);
        public int Retards => _context.Emprunts.Count(e => e.DateRetourPrevue < DateTime.Now && e.DateRetourEffective == null);
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ICommand ShowLivresCommand { get; }
        public ICommand ShowAdherentsCommand { get; }
        public ICommand ShowEmpruntsCommand { get; }

        public MainViewModel(BiblioGestContext context,LivreViewModel livreViewModel, AdherentViewModel adherentViewModel, EmpruntViewModel empruntViewModel)
        {
            _context = context;
            ShowLivresCommand = new RelayCommand(_ => CurrentView = livreViewModel);
            ShowAdherentsCommand = new RelayCommand(_ => CurrentView = adherentViewModel);
            ShowEmpruntsCommand = new RelayCommand(_ => CurrentView = empruntViewModel);
            CurrentView = livreViewModel; 
            
        }
        
    }
}