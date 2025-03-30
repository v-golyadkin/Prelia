using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab, _healthPrefab, _staminaPrefab;

    public void DropItem()
    {
        int randomNumber = Random.Range(1 , 5);

        if(randomNumber == 1)
        {
            Instantiate(_healthPrefab, transform.position, Quaternion.identity);
        }

        if(randomNumber == 2)
        {
            Instantiate(_staminaPrefab, transform.position, Quaternion.identity);
        }

        if(randomNumber >= 3)
        { 
            int randomAmountOfGold = Random.Range(1, 4);

            for(int i = 0; i < randomAmountOfGold; i++)
            {
                Instantiate(_coinPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
