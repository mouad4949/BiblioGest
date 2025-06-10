
# BiblioGest

![image](https://github.com/user-attachments/assets/89afad68-eb63-4c48-84b1-4fe94e57bc1d)

**BiblioGest**
 est une application de gestion de bibliothèque développée en WPF sous .NET 8.0, conçue pour répondre aux exigences d'un cahier des charges (CDC) précis. L'application permet de gérer efficacement les livres, les adhérents, et les emprunts, avec une interface utilisateur moderne et intuitive. Elle utilise une base de données SQLite pour stocker les données, Entity Framework Core pour l'accès aux données, et le modèle MVVM pour une architecture propre et maintenable.
 # BiblioGest

**BiblioGest** est une application de gestion de bibliothèque développée en WPF sous .NET 8.0, conçue pour répondre aux exigences d'un cahier des charges (CDC) précis. L'application permet de gérer efficacement les livres, les adhérents, et les emprunts, avec une interface utilisateur moderne et intuitive. Elle utilise une base de données SQLite pour stocker les données, Entity Framework Core pour l'accès aux données, et le modèle MVVM pour une architecture propre et maintenable.

## Table des matières

-   Présentation
-   Fonctionnalités
-   Architecture technique
-   Installation
-   Utilisation
    -   Connexion
    -   Tableau de bord
    -   Gestion des livres
    -   Gestion des adhérents
    -   Gestion des emprunts
    -   Fonctionnalités avancées
-   Captures d'écran
-   Dépendances
-   Contribuer
-   Licence

## Présentation

BiblioGest est une solution complète pour la gestion d'une bibliothèque, adaptée aux besoins des bibliothécaires et des administrateurs. L'application offre une interface utilisateur élégante avec un thème sombre, des animations fluides, et une navigation intuitive. Elle permet de suivre les livres disponibles, les adhérents inscrits, et les emprunts en cours, tout en fournissant des fonctionnalités avancées comme la recherche multicritères, les notifications de retard, et l'exportation de données au format CSV.

L'application a été développée en respectant les bonnes pratiques de développement logiciel, avec une séparation claire entre la logique métier, la présentation, et l'accès aux données. La base de données SQLite garantit une gestion légère et performante des données.

## Fonctionnalités

-   **Connexion sécurisée** : Authentification des utilisateurs avec identifiant et mot de passe.
-   **Tableau de bord** : Vue d'ensemble avec des statistiques sur les livres disponibles, les emprunts en cours, et les retards.
-   **Gestion des livres** : Ajout, modification, suppression, et recherche de livres par ISBN, titre, auteur, ou catégorie.
-   **Gestion des adhérents** : Gestion des informations des adhérents (nom, prénom, email, statut, etc.).
-   **Gestion des emprunts** : Enregistrement des emprunts, suivi des dates de retour, et notification des retards.
-   **Recherche avancée** : Filtres multicritères pour les livres, adhérents, et emprunts.
-   **Exportation CSV** : Exportation des données des livres, adhérents, et emprunts pour analyse externe.
-   **Statistiques** : Visualisation des données clés (nombre de livres par catégorie, emprunts par période, etc.).
-   **Thème moderne** : Interface utilisateur avec un design sombre, des animations, et une ergonomie optimisée.

## Architecture technique

BiblioGest est construit avec les technologies suivantes :

-   **Framework** : .NET 8.0
-   **Interface utilisateur** : WPF (Windows Presentation Foundation)
-   **Modèle architectural** : MVVM (Model-View-ViewModel)
-   **Base de données** : SQLite
-   **ORM** : Entity Framework Core 8.0.8
-   **Dépendances** :
    -   Microsoft.EntityFrameworkCore : Pour l'accès aux données.
    -   Microsoft.EntityFrameworkCore.Sqlite : Pour la base de données SQLite.
    -   Microsoft.EntityFrameworkCore.Design : Pour les migrations EF Core.
    -   Microsoft.Extensions.Configuration.Json : Pour la gestion des paramètres (chaîne de connexion).
    -   Microsoft.Extensions.DependencyInjection : Pour l'injection de dépendances.

### Structure du projet

-   **Data** : Contient le contexte EF Core (BiblioGestContext) pour l'accès à la base de données.
-   **Models** : Définit les entités (Livre, Adherent, Emprunt, Categorie).
-   **ViewModels** : Contient les ViewModels pour la logique de présentation (MainViewModel, LivreViewModel, etc.).
-   **Views** : Contient les interfaces utilisateur XAML (LoginView, MainView, LivreView, etc.).
-   **Themes** : Contient les styles globaux (Styles.xaml) pour le thème de l'application.
-   **Helpers** : Contient les classes utilitaires (ObservableObject, RelayCommand).

### Base de données

La base de données SQLite (bibliogest.db) contient les tables suivantes :

-   **Livres** : ISBN, titre, auteur, éditeur, année, exemplaires, catégorie.
-   **Adherents** : ID, nom, prénom, email, téléphone, date d'inscription, statut.
-   **Emprunts** : ID, ISBN, ID adhérent, dates d'emprunt, retour prévu, retour effectif.
-   **Categories** : ID, nom, description.

Les relations sont définies via des clés étrangères, et des données initiales (catégories "Roman" et "Science-Fiction") sont insérées via les migrations EF Core.

## Installation

Pour installer et exécuter BiblioGest, suivez ces étapes :

### Prérequis

-   Visual Studio 2022 avec le workload **.NET Desktop Development**.
-   .NET 8.0 SDK.
-   (Facultatif) DB Browser for SQLite pour visualiser la base de données.

## Utilisation

Voici un guide détaillé pour utiliser chaque partie de l'application.

### Connexion

L'application démarre avec une interface de connexion sécurisée. Les utilisateurs doivent entrer un identifiant et un mot de passe pour accéder au tableau de bord.

-   **Identifiant** : admin
-   **Mot de passe** : password
-   Une case "Se souvenir de moi" est disponible (non fonctionnelle dans la version actuelle).


### Tableau de bord

Après connexion, le tableau de bord affiche une vue d'ensemble avec :

-   Statistiques : Nombre de livres disponibles, emprunts en cours, et retards.
-   Menu latéral : Navigation vers les sections Livres, Adhérents, et Emprunts.

Le design utilise un thème sombre avec des cartes stylées pour les statistiques et des boutons animés pour la navigation.



### Gestion des livres

La section Livres permet de :

-   Consulter la liste des livres (ISBN, titre, auteur, éditeur, année, exemplaires, catégorie).
-   Ajouter un nouveau livre.
-   Rechercher par ISBN, titre, auteur, ou catégorie.
-   Exporter la liste au format CSV.

Les données sont stockées dans la table Livres de la base de données SQLite, avec une relation vers la table Categories.



### Gestion des adhérents

La section Adhérents permet de :

-   Consulter la liste des adhérents (ID, nom, prénom, email, statut).
-   Ajouter un nouvel adhérent.
-   Rechercher par nom, prénom, ou email.
-   Mettre à jour le statut (Actif/Inactif).

Les données sont stockées dans la table Adherents.



### Gestion des emprunts

La section Emprunts permet de :

-   Consulter la liste des emprunts (ID, livre, adhérent, dates d'emprunt, retour prévu, retour effectif).
-   Enregistrer un nouvel emprunt.
-   Vérifier les retards (emprunts dont la date de retour prévue est dépassée).
-   Marquer un retour effectif.

Les données sont stockées dans la table Emprunts, avec des relations vers Livres et Adherents.



### Fonctionnalités avancées

-   **Recherche multicritères** : Filtres dynamiques pour trouver rapidement des livres, adhérents, ou emprunts.
-   **Notifications de retard** : Mise en évidence des emprunts en retard dans la section Emprunts.
-   **Exportation CSV** : Génération de fichiers CSV pour les données des livres, adhérents, et emprunts.
-   **Statistiques** : Affichage des données agrégées (par exemple, nombre de livres par catégorie).



## Captures d'écran

Les captures d'écran ci-dessus illustrent chaque section de l'application. Pour une meilleure visualisation, placez les fichiers PNG correspondants dans le dossier Screenshots and update the paths in this README.

![image](https://github.com/user-attachments/assets/df6ed67e-c304-4b14-ba62-2a2d525891e3)

![image](https://github.com/user-attachments/assets/bff5393e-e754-45eb-93f1-4eb12c4045d5)

![image](https://github.com/user-attachments/assets/df890c69-36ea-4758-9271-de7eaf33035f)



## Dépendances

-   .NET 8.0
-   Entity Framework Core 8.0.8
-   SQLite
-   Visual Studio 2022


