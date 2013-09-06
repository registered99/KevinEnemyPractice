using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Shot :  FSprite
{
	
	private float speedX = 0.0f;
	private float speedY = 0.0f;
	
	private float frameCount = 0;
	
	
	public Shot() : base("Banana")
	{
		
		this.scale = 0.25f;
		this.speedX = 10.0f;
		
		//Debug.Log ("SHOT CREATED");
		///////TOUCHIN IT
		//Futile.touchManager.AddMultiTouchTarget(this);
		
	}
	
	public Shot(Enemy enemy) : base("Banana")
	{
		
		this.scale = 0.25f;
		this.speedX = -5.0f;
		
		//Debug.Log ("ENEMY SHOT CREATED");
		///////TOUCHIN IT
		//Futile.touchManager.AddMultiTouchTarget(this);
		
	}
	
	public void Update()
	{
		this.x += speedX;
		this.y += speedY;
		
		frameCount += 1;
		//Debug.Log (speedX);
	}
	
}