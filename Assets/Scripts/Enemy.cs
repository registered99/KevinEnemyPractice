using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Enemy :  FSprite
{
	
	
	private float speedX = 3.0f;
	private float speedY = 0.0f;
	private float shotrate = RXRandom.Range (60.0f,80.0f);
	
	private float frameCount = 0;
	
	private Shot _shot;
	private List<Shot> _shots = new List<Shot>();
	
	
	public Enemy() : base("Monkey_0")
	{
		
		
		this.x = Futile.screen.halfWidth;
		this.y = RXRandom.Range(-Futile.screen.halfHeight, Futile.screen.halfHeight);
		this.scale = 0.25f;
	
		
		//Debug.Log ("ENEMY CREATED");
		///////TOUCHIN IT
		//Futile.touchManager.AddMultiTouchTarget(this);
		
	}
	
	public void Update()
	{
		
		this.x -= speedX;
		this.y += speedY;
		
		if(frameCount%shotrate == 0)
		{
			_shot = new Shot(this);
			_shot.x = this.x;
			_shot.y = this.y;
			_shots.Add(_shot);
			Futile.stage.AddChild(_shot);
		}
		
		for(int b = _shots.Count - 1; b>=0; b--)
		{
			_shots[b].Update ();
			Shot shotted = _shots[b];
			if(shotted.x < -Futile.screen.halfWidth)
			{
				shotted.RemoveFromContainer();
				_shots.Remove(shotted);
				Debug.Log ("NO BAD BANANA");
			}
		}
		
		frameCount += 1;
		//Debug.Log (speedX);
	}
	
	public List<Shot> getShots()
	{
		return _shots;
	}
}