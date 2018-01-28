using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card_Class : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler  {

	public Initialize _In;

	//suits: of Hearts, Diamonds, Clubs, Spades
	public List<string> suits;
	public string card_suit;

	//values : 2,3,4,5,6,7,8,9,10,Jack,Queen,King,Ace,Joker
	public List<string> values;
	public string card_value;

	//containers - 1_Deck 2_Deck 1_Table 2_Table Left_Stack Right_Stack
	public List<GameObject> containers;
	public GameObject container;

	public Vector2 dragOffset = new Vector2(0f,0f);
	public Vector2 startPosition = new Vector2(0f,0f);

	//starting sprites for switching
	public Sprite onTopSprite;
	public Sprite onBackSprite;

	public void OnBeginDrag (PointerEventData eventData){
		dragOffset = eventData.position - (Vector2)this.transform.position;
	}

	public void OnDrag (PointerEventData eventData){
		if (checkIfDraggable()) {
			this.transform.position = eventData.position - dragOffset;
			iTween.PunchRotation (gameObject, new Vector3 (80f,1,1), 2);
		} else {
			iTween.MoveTo(gameObject,startPosition,0.3f);
			Debug.Log("You can't drag this card.");
		}
	}

	public void OnEndDrag (PointerEventData eventData)
	 {
		if (gameObject.transform.position.y > Screen.height/3 && gameObject.transform.position.x > Screen.width/2)
			{
				if (checkIfMoveIsPossible(gameObject, GameObject.Find("Right_Stack")) ||
				GameObject.Find("Right_Stack").transform.childCount == 0) {
					_In.moveThis(gameObject, GameObject.Find("Right_Stack"),_In.rightStack);
				} else {
					iTween.MoveTo(gameObject,startPosition,0.3f);
				}
		} else if (gameObject.transform.position.y > Screen.height/3 && gameObject.transform.position.x < Screen.width/2)
			{
				if (checkIfMoveIsPossible(gameObject, GameObject.Find("Left_Stack")) ||
				GameObject.Find("Left_Stack").transform.childCount == 0)
				{
				_In.moveThis(gameObject, GameObject.Find("Left_Stack"),_In.leftStack);
				} else {
					iTween.MoveTo(gameObject,startPosition,0.3f);
				}
		} else {
			iTween.MoveTo(gameObject,startPosition,0.3f);
			Debug.Log("You can't drag this card.");
		}
	}

	public void OnPointerClick(PointerEventData eventData)
    {
			Debug.Log("You clicked on card.");
			if (gameObject.transform.parent.gameObject == GameObject.Find("1_Deck")) {
				_In.checkForFreeSlot();
			}
    }

	public bool checkIfDraggable () {
			if (gameObject.transform.parent.parent.gameObject == GameObject.Find("1_Table") ||
					gameObject.transform.parent.parent.gameObject == GameObject.Find("2_Table")) {
				return true;
			} else {
				return false;
			}
		//bool end
	}

	public bool checkIfMoveIsPossible(GameObject thisGameObject, GameObject cardParent) {
		if (cardParent.transform.childCount>0) {
			if (
			thisGameObject.GetComponent<Card_Class>().card_suit ==
			cardParent.transform.GetChild(cardParent.transform.childCount-1)
				.gameObject.GetComponent<Card_Class>().card_suit ||

			thisGameObject.GetComponent<Card_Class>().card_value ==
			cardParent.transform.GetChild(cardParent.transform.childCount-1)
				.gameObject.GetComponent<Card_Class>().card_value ||

			thisGameObject.GetComponent<Card_Class>().card_value == "Joker" ||
			cardParent.transform.GetChild(cardParent.transform.childCount-1)
				.gameObject.GetComponent<Card_Class>().card_value == "Joker"
				) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

}
