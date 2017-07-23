using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic{
	public string name;
	private static Action<Unit> NullConsumeMP = (unit) => {};
	Action<Unit> ConsumeMP = NullConsumeMP;
	public Magic(string name, int MPconsumption){
		this.name = name;
		ConsumeMP = (Unit caster) => {
			Dictionary<string, int> MPChange=new Dictionary<string, int>();
			MPChange["MP"]=-MPconsumption;
			caster.ChangeStatsAndAddMessages(MPChange);
		};
	}
	public void Cast(Unit caster, Unit target){
		ConsumeMP (caster);
		int damage = GetDamage ();
	}
	public int GetDamage(){
		return 0;
	}
}