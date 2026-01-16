# 🍽️ RestauSimplon - API de Gestion de Commandes pour Restaurant

## 📌 Contexte

RestauSimplon est un restaurant qui souhaite digitaliser la gestion de ses commandes, actuellement effectuée sur papier. Ce projet a pour but de créer une **API REST** permettant de gérer les **articles du menu**, les **clients** et les **commandes**, tout en automatisant les calculs et en assurant une meilleure traçabilité.

[Lien Repo front](https://github.com/GoutteDooo/RestauSimplon-Front)
---

## 🎯 Objectifs Fonctionnels

### ✅ Fonctionnalités principales

1. **Gestion des Articles du Menu**
    - Ajouter, modifier, consulter, supprimer un article.
    - Informations : nom, prix, catégorie (`Entrée`, `Plat`, `Dessert`, `Boisson`), description (optionnel) et Disponible.

2. **Gestion des Clients**
    - Ajouter, modifier, consulter, supprimer un client.
    - Informations : nom, prénom, numéro de rue, nom de rue, ville, code postal, téléphone.

3. **Gestion des Commandes**
    - Associer une commande à un client.
    - savoir le type de commande (`livraison`, `sur place`, `à emporter`)
    - Ajouter un ou plusieurs articles dans une commande.
    - Calcul automatique du montant total.
    - Consultation des commandes :
        - Par client.
        - Par date.
        - savoir si une commande est terminée ou non

4. **Validation des Données**
    - Une commande doit contenir au moins un article.
    - Données obligatoires : noms, prix, etc.

---

## 💡 Fonctionnalités Bonus

1. **Gestion des Livraisons**
    - Statut de commande : `En cours`, `Livrée`.
    - Modification du statut.
    - Endpoint pour consulter les commandes en attente de livraison.

2. **Interface Front-End**
    - Interface simple (HTML/CSS, React, ou Next.js).
    - Deux interfaces : employé (gestion articles/clients) & client (passer commande).
    - Affichage et création de commandes via l'UI.

---

## 🏗️ Architecture Technique

- **Backend** : ASP.NET Core (Minimal API ou MVC)
- **ORM** : Entity Framework Core
- **Base de données** : Relationnelle (SQLite et/ou PostgreSQL)
- **Migrations EF Core** : pour gérer la structure de la base
- **Swagger** : documentation automatique des endpoints

---

## 🔁 Relations de Données

- Un client ⟶ plusieurs commandes.
- Une commande ⟷ plusieurs articles.
- Un article ⟷ plusieurs commandes.

---

## 🚀 Installation

1. **Cloner le repo**
   ```bash
   git clone https://github.com/GoutteDooo/restauSimplon.git
   cd restauSimplon
   
2. **Installer Dotnet EF de manière globale dans votre ordinateur**
   ouvrez votre powershell ou CMD en administrateur et ajoutez cette ligne
   ```powershell
   dotnet tool install --global dotnet-ef
   
3. **Installer Postgresql dans votre pc afin de pouvoir faire fonctionner notre projet
   https://www.postgresql.org/download/

4. **créer un fichier .txt nommé "db-password" pour lier votre bdd et votre projet avec le nom du mdp de votre UserAdmin dans postgresql

5. **Creer la migration dans la console de la solution**
   ```powershell
   dotnet ef migrations add InitialCreate
   
6. **Appliquer la migration afin qu'un fichier sqLite soit créé**
   ```powershell
   dotnet ef database update

---

Un fichier Restaurant.db sera ensuite créé après avoir appliqué la migration.
