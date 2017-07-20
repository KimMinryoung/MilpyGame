using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person {
	protected string name;
	protected Dictionary<string,int> stats;
	protected Dictionary<string,int> statMaxLimits;
	protected Dictionary<string,int> statMinLimits;

	public static DialogueManager dm;

	public Person () {
		stats=new Dictionary<string,int>();
		statMaxLimits = new Dictionary<string, int> ();
		statMinLimits = new Dictionary<string, int> ();
	}
	public Person(string name) : this(){
		this.name = "멜";
		AddStat ("체력", 100, 1, 30);
		AddStat ("정신", 100, 1, 35);
		AddStat ("민첩", 100, 1, 12);
		AddStat ("마력", 100, 1, 50);
		AddStat ("지능", 100, 1, 65);
		AddStat ("행운", 100, 1, 80);
		AddStat ("혐오", 100, 0, 0);
		AddStat ("복종", 100, 0, 0);
		AddStat ("공포", 100, 0, 0);
		AddStat ("광기", 100, 0, 0);
		AddStat ("애정", 100, 0, 0);
	}

	protected void AddStat(string statName, int max, int min){
		stats [statName] = min;
		statMaxLimits [statName] = max;
		statMinLimits [statName] = min;
	}
	protected void AddStat(string statName, int max, int min, int value){
		stats [statName] = value;
		statMaxLimits [statName] = max;
		statMinLimits [statName] = min;
	}

	public string GetName(){
		return name;
	}
	public int GetStat (string statName){
		return stats [statName];
	}
	public Dictionary<string, int> GetStats(){
		return stats;
	}

	protected string ChangeStat(string targetStat, int change){
		int prevStat = stats [targetStat];
		stats [targetStat] += change;
		MakeStatInLimit (targetStat);
		int realChange = stats [targetStat] - prevStat;

		string message = null;
		if (realChange > 0) {
			message = (name + "의 " + Util.PPIGA (targetStat) + " " + realChange + " 올랐다.");
		} else if (realChange < 0) {
			message = (name + "의 " + Util.PPIGA (targetStat) + " " + -realChange + " 떨어졌다.");
		}
		return message;
	}

	public void ChangeStats(Dictionary<string, int> statChangesList){
		List<string> messages=new List<string>();
		string message;
		foreach(var statChange in statChangesList){
			message = ChangeStat (statChange.Key, statChange.Value);
			if (message != null)
				messages.Add (message);
		}

		string doubleMessage;
		int i;
		for(i = 0 ; i + 1 < messages.Count ; i += 2){
			doubleMessage = messages [i] + "\n" + messages [i + 1];
			dm.LoadMessageLine (doubleMessage);
		}
		if (i == messages.Count - 1) {
			dm.LoadMessageLine (messages [i]);
		}
	}

	private int MakeStatInLimit(string statName){
		int prevStatValue = stats [statName];
		stats [statName] = (int)Util.Max (stats [statName], statMinLimits [statName]);
		stats [statName] = (int)Util.Min (stats [statName], statMaxLimits [statName]);
		return stats [statName] - prevStatValue;
	}
}
