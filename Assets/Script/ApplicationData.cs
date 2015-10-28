using UnityEngine;
using System.Collections;

/// <summary>
/// Application data.
/// 
/// Contains all the game information about the state of the player,
/// like score, money, last level played, etc.
/// </summary>
public class ApplicationData
{

	private static readonly string LAST_LEVEL_KEY = "0";
	private static readonly string MONEY_KEY = "1";
	private static readonly string SCORE_KEY = "2";

	public static void setLastLevel(int level){
		PlayerPrefs.SetInt (LAST_LEVEL_KEY, level);
	}

	public static void setMoney(int money){
		PlayerPrefs.SetInt (MONEY_KEY, money);
	}

	public static void setScore(int score){
		PlayerPrefs.SetInt (SCORE_KEY, score);
	}

	public static void addMoney(int money){
		PlayerPrefs.SetInt (MONEY_KEY, getMoney() + money);
	}

	public static void addScore(int score){
		PlayerPrefs.SetInt (SCORE_KEY, getScore() + score);
	}

	public static int getLastLevel(){
		if (!PlayerPrefs.HasKey (LAST_LEVEL_KEY)) {
			PlayerPrefs.SetInt (LAST_LEVEL_KEY, 1);
			PlayerPrefs.Save();
		}
		return PlayerPrefs.GetInt (LAST_LEVEL_KEY);

	}

	public static int getMoney(){
		if (!PlayerPrefs.HasKey (MONEY_KEY)) {
			PlayerPrefs.SetInt (MONEY_KEY, 0);
			PlayerPrefs.Save();
		}
		return PlayerPrefs.GetInt (MONEY_KEY);
	}

	public static int getScore(){
		if (!PlayerPrefs.HasKey (SCORE_KEY)) {
			PlayerPrefs.SetInt (SCORE_KEY, 0);
			PlayerPrefs.Save();
		}
		return PlayerPrefs.GetInt (SCORE_KEY);
	}

	public static void spendMoney(int money){
		PlayerPrefs.SetInt (MONEY_KEY, getMoney() - money);
	}

	public static void save(){
		PlayerPrefs.Save ();
	}

	public static bool canSpendMoney(int money){
		return getMoney() >= money;
	}

	public static void reset(){
		setLastLevel(1);
		setMoney(0);
		setScore(0);
	}

}

