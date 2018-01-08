using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player_1_Deck : MonoBehaviour, IPointerDownHandler  {

	public Deck _Deck_1;
	public Color tempCol;

public List<Sprite> Sprites = new List<Sprite>(); //List of Sprites added from the Editor to be created as GameObjects at runtime
// public GameObject ParentPanel; //Parent Panel you want the new Images to be children of

	// Use this for initialization
	void Start () {
		Debug.Log("Start");

		Color tempCol = new Color(0,0,0,0);

	}

	public void OnPointerDown (PointerEventData eventData){

		// Debug.Log(GameObject.Find("CardImage_1").GetComponent<UnityEngine.UI.Image>().color);
		// GameObject.Find("CardImage_1").GetComponent<UnityEngine.UI.Image>().color = new Color (1,1,1,1);

			// changeSprite ();
			GameObject NewObj = new GameObject(); //Create the GameObject
        Image NewImage = NewObj.AddComponent<Image>(); //Add the Image Component script
        NewImage.sprite = _Deck_1.player2_cards[1]; //Set the Sprite of the Image Component on the new GameObject
        NewObj.GetComponent<RectTransform>().transform.parent = GameObject.Find("First_Player_Cards").transform; //Assign the newly created Image GameObject as a Child of the Parent Panel.
        NewObj.SetActive(true); //Activate the GameObject    Cube2.transform.parent=Cube1.transform


		}

		public void changeSprite (){
			if (isEqualToNoColor("CardImage_1")) {
					GameObject.Find("CardImage_1").GetComponent<UnityEngine.UI.Image>().color = new Color(255,255,255);
					GameObject.Find("CardImage_1").GetComponent<UnityEngine.UI.Image>().sprite = _Deck_1.player2_cards[1];
					_Deck_1.player2_cards.Remove(_Deck_1.player2_cards[1]);
					return;
				} else if (isEqualToNoColor("CardImage_2")) {
						GameObject.Find("CardImage_2").GetComponent<UnityEngine.UI.Image>().color = new Color(255,255,255);
						GameObject.Find("CardImage_2").GetComponent<UnityEngine.UI.Image>().sprite = _Deck_1.player2_cards[1];
						_Deck_1.player2_cards.Remove(_Deck_1.player2_cards[1]);
					return;
				} else if (isEqualToNoColor("CardImage_3")) {
						GameObject.Find("CardImage_3").GetComponent<UnityEngine.UI.Image>().color = new Color(255,255,255);
						GameObject.Find("CardImage_3").GetComponent<UnityEngine.UI.Image>().sprite = _Deck_1.player2_cards[1];
						_Deck_1.player2_cards.Remove(_Deck_1.player2_cards[1]);
					return;
				} else if (isEqualToNoColor("CardImage_4")) {
						GameObject.Find("CardImage_4").GetComponent<UnityEngine.UI.Image>().color = new Color(255,255,255);
						GameObject.Find("CardImage_4").GetComponent<UnityEngine.UI.Image>().sprite = _Deck_1.player2_cards[1];
						_Deck_1.player2_cards.Remove(_Deck_1.player2_cards[1]);
					return;
				}
		}

		public bool isEqualToNoColor(string st){
			return GameObject.Find(st).GetComponent<UnityEngine.UI.Image>().color.Equals(tempCol);
		}

 	}
