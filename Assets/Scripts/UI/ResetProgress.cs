using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Scripts.UI
{
    public class ResetProgress : MonoBehaviour
    {
        public void ResetConfirm()
        {
            SaveSystem.SaveSystem.ResetAll();
            SceneManager.LoadScene(0);
        }
    }
}