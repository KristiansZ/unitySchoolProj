using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatManager : MonoBehaviour
{
    private EnemyController enemyController;

    public float initialMaxHealth = 20f;
    public float initialMoveSpeed = 3f;
    public float initialAttackSpeed = 10f;
    public float initialDamage = 15f;

    void Start()
    {
        StartCoroutine(IncreaseRandomStat());
    }

    IEnumerator IncreaseRandomStat()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);

            int randomStat = Random.Range(0, 4);

            switch (randomStat)
            {
                case 0:
                    initialMaxHealth += 10f;
                    break;
                case 1:
                    initialMoveSpeed += 1f;
                    break;
                case 2:
                    initialAttackSpeed += 0.5f;
                    break;
                case 3:
                    initialDamage += 5f;
                    break;
                default:
                    break;
            }
        }
    }
}