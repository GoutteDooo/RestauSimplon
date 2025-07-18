// Navigation SPA simple
document.addEventListener('DOMContentLoaded', function () {

    // Navigation
    const navLinks = document.querySelectorAll('nav a');
    const pages = document.querySelectorAll('.page');

    navLinks.forEach(link => {
        link.addEventListener('click', function (e) {
            e.preventDefault();

            // Masquer toutes les pages
            pages.forEach(page => page.classList.remove('active'));

            // Afficher la page ciblée
            const targetPage = this.getAttribute('href').substring(1);
            document.getElementById(targetPage).classList.add('active');
        });
    });

    // Appel API
    document.getElementById('apiBtn').addEventListener('click', async function () {
        const resultDiv = document.getElementById('result');
        resultDiv.innerHTML = 'Chargement...';

        try {
            const response = await fetch('/api/data');
            const data = await response.json();
            resultDiv.innerHTML = `<h3>Résultat API:</h3><pre>${JSON.stringify(data, null, 2)}</pre>`;
        } catch (error) {
            resultDiv.innerHTML = `<h3>Erreur:</h3><p>${error.message}</p>`;
        }
    });

    // Formulaire de contact
    document.getElementById('contactForm').addEventListener('submit', async function (e) {
        e.preventDefault();

        const formData = new FormData(this);
        const data = Object.fromEntries(formData);

        try {
            const response = await fetch('/api/contact', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data)
            });

            if (response.ok) {
                alert('Message envoyé avec succès!');
                this.reset();
            } else {
                alert('Erreur lors de l\'envoi');
            }
        } catch (error) {
            alert('Erreur: ' + error.message);
        }
    });
});

// Utilitaires
const utils = {
    // Afficher une notification
    showNotification: function (message, type = 'info') {
        const notification = document.createElement('div');
        notification.className = `notification ${type}`;
        notification.textContent = message;

        document.body.appendChild(notification);

        setTimeout(() => {
            notification.remove();
        }, 3000);
    },

    // Formater une date
    formatDate: function (date) {
        return new Date(date).toLocaleDateString('fr-FR');
    }
};
