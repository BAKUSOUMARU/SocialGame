using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UiChange : MonoBehaviour
{
   public void ChangeUI(GameObject nextui)
   {
      this.gameObject.SetActive(false);
      nextui.SetActive(true);
   }
}
