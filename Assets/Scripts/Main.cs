using UnityEngine;
using System;

public class Main : MonoBehaviour
{
	public BCharacter character;
	public Gamepage gamePage;
	public static Main instance;
	// Use this for initialization
	void Start () {
		instance = this;
		FutileParams fparams = new FutileParams(true,true,false,false);
		fparams.AddResolutionLevel(480.0f,	1.0f,	1.0f,	"_Scale1");
		fparams.origin = new Vector2(0.5f, 0.5f);
		Futile.instance.Init(fparams);
		
		Futile.atlasManager.LoadAtlas("Atlases/BananaLargeAtlas");
		Futile.atlasManager.LoadAtlas("Atlases/BananaGameAtlas");
		
		gamePage = new Gamepage();
		Futile.stage.AddChild(gamePage);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
