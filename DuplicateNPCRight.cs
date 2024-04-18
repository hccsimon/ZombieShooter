using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateNPCRight : MonoBehaviour
{
    int totalNPC = 2;
    private float ftime;
    int NPC_num = 0;

    void Update()
    {
        float timing = Random.Range(0, 100);

            if (timing <.1f && NPC_num < totalNPC)
            {
                GameObject Enemy = Instantiate(Resources.Load("ZombieRight", typeof(GameObject))) as GameObject;
                Enemy.gameObject.tag = "Renemy";
                Enemy.transform.position = new Vector3(-42 ,1.5f ,-40);
                NPC_num++;
            }
    }

    public void RSpawnMore()
    {
        NPC_num--;
    }
}
