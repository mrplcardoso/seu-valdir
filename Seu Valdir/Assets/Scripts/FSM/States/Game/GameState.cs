using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : State
{
	protected GameMachine gameMachine
	{ get { return (GameMachine)stateMachine; } }
}