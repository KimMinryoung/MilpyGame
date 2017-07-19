using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour {

	public Image background;
	public GameObject illustObject;
	public Image portrait;
	public Image nameBox;
	public Text nameText;
	public Image textBox;
	public Text textText;

	private Sprite transparentSprite;

	void Awake () {

		Dialogue.dd = this;
		
		if (!background)
			background=GameObject.Find ("Background").GetComponent<Image>();
		if (!illustObject)
			illustObject=GameObject.Find ("Illust");
		if (!portrait)
			portrait=GameObject.Find ("DialoguePortrait").GetComponent<Image>();
		if (!nameBox)
			nameBox=GameObject.Find ("nameBox").GetComponent<Image>();
		if (!textBox)
			textBox=GameObject.Find ("DialogueBox").GetComponent<Image>();

		transparentSprite = Resources.Load<Sprite> ("UIImages/transparent");

		background.sprite = transparentSprite;
		illustObject.GetComponent<Image>().sprite = transparentSprite;
		portrait.sprite = transparentSprite;
		nameBox.sprite = transparentSprite;
		textBox.sprite = transparentSprite;

		if (!nameText)
			nameText=GameObject.Find ("DialogueName").GetComponent<Text>();
		if (!textText)
			textText=GameObject.Find ("Dialogue").GetComponent<Text>();
		textText.text = null;
		nameText.text = null;

	}
}
