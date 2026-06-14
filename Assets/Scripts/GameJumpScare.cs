using UnityEngine;

public class GameJumpScare : MonoBehaviour
{
    [SerializeField] private GameObject _ghost;
    [SerializeField] private PlayerController _player;

    private int _count = 0;
    private int _maxCountToJumpScare = 5;

    private void Awake()
    {
        _ghost.SetActive(false);
    }

    private void OnEnable()
    {
        //_player.OnMove += JumpScare1;
    }

    private void OnDisable()
    {
       // _player.OnMove -= JumpScare1;
    }

    private void Update()
    {
           
    }

    private void JumpScare1(Vector3 playerPositon)
    {
        _count++;
        if (_count >= _maxCountToJumpScare)
        {
            _ghost.transform.position = playerPositon;

            _ghost.SetActive(true);
            _count= 0;
            Invoke(nameof(DisableGhost), 1f);
        }
    }
    private void JumpScare2()
    {

    }


    private void JumpScare3()
    {

    }

    private void DisableGhost()
    {
        _ghost.SetActive(false);
    }


}