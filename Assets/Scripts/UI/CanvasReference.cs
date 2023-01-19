using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class CanvasReference : MonoBehaviour, ICanvasReference
    {
        [Header("CanvasGame")]
        [SerializeField] GameObject _canvasGame;
        [SerializeField] GameObject _pointer;
        [SerializeField] GameObject _write;
        [SerializeField] GameObject _read;
        [SerializeField] Button _previous;
        [SerializeField] Button _next;
        [SerializeField] GameObject _ePrompt;
        [SerializeField] GameObject _seed;
        [SerializeField] GameObject _deadMenu;

        public GameObject GetCanvasGame()
        {
            return _canvasGame;
        }
        public GameObject GetPointer()
        {
            return _pointer;
        }
        public GameObject GetWrite()
        {
            return _write;
        }
        public GameObject GetRead()
        {
            return _read;
        }
        public Button GetPrevious()
        {
            return _previous;
        } 
        public Button GetNext()
        {
            return _next;
        }

        public GameObject GetEPrompt()
        {
            return _ePrompt;
        }
        public GameObject GetSeed()
        {
            return _seed;
        }
        public GameObject GetDeadMenu()
        {
            return _deadMenu;
        }


    }
}
