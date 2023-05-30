using Mono.Data.Sqlite;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


static class Database
{
    private const string fileName = "Shotgun_ride.bytes";
    private static string DBPath;
    private static SqliteConnection connection;
    private static SqliteCommand command;

    static Database()
    {
        DBPath = GetDatabasePath();
    }

    /// <summary> ���������� ���� � ��. ���� � ��� � ������ ����� �� ��������, �� �������� � � ��������� apk �����. </summary>
    private static string GetDatabasePath()
    {
        return Path.Combine(Application.streamingAssetsPath, fileName);
    }
/// <summary> ������������� ���� ������ � ��������� ����. </summary>
/// <param name="toPath"> ���� � ������� ����� ����������� ���� ������. </param>
    private static void UnpackDatabase()
    {
        string fromPath = Path.Combine(Application.streamingAssetsPath, fileName);

        WWW reader = new WWW(fromPath);
        while (!reader.isDone) { }

        File.WriteAllBytes(Application.dataPath + "/StreamingAssets/Shotgun_ride.bytes", reader.bytes);
    }

    /// <summary> ���� ����� ��������� ����������� � ��. </summary>
    private static void OpenConnection()
    {
        connection = new SqliteConnection("Data Source=" + DBPath);
        command = new SqliteCommand(connection);
        connection.Open();
    }

    /// <summary> ���� ����� ��������� ����������� � ��. </summary>
    public static void CloseConnection()
    {
        connection.Close();
        command.Dispose();
    }

    /// <summary> ���� ����� ��������� ������ query. </summary>
    /// <param name="query"> ���������� ������. </param>
    public static void ExecuteQueryWithoutAnswer(string query)
    {
        OpenConnection();
        command.CommandText = query;
        command.ExecuteNonQuery();
        CloseConnection();
    }
    /// <summary> ���� ����� ��������� ������ query � ���������� ����� �������. </summary>
    /// <param name="query"> ���������� ������. </param>
    /// <returns> ���������� �������� 1 ������ 1 �������, ���� ��� �������. </returns>
    public static string ExecuteQueryWithAnswer(string query)
    {
        OpenConnection();
        command.CommandText = query;
        var answer = command.ExecuteScalar();
        CloseConnection();

        if (answer != null) return answer.ToString();
        else return null;
    }

    /// <summary> ���� ����� ���������� �������, ������� �������� ����������� ������� ������� query. </summary>
    /// <param name="query"> ���������� ������. </param>
    public static DataTable GetTable(string query)
    {
        OpenConnection();

        SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);

        DataSet DS = new DataSet();
        adapter.Fill(DS);
        adapter.Dispose();

        CloseConnection();

        return DS.Tables[0];
    }
}