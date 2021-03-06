﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

	public Vector2 dragOffset = new Vector2(0f,0f);
	public Vector2 startPosition = new Vector2(0f,0f);
	public Deck _Deck;
	public bool isDraggable = true;
	public Slot_Watcher __Slot_Watcher;
	public Debbug_Logger_Script Deb;

	//Lists of same cards
	public List<string> c2s =  new List<string>();
	public List<string> c3s =  new List<string>();
	public List<string> c4s =  new List<string>();
	public List<string> c5s =  new List<string>();
	public List<string> c6s =  new List<string>();
	public List<string> c7s =  new List<string>();
	public List<string> c8s =  new List<string>();
	public List<string> c9s =  new List<string>();
	public List<string> c10s = new List<string>();
	public List<string> c11s = new List<string>();
	public List<string> c12s = new List<string>();
	public List<string> c13s = new List<string>();
	public List<string> c14s = new List<string>();

	void Start (){
			c2s. Add("c (2)");  c2s.Add( "c (15)"); c2s.Add("c (28)");  c2s.Add("c (41)");
			c3s .Add("c (3)");  c3s.Add( "c (16)"); c3s.Add("c (29)");  c3s.Add("c (42)");
			c4s .Add("c (4)");  c4s.Add( "c (17)"); c4s.Add("c (30)");  c4s.Add("c (43)");
			c5s .Add("c (5)");  c5s.Add( "c (18)"); c5s.Add("c (31)");  c5s.Add("c (44)");
			c6s .Add("c (6)");  c6s.Add( "c (19)"); c6s.Add("c (32)");  c6s.Add("c (45)");
			c7s .Add("c (7)");  c7s.Add( "c (20)"); c7s.Add("c (33)");  c7s.Add("c (46)");
			c8s .Add("c (8)");  c8s.Add( "c (21)"); c8s.Add("c (34)");  c8s.Add("c (47)");
			c9s .Add("c (9)");  c9s.Add( "c (22)"); c9s.Add("c (35)");  c9s.Add("c (48)");
			c10s.Add("c (10)"); c10s.Add("c (23)"); c10s.Add("c (36)"); c10s.Add("c (49)");
			c11s.Add("c (11)"); c11s.Add("c (24)"); c11s.Add("c (37)"); c11s.Add("c (50)");
			c12s.Add("c (12)"); c12s.Add("c (25)"); c12s.Add("c (38)"); c12s.Add("c (51)");
			c13s.Add("c (13)"); c13s.Add("c (26)"); c13s.Add("c (39)"); c13s.Add("c (52)");
			c14s.Add("c (14)"); c14s.Add("c (27)"); c14s.Add("c (40)"); c14s.Add("c (53)");
	}

	public void OnBeginDrag (PointerEventData eventData){
			if (isDraggable==true) {
				moveToTop();
				startPosition = this.transform.position;
				this.GetComponent<UnityEngine.UI.Image>().color= new Color (255,255,0);
				dragOffset = eventData.position - (Vector2)this.transform.position;
			} else {
				Deb.Debug_Logger("Card cannot be moved after drop.");
			}
	}

	public void OnDrag (PointerEventData eventData){
		if (isDraggable==true) {
			this.transform.position = eventData.position - dragOffset;
		} else {
			Deb.Debug_Logger("Card cannot be moved after drop.");
		}
	}

	public void OnEndDrag (PointerEventData eventData) {
		if (isDraggable==true) {
		this.GetComponent<UnityEngine.UI.Image>().color= new Color (1,1,1,1);
			if(this.transform.position.y < 110){
					this.transform.position = startPosition;
			} else if (this.transform.position.x < Screen.width / 2){
				//Card is dropped on LEFT STACK
				if (_Deck.stosLewa.Count==0) {
					addCardToThe("LeftCard");
					this.tag = "DELETE_ME";
				} else {
					if(checkIfDropAllowed("left")){
						addCardToThe("LeftCard");
						//change the name for deletion afterwards
						this.tag = "DELETE_ME";
						//ARE WE THERE ALREADY?
						if (checkIfThisMoveWins()) {
							Deb.Debug_Logger("Player won.");
						}
					} else {
						Deb.Debug_Logger("Wrong sign or value of the card. Try a different one.");
						this.transform.position = startPosition;
					}
				}
			} else if (this.transform.position.x > Screen.width / 2){
				//Card is dropped on RIGHT STACK
				if (_Deck.stosPrawa.Count==0) {
					addCardToThe("RightCard");
					this.tag = "DELETE_ME";
				} else {
					if(checkIfDropAllowed("right")){
						addCardToThe("RightCard");
						//change the name for deletion afterwards
						this.tag = "DELETE_ME";
						//ARE WE THERE ALREADY?
						if (checkIfThisMoveWins()) {
							Deb.Debug_Logger("Player won.");
						}
					} else {
						Deb.Debug_Logger("Wrong sign or value of the card. Try a different one.");
						this.transform.position = startPosition;
					}
				}
			} else {
				this.transform.position = startPosition;
			}
		} else {
			Deb.Debug_Logger("Card cannot be moved after drop.");
		}
	}

	public void moveToTop(string outsider = "not asigned"){
		if (outsider=="not asigned"){
			this.GetComponent<RectTransform>().transform.SetParent(GameObject.Find("Canvas").transform,true);
			this.GetComponent<RectTransform>().SetAsLastSibling();
		} else {
			GameObject tttemp = GameObject.Find(outsider);
			tttemp.GetComponent<RectTransform>().transform.SetParent(GameObject.Find("Canvas").transform,true);
			tttemp.GetComponent<RectTransform>().SetAsLastSibling();
		}
	}

	public void addCardToThe(string whichDeck, string outsider = "not asigned"){
		if (outsider=="not asigned") {
			this.transform.position =  Vector2.Lerp (
			this.transform.position,
			(Vector2)GameObject.Find(whichDeck).transform.position + new Vector2(Random.Range(0,3),Random.Range(0,3)), 1);
		}
		if (outsider=="not asigned") {
			if (whichDeck=="LeftCard") {
				_Deck.stosLewa.Add(this.GetComponent<UnityEngine.UI.Image>().sprite);
			} else if (whichDeck=="RightCard") {
				_Deck.stosPrawa.Add(this.GetComponent<UnityEngine.UI.Image>().sprite); }
		} else {
			if 				(whichDeck=="LeftCard" ) {
				_Deck.stosLewa.Add(GameObject.Find(outsider).GetComponent<UnityEngine.UI.Image>().sprite);
			} else if (whichDeck=="RightCard") {
				_Deck.stosPrawa.Add(GameObject.Find(outsider).GetComponent<UnityEngine.UI.Image>().sprite);
			}
		}
		if (outsider=="not asigned") { isDraggable = false; }
		//EMPTY slot for other cards
		if (outsider=="not asigned") { checkNamesForSlot(); } else { checkNamesForSlot(GameObject.Find(outsider).name); }
		Deb.Debug_Logger("The card has been dropped.");
	}

	public void checkNamesForSlot(string outsider = "not asigned"){
		if (this.name=="slot_1_real" || outsider=="slot_1_real"){
			__Slot_Watcher.slot1 = false;
		} else if (this.name=="slot_2_real" || outsider=="slot_2_real"){
			__Slot_Watcher.slot2 = false;
		} else if (this.name=="slot_3_real" || outsider=="slot_3_real"){
			__Slot_Watcher.slot3 = false;
		} else if (this.name=="slot_4_real" || outsider=="slot_4_real"){
			__Slot_Watcher.slot4 = false;
		} else if (this.name=="slot_5_real" || outsider=="slot_5_real"){
			__Slot_Watcher.slot5 = false;
		} else if (this.name=="slot_6_real" || outsider=="slot_6_real"){
			__Slot_Watcher.slot6 = false;
		} else if (this.name=="slot_7_real" || outsider=="slot_7_real"){
			__Slot_Watcher.slot7 = false;
		} else if (this.name=="slot_8_real" || outsider=="slot_8_real"){
			__Slot_Watcher.slot8 = false;
		}
		Deb.Debug_Logger(this.name + "is now empty.");
	}

	public bool checkIfDropAllowed (string side){
		if (side=="left") {
			// Deb.Debug_Logger(_Deck.stosLewa[_Deck.stosLewa.Count-1].name);
			if (_Deck.stosLewa[_Deck.stosLewa.Count-1].name=="j1"
			||  _Deck.stosLewa[_Deck.stosLewa.Count-1].name=="j2"
			||  _Deck.stosLewa[_Deck.stosLewa.Count-1].name=="j3"
			||  this.GetComponent<UnityEngine.UI.Image>().sprite.name=="j1"
			||  this.GetComponent<UnityEngine.UI.Image>().sprite.name=="j2"
			||  this.GetComponent<UnityEngine.UI.Image>().sprite.name=="j3"  ) {
				return true;
			} else if (checkWhatCardItIs(_Deck.stosLewa[_Deck.stosLewa.Count-1].name)==1
			&&  checkWhatCardItIs(this.GetComponent<UnityEngine.UI.Image>().sprite.name)==1 ) {
				return true;
			} else if (checkWhatCardItIs(_Deck.stosLewa[_Deck.stosLewa.Count-1].name)==2
			&&  			 checkWhatCardItIs(this.GetComponent<UnityEngine.UI.Image>().sprite.name)==2 ) {
				return true;
			} else if (checkWhatCardItIs(_Deck.stosLewa[_Deck.stosLewa.Count-1].name)==3
			&&  			 checkWhatCardItIs(this.GetComponent<UnityEngine.UI.Image>().sprite.name)==3 ) {
				return true;
			} else if (checkWhatCardItIs(_Deck.stosLewa[_Deck.stosLewa.Count-1].name)==4
			&&  			 checkWhatCardItIs(this.GetComponent<UnityEngine.UI.Image>().sprite.name)==4 ) {
				return true;
			} else if (
			checkIfTheCardValueIsTheSame(_Deck.stosLewa[_Deck.stosLewa.Count-1].name,
			this.GetComponent<UnityEngine.UI.Image>().sprite.name)
			){
				return true;
			} else {
				return false;
			}
		} else if (side=="right"){
			// Deb.Debug_Logger(_Deck.stosPrawa[_Deck.stosPrawa.Count-1].name);
			if (_Deck.stosPrawa[_Deck.stosPrawa.Count-1].name=="j1"
			||  _Deck.stosPrawa[_Deck.stosPrawa.Count-1].name=="j2"
			||  _Deck.stosPrawa[_Deck.stosPrawa.Count-1].name=="j3"
			||  this.GetComponent<UnityEngine.UI.Image>().sprite.name=="j1"
			||  this.GetComponent<UnityEngine.UI.Image>().sprite.name=="j2"
			||  this.GetComponent<UnityEngine.UI.Image>().sprite.name=="j3"  ) {
				return true;
			} else if (checkWhatCardItIs(_Deck.stosPrawa[_Deck.stosPrawa.Count-1].name)==1
			&&  checkWhatCardItIs(this.GetComponent<UnityEngine.UI.Image>().sprite.name)==1 ) {
				return true;
			} else if (checkWhatCardItIs(_Deck.stosPrawa[_Deck.stosPrawa.Count-1].name)==2
			&&  			 checkWhatCardItIs(this.GetComponent<UnityEngine.UI.Image>().sprite.name)==2 ) {
				return true;
			} else if (checkWhatCardItIs(_Deck.stosPrawa[_Deck.stosPrawa.Count-1].name)==3
			&&  			 checkWhatCardItIs(this.GetComponent<UnityEngine.UI.Image>().sprite.name)==3 ) {
				return true;
			} else if (checkWhatCardItIs(_Deck.stosPrawa[_Deck.stosPrawa.Count-1].name)==4
			&&  			 checkWhatCardItIs(this.GetComponent<UnityEngine.UI.Image>().sprite.name)==4 ) {
				return true;
			} else if (
			checkIfTheCardValueIsTheSame(_Deck.stosPrawa[_Deck.stosPrawa.Count-1].name,
			this.GetComponent<UnityEngine.UI.Image>().sprite.name)
			){
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
		// Deb.Debug_Logger(this.GetComponent<UnityEngine.UI.Image>().sprite.name);
	}

	public int checkWhatCardItIs(string shortName){
		int i = 0; // 0==undefined
		if (shortName=="c (2)"
		||  shortName=="c (3)"
		||  shortName=="c (4)"
		||  shortName=="c (5)"
		||  shortName=="c (6)"
		||  shortName=="c (7)"
		||  shortName=="c (8)"
		||  shortName=="c (9)"
		||  shortName=="c (10)"
		||  shortName=="c (11)"
		||  shortName=="c (12)"
		||  shortName=="c (13)"
		||  shortName=="c (14)") {
			i = 1;
		} else if (shortName=="c (15)"
		||  shortName=="c (16)"
		||  shortName=="c (17)"
		||  shortName=="c (18)"
		||  shortName=="c (19)"
		||  shortName=="c (20)"
		||  shortName=="c (21)"
		||  shortName=="c (22)"
		||  shortName=="c (23)"
		||  shortName=="c (24)"
		||  shortName=="c (25)"
		||  shortName=="c (26)"
		||  shortName=="c (27)"){
			i = 2;
		} else if (shortName=="c (28)"
		||  shortName=="c (29)"
		||  shortName=="c (30)"
		||  shortName=="c (31)"
		||  shortName=="c (32)"
		||  shortName=="c (33)"
		||  shortName=="c (34)"
		||  shortName=="c (35)"
		||  shortName=="c (36)"
		||  shortName=="c (37)"
		||  shortName=="c (38)"
		||  shortName=="c (39)"
		||  shortName=="c (40)"){
				i = 3;
		} else {
				i = 4;
		}
		return i;
	}

	public bool checkIfTheCardValueIsTheSame(string firstCard, string secondCard){
		if (c2s.Contains(firstCard) && c2s.Contains(secondCard)){
			return true;
		} else if (c2s.Contains(firstCard) && c2s.Contains(secondCard)){
			return true;
		} else if (c3s.Contains(firstCard) && c3s.Contains(secondCard)){
			return true;
		} else if (c4s.Contains(firstCard) && c4s.Contains(secondCard)){
			return true;
		} else if (c5s.Contains(firstCard) && c5s.Contains(secondCard)){
			return true;
		} else if (c6s.Contains(firstCard) && c6s.Contains(secondCard)){
			return true;
		} else if (c7s.Contains(firstCard) && c7s.Contains(secondCard)){
			return true;
		} else if (c8s.Contains(firstCard) && c8s.Contains(secondCard)){
			return true;
		} else if (c9s.Contains(firstCard) && c9s.Contains(secondCard)){
			return true;
		} else if (c10s.Contains(firstCard) && c10s.Contains(secondCard)){
			return true;
		} else if (c11s.Contains(firstCard) && c11s.Contains(secondCard)){
			return true;
		} else if (c12s.Contains(firstCard) && c12s.Contains(secondCard)){
			return true;
		} else if (c13s.Contains(firstCard) && c13s.Contains(secondCard)){
			return true;
		} else if (c14s.Contains(firstCard) && c14s.Contains(secondCard)){
			return true;
		} else {
			return false;
		}
	}

		public bool checkIfThisMoveWins(){
			if (_Deck.player1_cards.Count==0 &&
			__Slot_Watcher.slot1 == false &&
			__Slot_Watcher.slot2 == false &&
			__Slot_Watcher.slot3 == false &&
			__Slot_Watcher.slot4 == false) {
				return true;

			} else if (_Deck.player2_cards.Count==0 &&
			__Slot_Watcher.slot5 == false &&
			__Slot_Watcher.slot6 == false &&
			__Slot_Watcher.slot7 == false &&
			__Slot_Watcher.slot8 == false) {
				return true;

			} else {
				return false;
			}
		}

}
