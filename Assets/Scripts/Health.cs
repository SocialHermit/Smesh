using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour {

    public Text text;
    private float percent;

	// Use this for initialization
	void Start () {
        updatePercent();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void updatePercent()
    {
        text.text = percent.ToString() + "%";
    }

    private void TakeDamage(float damage)
    {
        percent += damage;
        updatePercent();
    }

    private void HealDamage(float heal)
    {
        percent -= heal;
        updatePercent();
    }
}
