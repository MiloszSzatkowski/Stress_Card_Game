using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour {

	public GameObject Card;
	public GameObject CardInstance;

	//stress button
	public GameObject StressButton;

	//positions
	public float w ;
	public float h ;

	public Vector2 center ;	public Vector2 leftStack ;
	public Vector2 rightStack ;	public Vector2 cardsIn1stDeck ;
	public Vector2 place_1 ;	public Vector2 place_2 ;
	public Vector2 place_3 ;	public Vector2 place_4 ;
	public Vector2 cardsIn2ndDeck ;
	public Vector2 place_5 ;	public Vector2 place_6 ;
	public Vector2 place_7 ;	public Vector2 place_8 ;
	public List<Vector2> list_of_places;

	// Use this for initialization
	void Start () {
		  w = GameObject.Find("Canvas").GetComponent<RectTransform> ().rect.width;
		  h = GameObject.Find("Canvas").GetComponent<RectTransform> ().rect.height;

		center = new Vector2 (w/2,h/2);
		leftStack = new Vector2 (w/8*3,h/2);		rightStack = new Vector2 (w/8*5,h/2);
		cardsIn1stDeck = new Vector2 (w/8,h/4);
		place_1 = new Vector2 (w/6*2,h/4);		place_2 = new Vector2 (w/6*3,h/4);
		place_3 = new Vector2 (w/6*4,h/4);		place_4 = new Vector2 (w/6*5,h/4);
		cardsIn2ndDeck = new Vector2 (w-w/8,h/4*3);
		place_5 = new Vector2 (w-w/6*2,h/4*3);		place_6 = new Vector2 (w-w/6*3,h/4*3);
		place_7 = new Vector2 (w-w/6*4,h/4*3);		place_8 = new Vector2 (w-w/6*5,h/4*3);

		list_of_places = new List<Vector2>
		{leftStack,rightStack,cardsIn1stDeck,place_1,place_2,place_3,place_4,cardsIn2ndDeck,place_5,place_6,place_7,place_8};
		int suits_counter = 0;
		int values_counter = 0;


		//LOOP ------------------------------------------------------------------------------
		for (int i = 0; i < 14*4; i++) {
			//counter
			CardInstance = Instantiate(Card, Vector2.zero , Quaternion.identity);

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

			values_counter=values_counter+1;

			//make this sprite default
			_Card_Class.onTopSprite = GameObject.Find("Textures").transform.GetChild(i).GetComponent<SpriteRenderer>().sprite;
			//add image to card
			CardInstance.GetComponent<UnityEngine.UI.Image>().sprite = _Card_Class.onTopSprite;
			//assign back texture for flipping Card_Deck
			_Card_Class.onBackSprite = GameObject.Find("ParentOfOtherTextures").transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

			//set proper Color
			CardInstance.GetComponent<UnityEngine.UI.Image>().color= new Color (1,1,1,1);

			//set parent to Canvas
			CardInstance.transform.SetParent(GameObject.Find("Temp").transform);

			//making the size dependant on window
			CardInstance.transform.localScale = new Vector3 (w/200,h/330,1f);

			//set position
			// Vector3 rand = new Vector3 (Random.Range(0,i*8),Random.Range(0,i*8),1f);
			// CardInstance.transform.Rotate(Vector3.up * 2);
      // cardsIn1stDeck place_1

				if (i<list_of_places.Count) {
					CardInstance.transform.position = list_of_places[i];
				}

				//enable particle System
				// CardInstance.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
				// CardInstance.transform.GetChild(1).GetComponent<ParticleSystem>().enableEmission = true;
			// Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.1f, 1f) + rand);
			}
			Debug.Log("Cards instantiated");
			//move all children to proper spaces
			moveAllChildren(GameObject.Find("Temp"), GameObject.Find("1_Deck"), cardsIn1stDeck);
			//shuffle
			shuffleChildren(GameObject.Find("1_Deck"));

			//add Stress button
			StressButton = Instantiate(GameObject.Find("Stress_Button"), Vector2.zero , Quaternion.identity);
			StressButton.transform.localScale = new Vector3 (w/200,h/330,1f);
			StressButton.transform.position = new Vector2 (w-w/3,h/9);
			StressButton.transform.SetParent(GameObject.Find("Temp").transform);
			InvokeRepeating("animateButton",0,3f);
		}

		public void animateButton (){
			iTween.ScaleTo(StressButton, new Vector3 (0.2f,0.2f,1f), 1f);
			iTween.ScaleTo(StressButton, new Vector3 (2f,2f,1f), 1f);
		}


		public void shuffleChildren(GameObject parentOfObjects){
			if (parentOfObjects.transform.childCount>1) {
				for (int i=parentOfObjects.transform.childCount-1; i >= 0; --i) {
					parentOfObjects.transform.GetChild(i).gameObject.transform.SetSiblingIndex(Random.Range(0, i));
				}
			}
		}

		public void moveAllChildren(GameObject old_parent, GameObject new_parent, Vector2 place = default(Vector2)){
			Debug.Log("Moved " + old_parent.transform.childCount + " objects from " + old_parent.name + " to " + new_parent.name);
			for (int i=old_parent.transform.childCount-1; i >= 0; --i) {
			Transform child = old_parent.transform.GetChild(i);
				child.gameObject.GetComponent<Card_Class>().startPosition = place;
			iTween.MoveTo(child.gameObject,place + new Vector2(Random.Range(0,8),Random.Range(0,8)),0.5f);
			child.SetParent(new_parent.transform, false);
			if (place == cardsIn1stDeck || place == cardsIn2ndDeck) {
				child.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = child.gameObject.GetComponent<Card_Class>().onBackSprite;
			} else if (place != cardsIn1stDeck && place != cardsIn2ndDeck){
					child.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = child.gameObject.GetComponent<Card_Class>().onTopSprite;
				}
			}
		}

		public void moveThis(GameObject go, GameObject new_parent, Vector2 place = default(Vector2)){
			Debug.Log("Moved " + go.name + " to " + new_parent.name);
			go.transform.SetParent(new_parent.transform, false);
				go.GetComponent<Card_Class>().startPosition = place;
			iTween.MoveTo(go,place + new Vector2(Random.Range(0,8),Random.Range(0,8)) ,0.5f);
			if (place == cardsIn1stDeck || place == cardsIn2ndDeck) {
				go.GetComponent<UnityEngine.UI.Image>().sprite = go.GetComponent<Card_Class>().onBackSprite;
			} else if (place != cardsIn1stDeck && place != cardsIn2ndDeck) {
				go.GetComponent<UnityEngine.UI.Image>().sprite = go.GetComponent<Card_Class>().onTopSprite;
			}
		}

		public void checkForFreeSlot()
		{
			GameObject LastChildren = GameObject.Find("1_Deck").transform.GetChild(0).gameObject;
			GameObject sp1 = GameObject.Find("spot_1");
			GameObject sp2 = GameObject.Find("spot_2");
			GameObject sp3 = GameObject.Find("spot_3");
			GameObject sp4 = GameObject.Find("spot_4");

						 if (sp1.transform.childCount==0){
				moveThis(LastChildren, sp1, place_1);

			} else if (sp2.transform.childCount==0 ){
				moveThis(LastChildren, sp2, place_2);

			} else if (sp3.transform.childCount==0 ){
				moveThis(LastChildren, sp3, place_3);

			} else if (sp4.transform.childCount==0){
				moveThis(LastChildren, sp4, place_4);
			}
		}

	//mono end
}
