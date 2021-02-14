using System.Collections;
using UnityEngine;
using System;

namespace ProjectRoboDK
{
    public class KR10OnlineW : MonoBehaviour
    {
        RoboDK RDK = null;
        RoboDK RDK2 = null;
        RoboDK.Item ROBOT = null;
        RoboDK.Item ROBOT2 = null;
        RoboDK.Item target = null;
        RoboDK.Item robFrame;
        RoboDK.Item toolFrame;
        RoboDK.Item robFrame2;
        RoboDK.Item toolFrame2;

        // List of the 6 axes.
        [SerializeField] Transform Axis1;
        [SerializeField] Transform Axis2;
        [SerializeField] Transform Axis3;
        [SerializeField] Transform Axis4;
        [SerializeField] Transform Axis5;
        [SerializeField] Transform Axis6;

        // List of the 6 pairs of buttons.
        [SerializeField] PushButton but;
        [SerializeField] PushButton but2;
        [SerializeField] PushButton but3;
        [SerializeField] PushButton but4;
        [SerializeField] PushButton but5;
        [SerializeField] PushButton but6;
        [SerializeField] PushButton but7;
        [SerializeField] PushButton but8;
        [SerializeField] PushButton but9;
        [SerializeField] PushButton but10;
        [SerializeField] PushButton but11;
        [SerializeField] PushButton but12;

        private ArrayList actions = new ArrayList();
        private String prevCommand;
        //How much to move the end of the robot per button press. In mm.
        private double movstp = 5.0;
        //Default joints for the robot
        private double[] hom = new double[6] { 0, -90, 90, 0, 0, 0 };
        // Start is called before the first frame update
        void Start()
        {
            RDK = new RoboDK("localhost", true);
            //RDK.HideRoboDK();
            //RDK2 = new RoboDK("10.101.48.141", false, 20500);
            RDK.Render(false);
            //RDK2.Render(false);
            ROBOT = RDK.getItem("KUKA KR 10 R1100 sixx");
            toolFrame = RDK.getItem("Tool 1");
            robFrame = RDK.getItem("Tool 2");
            //ROBOT2 = RDK2.getItem("KUKA KR 10 R1100 sixx");
            //toolFrame2 = RDK2.getItem("Tool 1");
            //robFrame2 = RDK2.getItem("Tool 2");
            ROBOT.setPoseTool(robFrame);
            //ROBOT2.setPoseTool(robFrame2);
            RDK.setRunMode(6);
            //target = RDK.AddTarget("Tar1");
            //double[] tes = new double[6] {300,-500,0,0,0,0 };
            //target.setPose(Mat.FromTxyzRxyz(tes));
        }

        // Update is called once per frame
        void Update()
        {
            if (but5.isPressed() && !ROBOT.Busy())
            {
                ROBOT.setPoseTool(robFrame);
                double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };
                move_xyzwpr[0] = movstp;
                Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);
                Mat robot_pose = ROBOT.Pose();
                double[] olJoints = ROBOT.Joints();
                Mat new_robot_pose;
                new_robot_pose = robot_pose * movement_pose;
                //ROBOT.setPose(new_robot_pose);
                ROBOT.MoveJ(new_robot_pose, false);

                //while (ROBOT.Busy())
                //{

                //}
                //double[] joints = ROBOT.Joints();
                //UnityEngine.Debug.Log("SteamVR Button pressed!");
                //UnityEngine.Debug.Log(joints[0]);
                //UnityEngine.Debug.Log(joints[1]);
                //UnityEngine.Debug.Log(joints[2]);
                //UnityEngine.Debug.Log(joints[3]);
                //UnityEngine.Debug.Log(joints[4]);
                //UnityEngine.Debug.Log(joints[5]);
                //ROBOT.MoveJ(new_robot_pose, false);

                //A1.Rotate(0, (float)olJoints[0] - (float)joints[0], 0);
                //A2.Rotate((float)joints[1] - (float)olJoints[1], 0, 0);
                //A3.Rotate((float)joints[2] - (float)olJoints[2], 0, 0);
                //A4.RotateAround(A4.position, A4.forward, (float)olJoints[3] - (float)joints[3]);
                //A5.Rotate((float)joints[4] - (float)olJoints[4], 0, 0);
                //A6.Rotate(0, 0, (float)olJoints[5] - (float)joints[5]);

                //A1.rotation = Quaternion.Euler(0, (float)joints[0], 0);
                //A2.rotation = Quaternion.Euler(0, 0, ((float)joints[1])*-1f);
                //A3.rotation = Quaternion.Euler(0, 0, (((float)joints[2])) * -1f);
                ////A4.rotation = Quaternion.Euler(0, (float)joints[3], 90f);
                //A5.rotation = Quaternion.Euler(0, 0, (float)joints[4]);
                //A6.rotation = Quaternion.Euler((float)joints[5], 0, 0);

                ////A1.Rotate(0, (float)joints[0] - A1.rotation.y, 0);
                ////A2.Rotate(0, 0, (float)joints[1] - (-A2.rotation.z));
                ////A3.Rotate(0, 0, (float)joints[2] - (-(A3.rotation.z + 90.0f)));
                //A4.Rotate(0, (float)joints[3] - A4.rotation.y, 0);
                ////A5.Rotate(0, 0, (float)joints[4] - (-A5.rotation.z));
                ////A6.Rotate((float)joints[5] - A6.rotation.x, 0, 0);
            }
            else if (but6.isPressed() && !ROBOT.Busy())
            {
                ROBOT.setPoseTool(robFrame);
                double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };
                move_xyzwpr[0] = -movstp;
                Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);
                Mat robot_pose = ROBOT.Pose();
                double[] olJoints = ROBOT.Joints();
                Mat new_robot_pose;
                new_robot_pose = robot_pose * movement_pose;
                //ROBOT.setPose(new_robot_pose);
                ROBOT.MoveJ(new_robot_pose, false);
                //while (ROBOT.Busy())
                //{

                //}
                //double[] joints = ROBOT.Joints();
                //UnityEngine.Debug.Log("SteamVR Button pressed!");
                //UnityEngine.Debug.Log(joints[0]);
                //UnityEngine.Debug.Log(joints[1]);
                //UnityEngine.Debug.Log(joints[2]);
                //UnityEngine.Debug.Log(joints[3]);
                //UnityEngine.Debug.Log(joints[4]);
                //UnityEngine.Debug.Log(joints[5]);

                //A1.Rotate(0, (float)olJoints[0] - (float)joints[0], 0);
                //A2.Rotate((float)joints[1] - (float)olJoints[1], 0, 0);
                //A3.Rotate((float)joints[2] - (float)olJoints[2], 0, 0);
                //A4.RotateAround(A4.position, A4.forward, (float)olJoints[3] - (float)joints[3]);
                //A5.Rotate((float)joints[4] - (float)olJoints[4], 0, 0);
                //A6.Rotate(0, 0, (float)olJoints[5] - (float)joints[5]);
            }
            else if (but7.isPressed() && !ROBOT.Busy())
            {
                ROBOT.setPoseTool(robFrame);
                double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };
                move_xyzwpr[1] = movstp;
                Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);
                Mat robot_pose = ROBOT.Pose();
                double[] olJoints = ROBOT.Joints();
                Mat new_robot_pose;
                new_robot_pose = robot_pose * movement_pose;
                //ROBOT.setPose(new_robot_pose);
                ROBOT.MoveJ(new_robot_pose, false);
                //while (ROBOT.Busy())
                //{

                //}
                //double[] joints = ROBOT.Joints();
                //UnityEngine.Debug.Log("SteamVR Button pressed!");
                //UnityEngine.Debug.Log(joints[0]);
                //UnityEngine.Debug.Log(joints[1]);
                //UnityEngine.Debug.Log(joints[2]);
                //UnityEngine.Debug.Log(joints[3]);
                //UnityEngine.Debug.Log(joints[4]);
                //UnityEngine.Debug.Log(joints[5]);

                //A1.Rotate(0, (float)olJoints[0] - (float)joints[0], 0);
                //A2.Rotate((float)joints[1] - (float)olJoints[1], 0, 0);
                //A3.Rotate((float)joints[2] - (float)olJoints[2], 0, 0);
                //A4.RotateAround(A4.position, A4.forward, (float)olJoints[3] - (float)joints[3]);
                //A5.Rotate((float)joints[4] - (float)olJoints[4], 0, 0);
                //A6.Rotate(0, 0, (float)olJoints[5] - (float)joints[5]);
            }
            else if (but8.isPressed() && !ROBOT.Busy())
            {
                ROBOT.setPoseTool(robFrame);
                double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };
                move_xyzwpr[1] = -movstp;
                Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);
                Mat robot_pose = ROBOT.Pose();
                double[] olJoints = ROBOT.Joints();
                Mat new_robot_pose;
                new_robot_pose = robot_pose * movement_pose;
                //ROBOT.setPose(new_robot_pose);
                ROBOT.MoveJ(new_robot_pose, false);
                //while (ROBOT.Busy())
                //{

                //}
                //double[] joints = ROBOT.Joints();
                //UnityEngine.Debug.Log("SteamVR Button pressed!");
                //UnityEngine.Debug.Log(joints[0]);
                //UnityEngine.Debug.Log(joints[1]);
                //UnityEngine.Debug.Log(joints[2]);
                //UnityEngine.Debug.Log(joints[3]);
                //UnityEngine.Debug.Log(joints[4]);
                //UnityEngine.Debug.Log(joints[5]);

                //A1.Rotate(0, (float)olJoints[0] - (float)joints[0], 0);
                //A2.Rotate((float)joints[1] - (float)olJoints[1], 0, 0);
                //A3.Rotate((float)joints[2] - (float)olJoints[2], 0, 0);
                //A4.RotateAround(A4.position, A4.forward, (float)olJoints[3] - (float)joints[3]);
                //A5.Rotate((float)joints[4] - (float)olJoints[4], 0, 0);
                //A6.Rotate(0, 0, (float)olJoints[5] - (float)joints[5]);
            }
            else if (but9.isPressed() && !ROBOT.Busy())
            {
                ROBOT.setPoseTool(robFrame);
                double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };
                move_xyzwpr[2] = movstp;
                Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);
                Mat robot_pose = ROBOT.Pose();
                double[] olJoints = ROBOT.Joints();
                Mat new_robot_pose;
                new_robot_pose = robot_pose * movement_pose;
                //ROBOT.setPose(new_robot_pose);
                ROBOT.MoveJ(new_robot_pose, false);
                //while (ROBOT.Busy())
                //{

                //}
                //double[] joints = ROBOT.Joints();
                //UnityEngine.Debug.Log("SteamVR Button pressed!");
                //UnityEngine.Debug.Log(joints[0]);
                //UnityEngine.Debug.Log(joints[1]);
                //UnityEngine.Debug.Log(joints[2]);
                //UnityEngine.Debug.Log(joints[3]);
                //UnityEngine.Debug.Log(joints[4]);
                //UnityEngine.Debug.Log(joints[5]);

                //A1.Rotate(0, (float)olJoints[0] - (float)joints[0], 0);
                //A2.Rotate((float)joints[1] - (float)olJoints[1], 0, 0);
                //A3.Rotate((float)joints[2] - (float)olJoints[2], 0, 0);
                //A4.RotateAround(A4.position, A4.forward, (float)olJoints[3] - (float)joints[3]);
                //A5.Rotate((float)joints[4] - (float)olJoints[4], 0, 0);
                //A6.Rotate(0, 0, (float)olJoints[5] - (float)joints[5]);
            }
            else if (but10.isPressed() && !ROBOT.Busy())
            {
                ROBOT.setPoseTool(robFrame);
                double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };
                move_xyzwpr[2] = -movstp;
                Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);
                Mat robot_pose = ROBOT.Pose();
                double[] olJoints = ROBOT.Joints();
                Mat new_robot_pose;
                new_robot_pose = robot_pose * movement_pose;
                //ROBOT.setPose(new_robot_pose);
                ROBOT.MoveJ(new_robot_pose, false);
                //while (ROBOT.Busy())
                //{

                //}
                //double[] joints = ROBOT.Joints();
                //UnityEngine.Debug.Log("SteamVR Button pressed!");
                //UnityEngine.Debug.Log(joints[0]);
                //UnityEngine.Debug.Log(joints[1]);
                //UnityEngine.Debug.Log(joints[2]);
                //UnityEngine.Debug.Log(joints[3]);
                //UnityEngine.Debug.Log(joints[4]);
                //UnityEngine.Debug.Log(joints[5]);

                //A1.Rotate(0, (float)olJoints[0] - (float)joints[0], 0);
                //A2.Rotate((float)joints[1] - (float)olJoints[1], 0, 0);
                //A3.Rotate((float)joints[2] - (float)olJoints[2], 0, 0);
                //A4.RotateAround(A4.position, A4.forward, (float)olJoints[3] - (float)joints[3]);
                //A5.Rotate((float)joints[4] - (float)olJoints[4], 0, 0);
                //A6.Rotate(0, 0, (float)olJoints[5] - (float)joints[5]);
            }
            else if (but11.isPressed() && !ROBOT.Busy())
            {
                ROBOT.setPoseTool(robFrame);
                double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };
                move_xyzwpr[3] = movstp;
                Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);
                Mat robot_pose = ROBOT.Pose();
                double[] olJoints = ROBOT.Joints();
                Mat new_robot_pose;
                new_robot_pose = robot_pose * movement_pose;
                //ROBOT.setPose(new_robot_pose);
                ROBOT.MoveJ(new_robot_pose, false);
                //while (ROBOT.Busy())
                //{

                //}
                //double[] joints = ROBOT.Joints();
                //UnityEngine.Debug.Log("SteamVR Button pressed!");
                //UnityEngine.Debug.Log(joints[0]);
                //UnityEngine.Debug.Log(joints[1]);
                //UnityEngine.Debug.Log(joints[2]);
                //UnityEngine.Debug.Log(joints[3]);
                //UnityEngine.Debug.Log(joints[4]);
                //UnityEngine.Debug.Log(joints[5]);

                //A1.Rotate(0, (float)olJoints[0] - (float)joints[0], 0);
                //A2.Rotate((float)joints[1] - (float)olJoints[1], 0, 0);
                //A3.Rotate((float)joints[2] - (float)olJoints[2], 0, 0);
                //A4.RotateAround(A4.position, A4.forward, (float)olJoints[3] - (float)joints[3]);
                //A5.Rotate((float)joints[4] - (float)olJoints[4], 0, 0);
                //A6.Rotate(0, 0, (float)olJoints[5] - (float)joints[5]);
            }
            else if (but12.isPressed() && !ROBOT.Busy())
            {
                ROBOT.setPoseTool(robFrame);
                double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };
                move_xyzwpr[3] = -movstp;
                Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);
                Mat robot_pose = ROBOT.Pose();
                double[] olJoints = ROBOT.Joints();
                Mat new_robot_pose;
                new_robot_pose = robot_pose * movement_pose;
                //ROBOT.setPose(new_robot_pose);
                ROBOT.MoveJ(new_robot_pose, false);
                //while (ROBOT.Busy())
                //{

                //}
                //double[] joints = ROBOT.Joints();
                //UnityEngine.Debug.Log("SteamVR Button pressed!");
                //UnityEngine.Debug.Log(joints[0]);
                //UnityEngine.Debug.Log(joints[1]);
                //UnityEngine.Debug.Log(joints[2]);
                //UnityEngine.Debug.Log(joints[3]);
                //UnityEngine.Debug.Log(joints[4]);
                //UnityEngine.Debug.Log(joints[5]);

                //A1.Rotate(0, (float)olJoints[0] - (float)joints[0], 0);
                //A2.Rotate((float)joints[1] - (float)olJoints[1], 0, 0);
                //A3.Rotate((float)joints[2] - (float)olJoints[2], 0, 0);
                //A4.RotateAround(A4.position, A4.forward, (float)olJoints[3] - (float)joints[3]);
                //A5.Rotate((float)joints[4] - (float)olJoints[4], 0, 0);
                //A6.Rotate(0, 0, (float)olJoints[5] - (float)joints[5]);
            }
        }
        void OnApplicationQuit()
        {
            ROBOT.setJoints(hom);
            //ROBOT2.setJoints(hom);
            //target.Delete();

        }
    }
}