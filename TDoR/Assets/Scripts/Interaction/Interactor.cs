using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private PromptUI _promptUI;
    private PlayerScript _playerScript;
    private UpgradeScript upgradeScript;

    [Header("Game Objects")]
    [SerializeField] private GameObject gameController;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Weapon;

    private readonly Collider[] _colliders = new Collider[2];
    [SerializeField] private int _numFound;

    WaveText _waveText;

    public void Start()
    {
        _waveText = GameObject.Find("GameController").GetComponent<WaveText>();
        _playerScript = Player.GetComponent<PlayerScript>();
        upgradeScript = gameController.GetComponent<UpgradeScript>();
    }

    private IInteractable _interactable;
    public void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();

            if (_interactable != null)
            {
                if (!_promptUI.isDisplayed) _promptUI.SetUp(_interactable.InteractionPrompt);

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    _interactable.Interact(this);
                    upgradeScript.Upgrade();
                    upgradePanel.SetActive(true);
                    _playerScript.Heal();
                    _waveText.comeBack.enabled = false;


                    WinLose.canInteract = false;
                    WinLose.hasInteracted = true;

                    Time.timeScale = 0;

                    Weapon.GetComponent<Weapon>().enabled = false;
                    Player.GetComponent<FirstPersonController>().enabled = false;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
        else
        {
            if (_interactable != null) _interactable = null;
            if (_promptUI.isDisplayed) _promptUI.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }


}
