﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

	public Vector2 dragOffset = new Vector2(0f,0f);
	public Vector2 startPosition = new Vector2(0f,0f);
	public Deck _Deck;
	public int index;

	void Start (){
		gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0);
		startPosition = this.transform.position;
		Color tempCol = new Color(0,0,0,0);
	}

	public void OnBeginDrag (PointerEventData eventData){
		dragOffset = eventData.position - (Vector2)this.transform.position;
	}

	public void OnDrag (PointerEventData eventData){
		this.transform.position = eventData.position - dragOffset;

	}

	public void OnEndDrag (PointerEventData eventData){

		if(this.transform.position.y < 110){
				this.transform.position = startPosition;
		} else if (this.transform.position.x < Screen.width / 2){
			this.transform.position =  Vector2.Lerp (
			this.transform.position,
			(Vector2)GameObject.Find("LeftCard").transform.position + new Vector2(Random.Range(0,3),Random.Range(0,3)), 1);
		} else if (this.transform.position.x > Screen.width / 2){
			this.transform.position =  Vector2.Lerp (
			this.transform.position,
			(Vector2)GameObject.Find("RightCard").transform.position + new Vector2(Random.Range(0,3),Random.Range(0,3)), 1);
		} else {
			this.transform.position = startPosition;
		}


		// index = Mathf.RoundToInt(Random.Range(0,_Deck.cards.Count));
		// gameObject.GetComponent<UnityEngine.UI.Image>().sprite = _Deck.cards_Instance[index];

	}

	// public bool isEqualToNoColor(string st){
	// 	// return GameObject.Find(st).GetComponent<UnityEngine.UI.Image>().color.Equals(tempCol);
	// }

}