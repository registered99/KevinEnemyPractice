using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Gamepage :  MonoBehaviour
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
	
	public void Start()
	{
		FutileParams fparams = new FutileParams(true, true, false, false);
		fparams.AddResolutionLevel(480.0f, 1.0f, 1.0f, "_Scale1");
		
		fparams.origin = new Vector2(0.5f, 0.5f);
		
		Futile.instance.Init (fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/BananaLargeAtlas");
		Futile.atlasManager.LoadAtlas("Atlases/BananaGameAtlas");
		
		_holder = new FContainer();
		
		_background = new FSprite("JungleClearBG");
		_naner = new FSprite("Banana");
		_naner2 = new FSprite("Banana");
		//_monkey = new FSprite("Monkey_0");
		
		Futile.stage.AddChild(_background);
		
		_naner.x = 0.0f;
		_naner.y = -23.0f;
		_naner2.x = 0.0f;
		_naner2.y = 23.0f;
		_naner2.rotation = 180.0f;
		
		_holder.AddChild(_naner2);
		_holder.AddChild(_naner);
		//_holder.AddChild(_monkey);
		_holder.scale = 0.5f;
		Futile.stage.AddChild(_holder);
		
		_shootbutton = new FButton("CloseButton_normal","CloseButton_over");
		_shootbutton.y = 0.0f;
		_shootbutton.x = Futile.screen.halfWidth - 12.5f;
		
		_shootbutton.scale = 0.5f;
		
		Futile.stage.AddChild(_shootbutton);
		
		
		_upbutton = new FButton("CloseButton_normal","CloseButton_over");
		_downbutton = new FButton("CloseButton_normal","CloseButton_over");
		_leftbutton = new FButton("CloseButton_normal","CloseButton_over");
		_rightbutton = new FButton("CloseButton_normal","CloseButton_over");
		
		_upbutton.x = 0.0f;
		_upbutton.y = 50.0f;
		_downbutton.x = 0.0f;
		_downbutton.y = -50.0f;
		_rightbutton.x = 50.0f;
		_rightbutton.y = 0.0f;
		_leftbutton.x = -50.0f;
		_leftbutton.y = 0.0f;
		
		_movepad = new FContainer();
		
		_movepad.AddChild(_upbutton);
		_movepad.AddChild(_downbutton);
		_movepad.AddChild(_rightbutton);
		_movepad.AddChild(_leftbutton);
		
		_movepad.x = -Futile.screen.halfWidth + 37;
		_movepad.y = 0.0f;
		_movepad.scale = 0.5f;
		
		Futile.stage.AddChild(_movepad);
		
		
		
		_shootbutton.SignalRelease += HandleShoot;
		_upbutton.SignalPress += HandleUp;
		_downbutton.SignalPress += HandleDown;
		_leftbutton.SignalPress += HandleLeft;
		_rightbutton.SignalPress += HandleRight;
		
		_upbutton.SignalRelease += HandleUpRelease;
		_downbutton.SignalRelease += HandleDownRelease;
		_rightbutton.SignalRelease += HandleRightRelease;
		_leftbutton.SignalRelease += HandleLeftRelease;
	
		
	}

	
	public void Update()
	{
		_holder.y += speedY;
		_holder.x += speedX;
		frameCount += 1;
		if(frameCount%60 == 0)
		{
			//Debug.Log ("Should spawn");
			_enemy = new Enemy();
			Futile.stage.AddChild(_enemy);
			_enemies.Add(_enemy);
		}
			
			
		for(int b = _shots.Count - 1; b>=0; b--)
		{
			_shots[b].Update();
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
	
	public void HandleShoot(FButton button)
	{
		_shot = new Shot();
		_shot.x = _holder.x + 10;
		_shot.y = _holder.y;
		Futile.stage.AddChild(_shot);
		_shots.Add(_shot);
	
	}
	
	public void HandleUp(FButton button)
	{
		speedY = 1f;
	}
	
	public void HandleDown(FButton button)
	{
		speedY = -1f;
	}
	
	public void HandleRight(FButton button)
	{
		speedX = 1f;
	}
	
	public void HandleLeft(FButton button)
	{
		speedX = -1f;
	}
	
	
	
	public void HandleUpRelease(FButton button)
	{
		speedY = 0f;
	}
	
	public void HandleDownRelease(FButton button)
	{
		speedY = 0f;
	}
	
	public void HandleRightRelease(FButton button)
	{
		speedX = 0f;
	}
	
	public void HandleLeftRelease(FButton button)
	{
		speedX = 0f;
	}
	
	
}