using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public interface ICanvasReference
    {
        public GameObject GetCanvasGame();
        public GameObject GetPointer();
        public GameObject GetWrite();
        public GameObject GetRead();
        public Button GetPrevious();
        public Button GetNext();
        public GameObject GetEPrompt();
        public GameObject GetSeed();
        public GameObject GetDeadMenu();
    }
}
