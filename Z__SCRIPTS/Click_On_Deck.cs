using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Click_On_Deck : MonoBehaviour, IPointerDownHandler   {

	public Slot_Watcher _Slot_Watcher;
	public Deck __Deck_1_;
	public GameObject NewCards;
	public Deck_Behaviour _Deck_Behaviour;
	public Debbug_Logger_Script Deb;

	// Use this for initialization
	void Start () {

	}

	public void OnPointerDown (PointerEventData eventData){

					if (this.transform.position.y<Screen.height/2) {

						if (_Slot_Watcher.slot1==false) {
							_Slot_Watcher.slot1=true; addCardCheck("slot_1");

						} else if (_Slot_Watcher.slot2==false){
							_Slot_Watcher.slot2=true; addCardCheck("slot_2");

						} else if (_Slot_Watcher.slot3==false){
							_Slot_Watcher.slot3=true; addCardCheck("slot_3");

						} else if (_Slot_Watcher.slot4==false){
							_Slot_Watcher.slot4=true;	addCardCheck("slot_4");

						} else {				Deb.Debug_Logger("You cannot withdraw more cards.");			}

				} else if (this.transform.position.y>Screen.height/2) {

						if (_Slot_Watcher.slot5==false) {
							_Slot_Watcher.slot5=true; addCardCheck("slot_5");

						} else if (_Slot_Watcher.slot6==false){
							_Slot_Watcher.slot6=true; addCardCheck("slot_6");

						} else if (_Slot_Watcher.slot7==false){
							_Slot_Watcher.slot7=true; addCardCheck("slot_7");

						} else if (_Slot_Watcher.slot8==false){
							_Slot_Watcher.slot8=true;	addCardCheck("slot_8");

						} else {				Deb.Debug_Logger("You cannot withdraw more cards.");			}

				}

		}

	public void addCardCheck(string slot){

			NewCards = Instantiate(GameObject.Find("Card_Prefab"), Vector2.zero, Quaternion.identity);
			NewCards.AddComponent<UnityEngine.UI.Image>();
			NewCards.GetComponent<UnityEngine.UI.Image>().color = new Color (1,1,1,1);

			if (this.transform.position.y<Screen.height/2) {
				NewCards.GetComponent<UnityEngine.UI.Image>().sprite = __Deck_1_.player1_cards[0]; //Set the Sprite
				__Deck_1_.player1_cards.Remove(__Deck_1_.player1_cards[0]);
				NewCards.transform.position = GameObject.Find(slot).transform.position;
				NewCards.GetComponent<RectTransform>().transform.SetParent(GameObject.Find("FP_Cards_On_Table").transform,true); //make a child of
			} else {
				NewCards.GetComponent<UnityEngine.UI.Image>().sprite = __Deck_1_.player2_cards[0]; //Set the Sprite
				__Deck_1_.player2_cards.Remove(__Deck_1_.player2_cards[0]);
				NewCards.transform.position = GameObject.Find(slot).transform.position;
				NewCards.GetComponent<RectTransform>().transform.SetParent(GameObject.Find("SP_Cards_On_Table").transform,true); //make a child of
			}

			Deb.Debug_Logger("Card has been added to " + slot);
			NewCards.name = slot + "_real";
			this.transform.position = (Vector2)this.transform.position - new Vector2(1000f,1000f);
			this.GetComponent<UnityEngine.UI.Image>().color = new Color (0,0,0,0);
			Deb.Debug_Logger("The card has been placed on the table.");
	}


}
