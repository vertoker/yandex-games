using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Game
{
    public class TrashBag : MonoBehaviour
    {
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Item>() != null)
            {
                ItemSpawner.DeleteItem(collision.transform.gameObject);
            }
        }
    }
}