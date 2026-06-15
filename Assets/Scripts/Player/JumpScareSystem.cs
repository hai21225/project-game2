using UnityEngine;

public class JumpScareSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _ghostPrefabs;
    [SerializeField] private FearSystem _fearSystem;

    private bool _isJumpScareActive = false;
    private float _jumpScareDuration = 3f;

    // Đã sửa lỗi 1: Đưa timer ra ngoài để không bị reset mỗi frame
    private float _timer = 0f;

    // Đã sửa lỗi 2: Biến để lưu con ma được sinh ra trên Map (bản Clone)
    private GameObject _spawnedGhost;

    private void OnEnable()
    {
        _fearSystem.OnFearThresholdReached += RandomJumpScare;
    }

    private void OnDisable()
    {
        if (_fearSystem != null)
            _fearSystem.OnFearThresholdReached -= RandomJumpScare;
    }

    private void Update()
    {
        if (_isJumpScareActive)
        {
            _timer += Time.deltaTime; // Tăng thời gian mỗi frame

            if (_timer >= _jumpScareDuration)
            {
                // Chỉ xóa con ma Clone đang xuất hiện trên Map, không xóa Prefab gốc
                if (_spawnedGhost != null)
                {
                    Destroy(_spawnedGhost);
                }

                _isJumpScareActive = false;
                _timer = 0f; // Reset bộ đếm về 0
            }
        }
    }

    private void RandomJumpScare()
    {
        // Nếu đang có một Jumpscare chạy rồi thì bỏ qua không kích hoạt thêm trùng nhau
        if (_isJumpScareActive) return;

        System.Action[] scareList = { JumpScare1, JumpScare2, JumpScare3 };
        int randomIndex = Random.Range(0, scareList.Length);
        scareList[randomIndex].Invoke();
    }

    private void JumpScare1()
    {
        // Lưu bản sao vào biến _spawnedGhost để quản lý
        _spawnedGhost = Instantiate(_ghostPrefabs[0], transform.position + new Vector3(0, 0, 2), Quaternion.identity);

        _timer = 0f; // Đảm bảo timer bắt đầu từ 0
        _isJumpScareActive = true;
    }

    private void JumpScare2()
    {
        _spawnedGhost = Instantiate(_ghostPrefabs[1], transform.position + new Vector3(0, 0, 2), Quaternion.identity);

        _timer = 0f;
        _isJumpScareActive = true;
    }

    private void JumpScare3()
    {
        _spawnedGhost = Instantiate(_ghostPrefabs[2], transform.position + new Vector3(0, 0, 2), Quaternion.identity);

        _timer = 0f;
        _isJumpScareActive = true;
    }
}