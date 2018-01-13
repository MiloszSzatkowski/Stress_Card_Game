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
				// end of Start
				Deb.Debug_Logger("Game has started. Good Luck.");
		}

	// end of MonoBehaviour
}
