using System;
using SQLite4Unity3d;

public class PurchaseInfo {
	public string userId { get; set; }
	public int coins { get; set; }
	public string objects { get; set; }
	public int objectLevel { get; set; }

	public override string ToString () {
		return string.Format ("[PurchaseInfo: UserId={0}, Coins={1}, Objects={2}, ObjectLevel={3}]", userId, coins,  objects, objectLevel);
	}
}
