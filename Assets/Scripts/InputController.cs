using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class InputController:MonoBehaviour
    {
        //是否触发tab事件
        public bool CanTab { get; set; }
        
        //点击事件
        public event Action OnTab = null;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (OnTab != null)
                    OnTab();
            }
        }

    }
}
