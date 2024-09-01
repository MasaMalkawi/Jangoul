using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public char let;
    public ScoreTyper st;
    private void OnTriggerEnter(Collider other)
    {
        st.TypeLetter(let);
    }
}
