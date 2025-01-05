using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private void Start()
    {
        TileGrid.Instance.OnCaptured += TileGrid_OnCaptured;
        TileGrid.Instance.OnIncorrect += TileGrid_OnIncorrect;
    }

    private void TileGrid_OnIncorrect(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.incorrect, Camera.main.transform.position);
    }

    private void TileGrid_OnCaptured(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.capture, Camera.main.transform.position);
    }

    private void PlaySound(AudioClip audioclip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioclip, position, volume);
    }
}
