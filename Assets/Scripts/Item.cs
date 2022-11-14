using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Linq;

namespace Scripts
{
    public class Item : MonoBehaviour
    {
        private GameObject obj1, obj2, obj3;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            float[] contactDistances = new float[collision.contacts.Length];
            for (int i = 0; i < collision.contacts.Length; i++)
                contactDistances[i] = Vector2.Distance(collision.contacts[i].rigidbody.transform.position, transform.position);
            contactDistances.OrderBy(p => p);

            if (contactDistances.Length >= 3)
            {
                var pos = (collision.contacts[0].point + collision.contacts[1].point + collision.contacts[2].point) / 3f;
                obj1 = collision.contacts[0].rigidbody.gameObject;
                obj2 = collision.contacts[1].rigidbody.gameObject;
                obj3 = collision.contacts[2].rigidbody.gameObject;
                ItemSpawner.ExecuteRecipe(obj1.name, obj2.name, obj3.name, pos, obj1, obj2, obj3);
            }
            else if (contactDistances.Length == 2)
            {
                var pos = (collision.contacts[0].point + collision.contacts[1].point) / 2f;
                obj1 = collision.contacts[0].rigidbody.gameObject;
                obj2 = collision.contacts[1].rigidbody.gameObject;
                ItemSpawner.ExecuteRecipe(obj1.name, obj2.name, string.Empty, pos, obj1, obj2, null);
            }
        }
    }
}