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

    public void FermerConnexion()
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

    public void CreerTable(string nom, string[] colonnes, string[] types)
    {
        string requete = "CREATE TABLE IF NOT EXISTS " + nom + "(" + colonnes[0] + " " + types[0];
        for (int i = 1; i < colonnes.Length; i++)
        {
            requete += ", " + colonnes[i] + " " + types[i];
        }
        requete += ")";
        ExecuterRequeteSansResultat(requete);
    }

    /*
    public void InsererUnique(string table, string cle, string valeur)
    {
        string requete = "INSERT INTO " + table + "(" + cle + ") VALUES (" + valeur + ")";
        ExecuterRequeteSansResultat(requete);
    }

    public void InsererSpecifique(string table, string[] cle, string[] valeurs)
    {
        string requete = "INSERT INTO " + table + "(";
        for (int i = 0; i < cle.Length; i++)
        {
            requete += cle[i];
            if (i < cle.Length - 1)
                requete += ", ";
        }
        requete += ") VALUES (";
        for (int i = 0; i < valeurs.Length; i++)
        {
            requete += "'" + valeurs[i] + "'";
            if (i < valeurs.Length - 1)
                requete += ", ";
        }
        requete += ")";
        ExecuterRequeteSansResultat(requete);
    }
    */

    public void Insertion(string table, string[] valeurs)
    {
        if (valeurs == null || valeurs.Length == 0)
        {
            Debug.LogError("Aucune valeur à insérer.");
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



    public List<List<object>> Selection(string table, string cle, string condition)
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
                row.Add(value);
            }
            resultat.Add(row);
        }
        lecteur.Close();
        return resultat;
    }


    public void Supprimer(string table, string condition, string valeur)
    {
        string requete = "DELETE FROM " + table + " WHERE " + condition + " = '" + valeur + "'";
        ExecuterRequeteSansResultat(requete);
    }

    private void ExecuterRequeteSansResultat(string requete)
    {
        IDbCommand cmdDB = connexionDB.CreateCommand();
        cmdDB.CommandText = requete;
        cmdDB.ExecuteNonQuery();
    }
}
