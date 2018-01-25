using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {

	public GameObject Card;
	public GameObject CardInstance;

	// Use this for initialization
	void Start () {
		int suits_counter = 0;
		int values_counter = 0;
		for (int i = 0; i < 13*4; i++) {
			//counter
			CardInstance = Instantiate(Card, Vector2.zero , Quaternion.identity);
			Debug.Log("Cards intantiated");

			Card_Class _Card_Class = CardInstance.GetComponent<Card_Class>();

			_Card_Class.suits = new List<string> {"Hearts", "Diamonds", "Clubs", "Spades"} ;
			_Card_Class.values = new List<string> {"2","3","4","5","6","7","8","9","10","Jack","Queen","King","Ace","Joker"} ;

			if (suits_counter == _Card_Class.suits.Count) {
				suits_counter = 0; }
			
				_Card_Class.card_value = (string) _Card_Class.suits[suits_counter];
				Debug.Log(suits_counter);

			if (values_counter == _Card_Class.values.Count) {
				values_counter = 0; }

				_Card_Class.card_suit  = (string) _Card_Class.values[values_counter];
				Debug.Log(values_counter);

			suits_counter=suits_counter+1;
			values_counter=values_counter+1;
		}
	}

}
