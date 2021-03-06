using Core.EventSystems;
using Core.IoC;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Ui
{
    [RequireComponent(typeof(Button))]
    internal class ButtonWidget : MonoBehaviour
    {
        [SerializeField] private string _onClickEvent;

        [Dependency] private readonly IGameEventManager _gameEventManager;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            Injector.ResolveDependencies(this);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            if(string.IsNullOrEmpty(_onClickEvent)) return;
            
            _gameEventManager.Broadcast(_onClickEvent);
        }
    }
}