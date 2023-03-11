using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCrossStreetState : CrossStreetState
{
  public NPCSpawnTrigger npcSpawn;
  Coroutine spawn;
  public bool loopUpdate;

  // Start is called before the first frame update
  void Start()
  {
    loopUpdate = false;
    npcSpawn.SpawnStart();
    crossStreetMachine.player.playerController.speed *= 0.65f;
  }

  public override void OnEnter()
  {
    loopUpdate = true;
    npcSpawn.instancing = true;
    spawn = StartCoroutine(npcSpawn.Spawn(1f));
  }

  // Update is called once per frame
  void Update()
  {
    if (!loopUpdate) return;
    crossStreetMachine.player.FrameUpdate();
    npcSpawn.UpdateNPC();
    if(crossStreetMachine.player.finished)
    { crossStreetMachine.ChangeState<EndCrossStreetState>(); }
  }

  public override void OnExit()
  {
    loopUpdate = false;
  }
}
