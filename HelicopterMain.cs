using System;
using BepInEx;
using UnityEngine;
using Utilla;
using GorillaLocomotion;
using UnityEngine.XR;

namespace HelicopterWooWoo
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]

    /* Please keep in mind that everything in this class belongs to Chineesje#0001 and you have no right to use this without credits!*/
    /* Using this product in any way of cheating is seen as stealing since this product has a license!*/

    public class HelicopterMain : BaseUnityPlugin
    {
        #region
        bool inRoom;
        Player gorilla;
        Vector2 joystickPosition;
        Transform monkeGoUp;
        bool monkeIsAllowedToFly;
        #endregion

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            gorilla = Player.Instance;
            monkeGoUp = gorilla.transform;
        }

        void Update()
        {
            if (!inRoom || gorilla == null)
                return;

            InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickPosition);

            if (joystickPosition.x < 0.7f)
            {
                monkeIsAllowedToFly = false;
            }
            if (joystickPosition.x > 0.7f)
            {
                monkeIsAllowedToFly = true;
            }
            if (monkeIsAllowedToFly)
            {
                monkeGoUp.position += new Vector3(0, .6f, 0);
            }
            /*Only works when you turn your right joystick to the right bc helicopters only go one direction duhhh */
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;
        }
    }
}
