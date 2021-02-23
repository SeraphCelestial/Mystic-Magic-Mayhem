using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Behavior : MonoBehaviour
{
    public int boss1Health = 1000;

    void Update()
    {
        if(boss1Health <= 0)
        {
            CreateSpells.spellSelectionUI.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
