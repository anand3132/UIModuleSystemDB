using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	public static DataService instance = new DataService("GameDB.db");
	public static DataService getInstance() {
		return instance;
	}
	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     

	}

	public bool CreateDB() {
		// _connection.DropTable<UserData> ();
		List<SQLiteConnection.ColumnInfo> list = _connection.GetTableInfo("UserData");
		if (list.Count == 0) {
			_connection.CreateTable<UserData> ();
			_connection.CreateTable<PurchaseInfo>();
			return true;
		}
		return false;
	}

	public IEnumerable<UserData> GetUser() {
		return _connection.Table<UserData>();
	}
	
	public IEnumerable<PurchaseInfo> GetPurchaseInfo() {
		return _connection.Table<PurchaseInfo>();
	}

	public UserData GetUser(string userID) {
		IEnumerable<UserData> rows = _connection.Table<UserData>();
		foreach(UserData u in rows) {
			if (u.userId == userID) {
				return u;
			}
		}
		return null;
	}

	public int GetEarnedCoinsForUser(string userID) {
		IEnumerable<UserData> rows = _connection.Table<UserData>();
		foreach(UserData u in rows) {
			if (u.userId == userID) {
				return u.earnedCoins;
			}
		}
		return -1;
	}

	public List<PurchaseInfo> GetPurchaseInfo(string userID) {
		IEnumerable<PurchaseInfo> rows = _connection.Table<PurchaseInfo>();
		List<PurchaseInfo> list = new List<PurchaseInfo>();
		foreach(PurchaseInfo p in rows) {
			if (p.userId == userID) {
				list.Add(p);
			}
		}
		return list;
	}

	public UserData CreateUser(string userId, int earnedCoins) {
		var p = new UserData{ userId = userId, earnedCoins = earnedCoins };
		_connection.Insert (p);
		return p;
	}

	public void UpdatePurchace(PurchaseInfo p) {
		_connection.Update(p);
	}

	public PurchaseInfo CreatePurchase(string userId, int coins, string objects, int objectLevel) {
		UserData user = GetUser(userId);
		if (user != null) {
			var p = new PurchaseInfo{ userId = userId, coins = coins, objects = objects, objectLevel = objectLevel };
			_connection.Insert (p);
			user.earnedCoins-=coins;
			_connection.Update(user);
			return p;
		}

		return null;
	}
}
