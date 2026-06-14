using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class NetworkButton : MonoBehaviour, IPointerClickHandler
{
    public enum ButtonType
    {
        Host,
        Client
    }

    [SerializeField] private ButtonType buttonType;

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Host:

                NetworkManager.Singleton.StartHost();
                Debug.Log("Host started");
                break;

            case ButtonType.Client:
                NetworkManager.Singleton.StartClient();
                Debug.Log("Client started");
                break;
        }
    }
}