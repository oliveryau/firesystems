using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ithas
{
    public class Indicator : MonoBehaviour
    {
        public GameObject indicatorBubble;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                indicatorBubble.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                indicatorBubble.SetActive(false);
            }
        }
    }
}