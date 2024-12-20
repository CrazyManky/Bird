using Project.Screpts.ShopItems;
using UnityEngine;

public class CharacterInstance : MonoBehaviour, IService
{
    [SerializeField] private BirdController _birdController;
    [SerializeField] private CharactersShopData _shopItemData;

    private BirdController _birdControllerInstance;

    public BirdController BirdControllerInstance => _birdControllerInstance;

    public void InstanceCharacter()
    {
        ResetCharacter();
        _birdControllerInstance = Instantiate(_birdController);
        _birdControllerInstance.SetSprite(_shopItemData.GetActiveSprite());
        _birdControllerInstance.transform.position = transform.position;
        _birdController.Pause();
    }

    private void ResetCharacter()
    {
        if (_birdControllerInstance != null)
        {
            Destroy(_birdControllerInstance.gameObject);
        }
    }
}