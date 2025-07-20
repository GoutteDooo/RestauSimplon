-- Insertion de clients
INSERT INTO "Clients" ("Nom", "Prenom", "NumeroRue", "NomRue", "Ville", "CodePostal", "Telephone") VALUES
                                                                                                       ('Dupont', 'Jean', '12', 'Rue de Paris', 'Lyon', '69000', '0601020304'),
                                                                                                       ('Martin', 'Claire', '45', 'Avenue des Champs', 'Marseille', '13000', '0611121314'),
                                                                                                       ('Durand', 'Paul', '18', 'Boulevard Haussmann', 'Paris', '75008', '0622334455'),
                                                                                                       ('Bernard', 'Lucie', '33', 'Rue Lafayette', 'Toulouse', '31000', '0633445566'),
                                                                                                       ('Petit', 'Marc', '7', 'Rue Nationale', 'Lille', '59000', '0644556677'),
                                                                                                       ('Robert', 'Sophie', '56', 'Chemin Vert', 'Nice', '06000', '0655667788'),
                                                                                                       ('Richard', 'Louis', '99', 'Rue du Faubourg', 'Strasbourg', '67000', '0666778899'),
                                                                                                       ('Moreau', 'Julie', '22', 'Avenue Victor Hugo', 'Nantes', '44000', '0677889900'),
                                                                                                       ('Laurent', 'Antoine', '88', 'Rue de la République', 'Bordeaux', '33000', '0688990011'),
                                                                                                       ('Simon', 'Camille', '17', 'Rue Neuve', 'Grenoble', '38000', '0699001122');

-- Insertion d'articles
INSERT INTO "Articles" ("Nom", "Prix", "Categorie", "Description", "Disponible") VALUES
                                                                                     ('Pizza Margherita', 9.99, 1, 'Pizza classique avec sauce tomate et mozzarella', true),
                                                                                     ('Burger Maison', 12.50, 2, 'Burger au bœuf avec frites maison', true),
                                                                                     ('Salade César', 8.00, 3, 'Salade verte, poulet, croutons, parmesan', true),
                                                                                     ('Tiramisu', 5.50, 4, 'Dessert italien au café et mascarpone', true);

-- Insertion de commandes
INSERT INTO "Commandes" ("DateCommande", "TypeCommande", "EstTermine", "ClientId") VALUES
                                                                                       (NOW(), 0, false, 1),
                                                                                       (NOW(), 1, true, 2),
                                                                                       (NOW(), 0, true, 3),
                                                                                       (NOW(), 1, false, 4),
                                                                                       (NOW(), 0, true, 5),
                                                                                       (NOW(), 1, false, 6),
                                                                                       (NOW(), 0, false, 7),
                                                                                       (NOW(), 1, true, 8);

-- Insertion des articles dans les commandes
INSERT INTO "CommandeArticles" ("IdCommande", "IdArticle", "Quantite", "ArticleId") VALUES
                                                                                        (1, 1, 2, 1),  -- Jean : 2 pizzas
                                                                                        (1, 4, 1, 4),  -- Jean : 1 tiramisu

                                                                                        (2, 2, 1, 2),  -- Claire : 1 burger
                                                                                        (2, 3, 1, 3),  -- Claire : 1 salade

                                                                                        (3, 1, 1, 1),  -- Paul : 1 pizza
                                                                                        (3, 2, 2, 2),  -- Paul : 2 burgers

                                                                                        (4, 3, 2, 3),  -- Lucie : 2 salades
                                                                                        (4, 4, 2, 4),  -- Lucie : 2 tiramisus

                                                                                        (5, 2, 1, 2),  -- Marc : 1 burger
                                                                                        (5, 1, 1, 1),  -- Marc : 1 pizza

                                                                                        (6, 1, 3, 1),  -- Sophie : 3 pizzas

                                                                                        (7, 3, 1, 3),  -- Louis : 1 salade

                                                                                        (8, 4, 2, 4);  -- Julie : 2 tiramisus
