using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_On_Stress : MonoBehaviour {

	public Deck _Main_Deck;
	public Draggable _Draggable;
	public Debbug_Logger_Script Deb;
	public List<Sprite> temp = new List<Sprite>();
	public GameObject Back_Card;

	//temporary integer
  public int tempCount;

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().onClick.AddListener(triggerStressEvent);
	}

	public void triggerStressEvent () {
		Deb.Debug_Logger(" You clicked 'STRESS!' ");
		if (_Main_Deck.areThereCardsOnPilesInTheMiddle()) {
			int howMany = _Main_Deck.stosLewa.Count +  _Main_Deck.stosPrawa.Count;
			Deb.Debug_Logger("There were " + howMany + " cards in the middle.");
			//main event
				if (checkIfTheCardsHaveTheSameValue() || _Main_Deck.blocked==true) {
					if (_Main_Deck.blocked==true) {
						Deb.Debug_Logger("You got stressed in a block situtation. Madness.");
					}
					passCardsToTheLoser(" Player 1 ");
					Deb.Debug_Logger("You nailed it. Cards were the same value or everyone got blocked.");
				} else if (_Main_Deck.stosLewa.Count==0 || _Main_Deck.stosPrawa.Count==0){
					Deb.Debug_Logger("There are not enough cards to be taken.");
				} else {
					//you lost it
					Deb.Debug_Logger("You lost it. Cards were different or you weren't blocked.");
					passCardsToTheLoser(" Player 2 ");
				}
				// else element of trigger
		} else { Deb.Debug_Logger("There are not enough cards in the middle."); }
	// end of trigger
	}

	public void passCardsToTheLoser(string whichPlayer){
		for (int i = 0; i < _Main_Deck.stosLewa.Count; i++) {
			temp.Add(_Main_Deck.stosLewa[i]);  }
		for (int i = 0; i < _Main_Deck.stosPrawa.Count; i++) {
			temp.Add(_Main_Deck.stosPrawa[i]); }
		//delete sprites on stacks
		GameObject[] tagOfCard = GameObject.FindGameObjectsWithTag("DELETE_ME");
		foreach (GameObject tagOf in tagOfCard ){
			Deb.Debug_Logger(tagOf.name + " " + "has been deleted.");
			Destroy(tagOf);
		}
		Deb.Debug_Logger("All cards added to temporary list.");
		//only player 1 is going to take cards for now -- fixed AI Slot_Watcher
		if 				(whichPlayer==" Player 1 ") {
			for (int i = 0; i < temp.Count; i++) {
				if (temp.Count > 0) {
				_Main_Deck.player1_cards.Add(temp[i]);
				InstantiateCardsInPlayerDeck(" Player 1 ");
				}
			}
		} else if (whichPlayer==" Player 2 ") {
			for (int i = 0; i < temp.Count; i++) {
				if (temp.Count > 0) {
				_Main_Deck.player2_cards.Add(temp[i]);
				InstantiateCardsInPlayerDeck(" Player 2 ");
				}
			}
		}
		Deb.Debug_Logger(temp.Count + " cards added to " + whichPlayer + " deck.");
		Deb.Debug_Logger(temp.Count + " cards removed from temporary list.");

				temp.Clear();

		deleteCardsOnStacks();
		Deb.Debug_Logger("All cards removed from real lists.");
	}

	public void deleteCardsOnStacks(){
		Deb.Debug_Logger(_Main_Deck.stosPrawa.Count + "cards from right stack removed.");
		_Main_Deck.stosPrawa.Clear();

		Deb.Debug_Logger(_Main_Deck.stosLewa.Count + " cards from left stack removed.");
		_Main_Deck.stosLewa.Clear();
	}

	public void InstantiateCardsInPlayerDeck(string whichPlayer){
		if (whichPlayer==" Player 1 ") {
			Back_Card = Instantiate(GameObject.Find("Back_Deck_Prefab"), Vector2.zero , Quaternion.identity);
			Back_Card.transform.position = (Vector2)GameObject.Find("Deck_1").transform.position + new Vector2(Random.Range(0,10),Random.Range(0,10));
			Back_Card.GetComponent<RectTransform>().transform.SetParent(GameObject.Find("FP_Cards_In_Deck").transform,true); //make a child of
		} else if (whichPlayer==" Player 2 "){
			Back_Card = Instantiate(GameObject.Find("Back_Deck_Prefab"), Vector2.zero , Quaternion.identity);
			Back_Card.transform.position = (Vector2)GameObject.Find("Deck_2").transform.position + new Vector2(Random.Range(0,10),Random.Range(0,10));
			Back_Card.GetComponent<RectTransform>().transform.SetParent(GameObject.Find("SP_Cards_In_Deck").transform,true); //make a child of
		}
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
