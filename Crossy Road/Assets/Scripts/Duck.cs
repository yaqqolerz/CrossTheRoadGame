using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class Duck : MonoBehaviour
{
    [SerializeField, Range(0,1)] private float moveDuration;
    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;
    [SerializeField] private AudioClip[] moveSounds;
    [SerializeField] private AudioSource MoveSounds;
    [SerializeField] private AudioClip[] carmoveSounds;
    [SerializeField] private AudioSource carMoveSounds;
    [SerializeField] private AudioSource EagleSound;
    [SerializeField] private AudioSource watersplashSound;
    [SerializeField] private AudioSource coinSound;
    [SerializeField] private AudioSource splatSound;


    [SerializeField] private bool OnLog = false;

    public UnityEvent<Vector3> OnJumpEnd;
    public UnityEvent<Vector3> OnMovingLog;
    public UnityEvent<int> OnGetCoin;

    public UnityEvent OnDead;
    public UnityEvent OnCarCollision;

    private bool isNotMoveable = false;

    private Vector3 currentPos;
    void Update()
    {
        if (isNotMoveable)
        {
            return;
        }
        if (DOTween.IsTweening(transform))
        {
            return;
        }
        Vector3 direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }
        if(direction == Vector3.zero)
        {
            return;
        }
        Move(direction);
    }

    private void getCurrentPos()
    {
        currentPos = this.transform.position;
        currentPos.x = Mathf.Round(currentPos.x);
        currentPos.y = Mathf.Round(currentPos.y);
        currentPos.z = Mathf.Round(currentPos.z);
    }

    private void Move(Vector3 direction)
    {
        var targetPos = transform.position + direction;

        if(targetPos.x < leftMoveLimit || 
            targetPos.x > rightMoveLimit || 
            targetPos.z < backMoveLimit ||
            Tree.AllPositions.Contains(targetPos))
        {
            targetPos = transform.position;
        }
        transform.DOJump(targetPos, 0.5f, 1, moveDuration).onComplete = BroadCastPoisitionOnJumpEnd;
        MoveSounds.clip = moveSounds[Random.Range(0, moveSounds.Length)];
        MoveSounds.Play();
        transform.forward = direction;
    }

    public void setNotMoveable(bool value)
    {
        isNotMoveable = value;
    }

    public void UpdateMoveLimit(int horizontalSize, int backLimit)
    {
        leftMoveLimit = -horizontalSize / 2;
        rightMoveLimit = horizontalSize / 2;
        backMoveLimit = backLimit + 2;
    }

    private void BroadCastPoisitionOnJumpEnd()
    {
        var pos = this.transform.position;
        this.transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
        OnJumpEnd.Invoke(transform.position);
    }

    private void BroadCastPoisitionOnMovingLog()
    {
        OnMovingLog.Invoke(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (isNotMoveable == true)
            {
                return;
            }
            transform.DOScale(new Vector3(1.1f,0.1f, 1.1f), 0.2f);
            isNotMoveable = true;
            OnCarCollision.Invoke();
            carMoveSounds.clip = carmoveSounds[Random.Range(0, carmoveSounds.Length)];
            carMoveSounds.Play();
            splatSound.Play();
            Invoke("Dead", 2);
        }
        else if (other.CompareTag("Coin")){
            var coin = other.GetComponent<Coin>();
            OnGetCoin.Invoke(coin.Value);
            coinSound.Play();
            coin.Collected();
        }
        else if (other.CompareTag("Eagle"))
        {
            if(this.transform != other.transform)
            {
                this.transform.SetParent(other.transform);
                EagleSound.Play();
                Invoke("Dead", 2);
            }
        }
        else if (other.CompareTag("WoodLog"))
        {
            OnLog = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("WoodLog"))
        {
            /*Debug.Log("OnLog");*/
            if (this.transform.position.x > leftMoveLimit || this.transform.position.x < rightMoveLimit)
            {
                this.transform.position = new Vector3(other.transform.position.x, this.transform.position.y, this.transform.position.z);
                if(this.transform.position.x < leftMoveLimit || this.transform.position.x > rightMoveLimit)
                {
                    OnLog = false;
                    isNotMoveable = true;
                    other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y - 1.1f * Time.deltaTime, other.transform.position.z);
                }
                else
                {
                    BroadCastPoisitionOnMovingLog();
                }
            }
        }
        else if (other.CompareTag("Water"))
        {
            if(OnLog != true)
            {
                watersplashSound.Play();
                isNotMoveable = true;
                transform.DOLocalMoveY(-1, 1);
                Invoke("Dead", 2);
            }
        }
        else if (other.CompareTag("Tree"))
        {
            var pos = this.transform.position;
            getCurrentPos();
            if (currentPos == other.transform.position)
            {
                this.transform.position = new Vector3(pos.x, pos.y, pos.z - 1);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WoodLog"))
        {
            OnLog = false;      
        }
    }

    private void Dead()
    {
        OnDead.Invoke();
    }
}
