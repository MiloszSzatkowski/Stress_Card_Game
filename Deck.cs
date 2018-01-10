using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

	// wszystkie karty w liście
	public List<Sprite> cards = new List<Sprite>();

	// stwórz kopię wszystkich kart
	public List<Sprite> cards_Instance = new List<Sprite>();

	//stwórz talię graczy
	public List<Sprite> player1_cards = new List<Sprite>();
	public List<Sprite> player2_cards = new List<Sprite>();

	//stwórz stosy na środku
	public List<Sprite> stosLewa = new List<Sprite>();
  public List<Sprite> stosPrawa = new List<Sprite>();

	//losowa lista do tasowania
	public List<int> random_list = new List<int>();

	public int temp;

	void Start () {
		//tworzenie listy losowych indeksów - tasowanie
		int counter = 0;

		while (counter<cards.Count) {
			temp = Mathf.RoundToInt(Random.Range(0,cards.Count));

				if (!random_list.Contains(temp)) {
					random_list.Add(temp);
					//zmieniaj index dopiero po dodaniu karty
					counter = counter + 1;
				}

			}

			// populacja kopii kartami ze starej listy
				for (int i = 0; i < cards.Count; i++)
					{
						cards_Instance.Add(cards[random_list[i]]);
					}

					//populacja do talii graczy
				for (int i = 0; i < cards_Instance.Count ; i++) {

					if (i < cards_Instance.Count/2) {
						player1_cards.Add(cards_Instance[i]);
					} else {
						player2_cards.Add(cards_Instance[i]);
					}

				}
				// koniec startu
		}

	// koniec MonoBehaviour
}
