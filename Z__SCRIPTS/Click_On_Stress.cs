using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_On_Stress : MonoBehaviour {

	public Deck _Main_Deck;
	public Draggable _Draggable;
	public Debbug_Logger_Script Deb;
	public List<Sprite> temp = new List<Sprite>();

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().onClick.AddListener(triggerStressEvent);
	}

	public void triggerStressEvent (){
		Deb.Debug_Logger("You clicked 'Stress!' ");
		if (checkIfTheCardsHaveTheSameValue()) {
			Deb.Debug_Logger("You nailed it. Cards were the same value.");
		} else if (_Main_Deck.stosLewa.Count==0 || _Main_Deck.stosPrawa.Count==0){
			Deb.Debug_Logger("There are simply no cards to be taken.");
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
		Deb.Debug_Logger(GameObject.FindGameObjectsWithTag("DELETE_ME")[0].name);
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
