using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpells : MonoBehaviour
{
    public static Canvas spellSelectionUI;
    public GameObject boss1;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        spellSelectionUI = GameObject.FindGameObjectWithTag("SpellUI").GetComponent<Canvas>();
    }

    public void CreateSpell1()
    {
        Instantiate(boss1, new Vector3(0, 3, 0), Quaternion.identity);
        Instantiate(player, new Vector3(0, -3.5f, 0), Quaternion.identity);
        spellSelectionUI.gameObject.SetActive(false);
    }
}
