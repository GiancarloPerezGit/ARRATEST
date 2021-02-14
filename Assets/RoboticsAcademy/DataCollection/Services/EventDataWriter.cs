using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


namespace RoboticsAcademy.DataCollection
{

    public class EventDataWriter : DataWriter
    { 
        protected override bool InitializeDataProvider()
        {
            Selectable[] selec = FindObjectsOfType<Selectable>();

            if (selec.Length > 0)
            {
                foreach (Selectable s in selec)
                {
                    EventTrigger trigger = s.gameObject.AddComponent<EventTrigger>();
                    var pointerDown = new EventTrigger.Entry();
                    pointerDown.eventID = EventTriggerType.PointerDown;
                    pointerDown.callback.AddListener((e) => CollectData(s));
                    trigger.triggers.Add(pointerDown);
                    print(s);

                    if (s.GetType() == typeof(Slider))
                    {
                        EventTrigger trigger_slider = s.gameObject.AddComponent<EventTrigger>();
                        var pointerUp = new EventTrigger.Entry();
                        pointerUp.eventID = EventTriggerType.PointerUp;
                        pointerUp.callback.AddListener((e) => CollectData(s));
                        trigger.triggers.Add(pointerUp);
                    }
                }
                return true;
            }
            else
            {
                Debug.LogWarning("No Selectables on the Scene");
                return false;
            }

        }

        protected override void WriteData() {/*Method not Needed*/}


        protected override void WriteHeader() 
        {
            dataStream.WriteLine("Selectable Name, Selectable Type, Time Clicked, Value");
        }

        protected void CollectData(Selectable A)
        {
            if (A.GetType() == typeof(Slider))
            {
                Slider H = (Slider)A;
                string selecClicked = string.Format("{0},{1},{2},{3}", A.name, A.GetType(), GetElapsedSeconds(), H.value);
                dataStream.WriteLine(selecClicked);
            }
            else
            {
                string selecClicked = string.Format("{0},{1},{2}", A.name, A.GetType(), GetElapsedSeconds());
                dataStream.WriteLine(selecClicked);
            }
        }
    }
}