﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {

	public static System.Random rnd=new System.Random(DateTime.Now.Millisecond);

	public static string PPIGA(string noun){
		int last = noun [noun.Length - 1];
		if (last >= '가' && last <= '힣') {
			if (last % 28 == '가' % 28)
				return noun + "가";
			else
				return noun + "이";
		}
		else {
			//에이비시디이에프쥐에이치아이제이케이 엘엠엔 오피큐 알 에스티유브이더블유엑스와이지
			if (last == 'L' || last=='M' || last == 'N' || last == 'R')
				return noun + "이";
			else
				return noun + "가";
		}
	}

	public static double Max(double a,double b){
		return System.Math.Max (a, b);
	}
	public static double Min(double a,double b){
		return System.Math.Min (a, b);
	}

	public static bool Compare(int target, int reference, string compareSymbol){
		if (compareSymbol == "==")
			return target == reference;
		else if (compareSymbol == "!=")
			return target != reference;
		else if (compareSymbol == ">")
			return target > reference;
		else if (compareSymbol == "<")
			return target < reference;
		else if (compareSymbol == ">=")
			return target >= reference;
		else if (compareSymbol == "<=")
			return target <= reference;
		else {
			Debug.Log ("이상한 compare symbol 입니다");
			return true;
		}
	}
}