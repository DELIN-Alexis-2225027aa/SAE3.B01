using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Cette classe fournit des m�thodes pour convertir des valeurs r�cup�r�es depuis une base de donn�es.
/// </summary>
public class ConvertisseurDeValeurs
{
    private GestionnaireDB gestionnaireDB;

    int resultatEntier;
    string resultatChaine;
    public string nom;

    /// <summary>
    /// Le d�marrage est appel� avant la premi�re mise � jour du frame.
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
        // Logique du constructeur, si n�cessaire
    }

    /// <summary>
    /// Convertit une ligne r�cup�r�e depuis la base de donn�es en tableau de cha�nes.
    /// </summary>
    /// <param name="ligne">Liste d'objets repr�sentant une ligne de base de donn�es.</param>
    /// <returns>Tableau de cha�nes.</returns>
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
    /// V�rifie si une cha�ne doit �tre divis�e en utilisant un d�limiteur.
    /// </summary>
    /// <param name="chaineAVerifier">Cha�ne � v�rifier.</param>
    /// <returns>Vrai si la cha�ne doit �tre divis�e, faux sinon.</returns>
    public bool VerifierSiChaineDoitEtreDivisee(string chaineAVerifier)
    {
        return chaineAVerifier.Contains("|");
    }

    /// <summary>
    /// Convertit une ligne r�cup�r�e depuis la base de donn�es en cha�ne.
    /// </summary>
    /// <param name="ligneAConvertir">Liste d'objets repr�sentant une ligne de base de donn�es.</param>
    /// <returns>Repr�sentation de la ligne en cha�ne.</returns>
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
    /// Convertit une cha�ne r�cup�r�e depuis la base de donn�es en tableau de cha�nes en utilisant un d�limiteur.
    /// </summary>
    /// <param name="chaineDB">Cha�ne r�cup�r�e depuis la base de donn�es.</param>
    /// <returns>Tableau de cha�nes.</returns>
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
    /// Convertit une cha�ne r�cup�r�e depuis la base de donn�es en tableau d'entiers en utilisant un d�limiteur.
    /// </summary>
    /// <param name="chaineDB">Cha�ne r�cup�r�e depuis la base de donn�es.</param>
    /// <returns>Tableau d'entiers.</returns>
    public int[] ConvertirChaineDBEnTableauDInt(string chaineDB)
    {
        return chaineDB.Split(',').Select(int.Parse).ToArray();
    }

    /// <summary>
    /// Remplace un espace r�serv� dans une cha�ne par un nom sp�cifi�.
    /// </summary>
    /// <param name="chaineAModifier">Cha�ne � modifier.</param>
    /// <param name="nomJoueur">Nom � utiliser pour remplacer l'espace r�serv�.</param>
    /// <returns>Cha�ne modifi�e.</returns>
    public string MettreNomDansChaine(string chaineAModifier, string nomJoueur)
    {
        chaineAModifier = chaineAModifier.Replace("$", nomJoueur);
        return chaineAModifier;
    }

    /// <summary>
    /// V�rifie si une cha�ne contient un espace r�serv� pour un nom.
    /// </summary>
    /// <param name="chaine">Cha�ne � v�rifier.</param>
    /// <returns>Vrai si la cha�ne contient un espace r�serv� pour un nom, faux sinon.</returns>
    public bool VerifierSiNomNecessaire(string chaine)
    {
        bool estNomNecessaire = chaine.Contains("$");
        return estNomNecessaire;
    }

    /// <summary>
    /// Obtient le nom du joueur depuis la base de donn�es.
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
