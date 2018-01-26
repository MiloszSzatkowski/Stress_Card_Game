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
		for (int i = 0; i < 14*4; i++) {
			//counter
			CardInstance = Instantiate(Card, Vector2.zero , Quaternion.identity);
			Debug.Log("Cards instantiated");

			Card_Class _Card_Class = CardInstance.GetComponent<Card_Class>();

			_Card_Class.containers = new List<GameObject> {
				GameObject.Find("1_Deck"),
				GameObject.Find("2_Deck"),
				GameObject.Find("1_Table"),
				GameObject.Find("2_Table"),
				GameObject.Find("Left_Stack"),
				GameObject.Find("Right_Stack")
			};
			_Card_Class.suits = new List<string> {"Clubs", "Diamonds", "Hearts", "Spades"} ;
			_Card_Class.values = new List<string> {"2","3","4","5","6","7","8","9","10","Jack","Queen","King","Ace","Joker"} ;

			if (values_counter == _Card_Class.values.Count) { values_counter = 0; }

				_Card_Class.card_value = (string) _Card_Class.values[values_counter];
				Debug.Log(values_counter);

				if (suits_counter == _Card_Class.suits.Count) { suits_counter = 0; }

				//if card value is Joker, make suit a Joker as well
					if(_Card_Class.card_value=="Joker"){
				_Card_Class.card_suit  = "Joker";
				suits_counter=suits_counter+1;}else{
						_Card_Class.card_suit  = (string) _Card_Class.suits[suits_counter];
					}

				Debug.Log(suits_counter);


			values_counter=values_counter+1;

			//add image to card
			CardInstance.GetComponent<UnityEngine.UI.Image>().sprite =
			GameObject.Find("Textures").transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;

			//set proper Color
			CardInstance.GetComponent<UnityEngine.UI.Image>().color= new Color (1,1,1,1);

			//set parent to Canvas
			CardInstance.transform.SetParent(GameObject.Find("Temp").transform);

			//set size
			// CardInstance.transform.localScale = new Vector3 (0.35f,0.55f,1f);

			//set position
			// Vector3 rand = new Vector3 (Random.Range(0,i*8),Random.Range(0,i*8),1f);
			// CardInstance.transform.Rotate(Vector3.up * 2);
			// CardInstance.transform.position = new Vector3(0.1f, 0.1f, 1f) + rand;
			// Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, 1f) + rand);
			}

			moveAllChildren(GameObject.Find("Temp"), GameObject.Find("1_Deck"));
		}

		public void moveAllChildren(GameObject old_parent, GameObject new_parent){
			Debug.Log("Moved " + old_parent.transform.childCount + " objects from " + old_parent.name + " to " + new_parent.name);
			for (int i=old_parent.transform.childCount-1; i >= 0; --i) {
			Transform child = old_parent.transform.GetChild(i);
			child.SetParent(new_parent.transform, false);
			}
		}

	//mono end
}
