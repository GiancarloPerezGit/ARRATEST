using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SwitchController : MonoBehaviour
{
    Animator animator;
    Toggle toggle;
    void Awake()
    {
        animator = GetComponent<Animator>();
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ButtonAnimate);
    }
    void ButtonAnimate(bool on)
    {
        animator.SetBool("active", on);
    }
}
