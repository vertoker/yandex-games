using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Linq;

namespace Scripts
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private bool contact = false;
        [SerializeField] private bool active = false;
        private Coroutine coroutine;

        private void OnDisable()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }
        public void Activate()
        {
            active = true;
            coroutine = StartCoroutine(Counter());
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            contact = true;
            if (!active)
                return;
            active = false;

            //Debug.Log("////////////////////////////////////////////////");
            ContactData[] contactDistances = new ContactData[collision.contacts.Length];
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                contactDistances[i] = new ContactData() 
                {
                    distance = Vector2.Distance(collision.contacts[i].rigidbody.transform.position, transform.position),
                    obj = collision.contacts[i].rigidbody.gameObject
                };
            }
            contactDistances.OrderBy(p => p.distance);

            if (contactDistances.Length >= 2)
            {
                //Debug.Log(gameObject.name);
                //Debug.Log(collision.contacts[0].collider.name);
                //Debug.Log(collision.contacts[1].collider.name);
                var pos = (gameObject.transform.position + contactDistances[0].obj.transform.position + contactDistances[1].obj.transform.position) / 3f;
                ItemSpawner.ExecuteRecipe(gameObject.name, contactDistances[0].obj.name, contactDistances[1].obj.name, pos, gameObject, contactDistances[0].obj, contactDistances[1].obj);
            }
            else if (contactDistances.Length == 1)
            {
                //Debug.Log(gameObject.name);
                //Debug.Log(collision.contacts[0].collider.name);
                var pos = (gameObject.transform.position + contactDistances[0].obj.transform.position) / 2f;
                ItemSpawner.ExecuteRecipe(gameObject.name, contactDistances[0].obj.name, string.Empty, pos, gameObject, contactDistances[0].obj, null);
            }
            if (gameObject.activeSelf)
                coroutine = StartCoroutine(Counter());
        }

        IEnumerator Counter()
        {
            yield return new WaitForSeconds(0.25f);
            if (contact)
                active = false;
            contact = false;
        }

        struct ContactData
        {
            public float distance;
            public GameObject obj;
        }
    }
}