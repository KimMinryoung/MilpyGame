using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Person{
	Person person;
	public Unit(Person person){
		this.person = person;
		AddStat ("HP", 10 * person.GetStat ("체력"), 0, 10 * person.GetStat ("체력"));
		AddStat ("MP", 10 * person.GetStat ("마력"), 0, 10 * person.GetStat ("마력"));
		AddStat ("마법력", 1000, 1, person.GetStat ("마력"));
		AddStat ("마법기술", 1000, 1, person.GetStat ("지능"));
		AddStat ("저항력", 1000, 1, person.GetStat ("정신"));
		AddStat ("순발력", 1000, 1, person.GetStat ("민첩"));
		AddStat ("운", 100, 1, person.GetStat ("행운"));
	}
}
