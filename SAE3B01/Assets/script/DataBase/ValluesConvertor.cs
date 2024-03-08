using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Cette classe fournit des méthodes pour convertir des valeurs récupérées depuis une base de données.
/// </summary>
public class ConvertisseurDeValeurs
{
    private GestionnaireDB gestionnaireDB;

    int resultatEntier;
    string resultatChaine;
    public string nom;

    /// <summary>
    /// Le démarrage est appelé avant la première mise à jour du frame.
    /// </summary>
    void Start()
    {
        gestionnaireDB = new GestionnaireDB();
    }

    /// <summary>
    /// Constructeur de la classe ConvertisseurDeValeurs.
    /// </summary>
    public ConvertisseurDeValeurs()
    {
        // Logique du constructeur, si nécessaire
    }

    /// <summary>
    /// Convertit une ligne récupérée depuis la base de données en tableau de chaînes.
    /// </summary>
    /// <param name="ligne">Liste d'objets représentant une ligne de base de données.</param>
    /// <returns>Tableau de chaînes.</returns>
    public string[] ConvertirLigneEnTableauDeChaine(List<object> ligne)
    {
        string chaine = ConvertirLigneEnChaine(ligne);

        if (chaine.Any(char.IsLetter))
        {
            if (VerifierSiChaineDoitEtreDivisee(chaine))
            {
                return ConvertirChaineDBEnTableauDeChaine(chaine);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Vérifie si une chaîne doit être divisée en utilisant un délimiteur.
    /// </summary>
    /// <param name="chaineAVerifier">Chaîne à vérifier.</param>
    /// <returns>Vrai si la chaîne doit être divisée, faux sinon.</returns>
    public bool VerifierSiChaineDoitEtreDivisee(string chaineAVerifier)
    {
        return chaineAVerifier.Contains("|");
    }

    /// <summary>
    /// Convertit une ligne récupérée depuis la base de données en chaîne.
    /// </summary>
    /// <param name="ligneAConvertir">Liste d'objets représentant une ligne de base de données.</param>
    /// <returns>Représentation de la ligne en chaîne.</returns>
    public string ConvertirLigneEnChaine(List<object> ligneAConvertir)
    {
        byte[] tableauBytes = (byte[])ligneAConvertir[0];
        resultatChaine = System.Text.Encoding.UTF8.GetString(tableauBytes);
        if (VerifierSiNomNecessaire(resultatChaine))
        {
            string nomJoueur = ObtenirNom();
            resultatChaine = MettreNomDansChaine(resultatChaine, nomJoueur);
        }
        return resultatChaine;
    }

    /// <summary>
    /// Convertit une chaîne récupérée depuis la base de données en tableau de chaînes en utilisant un délimiteur.
    /// </summary>
    /// <param name="chaineDB">Chaîne récupérée depuis la base de données.</param>
    /// <returns>Tableau de chaînes.</returns>
    public string[] ConvertirChaineDBEnTableauDeChaine(string chaineDB)
    {
        return chaineDB.Split('|');
    }

    /// <summary>
    /// OnDestroy pour nettoyer les ressources.
    /// </summary>
    void OnDestroy()
    {
        gestionnaireDB.FermerConnexion();
    }

    /// <summary>
    /// Convertit une chaîne récupérée depuis la base de données en tableau d'entiers en utilisant un délimiteur.
    /// </summary>
    /// <param name="chaineDB">Chaîne récupérée depuis la base de données.</param>
    /// <returns>Tableau d'entiers.</returns>
    public int[] ConvertirChaineDBEnTableauDInt(string chaineDB)
    {
        return chaineDB.Split(',').Select(int.Parse).ToArray();
    }

    /// <summary>
    /// Remplace un espace réservé dans une chaîne par un nom spécifié.
    /// </summary>
    /// <param name="chaineAModifier">Chaîne à modifier.</param>
    /// <param name="nomJoueur">Nom à utiliser pour remplacer l'espace réservé.</param>
    /// <returns>Chaîne modifiée.</returns>
    public string MettreNomDansChaine(string chaineAModifier, string nomJoueur)
    {
        chaineAModifier = chaineAModifier.Replace("$", nomJoueur);
        return chaineAModifier;
    }

    /// <summary>
    /// Vérifie si une chaîne contient un espace réservé pour un nom.
    /// </summary>
    /// <param name="chaine">Chaîne à vérifier.</param>
    /// <returns>Vrai si la chaîne contient un espace réservé pour un nom, faux sinon.</returns>
    public bool VerifierSiNomNecessaire(string chaine)
    {
        bool estNomNecessaire = chaine.Contains("$");
        return estNomNecessaire;
    }

    /// <summary>
    /// Obtient le nom du joueur depuis la base de données.
    /// </summary>
    /// <returns>Nom du joueur.</returns>
    public string ObtenirNom()
    {
        gestionnaireDB = new GestionnaireDB();

        List<List<object>> resultat = gestionnaireDB.Selectionner("DonneesJoueur", "nomJoueur", "1");
        foreach (List<object> ligneNom in resultat)
        {
            byte[] tableauBytes = (byte[])ligneNom[0];
            nom = System.Text.Encoding.UTF8.GetString(tableauBytes);
        }

        return nom;
    }
}
