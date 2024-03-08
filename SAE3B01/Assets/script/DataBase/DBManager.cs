using UnityEngine;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;

public class DBManager
{
    private string cheminConnection;
    private SQLiteConnection connexionDB;

    public DBManager()
    {
        cheminConnection = "URI=file:dataBase.db";
        connexionDB = new SQLiteConnection(cheminConnection);
        connexionDB.Open();
    }

    public void CloseConnexion()
    {
        if (connexionDB != null && connexionDB.State != ConnectionState.Closed)
        {
            connexionDB.Close();
        }
    }

    public IDataReader RequeteDeBase(string requete)
    {
        IDbCommand cmdDB = connexionDB.CreateCommand();
        cmdDB.CommandText = requete;
        return cmdDB.ExecuteReader();
    }

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
                if (value is byte[]) //regarde si la donnée est de type Blob
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


    public void Delete(string table, string condition, string valeur)
    {
        string requete = "DELETE FROM " + table + " WHERE " + condition + " = '" + valeur + "'";
        ExecuteRequestWithoutResult(requete);
    }

    public void DeleteEverythingFromTable(string table)
    {
        string requete = "DELETE FROM " + table;
        ExecuteRequestWithoutResult(requete);
    }

    private void ExecuteRequestWithoutResult(string requete)
    {
        IDbCommand cmdDB = connexionDB.CreateCommand();
        cmdDB.CommandText = requete;
        cmdDB.ExecuteNonQuery();
    }

    public void Droptable(string table)
    {
        string requete = "DROP TABLE " + table;
        ExecuteRequestWithoutResult(requete);
    }
    public void UpdateTuple(DBManager dbManager, string tableName, string columnName, string newValue, string conditionColumn, string conditionValue)
    {
        string updateQuery = $"UPDATE {tableName} SET \"{columnName}\" = '{newValue}' WHERE \"{conditionColumn}\" = '{conditionValue}'";
        string requete = updateQuery;

        dbManager.ExecuteRequestWithoutResult(requete);  
    }
}