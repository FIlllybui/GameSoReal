using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : MonoBehaviour
{
   private Rigidbody _rb;
   private bool _hasCued;

   private void Awake()
   {
      _rb = GetComponent<Rigidbody>();
   }

   private void Cue()
   {
      _hasCued = true;
   }

   private void Update()
   {
      if (_hasCued && _rb.velocity.magnitude !=0)
      {
         _rb.velocity.Normalize();
      }
   }
}
