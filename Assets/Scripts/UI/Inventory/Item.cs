
using UnityEngine;
using Zenject;


public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _data;
    private InventoryController _controller;
    [Inject]
    public void Construct(InventoryController interfaceController)
    {
        _controller = interfaceController;
    }
    private void ItemPickUp()
    {
        _controller.AddItem(_data);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent<PlayerCollision>(out _))
        {
            ItemPickUp();
        }
    }
}
