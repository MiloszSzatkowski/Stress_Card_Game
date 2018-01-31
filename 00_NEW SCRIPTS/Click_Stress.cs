using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Click_Stress : MonoBehaviour, IPointerClickHandler  {

public Initialize _Initialize;
public GameObject player1;
public GameObject player2;
public GameObject left;
public GameObject right;
public bool playerOne = true;

	// Use this for initialization
	void Start () {
		player1 = GameObject.Find("1_Deck");
		player2 = GameObject.Find("2_Deck");
		left = GameObject.Find("Left_Stack");
		right = GameObject.Find("Right_Stack");
	}

	public void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log("You clicked on Stress Button.");
			if (playerOne==true) {
				if ( blocked() || cardsHaveSameValueOrThereIsAJoker() ) {
					passCards("2");
					Debug.Log("Stress! Player 2 gets the cards.");
				} else {
					Debug.Log("Stress! Player 1 gets the cards.");
					passCards("1");
				}
			} else {
				if ( blocked() || cardsHaveSameValueOrThereIsAJoker() ) {
					passCards("2");
					Debug.Log("Stress! Player 2 gets the cards.");
				} else {
					Debug.Log("Stress! Player 1 gets the cards.");
					passCards("1");
				}
			}
		}

		public void passCards(string player){
			if (player=="1") {
				if (left.transform.childCount>0) {
					_Initialize.moveAllChildren(left,player1,_Initialize.cardsIn1stDeck);
				} else {
					Debug.Log("No cards on left Stack.");
				}
				if (right.transform.childCount>0) {
					_Initialize.moveAllChildren(right,player1,_Initialize.cardsIn1stDeck);
				} else {
					Debug.Log("No cards on right Stack.");
				}
			} else if (player=="2"){
				if (left.transform.childCount>0) {
					_Initialize.moveAllChildren(left,player2,_Initialize.cardsIn2ndDeck);
				} else {
					Debug.Log("No cards on left Stack.");
				}
				if (right.transform.childCount>0) {
					_Initialize.moveAllChildren(right,player2,_Initialize.cardsIn2ndDeck);
				}	else {
					Debug.Log("No cards on right Stack.");
				}
			}
		}

		public bool blocked(){
			int full = 0;
			int full2 = 0;
			List<GameObject> parents = new List<GameObject> {
			player1,player2,left,right,
			GameObject.Find("spot_1"),
			GameObject.Find("spot_2"),
			GameObject.Find("spot_3"),
			GameObject.Find("spot_4"),
			GameObject.Find("spot_5"),
			GameObject.Find("spot_6"),
			GameObject.Find("spot_7"),
			GameObject.Find("spot_8")};
			List<GameObject> matches = new List<GameObject> {
			GameObject.Find("spot_1"),
			GameObject.Find("spot_2"),
			GameObject.Find("spot_3"),
			GameObject.Find("spot_4"),
			GameObject.Find("spot_5"),
			GameObject.Find("spot_6"),
			GameObject.Find("spot_7"),
			GameObject.Find("spot_8")};
			for (int i = 0; i < parents.Count; i++) {
				if(parents[i].transform.childCount>0){
					full++;
				}
			}
			if (full==parents.Count) {
				for (int i = 0; i < matches.Count; i++) {
					if(matches[i].transform.GetChild(matches[i].transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value
					== left.transform.GetChild(left.transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value ||
					matches[i].transform.GetChild(matches[i].transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value
					== right.transform.GetChild(right.transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value ||
					matches[i].transform.GetChild(matches[i].transform.childCount-1).gameObject.GetComponent<Card_Class>().card_suit
					== left.transform.GetChild(left.transform.childCount-1).gameObject.GetComponent<Card_Class>().card_suit ||
					matches[i].transform.GetChild(matches[i].transform.childCount-1).gameObject.GetComponent<Card_Class>().card_suit
					== right.transform.GetChild(right.transform.childCount-1).gameObject.GetComponent<Card_Class>().card_suit ||
					matches[i].transform.GetChild(matches[i].transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value == "Joker" ||
					left.transform.GetChild(left.transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value == "Joker" ||
					right.transform.GetChild(right.transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value == "Joker" ){
						full2++;
					}
				}
			}
			if (full==parents.Count && full2==matches.Count) {
				Debug.Log("You are blocked.");
				return true;
			} else {
				Debug.Log("You were not blocked.");
				return false;
			}
		}

		public bool cardsHaveSameValueOrThereIsAJoker() {
			if (left.transform.childCount>0 && right.transform.childCount>0){
				if (left.transform.GetChild(left.transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value ==
				right.transform.GetChild(right.transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value) {
					return true;
				} else if (left.transform.GetChild(left.transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value == "Joker"
				|| right.transform.GetChild(right.transform.childCount-1).gameObject.GetComponent<Card_Class>().card_value == "Joker") {
					return true;
				} else {
					return false;
				}
			} else {
				Debug.Log("At least 2 cards are needed to compare.");
				return false;
			}
		}

}
