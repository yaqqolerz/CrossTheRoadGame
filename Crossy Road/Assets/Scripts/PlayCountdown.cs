using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class PlayCountdown : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text tmpText;

    [SerializeField] private AudioSource timerBeep;
    [SerializeField] private AudioSource StartBeep;

    public UnityEvent OnStart;
    public UnityEvent OnEnd;
    

    private void Start()
    {
        OnStart.Invoke();
        var sequence = DOTween.Sequence();
        timerBeep.Play();
        tmpText.transform.localScale = Vector3.zero;
        tmpText.text = "3";

        sequence.Append(tmpText.transform.DOScale(
            Vector3.one, 1).OnComplete(() => {
                tmpText.transform.localScale = Vector3.zero;
                timerBeep.Play();
                tmpText.text = "2";
            }));
        sequence.Append(tmpText.transform.DOScale(
            Vector3.one, 1).OnComplete(() => {
                tmpText.transform.localScale = Vector3.zero;
                timerBeep.Play();
                tmpText.text = "1";
            }));
        sequence.Append(tmpText.transform.DOScale(
            Vector3.one, 1).OnComplete(() => {
                tmpText.transform.localScale = Vector3.zero;
                StartBeep.Play();
                tmpText.text = "GO!";
                OnEnd.Invoke();
            }));
    }
}
