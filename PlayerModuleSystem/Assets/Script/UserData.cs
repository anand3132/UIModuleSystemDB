using System;
using SQLite4Unity3d;

public class UserData {
	[PrimaryKey]
	public string userId { get; set; }
	public int earnedCoins {get; set; }

	public override string ToString () {
		return string.Format ("[UserData: UserId={0}, EarnedCoins={1}]", userId, earnedCoins);
	}
}

