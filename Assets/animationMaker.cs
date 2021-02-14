using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
namespace ProjectRoboDK
{
    public class animationMaker : MonoBehaviour
    {
        // Start is called before the first frame update
        RoboDK RDK = null;
        RoboDK.Item ROBOT;
        RoboDK.Item prog;
        public TextAsset motion;
        public String pathToAssetFolder;
        public String nameOfAnimation;
        void Start()
        {
            RDK = new RoboDK();
            ROBOT = RDK.getItem("KUKA KR 10 R1100 sixx");
            String error = "";
            Mat jlist = new Mat(10, 10);
            prog = RDK.getItem("Prog1");
            prog.InstructionListJoints(out error, out jlist, 1, 5, pathToAssetFolder + "/" + nameOfAnimation + ".csv");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}