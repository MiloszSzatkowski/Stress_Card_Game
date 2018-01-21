using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

	// all of the cards in order
	public List<Sprite> cards = new List<Sprite>();

	// copy of all of the cards
	public List<Sprite> cards_Instance = new List<Sprite>();

	// players' decks
	public List<Sprite> player1_cards = new List<Sprite>();
	public List<Sprite> player2_cards = new List<Sprite>();

	// stacks on left and right
	public List<Sprite> stosLewa = new List<Sprite>();
  public List<Sprite> stosPrawa = new List<Sprite>();

	// list of random indexes for shuffling
	public List<int> random_list = new List<int>();

	public int temp;
	public Debbug_Logger_Script Deb;
	public Draggable _Draggable;
	public Slot_Watcher _Slot_Watcher;
	public List<string> AiOnTableCards = new List<string>();
	public List<string> allPlayersCardsOnTheTable = new List<string>();

	//both players are blocked
	public bool blocked = false;

	void Start () {
		//creating random list of indexes
		int counter = 0;

		while (counter<cards.Count) {
			temp = Mathf.RoundToInt(Random.Range(0,cards.Count));

				if (!random_list.Contains(temp)) {
					random_list.Add(temp);
					//keep indexes different and end loop only if different numbers were added
					counter = counter + 1;
				}

			}

			// populating card deck copy by shuffled indexes
				for (int i = 0; i < cards.Count; i++)
					{
						cards_Instance.Add(cards[random_list[i]]);
					}

					// splitting card deck for players
				for (int i = 0; i < cards_Instance.Count ; i++) {

					if (i < cards_Instance.Count/2) {
						player1_cards.Add(cards_Instance[i]);
					} else {
						player2_cards.Add(cards_Instance[i]);
					}

				}
				//Add cards to check for Ai
				AiOnTableCards.Add("slot_5_real"); AiOnTableCards.Add("slot_6_real");
				AiOnTableCards.Add("slot_7_real"); AiOnTableCards.Add("slot_8_real");
				//Add cards to all players Cards Finder
				for (int i = 0; i < AiOnTableCards.Count; i++) {
					allPlayersCardsOnTheTable.Add( AiOnTableCards[i] );
				}
				allPlayersCardsOnTheTable.Add("slot_1_real");
				allPlayersCardsOnTheTable.Add("slot_2_real");
				allPlayersCardsOnTheTable.Add("slot_3_real");
				allPlayersCardsOnTheTable.Add("slot_4_real");
				// end of Start
				Deb.Debug_Logger("Game has started. Good Luck.");
				//Update started
				InvokeRepeating("blockEvent", 0, 0.8f);
				InvokeRepeating("Ai", 0, 1f);
		}

		void Ai(){
			for (int i = 0; i < AiOnTableCards.Count; i++) {
					if (GameObject.Find("SP_Cards_On_Table/" + AiOnTableCards[i]) != null) {
						GameObject tempObj = GameObject.Find("SP_Cards_On_Table/" + AiOnTableCards[i]);
						string tempName = GameObject.Find("SP_Cards_On_Table/" + AiOnTableCards[i]).GetComponent<UnityEngine.UI.Image>().sprite.name;
						if (stosLewa.Count  != 0) {
									if (_Draggable.checkWhatCardItIs(tempName) ==
									_Draggable.checkWhatCardItIs(stosLewa[stosLewa.Count-1].name)
									  ) {
											//the card can be dropped
									_Draggable.moveToTop("SP_Cards_On_Table/" + AiOnTableCards[i]);
									_Draggable.addCardToThe("LeftCard", tempObj.name);
									GameObject.Find(AiOnTableCards[i]).tag = "DELETE_ME";
									iTween.MoveTo(tempObj,(Vector2)GameObject.Find("LeftCard").transform.position,1);
									// break;
									}
						} else if (stosPrawa.Count != 0) {
									if (_Draggable.checkWhatCardItIs(tempName) ==
									_Draggable.checkWhatCardItIs(stosPrawa[stosPrawa.Count-1].name)
										)	{
											//the card can be dropped
									_Draggable.moveToTop("SP_Cards_On_Table/" + AiOnTableCards[i]);
									_Draggable.addCardToThe("RightCard", tempObj.name);
									GameObject.Find(AiOnTableCards[i]).tag = "DELETE_ME";
									iTween.MoveTo(tempObj,(Vector2)GameObject.Find("RightCard").transform.position,1);
									// break;
									}
						}
				}
			}
			//Ai fn end
		}

		void Drag (string checkedNameOfCard) {
			// iTween.MoveTo(tempObj,(Vector2)GameObject.Find("LeftCard").transform.position,1);
		}

		//UPDATE

		void blockEvent(){
			if(areAllCardsOnTheTableBlocked()){
				if (!AreTherePossibleMoves()) {
					Deb.Debug_Logger("Block Event happening.");
					blocked = true;
				} else {
					blocked = false;
				}
			} else {
				blocked = false;
			}
		}

		public bool AreTherePossibleMoves (){
			for (int i = 0; i < allPlayersCardsOnTheTable.Count; i++) {
				GameObject tempObj1 = GameObject.Find("SP_Cards_On_Table/" + allPlayersCardsOnTheTable[i]);
				GameObject tempObj2 = GameObject.Find("FP_Cards_On_Table/" + allPlayersCardsOnTheTable[i]);
					if (tempObj1) {
								 if (
								 checkEqualityOfIntegers(
								 		_Draggable.checkWhatCardItIs(tempObj1.GetComponent<UnityEngine.UI.Image>().sprite.name),
								 		_Draggable.checkWhatCardItIs(stosLewa[stosLewa.Count-1].name),
								 		_Draggable.checkWhatCardItIs(stosPrawa[stosPrawa.Count-1].name)
								 		)
								 ){
									 // Deb.Debbug_Logger("There are some possible moves for 1st player.");
									 return true;
								 }
				  } else if (tempObj2) {
								if (
								checkEqualityOfIntegers(
										_Draggable.checkWhatCardItIs(tempObj2.GetComponent<UnityEngine.UI.Image>().sprite.name),
										_Draggable.checkWhatCardItIs(stosLewa[stosLewa.Count-1].name),
										_Draggable.checkWhatCardItIs(stosPrawa[stosPrawa.Count-1].name)
										)
								){
									// Deb.Debbug_Logger("There are some possible moves for 2nd player.");
									return true;
								}
				}
				//end of loop
			}
			return false;
			//end of void
		}

		public bool checkEqualityOfIntegers (int str1, int str2, int str3 = 0, int str4 = 0){
			if (str1==str2) {
				return true;
			} else if (str1==str3){
				return true;
			} else if (str1==str4){
				return true;
			} else {
				return false;
			}
		}

		public bool areAllCardsOnTheTableBlocked(){
			if (allPlacesAreOccupated()
			&& areThereCardsOnPilesInTheMiddle())
		  {
				Deb.Debug_Logger("All places are occupated.");
				return true;
			} else if (
				 player1_cards.Count == 0
			&& player2_cards.Count == 0
			&& doPlayerHaveCardsOnTable("player_1")
			&& doPlayerHaveCardsOnTable("player_2")
			&& areThereCardsOnPilesInTheMiddle()){
				Deb.Debug_Logger("There are cards in the middle and no cards in the players' decks.");
				return true;
			} else if (
			player1_cards.Count == 0
			&& doPlayerHaveCardsOnTable("player_1")
			&& areThereCardsOnPilesInTheMiddle()
			){
				return true;
			} else if (
			player2_cards.Count == 0
			&& doPlayerHaveCardsOnTable("player_2")
			&& areThereCardsOnPilesInTheMiddle()
			){
				return true;
			} else {
				return false;
			}
		}
		public bool doPlayerHaveCardsOnTable(string player){
			if (player=="player_1"){
				if (_Slot_Watcher.slot1==true
				||	_Slot_Watcher.slot2==true
				||	_Slot_Watcher.slot3==true
				||	_Slot_Watcher.slot4==true) {
					return true;
				} else {
					return false;
				}
			} else if (player=="player_2"){
				if (_Slot_Watcher.slot5==true
				||	_Slot_Watcher.slot6==true
				||	_Slot_Watcher.slot7==true
				||	_Slot_Watcher.slot8==true) {
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
		}

		public bool areThereCardsOnPilesInTheMiddle(){
			if (stosLewa.Count  > 0
			&&  stosPrawa.Count > 0) {
				return true;
			} else {
				return false;
			}
		}

		public bool allPlacesAreOccupated(){
			if (
					_Slot_Watcher.slot1==true
			&&	_Slot_Watcher.slot2==true
			&&	_Slot_Watcher.slot3==true
			&&	_Slot_Watcher.slot4==true
			&&	_Slot_Watcher.slot5==true
			&&	_Slot_Watcher.slot6==true
			&&	_Slot_Watcher.slot7==true
			&&	_Slot_Watcher.slot8==true
			){ return true;  } else
			 { return false; }
		}

	// end of MonoBehaviour
}
