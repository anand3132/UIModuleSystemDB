using System;
using SQLite4Unity3d;

public class PurchaseInfo {
	[PrimaryKey, AutoIncrement]
	public int id {get; set;}
	public string userId { get; set; }
	public int coins { get; set; }
	public string objects { get; set; }
	public int objectLevel { get; set; }

	public override string ToString () {
		return string.Format ("[PurchaseInfo: id={0}, UserId={1}, Coins={2}, Objects={3}, ObjectLevel={4}]", id, userId, coins,  objects, objectLevel);
	}
}
