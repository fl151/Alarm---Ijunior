using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Home : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;
    [SerializeField] private UnityEvent _comedIn;
    [SerializeField] private UnityEvent _comedOut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _comedIn.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _comedOut.Invoke();
        }
    }
}
