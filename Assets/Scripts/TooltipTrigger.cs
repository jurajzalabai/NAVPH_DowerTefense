using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public string header;
    public TowerController tower;

    public WeaponController weapon;

    public void OnPointerEnter(PointerEventData eventData){
        if(tower != null){
            TooltipSystem.SetText(header, new string[]{
                "cost",
                "damage",
                "range",
                "fire rate",
                "splash area",
                "slow rate",
                "slow duration"
            }, new string[]{
                tower.cost.ToString(),
                tower.damage.ToString(),
                tower.range.ToString(),
                tower.fireRate.ToString(),
                tower.splashArea.ToString(),
                tower.slowMultiplier.ToString(),
                tower.slowDuration.ToString()
            });
            TooltipSystem.SetImage(tower.towerImage);
            TooltipSystem.Show();
        }
        else if(weapon != null){
            TooltipSystem.SetText(header, new string[]{
                "cost",
                "damage",
                "magazine size",
                "fire rate",
                "reload time",
            }, new string[]{
                weapon.cost.ToString(),
                weapon.weaponBullet.GetComponent<BulletController>().bulletDamage.ToString(),
                weapon.magazineSize.ToString(),
                weapon.fireRate.ToString(),
                weapon.reloadTime.ToString(),
            });
            TooltipSystem.SetImage(weapon.weaponImage);
            TooltipSystem.Show();
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        TooltipSystem.Hide();
    }
    

}
