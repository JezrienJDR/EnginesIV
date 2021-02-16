using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : WeaponComponent
{
        public Vector3 hitPoint = Vector3.zero;

    public override void Fire()
    {

        if (stats.ShotsInClip > 0)
        {

            base.Fire();

            //Debug.Log("BOOM");

            Ray screenRay = MainCam.ScreenPointToRay(new Vector3(ret.currentAimPosition.x, ret.currentAimPosition.y, 0));



            if (Physics.Raycast(screenRay, out RaycastHit hit, stats.Range, stats.layerMask))
            {
                hitPoint = hit.point;
                Vector3 hitDir = hit.point - MainCam.transform.position;
                Debug.DrawRay(MainCam.transform.position, hitDir * stats.Range, Color.red);

                //OnDrawGizmos(hit.point);
            }
        }
        else
        {
            sock.StopFiring();
            sock.StartReload();
        }
    }

    private void OnDrawGizmos()
    {
        if(hitPoint != Vector3.zero)
        {
            Gizmos.DrawWireSphere(hitPoint, 1.0f);
        }
        //Gizmos.DrawWireSphere(hitPoint, 3.0f);
    }

}
