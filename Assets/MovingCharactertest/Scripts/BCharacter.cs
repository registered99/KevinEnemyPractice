using System;
using UnityEngine;

public class BCharacter : FSprite
{
	public BCharacter () : base("theTriangles")
	{
		ListenForUpdate (HandleUpdate);
	}
	private void HandleUpdate(){
	}

}

