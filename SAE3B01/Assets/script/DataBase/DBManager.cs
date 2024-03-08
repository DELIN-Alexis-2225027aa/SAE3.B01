using UnityEngine;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;

/// <summary>
/// Gère l'accès à la base de données SQLite.
/// </summary>
public class DBManager
{
    private string cheminConnection;
    private SQLiteConnection connexionDB;

    /// <summary>
    /// Constructeur de la classe. Initialise la connexion à la base de données.
    /// </summary>
    public DBManager()
    {
        cheminConnection = "URI=file:dataBase.db";
        connexionDB = new SQLiteConnection(cheminConnection);
        connexionDB.Open();
    }

    /// <summary>
    /// Ferme la connexion à la base de données si elle est ouverte.
    /// </summary>
    public void CloseConnexion()
    {
        if (connexionDB != null && connexionDB.State != ConnectionState.Closed)
        {
            connexionDB.Close();
        }
    }

    /// <summary>
    /// Exécute une requête de base et retourne un lecteur de données.
    /// </summary>
    public IDataReader RequeteDeBase(string requete)
    {
        IDbCommand cmdDB = connexionDB.CreateCommand();
        cmdDB.CommandText = requete;
        return cmdDB.ExecuteReader();
    }

    /// <summary>
    /// Crée une table dans la base de données.
    /// </summary>
    public void CreateTable(string nom, string[] colonnes, string[] types)
    {
        // Construction de la requête CREATE TABLE
        string requete = "CREATE TABLE IF NOT EXISTS " + nom + "(" + colonnes[0] + " " + types[0];
        for (int i = 1; i < colonnes.Length; i++)
        {
            requete += ", " + colonnes[i] + " " + types[i];
        }
        requete += ")";
        // Exécution de la requête
        ExecuteRequestWithoutResult(requete);
    }

    /// <summary>
    /// Insère des valeurs dans une table de la base de données.
    /// </summary>
    public void Insert(string table, string[] valeurs)
    {
        // Vérification des valeurs
        if (valeurs == null || valeurs.Length == 0)
        {
            Debug.LogError("Aucune valeur à insérer.");
            return;
        }

        // Construction de la requête INSERT INTO
        string requete = "INSERT INTO " + table + " VALUES (";
        for (int i = 0; i < valeurs.Length; i++)
        {
            requete += "@valeur" + i;
            if (i < valeurs.Length - 1)
                requete += ", ";
        }
        requete += ")";

        // Exécution de la requête avec les paramètres
        using (IDbCommand cmdDB = connexionDB.CreateCommand())
        {
            cmdDB.CommandText = requete;
            var parameters = ((SQLiteCommand)cmdDB).Parameters;

            for (int i = 0; i < valeurs.Length; i++)
            {
                parameters.AddWithValue("@valeur" + i, valeurs[i]);
            }
            cmdDB.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Insère une seule valeur dans une table de la base de données.
    /// </summary>
    public void InsertOneValue(string table, string valeur)
    {
        // Vérification de la valeur
        if (string.IsNullOrEmpty(valeur))
        {
            Debug.LogError("Aucune valeur à insérer.");
            return;
        }

        // Construction de la requête INSERT INTO
        string requete = "INSERT INTO " + table + " VALUES (@valeur)";

        // Exécution de la requête avec les paramètres
        using (IDbCommand cmdDB = connexionDB.CreateCommand())
        {
            cmdDB.CommandText = requete;
            var parameters = ((SQLiteCommand)cmdDB).Parameters;

            parameters.AddWithValue("@valeur", valeur);

            cmdDB.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Sélectionne des données de la base de données en fonction de la table, de la clé et de la condition.
    /// </summary>
    public List<List<object>> Select(string table, string cle, string condition)
    {
        // Liste pour stocker les résultats de la requête SELECT
        List<List<object>> resultat = new List<List<object>>();

        // Construction de la requête SELECT
        string requete = "SELECT " + cle + " FROM " + table + " WHERE " + condition;

        // Exécution de la requête et récupération du lecteur de données
        IDataReader lecteur = RequeteDeBase(requete);

        // Traitement des résultats
        while (lecteur.Read())
        {
            List<object> row = new List<object>();
            for (int i = 0; i < lecteur.FieldCount; i++)
            {
                object value = lecteur.GetValue(i);
                if (value is byte[]) // Vérifie si la donnée est de type Blob
                {
                    byte[] byteArray = (byte[])value;
                    row.Add(byteArray);
                }
                else
                {
                    if (value != null && value.GetType() != typeof(string))
                    {
                        value = value.ToString();
                    }
                }

                row.Add(value);
            }
            resultat.Add(row);
        }
        lecteur.Close();

        return resultat;
    }

    /// <summary>
    /// Supprime des données de la base de données en fonction de la table, de la condition et de la valeur.
    /// </summary>
    public void Delete(string table, string condition, string valeur)
    {
        // Construction de la requête DELETE
        string requete = "DELETE FROM " + table + " WHERE " + condition + " = '" + valeur + "'";
        // Exécution de la requête
        ExecuteRequestWithoutResult(requete);
    }

    /// <summary>
    /// Supprime toutes les données d'une table de la base de données.
    /// </summary>
    public void DeleteEverythingFromTable(string table)
    {
        // Construction de la requête DELETE
        string requete = "DELETE FROM " + table;
        // Exécution de la requête
        ExecuteRequestWithoutResult(requete);
    }

    /// <summary>
    /// Exécute une requête sans résultat.
    /// </summary>
    private void ExecuteRequestWithoutResult(string requete)
    {
        IDbCommand cmdDB = connexionDB.CreateCommand();
        cmdDB.CommandText = requete;
        cmdDB.ExecuteNonQuery();
    }

    /// <summary>
    /// Supprime une table de la base de données.
    /// </summary>
    public void Droptable(string table)
    {
        // Construction de la requête DROP TABLE
        string requete = "DROP TABLE " + table;
        // Exécution de la requête
        ExecuteRequestWithoutResult(requete);
    }

    /// <summary>
    /// Met à jour une ligne dans une table de la base de données.
    /// </summary>
    public void UpdateTuple(DBManager dbManager, string tableName, string columnName, string newValue, string conditionColumn, string conditionValue)
    {
        // Construction de la requête UPDATE
        string updateQuery = $"UPDATE {tableName} SET \"{columnName}\" = '{newValue}' WHERE \"{conditionColumn}\" = '{conditionValue}'";
        string requete = updateQuery;

        // Exécution de la requête
        dbManager.ExecuteRequestWithoutResult(requete);
    }
}
