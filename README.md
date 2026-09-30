# Calculatrice mobile – .NET MAUI

Application mobile de calculatrice développée avec **.NET MAUI** et **C#**, dans le cadre de l’Activité 4 – Atelier de développement Mobile.

L'application a été conçue avec une interface **XAML** et testée sur un téléphone Android réel via le **débogage sans fil**.

## Technologies utilisées

* .NET 9
* .NET MAUI
* C#
* XAML
* Visual Studio 2022
* Android 15 (API 35)

## Fonctionnalités

### Opérations

* Addition
* Soustraction
* Multiplication
* Division
* Nombres décimaux
* Pourcentage `%`
* Changement de signe `±`
* Parenthèses `(` et `)`
* Affichage de l'opération au-dessus du résultat

### Contrôles

* `C` : réinitialisation complète
* `←` : suppression du dernier caractère
* `=` : calcul de l'expression

### Gestion des erreurs

L'application gère notamment :

* La division par zéro avec le message « Division par zéro impossible »
* Les expressions invalides avec le message « Erreur de syntaxe »
* Les opérateurs consécutifs : le dernier opérateur remplace le précédent

## Interface et layouts

L'interface utilise plusieurs types de layouts et conteneurs .NET MAUI :

| Layout / Conteneur      | Utilisation                                                             |
| ----------------------- | ----------------------------------------------------------------------- |
| `ScrollView`            | Permet l'adaptation de l'interface aux petits écrans et au mode paysage |
| `VerticalStackLayout`   | Organise verticalement les différentes parties de l'application         |
| `HorizontalStackLayout` | Organise horizontalement la zone d'affichage de l'opération             |
| `Grid`                  | Organise les boutons de la calculatrice en lignes et colonnes           |
| `Border`                | Encadre la zone d'affichage avec des coins arrondis                     |

## Structure du projet

```text
CalcultriceMaui/
│
├── MainPage.xaml          # Interface de la calculatrice
├── MainPage.xaml.cs       # Logique et gestion des événements
├── App.xaml               # Ressources et configuration de l'application
├── AppShell.xaml          # Structure de navigation
├── MauiProgram.cs         # Configuration de l'application
│
└── Platforms/
    ├── Android/
    └── Windows/
```

## Installation et exécution

### Prérequis

* Visual Studio 2022
* Charge de travail « Développement d'applications multiplateformes .NET (MAUI) »
* SDK .NET 9
* Un appareil Android ou un émulateur Android

### Cloner le projet

```bash
git clone https://github.com/FredericMelissa/calculatrice-maui.git
```

### Ouvrir le projet

Ouvrir le fichier `.sln` dans Visual Studio 2022.

Sélectionner ensuite une cible Android ou Windows, puis lancer l'application avec :

```text
F5
```

## Tests

L'application a été compilée et testée sur un téléphone Android réel sous **Android 15 (API 35)**.

Les principales fonctionnalités ont été vérifiées, notamment les opérations arithmétiques, les nombres décimaux, les pourcentages, les parenthèses et la gestion de la division par zéro.

## Auteur

DJOUKA FONGANG FREDERIC MELISSA

Activité 4 – Atelier de développement Mobile
2026
