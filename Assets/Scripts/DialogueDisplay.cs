using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour {

	public Transform manager;

	private Image background;
	private Transform illustObject;
	private Image portrait;
	private Image nameBox;
	private Text nameText;
	private Image textBox;
	private Text textText;

	private Sprite transparentSprite;

	void Awake () {

		Dialogue.dd = this;
		DialogueManager.dd = this;

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

		if (!nameText)
			nameText=manager.Find ("NameText").GetComponent<Text>();
		if (!textText)
			textText=manager.Find ("TextText").GetComponent<Text>();
		
	}
	void Start(){
		DialogueDisplayClear ();
	}
	public void DialogueDisplayClear(){
		RemoveBackgroundSprite ();
		RemovePortraitSprite ();
		RemoveIllustSprite ();
		DisableNameBox ();
		DisableTextBox ();
		PutNameText (null);
		PutTextText (null);
	}
	public void DisableNameBox(){
		nameBox.enabled = false;
	}
	public void EnableNameBox(){
		nameBox.enabled = true;
	}
	public void DisableTextBox(){
		textBox.enabled = false;
	}
	public void EnableTextBox(){
		textBox.enabled = true;
	}
	public void PutNameText(string text){
		nameText.text = text;
	}
	public void PutTextText(string text){
		textText.text = text;
	}
	public void RemoveBackgroundSprite(){
		PutBackgroundSprite (transparentSprite);
	}
	public void PutBackgroundSprite(Sprite sprite){
		background.sprite = sprite;
	}
	public void RemovePortraitSprite(){
		PutPortraitSprite (transparentSprite);
	}
	public void PutPortraitSprite(Sprite sprite){
		portrait.sprite = sprite;
	}
	public void RemoveIllustSprite(){
		PutIllustSprite (transparentSprite);
	}
	public void PutIllustSprite(Sprite sprite){
		illustObject.GetComponent<Image>().sprite = sprite;
		illustObject.GetComponent<RectTransform> ().sizeDelta = sprite.rect.size;
	}
}
