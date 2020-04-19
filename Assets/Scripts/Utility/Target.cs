using System;
using System.Collections;
using UnityEngine;

namespace LudumDare46
{

    public class Target : MonoBehaviour
    {

        public Action<Target> OnTeleport { get; internal set; }

        public void Teleport()
        {
            // Use the center of the node as the destination for example
            var destination1 = Utils.GetRandomWalkableNode();

            transform.position = destination1;

            OnTeleport?.Invoke(this);

        }

    }

}