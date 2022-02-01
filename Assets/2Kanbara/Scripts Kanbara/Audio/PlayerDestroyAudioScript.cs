using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerDestroyAudioScript : MonoBehaviour
{
    async void Start()
    {
        await Task.Delay(1500);
        Destroy(this.gameObject);
    }
}
