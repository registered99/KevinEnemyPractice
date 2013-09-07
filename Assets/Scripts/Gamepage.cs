using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Gamepage : FContainer, FMultiTouchableInterface
{
	private FSprite _background;
	private FSprite _naner;
	private FSprite _naner2;
	private FContainer _holder;
	private FButton _shootbutton;
	private FSprite _monkey;
	
	private Enemy _enemy;
	private List<Enemy> _enemies = new List<Enemy>();
	
	private Shot _shot;
	private List<Shot> _shots = new List<Shot>();
	
	private FButton _upbutton;
	private FButton _downbutton;
	private FButton _rightbutton;
	private FButton _leftbutton;
	
	private FContainer _movepad;
	
	private float speedY = 0.0f;
	private float speedX = 0.0f;
	private float frameCount = 0;
	
	public Gamepage()
	{
		
		EnableMultiTouch ();
		
		ListenForUpdate (HandleUpdate);
		
		_holder = new FContainer();

		_background = new FSprite("JungleClearBG");
		_naner = new FSprite("Banana");
		_naner2 = new FSprite("Banana");
		//_monkey = new FSprite("Monkey_0");
		
		AddChild(_background);
		
		_naner.x = 0.0f;
		_naner.y = -23.0f;
		_naner2.x = 0.0f;
		_naner2.y = 23.0f;
		_naner2.rotation = 180.0f;
		
		AddChild(_naner2);
		AddChild(_naner);
		
		Futile.stage.AddChild(_holder);
	}

	
	public void HandleUpdate()
	{
		frameCount += 1;
		if(frameCount%60 == 0)
		{
			_enemy = new Enemy();
			Futile.stage.AddChild(_enemy);
			_enemies.Add(_enemy);
		}
			
			
		for(int b = _shots.Count - 1; b>=0; b--)
		{
			_shots[b].Update(); // Shouldn't call update like this, i think
			Shot shotted = _shots[b];
			if(shotted.x > Futile.screen.halfWidth || shotted.x < -Futile.screen.halfWidth)
			{
				shotted.RemoveFromContainer();
				_shots.Remove(shotted);
				Debug.Log ("NO BANANA");
			}
		}
		
		for(int c = _enemies.Count - 1; c>=0; c--)
		{
			_enemies[c].Update();
			Enemy anamy = _enemies[c];
			if(anamy.x < -Futile.screen.halfWidth)
			{
				anamy.RemoveFromContainer();
				_enemies.Remove(anamy);
				Debug.Log ("enemy gone");
			}
		}
		
		
		for(int b = _shots.Count - 1; b>=0; b--)
		{

			Shot shotted = _shots[b];
			
			for(int c = _enemies.Count - 1; c>=0; c--)
				{
					Enemy anamy = _enemies[c];
				
				
					Vector2 hitbox = shotted.GetPosition();
					Rect monkeyBox = anamy.GetTextureRectRelativeToContainer();
					
				
					if(monkeyBox.Contains(hitbox))
					{
						List<Shot> deadShots = anamy.getShots();
						for(int j = deadShots.Count - 1; j>=0; j--)
						{
							_shots.Add(deadShots[j]);
						}
						
						anamy.RemoveFromContainer();
						_enemies.Remove(anamy);
						shotted.RemoveFromContainer();
						_shots.Remove(shotted);
						Debug.Log ("KILLED EM");
					}
				}
		}
		
		
		
	}
	
	public void HandleShoot()
	{
		_shot = new Shot();
		_shot.x = _holder.x + 10;
		_shot.y = _holder.y;
		AddChild(_shot);
		_shots.Add(_shot);
	
	}
	
	public void HandleMultiTouch(FTouch[] touches)
	{
		if (touches.Length > 0){
			MoveCharacter(touches[0].position);
		}
		if (touches.Length > 1){
			HandleShoot();
		}
	}
	
	private void MoveCharacter(Vector2 position)
	{
		_naner.x = position.x;
		_naner.y = position.y-23.0f;
		_naner2.x = position.x;
		_naner2.y = position.y+23.0f;
		_holder.x = position.x;
		_holder.y = position.y;
		
	}
	
	
	
	
	
}