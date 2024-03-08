using UnityEngine;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;

/// <summary>
/// Gère les opérations sur la base de données SQLite.
/// </summary>
public class DBManager
{
    private string cheminConnection;
    private SQLiteConnection connexionDB;

    /// <summary>
    /// Initialise la connexion à la base de données SQLite.
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
    /// Exécute une requête de base sur la base de données.
    /// </summary>
    /// <param name="requete">Requête SQL à exécuter.</param>
    /// <returns>Le résultat de la requête sous forme de lecteur de données.</returns>
    public IDataReader RequeteDeBase(string requete)
    {
        IDbCommand cmdDB = connexionDB.CreateCommand();
        cmdDB.CommandText = requete;
        return cmdDB.ExecuteReader();
    }

    /// <summary>
    /// Crée une table dans la base de données.
    /// </summary>
    /// <param name="nom">Nom de la table.</param>
    /// <param name="colonnes">Noms des colonnes.</param>
    /// <param name="types">Types de données des colonnes.</param>
    public void CreateTable(string nom, string[] colonnes, string[] types)
    {
        string requete = "CREATE TABLE IF NOT EXISTS " + nom + "(" + colonnes[0] + " " + types[0];
        for (int i = 1; i < colonnes.Length; i++)
        {
            requete += ", " + colonnes[i] + " " + types[i];
        }
        requete += ")";
        ExecuteRequestWithoutResult(requete);
    }

    /// <summary>
    /// Insère une nouvelle ligne dans la table avec les valeurs spécifiées.
    /// </summary>
    /// <param name="table">Nom de la table.</param>
    /// <param name="valeurs">Valeurs à insérer.</param>
    public void Insert(string table, string[] valeurs)
    {
        if (valeurs == null || valeurs.Length == 0)
        {
            return;
        }

        string requete = "INSERT INTO " + table + " VALUES (";
        for (int i = 0; i < valeurs.Length; i++)
        {
            requete += "@valeur" + i;
            if (i < valeurs.Length - 1)
                requete += ", ";
        }
        requete += ")";

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
    /// Insère une seule valeur dans la table.
    /// </summary>
    /// <param name="table">Nom de la table.</param>
    /// <param name="valeur">Valeur à insérer.</param>
    public void InsertOneValue(string table, string valeur)
    {
        if (string.IsNullOrEmpty(valeur))
        {
            return;
        }

        string requete = "INSERT INTO " + table + " VALUES (@valeur)";

        using (IDbCommand cmdDB = connexionDB.CreateCommand())
        {
            cmdDB.CommandText = requete;
            var parameters = ((SQLiteCommand)cmdDB).Parameters;

            parameters.AddWithValue("@valeur", valeur);

            cmdDB.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Sélectionne des données de la base de données en fonction de la table, de la clé et de la condition spécifiées.
    /// </summary>
    /// <param name="table">Nom de la table.</param>
    /// <param name="cle">Clé à sélectionner.</param>
    /// <param name="condition">Condition de sélection.</param>
    /// <returns>Liste des lignes résultantes sous forme de listes d'objets.</returns>
    public List<List<object>> Select(string table, string cle, string condition)
    {
        List<List<object>> resultat = new List<List<object>>();
        string requete = "SELECT " + cle + " FROM " + table + " WHERE " + condition;

        IDataReader lecteur = RequeteDeBase(requete);

        while (lecteur.Read())
        {
            List<object> row = new List<object>();
            for (int i = 0; i < lecteur.FieldCount; i++)
            {
                object value = lecteur.GetValue(i);
                if (value is byte[]) // Regarde si la donnée est de type Blob
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
    /// Supprime des données de la table en fonction de la condition spécifiée.
    /// </summary>
    /// <param name="table">Nom de la table.</param>
    /// <param name="condition">Condition de suppression.</param>
    /// <param name="valeur">Valeur de la condition.</param>
    public void Delete(string table, string condition, string valeur)
    {
        string requete = "DELETE FROM " + table + " WHERE " + condition + " = '" + valeur + "'";
        ExecuteRequestWithoutResult(requete);
    }

    /// <summary>
    /// Supprime toutes les données de la table spécifiée.
    /// </summary>
    /// <param name="table">Nom de la table.</param>
    public void DeleteEverythingFromTable(string table)
    {
        string requete = "DELETE FROM " + table;
        ExecuteRequestWithoutResult(requete);
    }

    /// <summary>
    /// Exécute une requête SQL sans retour de résultat.
    /// </summary>
    /// <param name="requete">Requête SQL à exécuter.</param>
    private void ExecuteRequestWithoutResult(string requete)
    {
        IDbCommand cmdDB = connexionDB.CreateCommand();
        cmdDB.CommandText = requete;
        cmdDB.ExecuteNonQuery();
    }

    /// <summary>
    /// Supprime la table spécifiée de la base de données.
    /// </summary>
    /// <param name="table">Nom de la table à supprimer.</param>
    public void Droptable(string table)
    {
        string requete = "DROP TABLE " + table;
        ExecuteRequestWithoutResult(requete);
    }

    /// <summary>
    /// Met à jour une tuple dans la table spécifiée.
    /// </summary>
    /// <param name="dbManager">Gestionnaire de la base de données.</param>
    /// <param name="tableName">Nom de la table.</param>
    /// <param name="columnName">Nom de la colonne à mettre à jour.</param>
    /// <param name="newValue">Nouvelle valeur à affecter.</param>
    /// <param name="conditionColumn">Colonne de condition.</param>
    /// <param name="conditionValue">Valeur de condition.</param>
    public void UpdateTuple(DBManager dbManager, string tableName, string columnName, string newValue, string conditionColumn, string conditionValue)
    {
        string updateQuery = $"UPDATE {tableName} SET \"{columnName}\" = '{newValue}' WHERE \"{conditionColumn}\" = '{conditionValue}'";
        string requete = updateQuery;

        dbManager.ExecuteRequestWithoutResult(requete);
    }
}
