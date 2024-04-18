using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateNPCLeft : MonoBehaviour
{
    int totalNPC = 2;
    private float ftime;
    int NPC_num = 0;

    void Update()
    {
        float timing = Random.Range(0, 100);

            if (timing <.1f && NPC_num < totalNPC)
            {
                GameObject Enemy = Instantiate(Resources.Load("ZombieLeft", typeof(GameObject))) as GameObject;
                Enemy.gameObject.tag = "Lenemy";
                Enemy.transform.position = new Vector3(-21 ,1.5f ,-40);
                NPC_num++;
            }
    }

    public void LSpawnMore()
    {
        NPC_num--;
    }
}
