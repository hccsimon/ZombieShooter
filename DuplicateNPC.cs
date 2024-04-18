using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateNPC : MonoBehaviour
{
    int totalNPC = 2;
    private float ftime;
    int NPC_num = 0;

    void Update()
    {
        float timing = Random.Range(0, 100);

            if (timing <.1f && NPC_num < totalNPC)
            {
                GameObject Enemy = Instantiate(Resources.Load("Zombie", typeof(GameObject))) as GameObject;
                Enemy.gameObject.tag = "Menemy";
                Enemy.transform.position = new Vector3(-30 ,1.5f ,-40);
                NPC_num++;
            }
    }

    public void MSpawnMore()
    {
        NPC_num--;
    }
}
