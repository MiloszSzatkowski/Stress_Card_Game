using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_On_Stress : MonoBehaviour {

	public Deck _Main_Deck;
	public Draggable _Draggable;
	public Debbug_Logger_Script Deb;
	public List<Sprite> temp = new List<Sprite>();

	//temporary integer
public int tempCount;

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().onClick.AddListener(triggerStressEvent);
	}

	public void triggerStressEvent (){
		Deb.Debug_Logger("You clicked 'Stress!' ");
		if (checkIfTheCardsHaveTheSameValue() || _Main_Deck.blocked==true) {
			Deb.Debug_Logger("You nailed it. Cards were the same value.");
		} else if (_Main_Deck.stosLewa.Count==0 || _Main_Deck.stosPrawa.Count==0){
			Deb.Debug_Logger("There are no cards to be taken.");
		} else {
			Deb.Debug_Logger("You lost it. Cards were different.");
			passCardsToTheLoser();
		}
	}

	public void passCardsToTheLoser(){
		for (int i = 0; i < _Main_Deck.stosLewa.Count; i++) {
			temp.Add(_Main_Deck.stosLewa[i]);
		}
		for (int i = 0; i < _Main_Deck.stosPrawa.Count; i++) {
			temp.Add(_Main_Deck.stosPrawa[i]);
		}
		GameObject[] tagOfCard = GameObject.FindGameObjectsWithTag("DELETE_ME");
		foreach (GameObject tagOf in tagOfCard ){
			Deb.Debug_Logger(tagOf.name + " " + "has been deleted.");
			Destroy(tagOf);
		}
		Deb.Debug_Logger("All cards added to temporary list.");
		//only player 1 is going to take cards for now -- fixed AI Slot_Watcher
		for (int i = 0; i < temp.Count; i++) {
			_Main_Deck.player1_cards.Add(temp[i]);
		}
		string whichPlayer = "Player's 1";
		Deb.Debug_Logger("Cards added to" + whichPlayer + "deck.");
		tempCount = temp.Count;
		for (int i = 0; i < tempCount; i++) {
			temp.Remove(temp[i]);
		}
		Deb.Debug_Logger("All cards removed from temporary list.");
		deleteCardsOnStacks();
		Deb.Debug_Logger("All cards removed from real lists.");
	}

	public void deleteCardsOnStacks(){
		tempCount = _Main_Deck.stosPrawa.Count;
		for (int i = 0; i < tempCount; i++) {
			_Main_Deck.stosPrawa.Remove(_Main_Deck.stosPrawa[i]);
		}
		Deb.Debug_Logger("Cards from right stack removed.");
		tempCount = _Main_Deck.stosLewa.Count;
		for (int i = 0; i < tempCount; i++) {
			_Main_Deck.stosPrawa.Remove(_Main_Deck.stosLewa[i]);
		}
		Deb.Debug_Logger("Cards from left stack removed.");
	}

	public bool checkIfTheCardsHaveTheSameValue(){
		if (_Draggable.checkIfTheCardValueIsTheSame(
					_Main_Deck.stosLewa[_Main_Deck.stosLewa.Count-1].name,
					_Main_Deck.stosPrawa[_Main_Deck.stosPrawa.Count-1].name
					)
			 ) {
				 Deb.Debug_Logger("The cards have the same value.");
			return true;
		} else if (
		checkIfCardIsJoker(_Main_Deck.stosLewa[_Main_Deck.stosLewa.Count-1].name)
 ||	checkIfCardIsJoker(_Main_Deck.stosPrawa[_Main_Deck.stosPrawa.Count-1].name)
		){
			Deb.Debug_Logger("One of the cards was a Joker.");
			return true;
		} else {
			return false;
		}
	}

	public bool checkIfCardIsJoker(string testedCard){
		if(testedCard=="j1" || testedCard=="j2" || testedCard=="j3"){
			return true;
		} else {
			return false;
		}
	}

}
