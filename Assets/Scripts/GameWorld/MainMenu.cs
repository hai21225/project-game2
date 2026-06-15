using Unity.Netcode;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnConnected;
    }

    private void OnConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            menuCanvas.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
            NetworkManager.Singleton.OnClientConnectedCallback -= OnConnected;
    }
}