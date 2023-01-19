using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Message : MonoBehaviour
    {
        public string message;
        public List<string> messages = new List<string>();
        public int id;
        private void Start()
        {
            messages.Add(message);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Message"))
            {
                Message other = collision.gameObject.GetComponent<Message>();

                if (other.GetInstanceID() < transform.GetInstanceID())
                {
                    foreach (string msg in other.messages)
                    {
                        messages.Add(msg);
                    }
                    Destroy(collision.gameObject);
                }
                else Destroy(gameObject);
            }
        }
    }
