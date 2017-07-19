using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour {

	public Transform manager;

	public Image background;
	public Transform illustObject;
	public Image portrait;
	public Image nameBox;
	public Text nameText;
	public Image textBox;
	public Text textText;

	public Sprite transparentSprite;

	void Awake () {

		Dialogue.dd = this;

		if (!manager)
			manager=GameObject.Find ("DialogueManager").GetComponent<Transform>();
		
		if (!background)
			background=manager.Find ("Background").GetComponent<Image>();
		if (!illustObject)
			illustObject=manager.Find ("Illust");
		if (!portrait)
			portrait=manager.Find ("Portrait").GetComponent<Image>();
		if (!nameBox)
			nameBox=manager.Find ("NameBox").GetComponent<Image>();
		if (!textBox)
			textBox=manager.Find ("TextBox").GetComponent<Image>();

		transparentSprite = Resources.Load<Sprite> ("UIImages/transparent");

		background.sprite = transparentSprite;
		illustObject.GetComponent<Image>().sprite = transparentSprite;
		portrait.sprite = transparentSprite;

		if (!nameText)
			nameText=manager.Find ("NameText").GetComponent<Text>();
		if (!textText)
			textText=manager.Find ("TextText").GetComponent<Text>();
		textText.text = null;
		nameText.text = null;
	}
	void Start(){

		Dialogue test = new Dialogue ();
		test.LoadDialogueLine("밀피\\\\내 이름은 밀피.|장점이라곤 운이 좋다는 것뿐인 인간이다.");
		test.ExecuteDialogue ();

	}
}
