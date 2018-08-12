using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DeleteAfterReading.Model;

namespace DeleteAfterReading
{
    public class EmailView : MonoBehaviour {

        public TextMeshPro to;
        public TextMeshPro from;
        public TextMeshPro body;



        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        public void LoadExternalDisk(Disk d)
        {
            to.text = "To: " + d.to;
            from.text = "From: "+d.from;
            body.text = d.text;
        }
    }
}