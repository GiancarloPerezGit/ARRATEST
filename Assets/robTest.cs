using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectRoboDK
{
    public class robTest : MonoBehaviour
    {
        RoboDK RDK = null;
        RoboDK.Item ROBOT = null;
        RoboDK.Item target = null;
        RoboDK.Item target2 = null;
        RoboDK.Item prevTarget = null;
        RoboDK.Item[] targets = null;
        RoboDK.Item FRAME = null;
        RoboDK.Item prog = null;
        // Start is called before the first frame update
        void Start()
        {
            RDK = new RoboDK();
            ROBOT = RDK.getItem("KUKA KR 10 R1100 sixx");
            FRAME = RDK.getItem("KUKA KR 10 R1100 sixx Base");
            prog = RDK.getItem("Prog1");
            prevTarget = RDK.getItem("Target 1");
            target = RDK.getItem("Target 2");
            prog.MoveJ(prevTarget);
            prog.MoveJ(target);
            prevTarget = RDK.getItem("Target 3");
            target = RDK.getItem("Target 4");
            prog = RDK.AddProgram("T");
            prog.MoveC(prevTarget, target);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}