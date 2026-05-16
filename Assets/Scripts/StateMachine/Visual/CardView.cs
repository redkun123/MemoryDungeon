using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardView : MonoBehaviour
{
    public IEnumerator PlayMoveToCenter()
    {
        yield return new WaitForSeconds(0.3f);
    }

    public IEnumerator PlayDiscardAnimation()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
