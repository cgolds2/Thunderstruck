using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossScript : EnemyScript
{
    // Start is called before the first frame update
    void Start()
    {
        base.EnemyStart();
        Health = 10;
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyUpdate();
    }
}
